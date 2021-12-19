using System;

namespace ReactDotNet
{


    [Serializable]
    public class ComponentRequest
    {
        public string FullName { get; set; }
        public string StateAsJson { get; set; }
        public string MethodName { get; set; }
        public ReactElement RootElement { get; set; }
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

            return request;
        }
    }
}