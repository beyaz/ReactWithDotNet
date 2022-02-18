using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactDotNet;

using static Array;

[Serializable]
public class ClientTask
{
    public bool ComebackWithLastAction { get; set; }
    public bool HistoryPushState { get; set; }
    public string HistoryPushStateTitle { get; set; }
    public string HistoryPushStateUrl { get; set; }

}

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
    public IReadOnlyDictionary<string,ClientStateInfo> ChildStates { get; set; }
}

[Serializable]
public class ComponentResponse
{
    public Element Element { get; set; }

    public string ErrorMessage { get; set; }

    public string ElementAsJsonString { get; set; }
}


public static class ComponentRequestHandler
{
    public static string GetFullName(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }

    public static ComponentResponse HandleRequest(ComponentRequest request, Func<string, Type> findType)
    {
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

        if (request.MethodName == "FetchComponent")
        {
            var type = findType(request.FullName);

            if (type != null)
            {
                var instance = (Element) Activator.CreateInstance(type);

                if (instance != null)
                {
                    return new ComponentResponse {Element = instance};
                }
            }
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

            try
            {
                methodInfo.Invoke(instance, createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray));
            }
            catch (Exception exception)
            {
                return new ComponentResponse {ErrorMessage = $"Method invocation error.{exception}"};
            }

            

            return new ComponentResponse
            {
                ElementAsJsonString = ComponentSerializer.SerializeComponent(instance, request.ChildStates)
            };
        }

        return new ComponentResponse {ErrorMessage = $"Not implemented method. {request.MethodName}"};
    }

    

   
}


class ElementSerializationExtraData
{
    public IReadOnlyDictionary<string,ClientStateInfo> ChildStates { get; set; }
    public string BreadCrumpPath { get; set; }
}
static class ComponentSerializer
{
    public static string SerializeComponent(Element instance, IReadOnlyDictionary<string,ClientStateInfo> childStates)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            Converters = {new DummyConverter {ElementSerializationExtraData = new ElementSerializationExtraData
            {
                ChildStates = childStates,
                BreadCrumpPath = "0"
            }}}
        }.ModifyForReactDotNet();

        return System.Text.Json.JsonSerializer.Serialize(instance, jsonSerializerOptions);
    }

    public static ElementSerializationExtraData GetElementSerializationExtraData(this JsonSerializerOptions options)
    {
        return (options.Converters[0] as DummyConverter)?.ElementSerializationExtraData ?? new ElementSerializationExtraData()
        {
            
        };
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