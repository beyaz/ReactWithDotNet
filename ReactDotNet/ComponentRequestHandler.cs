using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;

namespace ReactDotNet;

using static Array;

[Serializable]
public class ClientTask
{
    public bool ComebackWithLastAction { get; set; }
}

[Serializable]
public class ComponentRequest
{
    public string FullName { get; set; }
    public string StateAsJson { get; set; }
    public string MethodName { get; set; }
    public string EventHandlerMethodName { get; set; }
    public string[] EventArgumentsAsJsonArray { get; set; }
    public double AvailableWidth { get; set; }
    public double AvailableHeight { get; set; }
    public double ScrollY { get; set; }
}

[Serializable]
public class ComponentResponse
{
    public Element Element { get; set; }

    public string ErrorMessage { get; set; }
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

            return new ComponentResponse {Element = instance};
        }

        return new ComponentResponse {ErrorMessage = $"Not implemented method. {request.MethodName}"};
    }
}