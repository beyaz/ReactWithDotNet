using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

using static Array;

[Serializable]
public sealed class ClientStateInfo
{
    public string FullTypeNameOfState { get; set; }
    public string StateAsJson { get; set; }
}

[Serializable]
public class ComponentRequest
{
    public double AvailableHeight { get; set; }
    public double AvailableWidth { get; set; }
    public IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; set; }
    public string[] EventArgumentsAsJsonArray { get; set; }
    public string EventHandlerMethodName { get; set; }
    public string FullName { get; set; }
    public string MethodName { get; set; }

    public string SearchPartOfUrl { get; set; }
}

[Serializable]
public class ComponentResponse
{
    #region Public Properties
    public string ElementAsJsonString { get; set; }

    public IReadOnlyList<object> ClientTasks { get; set; }

    public string ErrorMessage { get; set; }

    public string NavigateToUrl { get; set; }

    public object ElementAsJson { get; set; }

    public IReadOnlyList<string> Trace { get; set; }
    #endregion
}

public static class ComponentRequestHandler
{
    #region Public Methods
    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    static  ReactContext CreateContext(ComponentRequest request)
    {
        var context = new ReactContext();

        context.Insert(BrowserInformation.UrlParameters, Mixin.ParseQueryString(request.SearchPartOfUrl));
        context.Insert(BrowserInformation.AvailableWidth, request.AvailableWidth);
        context.Insert(BrowserInformation.AvailableHeight, request.AvailableHeight);

        return context;
    }

    public static ComponentResponse HandleRequest(ComponentRequest request, Func<string, Type> findType)
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

            var instance = (Element)Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not instanstied.{request.FullName}" };
            }

            var stateTree = new StateTree
            {
                ChildStates    = request.CapturedStateTree,
                BreadCrumpPath = "0",
                RootElement    = instance,
                Context        = context
            };

            trace.Add($"Serialization started at {stopwatch.ElapsedMilliseconds}");
            
            var map = instance.ToMap(stateTree);

            trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");
            
            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJson = map,
                Trace         = trace,
                ClientTasks = context.ClientTask.ToArray()
            };
        }

        ComponentResponse handleComponentEvent()
        {
            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not found.{request.FullName}" };
            }

            var instance = (Element)Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse { ErrorMessage = $"Type not instanstied.{request.FullName}" };
            }

           
            
            
            // Init state
            {
                var errorMessage = setState(type, instance, request.CapturedStateTree["0"].StateAsJson);
                if (errorMessage != null)
                {
                    return new ComponentResponse { ErrorMessage = errorMessage };
                }

                if (instance is ReactStatefulComponent reactStatefulComponent2)
                {
                    reactStatefulComponent2.Context = context;
                    
                    reactStatefulComponent2.OnStateInitialized();
                }
            }

            // Find method
            var methodInfo = type.GetMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
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
                    RootElement    = instance,
                    Context = context
                };

                var map = instance.ToMap(stateTree);

                trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");
            

            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJson = map,
                Trace         = trace,
                ClientTasks   = context.ClientTask.ToArray()
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
    #endregion

}

public static class Json
{
    public static object DeserializeJsonByNewtonsoft(string json, Type returnType)
    {
        try
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json, returnType);
        }
        catch (Exception exception)
        {
            exception.ToString();
            throw;
        }
    }
}

public class StateTree
{
    #region Public Properties
    public ReactContext Context { get; set; }
    public string BreadCrumpPath { get; set; }
    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; set; }

    public Element RootElement { get; set; }
    #endregion
}

static class ComponentSerializer
{
    #region Public Methods
    public static StateTree GetElementSerializationExtraData(this JsonSerializerOptions options)
    {
        return (options.Converters[0] as DummyConverter)?.stateTree ?? new StateTree();
    }

    public static string SerializeComponent(Element instance, IReadOnlyDictionary<string, ClientStateInfo> childStates)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            WriteIndented = true,
            Converters =
            {
                new DummyConverter
                {
                    stateTree = new StateTree
                    {
                        ChildStates    = childStates,
                        BreadCrumpPath = "0",
                        RootElement    = instance
                    }
                }
            }
        }.ModifyForReactWithDotNet();

        return JsonSerializer.Serialize(instance, jsonSerializerOptions);
    }
    #endregion

    class Dummy
    {
    }

    class DummyConverter : JsonConverter<Dummy>
    {
        #region Public Properties
        public StateTree stateTree { get; set; }
        #endregion

        #region Public Methods
        public override Dummy Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Dummy value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}