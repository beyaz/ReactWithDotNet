using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using static System.Array;

namespace ReactDotNet
{
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
        
    }

    [Serializable]
    public class ComponentResponse
    {
       
    }

    public static class ComponentRequestHandler
    {
        public static string GetFullName(this Type type)
        {
            return $"{type.FullName},{type.Assembly.GetName().Name}";
        }
        public static Element HandleRequest(ComponentRequest request, Func<string,Type> findType)
        {
            void setState(Type typeOfInstance,object instance, string stateAsJson)
            {
                var statePropertyInfo = typeOfInstance.GetProperty("state", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                if (statePropertyInfo == null)
                {
                    throw new MissingMemberException(typeOfInstance.FullName, "state");
                }

                var state = JsonSerializer.Deserialize(stateAsJson, statePropertyInfo.PropertyType, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

                statePropertyInfo.SetValue(instance,state);
            } 
            if (request.MethodName == "FetchComponent")
            {
                var type = findType(request.FullName);

                if (type != null)
                {
                    var instance = (Element)Activator.CreateInstance(type);

                    if (instance != null)
                    {
                        return instance;
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

                if (type != null)
                {
                    var instance = (Element)Activator.CreateInstance(type);

                    if (instance != null)
                    {
                        setState(type, instance, request.StateAsJson);

                        var methodInfo = type.GetMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                        methodInfo?.Invoke(instance, createMethodArguments(methodInfo, request.EventArgumentsAsJsonArray));

                        return instance;
                    }
                }
            }

            throw new NotImplementedException(request.MethodName);
        }
    }
}