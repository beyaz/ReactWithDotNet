using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

public sealed class ProcessReactWithDotNetRequestInput
{
    public ComponentRequest componentRequest;

    public Func<string, Type> findType;
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; set; }

    public HttpContext HttpContext { get; set; }
}

public static class ReactWithDotNetRequestProcessor
{
    public static async Task ProcessReactWithDotNetRequest(ProcessReactWithDotNetRequestInput input)
    {
        var context = input.HttpContext;

        ComponentRequest componentRequest = null;
        {
            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true);
            
            componentRequest = DeserializeJson<ComponentRequest>(await reader.ReadToEndAsync());
        }
        input.componentRequest = componentRequest;

        input.findType = Type.GetType;

        var response = ComponentRequestHandler.HandleRequest(input);

        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(response.ToJson());
    }
}