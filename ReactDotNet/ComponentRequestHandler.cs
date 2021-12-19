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

    [Serializable]
    public class ComponentResponse
    {
        public string FullName { get; set; }
        public object State { get; set; }
        public ReactElement RootElement { get; set; }
    }

    public static class ComponentRequestHandler
    {
        public static string GetFullName(this Type type)
        {
            return $"{type.FullName},{type.Assembly.GetName().Name}";
        }
        public static ComponentResponse HandleRequest(ComponentRequest request, Func<string,Type> findType)
        {
            void setState(Type typeOfInstance,object instance, string stateAsJson)
            {
                var stateFieldInfo = typeOfInstance.GetField("state", BindingFlags.NonPublic | BindingFlags.Instance);
                if (stateFieldInfo == null)
                {
                    throw new MissingMemberException(typeOfInstance.FullName, "state");
                }

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

                        return new ComponentResponse
                        {
                            FullName    = type.GetFullName(),
                            State       = state,
                            RootElement = instance.ToReactElement()
                        };
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

                        return new ComponentResponse
                        {
                            FullName    = type.GetFullName(),
                            State       = state,
                            RootElement = instance.ToReactElement()
                        };
                    }
                }
            }


            throw new NotImplementedException(request.MethodName);
        }
    }
}