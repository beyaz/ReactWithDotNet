﻿using System.Reflection;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace ReactWithDotNet;

sealed class ClientStateInfo
{
    public int? ComponentUniqueIdentifier { get; init; }
    public IReadOnlyDictionary<string, object> DotNetProperties { get; init; }
    public string FullTypeNameOfComponent { get; init; }
    public string StateAsJson { get; init; }
}

sealed class ComponentRequest
{
    public int CallFunctionId { get; init; }

    public IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; init; }

    public string CapturedStateTreeRootNodeKey { get; init; }

    public double? ClientHeight { get; init; }

    public double? ClientWidth { get; init; }

    public int ComponentKey { get; set; }

    public int ComponentUniqueIdentifier { get; set; }

    public string[] EventArgumentsAsJsonArray { get; init; }

    public string EventHandlerMethodName { get; init; }

    public string FullName { get; init; }

    public int LastUsedComponentUniqueIdentifier { get; init; }

    public string MethodName { get; init; }
}

class ComponentResponse
{
    internal ReactContext ReactContext;
    public int CallFunctionId { get; set; }

    public IReadOnlyList<Client.ClientTask> ClientTaskList { get; init; }

    public object DynamicStyles { get; set; }

    public object ElementAsJson { get; init; }

    public string ErrorMessage { get; init; }

    public int LastUsedComponentUniqueIdentifier { get; set; }

    public JsonMap NewDotNetProperties { get; init; }

    public object NewState { get; init; }

    public bool? SkipRender { get; init; }
}

static class ComponentRequestHandler
{
    public static async Task<ComponentResponse> HandleRequest(ProcessReactWithDotNetRequestInput input)
    {
        var request = input.ComponentRequest;

        var findType = TypeSerializer.DeserializeToType;

        var beforeSerializeElementToClient = input.BeforeSerializeElementToClient;

        var context = input.ReactContext;

        var tracer = new Tracer();

        try
        {
            if (context == null)
            {
                context = new()
                {
                    CapturedStateTree = request.CapturedStateTree,
                    ClientWidth       = request.ClientWidth,
                    ClientHeight      = request.ClientHeight,
                    HttpContext       = input.HttpContext,
                    wwwroot           = GetwwwrootFolder(input.HttpContext),

                    Request = (
                        Path: GetRequestPath(input.HttpContext),
                        Query: GetRequestQuery(input.HttpContext)
                    )
                };

                context.Set(typeof(HttpContext).FullName, input.HttpContext);

                var task = input.OnReactContextCreated?.Invoke(context);
                if (task is not null)
                {
                    await task;
                }

                if (input.OnReactContextCreated is not null && tracer.ElapsedMilliseconds >= 5)
                {
                    tracer.Trace($"{input.OnReactContextCreated.Method.DeclaringType?.Name}::{input.OnReactContextCreated.Method.Name}",tracer.ElapsedMilliseconds);
                }
            }

            Exception exception = null;
            try
            {
                if (request.MethodName == "FetchComponent")
                {
                    return await fetchComponent();
                }

                if (request.MethodName == "HandleComponentEvent")
                {
                    return await handleComponentEvent();
                }
                
                return new() { ErrorMessage = $"Not implemented method. {request.MethodName}" };
            }
            catch(Exception e)
            {
                exception = e;
            }
            finally
            {
                var task = input.OnReactContextDisposed?.Invoke(context, exception);
                if (task is not null)
                {
                    await task;
                }
            }

            return new() { ErrorMessage = exception.ToString() };
        }
        catch (Exception exception)
        {
            return new() { ErrorMessage = exception.ToString() };
        }

        async Task<ComponentResponse> fetchComponent()
        {
            if (string.IsNullOrWhiteSpace(request.FullName))
            {
                return new() { ErrorMessage = "request.FullName is empty." };
            }

            var type = findType(request.FullName);
            if (type == null)
            {
                return new() { ErrorMessage = $"Type not found. {request.FullName}" };
            }

            var instance = (Element)(input.Instance ?? ReflectionHelper.CreateNewInstance(type));
            if (instance == null)
            {
                return new() { ErrorMessage = $"Type instance not created. {request.FullName}" };
            }

            if (instance is ReactComponentBase reactComponentBase)
            {
                reactComponentBase.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier++;
                reactComponentBase.key                       = "0";
                reactComponentBase.Context                   = context;
                await reactComponentBase.InvokeConstructor();
            }
            else if (instance is PureComponent reactPureComponent)
            {
                reactPureComponent.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier++;
                reactPureComponent.key                       = "0";
                reactPureComponent.Context                   = context;
            }
            else
            {
                throw DeveloperException($"{instance.GetType().FullName} should be inherit from {nameof(PureComponent)} or {nameof(Component)}");
            }

            // maybe developer forget init state
            if (instance is Component<EmptyState> { IsStateNull: true } reactComponent)
            {
                reactComponent.InitState(new());
            }

            var stateTree = new StateTree
            {
                ChildStates = request.CapturedStateTree
            };

            var serializerContext = new ElementSerializerContext(context, stateTree, beforeSerializeElementToClient)
            {
                Tracer                                                = tracer,
                ComponentUniqueIdentifierNextValue                    = request.ComponentUniqueIdentifier,
                CalculateSuspenseFallbackForThirdPartyReactComponents = input.CalculateSuspenseFallbackForThirdPartyReactComponents
            };

            var map = await instance.ToJsonMap(serializerContext);

            tracer.WriteToResponseHeader(input.HttpContext);
            
            return new()
            {
                CallFunctionId                    = request.CallFunctionId,
                ElementAsJson                     = map,
                DynamicStyles                     = DynamicStyleContentForEmbedInClient.CalculateCssClassList(serializerContext.DynamicStyles.Map),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue - 1,
                ReactContext                      = context
            };
        }

        async Task<ComponentResponse> handleComponentEvent()
        {
            var type = findType(request.FullName);
            if (type == null)
            {
                return new() { ErrorMessage = $"Type not found.{request.FullName}" };
            }

            var instance = (ReactComponentBase)ReflectionHelper.CreateNewInstance(type);
            if (instance == null)
            {
                return new() { ErrorMessage = $"Instance not created.{request.FullName}" };
            }

            instance.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier;

            // Init state
            {
                var errorMessage = setState(type, instance, request.CapturedStateTree[request.CapturedStateTreeRootNodeKey].StateAsJson);
                if (errorMessage != null)
                {
                    return new() { ErrorMessage = errorMessage };
                }

                instance.Context = context;
            }

            // transfer properties
            {
                var errorMessage = ElementSerializer.TransferPropertiesToDotNetComponent(instance, type, request.CapturedStateTree[request.CapturedStateTreeRootNodeKey].DotNetProperties);
                if (errorMessage is not null)
                {
                    return new() { ErrorMessage = errorMessage };
                }
            }

            // Find method
            MethodInfo methodInfo = null;
            {
                if (!MethodAccess.TryResolveMethodInfo(request.EventHandlerMethodName, ref methodInfo))
                {
                    methodInfo = type.FindMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                    if (methodInfo == null)
                    {
                        return new() { ErrorMessage = $"Method not found.{type.FullName}::{request.EventHandlerMethodName}" };
                    }
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
                        return new() { ErrorMessage = parameters.errorMessage };
                    }

                    object response;
                    if (instance is FunctionalComponent functionalComponent)
                    {
                        functionalComponent.InitializeTarget();

                        var targetInstance = functionalComponent._target;

                        // todo: think more and more comment here
                        if (methodInfo.DeclaringType != null && methodInfo.DeclaringType != targetInstance.GetType())
                        {
                            var newTarget = ReflectionHelper.CreateNewInstance(methodInfo.DeclaringType);
                            foreach (var fieldInfo in methodInfo.DeclaringType.GetFields())
                            {
                                if (fieldInfo.FieldType.IsFunctionalComponent())
                                {
                                    fieldInfo.SetValue(newTarget, functionalComponent);
                                    continue;
                                }

                                if (fieldInfo.FieldType == targetInstance.GetType())
                                {
                                    fieldInfo.SetValue(newTarget, targetInstance);
                                    continue;
                                }

                                throw DeveloperException($"Scope cannot be resolved. Too complex. {fieldInfo.Name}");
                            }

                            targetInstance = newTarget;
                        }

                        response = methodInfo.Invoke(targetInstance, parameters.value);
                    }
                    else
                    {
                        response = methodInfo.Invoke(instance, parameters.value);
                    }

                    if (response is Task task)
                    {
                        await task;
                    }
                }

                var end = tracer.ElapsedMilliseconds;
                if (end - begin >= 5)
                {
                    tracer.Trace($"{methodInfo.DeclaringType?.Name}::{methodInfo.Name}",end - begin);
                }
            }
            catch (Exception exception)
            {
                return new()
                {
                    ErrorMessage = $"Error occured when invoking method.Method: '{methodInfo.DeclaringType}::{methodInfo.Name}'. {Environment.NewLine}Exception: {exception}"
                };
            }

            if (methodInfo.GetCalculated().SkipRender)
            {
                var typeInfo = type.Calculated();

                if (instance is FunctionalComponent functionalComponent)
                {
                    functionalComponent.CalculateScopeFromTarget(null);
                }

                var newState = typeInfo.StateProperty.GetValueFunc(instance);

                var dotNetProperties = new JsonMap();
                {
                    foreach (var item in typeInfo.DotNetPropertiesOfType)
                    {
                        var propertyValue = item.GetValueFunc(instance);

                        if (item.DefaultValue == propertyValue)
                        {
                            continue;
                        }

                        dotNetProperties.Add(item.PropertyInfo.Name, propertyValue);
                    }
                }

                return new()
                {
                    SkipRender          = true,
                    NewState            = newState,
                    NewDotNetProperties = dotNetProperties,
                    ClientTaskList      = instance._client is not null && instance._client.TaskList.Count > 0 ? instance._client.TaskList : null,
                };
            }

            var stateTree = new StateTree
            {
                ChildStates = request.CapturedStateTree
            };

            var serializerContext = new ElementSerializerContext(context, stateTree, beforeSerializeElementToClient)
            {
                Tracer                             = tracer,
                ComponentUniqueIdentifierNextValue = request.LastUsedComponentUniqueIdentifier + 1
            };

            var map = await instance.ToJsonMap(serializerContext);
            
            tracer.WriteToResponseHeader(input.HttpContext);

            return new()
            {
                CallFunctionId                    = request.CallFunctionId,
                ElementAsJson                     = map,
                DynamicStyles                     = DynamicStyleContentForEmbedInClient.CalculateCssClassList(serializerContext.DynamicStyles.Map),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue - 1,
                ReactContext                      = context
            };
        }

        string setState(Type typeOfInstance, object instance, string stateAsJson)
        {
            var statePropertyInfo = typeOfInstance.GetProperty("state", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (statePropertyInfo == null)
            {
                return $"MissingMember at {typeOfInstance.FullName}::state";
            }

            var state = DeserializeJsonBySystemTextJson(stateAsJson, statePropertyInfo.PropertyType);

            statePropertyInfo.SetValue(instance, state);

            return null;
        }

        static (bool hasError, string errorMessage, object[] value) createMethodArguments(MethodInfo methodInfo, IReadOnlyList<string> eventArgumentsAsJsonArray)
        {
            var parameterInfoList = methodInfo.GetParameters();
            if (parameterInfoList.Length == 0)
            {
                return (false, null, []);
            }

            var eventArguments = new object[parameterInfoList.Length];

            for (var i = 0; i < parameterInfoList.Length; i++)
            {
                var parameterInfo = parameterInfoList[i];

                try
                {
                    eventArguments[i] = DeserializeJsonBySystemTextJson(eventArgumentsAsJsonArray[i], parameterInfo.ParameterType);
                    if (eventArguments[i] is UIEvent uiEvent)
                    {
                        ShadowHtmlElement.Fix(uiEvent.target);
                        ShadowHtmlElement.Fix(uiEvent.currentTarget);
                    }
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

            return (false, null, eventArguments);
        }

        static string GetwwwrootFolder(HttpContext httpContext)
        {
            if (httpContext == null)
            {
                return null;
            }

            string requestPath;
            {
                var request = httpContext.Request;

                requestPath = request.Path;

                if (requestPath == RequestHandlerPath)
                {
                    var headers = request.Headers;

                    if (headers.TryGetValue(HeaderNames.Referer, out var referer) &&
                        headers.TryGetValue(HeaderNames.Host, out var host) &&
                        referer[0] is not null &&
                        host[0] is not null)
                    {
                        var path = referer[0].Substring(referer[0].IndexOf(host[0], StringComparison.OrdinalIgnoreCase));

                        path = path.Substring(path.IndexOf('/'));
                        if (path.Length > 1)
                        {
                            requestPath = path;
                        }
                    }
                }
            }

            var deep = (requestPath + "").Split('/', StringSplitOptions.RemoveEmptyEntries).Length;
            if (deep > 1)
            {
                deep--;
            }

            return string.Join(string.Empty, Enumerable.Range(0, deep).Select(_ => "../")) + "wwwroot";
        }

        static string GetRequestPath(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                return null;
            }

            var request = httpContext.Request;

            if (request.Path == RequestHandlerPath)
            {
                if (request.Headers.TryGetValue(HeaderNames.Referer, out var referer) && referer[0] is not null)
                {
                    return new Uri(referer[0]).LocalPath;
                }
            }

            return request.Path;
        }

        static IQueryCollection GetRequestQuery(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                return new QueryCollection();
            }
            
            var request = httpContext.Request;

            if (request.Path == RequestHandlerPath)
            {
                if (request.Headers.TryGetValue(HeaderNames.Referer, out var referer) && referer[0] is not null)
                {
                    var nameValueCollection = HttpUtility.ParseQueryString(new Uri(referer[0]).Query);

                    var dictionary = nameValueCollection.AllKeys.ToDictionary(x => x, x => new StringValues(nameValueCollection.GetValues(x)));

                    return new QueryCollection(dictionary);
                }
            }

            return request.Query;
        }
    }
}

sealed class StateTree
{
    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; init; }
}

partial class Mixin
{
    public static string RequestHandlerPath;
}