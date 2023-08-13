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

    public Func<HttpContext, ReactContext, Task> OnReactContextCreated { get; set; }
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
    public static async Task<string> CalculateComponentRenderInfo(CalculateComponentRenderInfoInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (input.Component is null)
        {
            throw new ArgumentNullException(nameof(input.Component));
        }

        var request = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                    = input.HttpContext,
            FindType                       = Type.GetType,
            Instance                       = input.Component,
            OnReactContextCreated          = input.OnReactContextCreated,
            BeforeSerializeElementToClient = input.BeforeSerializeElementToClient,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = input.Component.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                QueryString                       = input.QueryString
            }
        };

        var componentResponse = await ComponentRequestHandler.HandleRequest(request);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return componentResponse.ToJson();
    }

    public static async Task<string> CalculateComponentHtmlText(CalculateComponentHtmlTextInput calculateComponentHtmlTextInput)
    {
        if (calculateComponentHtmlTextInput is null)
        {
            throw new ArgumentNullException(nameof(calculateComponentHtmlTextInput));
        }

        if (calculateComponentHtmlTextInput.Component is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(calculateComponentHtmlTextInput), nameof(calculateComponentHtmlTextInput.Component)));
        }

        var element = calculateComponentHtmlTextInput.Component;

        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                                           = calculateComponentHtmlTextInput.HttpContext,
            FindType                                              = Type.GetType,
            Instance                                              = element,
            OnReactContextCreated                                 = calculateComponentHtmlTextInput.OnReactContextCreated,
            BeforeSerializeElementToClient                        = calculateComponentHtmlTextInput.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = element.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                QueryString                       = calculateComponentHtmlTextInput.QueryString
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
            BeforeSerializeElementToClient = calculateRenderInfoInput.BeforeSerializeElementToClient
        };

        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);

        return componentResponse.ToJson();
    }
}

public sealed class CalculateComponentHtmlTextInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public HttpContext HttpContext { get; init; }

    public Func<HttpContext, ReactContext, Task> OnReactContextCreated { get; init; }

    public string QueryString { get; init; }
    
    public Element Component { get; init; }
}

public sealed class CalculateRenderInfoInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public HttpContext HttpContext { get; init; }

    public Func<HttpContext, ReactContext, Task> OnReactContextCreated { get; init; }
}

public sealed class CalculateComponentRenderInfoInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }
    public Element Component { get; init; }
    public HttpContext HttpContext { get; init; }
    public Func<HttpContext, ReactContext, Task> OnReactContextCreated { get; init; }
    public string QueryString { get; init; }
}