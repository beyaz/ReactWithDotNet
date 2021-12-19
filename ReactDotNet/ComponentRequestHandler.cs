using System;
using System.Reflection;

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
            if (request.MethodName == "FetchComponent")
            {
                var type = findType(request.FullName);

                if (type != null)
                {
                    var instance = (IReactComponent)Activator.CreateInstance(type);

                    if (instance != null)
                    {
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
                        var methodInfo = type.GetMethod(request.EventHandlerMethodName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        
                        methodInfo?.Invoke(instance,Array.Empty<object>());

                        request.RootElement = instance.render();
                    }
                }
            }
            

            return request;
        }
    }
}