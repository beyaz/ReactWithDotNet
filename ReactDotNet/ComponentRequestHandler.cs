using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet;

using static Array;






[Serializable]
public sealed class ClientStateInfo
{
    public string StateAsJson { get; set; }
    public string FullTypeNameOfState { get; set; }
}

[Serializable]
public class ComponentRequest
{
    public string FullName { get; set; }
    public string StateAsJson { get; set; }
    public string MethodName { get; set; }
    public string EventHandlerMethodName { get; set; }
    public string[] EventArgumentsAsJsonArray { get; set; }
    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; set; }

    public string SearchPartOfUrl { get; set; }
    public double AvailableWidth { get; set; }
    public double AvailableHeight { get; set; }

}

[Serializable]
public class ComponentResponse
{
    public string ErrorMessage { get; set; }

    public string ElementAsJsonString { get; set; }

    public IReadOnlyList<string> Trace { get; set; }

    public string NavigateToUrl { get; set; }
}

public static class ComponentRequestHandler
{
    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    public static ComponentResponse HandleRequest(ComponentRequest request, Func<string, Type> findType)
    {
        var trace = new List<string>();

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        trace.Add($"BEGIN {stopwatch.ElapsedMilliseconds}");


        if (request.MethodName == "FetchComponent")
        {
            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse {ErrorMessage = $"Type not found.{request.FullName}"};
            }

            var instance = (Element) Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse {ErrorMessage = $"Type not instanstied.{request.FullName}"};
            }

            trace.Add($"Calling constructor started at {stopwatch.ElapsedMilliseconds}");

            if (instance is IReactStatefulComponent reactStatefulComponent)
            {
                initializeBrowserInformation(reactStatefulComponent);

                reactStatefulComponent.constructor();
            }

            trace.Add($"Calling constructor finished at {stopwatch.ElapsedMilliseconds}");


            trace.Add($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            var elementAsJsonString = ComponentSerializer.SerializeComponent(instance, request.ChildStates);

            trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJsonString = elementAsJsonString,
                Trace= trace
            };
        }

        if (request.MethodName == "HandleComponentEvent")
        {
            var type = findType(request.FullName);
            if (type == null)
            {
                return new ComponentResponse {ErrorMessage = $"Type not found.{request.FullName}"};
            }

            var instance = (Element) Activator.CreateInstance(type);
            if (instance == null)
            {
                return new ComponentResponse {ErrorMessage = $"Type not instanstied.{request.FullName}"};
            }

            if (instance is IReactStatefulComponent reactStatefulComponent)
            {
                initializeBrowserInformation(reactStatefulComponent);
            }

            var errorMessage = setState(type, instance, request.StateAsJson);
            if (errorMessage != null)
            {
                return new ComponentResponse {ErrorMessage = errorMessage};
            }

            var methodInfo = type.GetMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo == null)
            {
                return new ComponentResponse {ErrorMessage = $"Method not found.{type.FullName}::{request.EventHandlerMethodName}"};
            }

            trace.Add($"Method '{methodInfo.Name}' invocation started at {stopwatch.ElapsedMilliseconds}");
            try
            {
                methodInfo.Invoke(instance, createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray));
            }
            catch (Exception exception)
            {
                return new ComponentResponse {ErrorMessage = $"Method invocation error.{exception}"};
            }
            trace.Add($"Method '{methodInfo.Name}' invocation finished at {stopwatch.ElapsedMilliseconds}");

            trace.Add($"Serialization started at {stopwatch.ElapsedMilliseconds}");

            var elementAsJsonString = ComponentSerializer.SerializeComponent(instance, request.ChildStates);

            trace.Add($"Serialization finished at {stopwatch.ElapsedMilliseconds}");

            trace.Add($"END {stopwatch.ElapsedMilliseconds}");

            return new ComponentResponse
            {
                ElementAsJsonString = elementAsJsonString,
                Trace               = trace
            };
        }

        return new ComponentResponse {ErrorMessage = $"Not implemented method. {request.MethodName}"};

        void initializeBrowserInformation(IReactStatefulComponent reactStatefulComponent)
        {
            var context = reactStatefulComponent.Context ??= new ReactContext();
            
            context.Insert(BrowserInformation.UrlParameters, Mixin.ParseQueryString(request.SearchPartOfUrl));
            context.Insert(BrowserInformation.AvailableWidth, request.AvailableWidth);
            context.Insert(BrowserInformation.AvailableHeight, request.AvailableHeight);
        }

        string setState(Type typeOfInstance, object instance, string stateAsJson)
        {
            var statePropertyInfo = typeOfInstance.GetProperty("state", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (statePropertyInfo == null)
            {
                return $"MissingMember at {typeOfInstance.FullName}::state";
            }

            var state = JsonSerializer.Deserialize(stateAsJson, statePropertyInfo.PropertyType, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

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
                eventArguments[i] = JsonSerializer.Deserialize(eventArgumentsAsJsonArray[i], parameterInfoList[i].ParameterType);
            }

            return eventArguments;
        }
    }
}

class ElementSerializationExtraData
{
    public IReadOnlyDictionary<string, ClientStateInfo> ChildStates { get; set; }
    public string BreadCrumpPath { get; set; }

    public Element RootElement { get; set; }

}

static class ComponentSerializer
{
    public static string SerializeComponent(Element instance, IReadOnlyDictionary<string, ClientStateInfo> childStates)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters =
            {
                new DummyConverter
                {
                    ElementSerializationExtraData = new ElementSerializationExtraData
                    {
                        ChildStates    = childStates,
                        BreadCrumpPath = "0",
                        RootElement = instance
                    }
                }
            }
        }.ModifyForReactDotNet();

        return JsonSerializer.Serialize(instance, jsonSerializerOptions);
    }

    public static ElementSerializationExtraData GetElementSerializationExtraData(this JsonSerializerOptions options)
    {
        return (options.Converters[0] as DummyConverter)?.ElementSerializationExtraData ?? new ElementSerializationExtraData();
    }

    class Dummy
    {
    }

    class DummyConverter : JsonConverter<Dummy>
    {
        public ElementSerializationExtraData ElementSerializationExtraData { get; set; }

        public override Dummy Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Dummy value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}