using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

sealed class ProcessReactWithDotNetRequestInput
{
    internal Func<string, Type> FindType;

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; set; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }
    public ComponentRequest ComponentRequest { get; set; }

    public HttpContext HttpContext { get; set; }

    public Element Instance { get; set; }

    public Func<ReactContext, Task> OnReactContextCreated { get; set; }
}

static class ReactWithDotNetRequestProcessor
{
    public static async Task<ComponentResponse> ProcessReactWithDotNetRequest(ProcessReactWithDotNetRequestInput input)
    {
        var httpContext = input.HttpContext;

        input.ComponentRequest ??= await readJson();

        input.FindType = Type.GetType;

        return await ComponentRequestHandler.HandleRequest(input);

        async Task<ComponentRequest> readJson()
        {
            using var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8, true, 1024, true);

            return DeserializeJson<ComponentRequest>(await reader.ReadToEndAsync());
        }
    }
}

partial class Mixin
{
    public static async Task<string> CalculateHtmlText(CalculateHtmlTextInput calculateHtmlTextInput)
    {
        if (calculateHtmlTextInput is null)
        {
            throw new ArgumentNullException(nameof(calculateHtmlTextInput));
        }

        if (calculateHtmlTextInput.ReactComponent is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(calculateHtmlTextInput), nameof(calculateHtmlTextInput.ReactComponent)));
        }

        var element = calculateHtmlTextInput.ReactComponent;

        var input = new ProcessReactWithDotNetRequestInput
        {
            FindType                                              = Type.GetType,
            Instance                                              = element,
            OnReactContextCreated                                 = calculateHtmlTextInput.OnReactContextCreated,
            BeforeSerializeElementToClient                        = calculateHtmlTextInput.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = element.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                QueryString                       = calculateHtmlTextInput.QueryString
            }
        };

        var componentResponse = await ComponentRequestHandler.HandleRequest(input);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return HtmlTextGenerator.ToHtml(componentResponse);
    }

    public static async Task<string> CalculateRenderInfo(CalculateRenderInfoInput calculateRenderInfoInput)
    {
        if (calculateRenderInfoInput is null)
        {
            throw new ArgumentNullException(nameof(calculateRenderInfoInput));
        }

        if (calculateRenderInfoInput.HttpContext is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(calculateRenderInfoInput), nameof(calculateRenderInfoInput.HttpContext)));
        }

        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                    = calculateRenderInfoInput.HttpContext,
            OnReactContextCreated          = calculateRenderInfoInput.OnReactContextCreated,
            BeforeSerializeElementToClient = calculateRenderInfoInput.BeforeSerializeElementToClient,
        };

        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);

        return componentResponse.ToJson();
    }

    public static async Task<string> CalculateRenderInfo(Element component,
                                                         string queryString,
                                                         Func<ReactContext, Task> onReactContextCreated = null,
                                                         Action<Element, ReactContext> beforeSerializeElementToClient = null)
    {
        if (component is null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        var input = new ProcessReactWithDotNetRequestInput
        {
            FindType                       = Type.GetType,
            Instance                       = component,
            OnReactContextCreated          = onReactContextCreated,
            BeforeSerializeElementToClient = beforeSerializeElementToClient,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = component.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                QueryString                       = queryString
            }
        };

        var componentResponse = await ComponentRequestHandler.HandleRequest(input);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return componentResponse.ToJson();
    }
}

public sealed class CalculateHtmlTextInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public Func<ReactContext, Task> OnReactContextCreated { get; init; }

    public string QueryString { get; init; }
    public Element ReactComponent { get; init; }
}

public sealed class CalculateRenderInfoInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }
    public HttpContext HttpContext { get; init; }

    public Func<ReactContext, Task> OnReactContextCreated { get; init; }
}