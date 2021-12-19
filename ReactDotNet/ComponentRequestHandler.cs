using System;
using System.Reflection;
using System.Text.Json;

namespace ReactDotNet
{


    [Serializable]
    public class ComponentRequest
    {
        public string FullName { get; set; }
        public string StateAsJson { get; set; }
        public string MethodName { get; set; }
        public ReactElement RootElement { get; set; }
        public string EventHandlerMethodName { get; set; }
    }

    public static class ComponentRequestHandler
    {
        public static ComponentRequest HandleRequest(ComponentRequest request, Func<string,Type> findType)
        {
            void setState(Type typeOfInstance,object instance, string stateAsJson)
            {
                var stateFieldInfo = typeOfInstance.GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
                if (stateFieldInfo == null)
                {
                    throw new MissingMemberException(typeOfInstance.FullName, "state");
                }

                stateAsJson = stateAsJson.RemoveFromStart("\"").RemoveFromEnd("\"");

                

                var state = JsonSerializer.Deserialize(stateAsJson, stateFieldInfo.FieldType, JsonSerializationOptionHelper.Modify(new JsonSerializerOptions()));

                stateFieldInfo.SetValue(instance,state);
            }

            if (request.MethodName == "FetchComponent")
            {
                var type = findType(request.FullName);

                if (type != null)
                {
                    var instance = (IReactComponent)Activator.CreateInstance(type);

                    if (instance != null)
                    {
                        var state = type.GetField("state", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance);

                        request.StateAsJson = JsonSerializationOptionHelper.ToJson(state);
                        request.RootElement = instance.render();
                    }
                }
            }

            if (request.MethodName == "HandleComponentEvent")
            {
                var type = findType(request.FullName);

                if (type != null)
                {
                    var instance = (IReactComponent)Activator.CreateInstance(type);

                    if (instance != null)
                    {
                        setState(type, instance, request.StateAsJson);

                        var methodInfo = type.GetMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        
                        methodInfo?.Invoke(instance,Array.Empty<object>());

                        var state = type.GetField("state", BindingFlags.NonPublic | BindingFlags.Instance)?.GetValue(instance);

                        request.StateAsJson = JsonSerializationOptionHelper.ToJson(state);
                        request.RootElement = instance.render();
                    }
                }
            }
            

            return request;
        }
    }
}