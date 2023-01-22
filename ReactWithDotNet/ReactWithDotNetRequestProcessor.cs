using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

public sealed class ProcessReactWithDotNetRequestInput
{
    public ComponentRequest ComponentRequest { get; set; }

    internal Func<string, Type> findType;

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; set; }

    public HttpContext HttpContext { get; set; }

    public Action<ReactContext> OnReactContextCreated { get; set; }
    
    public Element Instance { get; set; }
}

public static class ReactWithDotNetRequestProcessor
{
    public static async Task<ComponentResponse> ProcessReactWithDotNetRequest(ProcessReactWithDotNetRequestInput input)
    {
        var httpContext = input.HttpContext;

        input.ComponentRequest ??= await readJson();

        input.findType = Type.GetType;

        return ComponentRequestHandler.HandleRequest(input);

        async Task<ComponentRequest> readJson()
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true);

            return DeserializeJson<ComponentRequest>(await reader.ReadToEndAsync());
        }
    }
}

partial class Mixin
{
    public static async Task<string> CalculateHtmlText(Element element, HttpContext httpContext)
    {
        var input = new ProcessReactWithDotNetRequestInput
        {
            Instance    = element,
            HttpContext = httpContext,
            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = element.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                SearchPartOfUrl                   = httpContext.Request.QueryString.ToString()
            }
        };

        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);

        return componentResponse.ToHtml();
    }
}