using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;

namespace ReactWithDotNet;

using static Array;

sealed class ClientStateInfo
{
    public int? ComponentUniqueIdentifier { get; set; }
    public IReadOnlyDictionary<string, object> DotNetProperties { get; set; }
    public string FullTypeNameOfComponent { get; set; }
    public string StateAsJson { get; set; }
}

sealed class ComponentRequest
{
    public int CallFunctionId { get; set; }

    public IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; set; }

    public double? ClientHeight { get; set; }

    public double? ClientWidth { get; set; }

    public int ComponentKey { get; set; }

    public int ComponentUniqueIdentifier { get; set; }

    public string[] EventArgumentsAsJsonArray { get; set; }

    public string EventHandlerMethodName { get; set; }

    public string FullName { get; set; }

    public int LastUsedComponentUniqueIdentifier { get; set; }

    public string MethodName { get; set; }

    public string QueryString { get; set; }
}

class ComponentResponse
{
    public int CallFunctionId { get; set; }

    public object DynamicStyles { get; set; }

    public object ElementAsJson { get; set; }

    public string ErrorMessage { get; set; }

    public int LastUsedComponentUniqueIdentifier { get; set; }

    public IReadOnlyCollection<string> Trace { get; set; }
}

static class ComponentRequestHandler
{
    public static async Task<ComponentResponse> HandleRequest(ProcessReactWithDotNetRequestInput input)
    {
        var request = input.ComponentRequest;

        Func<string, Type> findType = Type.GetType;

        var beforeSerializeElementToClient = input.BeforeSerializeElementToClient;

        ReactContext context;

        var tracer = new Tracer();

        try
        {
            context = CreateContext(request);

            var task = input.OnReactContextCreated?.Invoke(input.HttpContext, context);
            if (task is not null)
            {
                await task;
            }

            if (tracer.ElapsedMilliseconds >= 3 && input.OnReactContextCreated is not null)
            {
                tracer.Trace($"{input.OnReactContextCreated.Method.DeclaringType}::{input.OnReactContextCreated.Method.Name} invoked in {tracer.ElapsedMilliseconds} milliseconds");
            }

            if (request.MethodName == "FetchComponent")
            {
                return await fetchComponent();
            }

            if (request.MethodName == "HandleComponentEvent")
            {
                return await handleComponentEvent();
            }

            return new ComponentResponse { ErrorMessage = $"Not implemented method. {request.MethodName}" };
        }
        catch (Exception exception)
        {
            return new ComponentResponse { ErrorMessage = exception.ToString() };
        }

        async Task<ComponentResponse> fetchComponent()
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                return new ComponentResponse { ErrorMessage = "request.FullName is empty." };
            }

            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not found. {request.FullName}" };
            }

            var instance = (Element)(input.Instance ?? Activator.CreateInstance(type));
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type instance not created. {request.FullName}" };
            }

            if (instance is ReactComponentBase reactComponentBase)
            {
                reactComponentBase.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier++;
                reactComponentBase.key                       = "0";
                reactComponentBase.Context                   = context;
                await reactComponentBase.InvokeConstructor();
            }
            else if (instance is ReactPureComponent reactPureComponent)
            {
                reactPureComponent.key     = "0";
                reactPureComponent.Context = context;
            }
            else
            {
                throw DeveloperException($"{instance.GetType().FullName} should be inherit from {nameof(ReactPureComponent)} or {nameof(ReactComponent)}");
            }

            // maybe developer forget init state
            if (instance is ReactComponent<EmptyState> { IsStateNull: true } reactComponent)
            {
                reactComponent.InitState(new EmptyState());
            }

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumbPath = "0"
            };

            var serializerContext = new ElementSerializerContext
            {
                Tracer                                                = tracer,
                ComponentUniqueIdentifierNextValue                    = request.ComponentUniqueIdentifier,
                StateTree                                             = stateTree,
                BeforeSerializeElementToClient                        = beforeSerializeElementToClient,
                ReactContext                                          = context,
                CalculateSuspenseFallbackForThirdPartyReactComponents = input.CalculateSuspenseFallbackForThirdPartyReactComponents
            };

            var map = await instance.ToJsonMap(serializerContext);

            tracer.Trace($"Total time in ReactWithDotnet is {tracer.ElapsedMilliseconds} milliseconds.");

            return new ComponentResponse
            {
                CallFunctionId                    = request.CallFunctionId,
                ElementAsJson                     = map,
                Trace                             = tracer.Messages,
                DynamicStyles                     = serializerContext.DynamicStyles.CalculateCssClassList(),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue - 1
            };
        }

        async Task<ComponentResponse> handleComponentEvent()
        {
            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not found.{request.FullName}" };
            }

            var instance = (ReactComponentBase)Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Instance not created.{request.FullName}" };
            }

            instance.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier;

            // transfer properties
            {
                var errorMessage = ElementSerializer.TransferPropertiesToDotNetComponent(instance, type, request.CapturedStateTree["0"].DotNetProperties);
                if (errorMessage is not null)
                {
                    return new ComponentResponse { ErrorMessage = errorMessage };
                }
            }

            // Init state
            {
                var errorMessage = setState(type, instance, request.CapturedStateTree["0"].StateAsJson);
                if (errorMessage != null)
                {
                    return new ComponentResponse { ErrorMessage = errorMessage };
                }

                instance.Context = context;
            }

            // Find method
            MethodInfo methodInfo;
            {
                var metadataToken = TryGetMethodInfoMetadataTokenFromName(request.EventHandlerMethodName);
                if (metadataToken.HasValue)
                {
                    methodInfo = (MethodInfo)type.Module.ResolveMethod(metadataToken.Value);
                }
                else
                {
                    methodInfo = type.FindMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                }

                if (methodInfo == null)
                {
                    return new ComponentResponse { ErrorMessage = $"Method not found.{type.FullName}::{request.EventHandlerMethodName}" };
                }
            }

            // Invoke method
            instance.key = request.ComponentKey.ToString();
            request.ComponentKey++;

            try
            {
                var begin = tracer.ElapsedMilliseconds;

                // i n v o k e
                {
                    var parameters = createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray);
                    if (parameters.hasError)
                    {
                        return new ComponentResponse { ErrorMessage = parameters.errorMessage };
                    }

                    var response = methodInfo.Invoke(instance, parameters.value);
                    if (response is Task task)
                    {
                        await task;
                    }
                }

                var end = tracer.ElapsedMilliseconds;
                if (end - begin >= 3)
                {
                    tracer.Trace($"Method '{methodInfo.Name}' invocation finished in {end - begin} milliseconds");
                }
            }
            catch (Exception exception)
            {
                return new ComponentResponse { ErrorMessage = $"Error occured when invoking method.Method: {methodInfo.Name}. Exception: {exception}" };
            }

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumbPath = "0"
            };

            var serializerContext = new ElementSerializerContext
            {
                Tracer                             = tracer,
                ComponentUniqueIdentifierNextValue = request.LastUsedComponentUniqueIdentifier + 1,
                StateTree                          = stateTree,
                BeforeSerializeElementToClient     = beforeSerializeElementToClient,
                ReactContext                       = context
            };

            var map = await instance.ToJsonMap(serializerContext);

            tracer.Trace($"Total time in ReactWithDotnet is {tracer.ElapsedMilliseconds} milliseconds.");

            return new ComponentResponse
            {
                CallFunctionId                    = request.CallFunctionId,
                ElementAsJson                     = map,
                Trace                             = tracer.Messages,
                DynamicStyles                     = serializerContext.DynamicStyles.CalculateCssClassList(),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue - 1
            };
        }

        string setState(Type typeOfInstance, object instance, string stateAsJson)
        {
            var statePropertyInfo = typeOfInstance.GetProperty("state", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (statePropertyInfo == null)
            {
                return $"MissingMember at {typeOfInstance.FullName}::state";
            }

            var state = DeserializeJson(stateAsJson, statePropertyInfo.PropertyType);

            statePropertyInfo.SetValue(instance, state);

            return null;
        }

        static (bool hasError, string errorMessage, object[] value) createMethodArguments(MethodInfo methodInfo, IReadOnlyList<string> eventArgumentsAsJsonArray)
        {
            var parameterInfoList = methodInfo.GetParameters();
            if (parameterInfoList.Length == 0)
            {
                return (default, default, Empty<object>());
            }

            var eventArguments = new object[parameterInfoList.Length];

            for (var i = 0; i < parameterInfoList.Length; i++)
            {
                var parameterInfo = parameterInfoList[i];

                try
                {
                    eventArguments[i] = DeserializeJson(eventArgumentsAsJsonArray[i], parameterInfo.ParameterType);
                }
                catch (Exception)
                {
                    return (hasError: true,
                        errorMessage: $"""
                                       Methot parameter not deserialized.
                                       Method: {methodInfo.DeclaringType}::{methodInfo.Name}
                                       Parameter Index: {i}
                                       Parameter Name: {parameterInfo.Name}
                                       Parameter Type: {parameterInfo.ParameterType}
                                       Value: {eventArgumentsAsJsonArray[i]}
                                       """,
                        value: null);
                }
            }

            return (default, default, eventArguments);
        }
    }

    static ReactContext CreateContext(ComponentRequest request)
    {
        var context = new ReactContext
        {
            Query        = string.IsNullOrWhiteSpace(request.QueryString) ? new NameValueCollection() : HttpUtility.ParseQueryString(request.QueryString),
            ClientWidth  = request.ClientWidth,
            ClientHeight = request.ClientHeight,

            CapturedStateTree = request.CapturedStateTree
        };

        return context;
    }
}

partial class Mixin
{
#pragma warning disable CA2200
    public static object DeserializeJson(string json, Type returnType)
    {
        try
        {
            return JsonConvert.DeserializeObject(json, returnType);
        }
        catch (Exception exception)
        {
            // ReSharper disable once PossibleIntendedRethrow
            throw exception;
        }
    }
#pragma warning restore CA2200

#pragma warning disable CA2200
    public static T DeserializeJson<T>(string json)
    {
        try
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
        catch (Exception exception)
        {
            // ReSharper disable once PossibleIntendedRethrow

            throw exception;
        }
    }
#pragma warning restore CA2200
}

sealed class StateTree
{
    public string BreadCrumbPath { get; set; }

    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; init; }

    public int CurrentOrder { get; set; }
}