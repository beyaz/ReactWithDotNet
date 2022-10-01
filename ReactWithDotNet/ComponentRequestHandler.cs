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
    public IReadOnlyDictionary<string, object> DotNetProperties { get; set; }
    public string FullTypeNameOfState { get; set; }
    public string StateAsJson { get; set; }
}

[Serializable]
public class ComponentRequest
{
    public IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; set; }

    public double ClientHeight { get; set; }

    public double ClientWidth { get; set; }

    public int ComponentRefId { get; set; }

    public string[] EventArgumentsAsJsonArray { get; set; }

    public string EventHandlerMethodName { get; set; }

    public string FullName { get; set; }

    public string MethodName { get; set; }

    public string SearchPartOfUrl { get; set; }
}

[Serializable]
public class ComponentResponse
{
    public string[] DynamicStyles { get; set; }

    public object ElementAsJson { get; set; }

    public string ErrorMessage { get; set; }

    public IReadOnlyList<string> Trace { get; set; }
}

public static class ComponentRequestHandler
{
    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    public static ComponentResponse HandleRequest(ComponentRequest request, Func<string, Type> findType, Action<Element, ReactContext> beforeSerializeElementToClient = null)
    {
        var trace = new List<string>();

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        trace.Add($"BEGIN {stopwatch.ElapsedMilliseconds}");

        var context = CreateContext(request);

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

            instance.Context = context;
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

            trace.Add($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            var serializerContext = new ElementSerializerContext
            {
                ComponentRefId                 = request.ComponentRefId,
                StateTree                      = stateTree,
                BeforeSerializeElementToClient = beforeSerializeElementToClient,
                ReactContext                   = context
            };

            var map = instance.ToMap2(serializerContext);

            trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJson = map,
                Trace         = trace,
                DynamicStyles = serializerContext.DynamicStyles.CalculateCssClassList()
            };
        }

        ComponentResponse handleComponentEvent()
        {
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
            {
                trace.Add($"Method '{methodInfo.Name}' invocation started at {stopwatch.ElapsedMilliseconds}");
                try
                {
                    methodInfo.Invoke(instance, createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray));
                }
                catch (Exception exception)
                {
                    return new ComponentResponse { ErrorMessage = $"Method invocation error.{exception}" };
                }

                trace.Add($"Method '{methodInfo.Name}' invocation finished at {stopwatch.ElapsedMilliseconds}");
            }

            // Serialize to json

            trace.Add($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumpPath = "0",
            };

            var serializerContext = new ElementSerializerContext
            {
                ComponentRefId                 = request.ComponentRefId,
                StateTree                      = stateTree,
                BeforeSerializeElementToClient = beforeSerializeElementToClient,
                ReactContext                   = context
            };

            var map = instance.ToMap2(serializerContext);

            trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJson = map,
                Trace         = trace,
                DynamicStyles = serializerContext.DynamicStyles.CalculateCssClassList()
            };
        }

        string setState(Type typeOfInstance, object instance, string stateAsJson)
        {
            var statePropertyInfo = typeOfInstance.GetProperty("state", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (statePropertyInfo == null)
            {
                return $"MissingMember at {typeOfInstance.FullName}::state";
            }

            var state = Json.DeserializeJsonByNewtonsoft(stateAsJson, statePropertyInfo.PropertyType);

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
                eventArguments[i] = Json.DeserializeJsonByNewtonsoft(eventArgumentsAsJsonArray[i], parameterInfoList[i].ParameterType);
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

public static class Json
{
    public static object DeserializeJsonByNewtonsoft(string json, Type returnType)
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
}

public class StateTree
{
    public string BreadCrumpPath { get; set; }

    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; init; }

    public int CurrentOrder { get; set; }
}