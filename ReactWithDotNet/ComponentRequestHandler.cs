using System.Collections.Specialized;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReactWithDotNet;

using static Array;

[Serializable]
public sealed class ClientStateInfo
{
    public int? ComponentUniqueIdentifier { get; set; }
    public IReadOnlyDictionary<string, object> DotNetProperties { get; set; }
    public string FullTypeNameOfComponent { get; set; }
    public string FullTypeNameOfState { get; set; }
    public string StateAsJson { get; set; }
}

[Serializable]
public class ComponentRequest
{
    public IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; set; }

    public double ClientHeight { get; set; }

    public double ClientWidth { get; set; }

    public int ComponentKey { get; set; }

    public int ComponentUniqueIdentifier { get; set; }

    public string[] EventArgumentsAsJsonArray { get; set; }

    public string EventHandlerMethodName { get; set; }

    public string FullName { get; set; }

    public int LastUsedComponentUniqueIdentifier { get; set; }

    public string MethodName { get; set; }

    public string SearchPartOfUrl { get; set; }
}

[Serializable]
public class ComponentResponse
{
    public string[] DynamicStyles { get; set; }

    public object ElementAsJson { get; set; }

    public string ErrorMessage { get; set; }

    public int LastUsedComponentUniqueIdentifier { get; set; }

    public LinkedList<string> Trace { get; set; }
}


static class ComponentRequestHandler
{
   

    public static ComponentResponse HandleRequest(ProcessReactWithDotNetRequestInput input)
    {
        var request = input.componentRequest;

        var findType = input.findType;

        var beforeSerializeElementToClient = input.BeforeSerializeElementToClient;

        var context = CreateContext(request);

        input.OnReactContextCreated?.Invoke(context);

        if (request.MethodName == "FetchComponent")
        {
            return fetchComponent();
        }

        if (request.MethodName == "HandleComponentEvent")
        {
            return handleComponentEvent();
        }

        return new ComponentResponse { ErrorMessage = $"Not implemented method. {request.MethodName}" };

        ComponentResponse fetchComponent()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not found.{request.FullName}" };
            }

            var instance = (ReactStatefulComponent)Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not instanstied.{request.FullName}" };
            }

            instance.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier++;
            instance.key                       = "0";
            instance.Context                   = context;
            instance.InvokeConstructor();

            // maybe developer forget init state
            if (instance is ReactComponent<EmptyState> reactComponent && reactComponent.state == null)
            {
                reactComponent.state = new EmptyState();
            }

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumpPath = "0"
            };

            var serializerContext = new ElementSerializerContext
            {
                ComponentUniqueIdentifierNextValue = request.ComponentUniqueIdentifier,
                StateTree                          = stateTree,
                BeforeSerializeElementToClient     = beforeSerializeElementToClient,
                ReactContext                       = context
            };

            var tracer = serializerContext.Tracer;

            tracer.Trace($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            tracer.traceIndentLevel++;

            var map = instance.ToJsonMap(serializerContext);

            tracer.traceIndentLevel--;

            tracer.Trace($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            tracer.Trace($"Total time in ReactWithDotnet is {stopwatch.ElapsedMilliseconds} milliseconds.");

            return new ComponentResponse
            {
                ElementAsJson                     = map,
                Trace                             = tracer.traceMessages,
                DynamicStyles                     = serializerContext.DynamicStyles.CalculateCssClassList(),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue
            };
        }

        ComponentResponse handleComponentEvent()
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not found.{request.FullName}" };
            }

            var instance = (ReactStatefulComponent)Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not instanstied.{request.FullName}" };
            }

            instance.ComponentUniqueIdentifier = request.ComponentUniqueIdentifier;

            // transfer properties
            {
                var props = request.CapturedStateTree["0"].DotNetProperties;
                if (props is not null)
                {
                    foreach (var (key, value) in props)
                    {
                        if (key == "$childrenCount")
                        {
                            var childrenCount = Convert.ToInt32(value);
                            for (var i = 0; i < childrenCount; i++)
                            {
                                instance.children.Add(new FakeChild { Index = i });
                            }

                            continue;
                        }

                        var property = type.GetProperty(key);
                        if (property == null)
                        {
                            return new ComponentResponse { ErrorMessage = $"Property not found.{request.FullName}::{key}" };
                        }

                        var propertyValue = value;
                        if (value is JToken jToken)
                        {
                            propertyValue = jToken.ToObject(property.PropertyType);
                        }

                        if (property.PropertyType == typeof(int))
                        {
                            propertyValue = Convert.ChangeType(propertyValue, TypeCode.Int32);
                        }

                        if (property.PropertyType == typeof(int?) && propertyValue is not null)
                        {
                            propertyValue = Convert.ChangeType(propertyValue, TypeCode.Int32);
                        }

                        property.SetValue(instance, propertyValue);
                    }
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
            var methodInfo = type.FindMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                return new ComponentResponse { ErrorMessage = $"Method not found.{type.FullName}::{request.EventHandlerMethodName}" };
            }

            // Invoke method

            instance.key = request.ComponentKey.ToString();
            request.ComponentKey++;

            try
            {
                methodInfo.Invoke(instance, createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray));
            }
            catch (Exception exception)
            {
                return new ComponentResponse { ErrorMessage = $"Method invocation error.{exception}" };
            }

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumpPath = "0",
            };

            var serializerContext = new ElementSerializerContext
            {
                ComponentUniqueIdentifierNextValue = request.LastUsedComponentUniqueIdentifier + 1,
                StateTree                          = stateTree,
                BeforeSerializeElementToClient     = beforeSerializeElementToClient,
                ReactContext                       = context
            };

            var tracer = serializerContext.Tracer;

            tracer.Trace($"Method '{methodInfo.Name}' invoked in {stopwatch.ElapsedMilliseconds} milliseconds.");

            tracer.Trace($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            tracer.traceIndentLevel++;

            var map = instance.ToJsonMap(serializerContext);

            tracer.traceIndentLevel--;

            tracer.Trace($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            tracer.Trace($"Total time in ReactWithDotnet is {stopwatch.ElapsedMilliseconds} milliseconds.");

            return new ComponentResponse
            {
                ElementAsJson                     = map,
                Trace                             = tracer.traceMessages,
                DynamicStyles                     = serializerContext.DynamicStyles.CalculateCssClassList(),
                LastUsedComponentUniqueIdentifier = serializerContext.ComponentUniqueIdentifierNextValue
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

        object[] createMethodArguments(MethodInfo methodInfo, IReadOnlyList<string> eventArgumentsAsJsonArray)
        {
            var parameterInfoList = methodInfo.GetParameters();
            if (parameterInfoList.Length == 0)
            {
                return Empty<object>();
            }

            var eventArguments = new object[parameterInfoList.Length];

            for (var i = 0; i < parameterInfoList.Length; i++)
            {
                eventArguments[i] = DeserializeJson(eventArgumentsAsJsonArray[i], parameterInfoList[i].ParameterType);
            }

            return eventArguments;
        }
    }

    static ReactContext CreateContext(ComponentRequest request)
    {
        var context = new ReactContext
        {
            Query        = string.IsNullOrWhiteSpace(request.SearchPartOfUrl) ? new NameValueCollection() : HttpUtility.ParseQueryString(request.SearchPartOfUrl),
            ClientWidth  = request.ClientWidth,
            ClientHeight = request.ClientHeight
        };

        return context;
    }
}

partial class Mixin
{
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
}

public class StateTree
{
    public string BreadCrumpPath { get; set; }

    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; init; }

    public int CurrentOrder { get; set; }
}