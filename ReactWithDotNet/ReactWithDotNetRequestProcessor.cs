using System.Collections;
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
    public static async Task<string> CalculateComponentHtmlText(CalculateComponentHtmlTextInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        if (input.Component is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(input), nameof(input.Component)));
        }

        var request = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                                           = input.HttpContext,
            FindType                                              = Type.GetType,
            Instance                                              = input.Component,
            OnReactContextCreated                                 = input.OnReactContextCreated,
            BeforeSerializeElementToClient                        = input.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

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

        Inject(componentResponse, input.Component);

        return HtmlTextGenerator.ToHtml(componentResponse);
    }

    public static async Task<ComponentRenderInfo> CalculateComponentRenderInfo(CalculateComponentRenderInfoInput input)
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
            HttpContext                                           = input.HttpContext,
            FindType                                              = Type.GetType,
            Instance                                              = input.Component,
            OnReactContextCreated                                 = input.OnReactContextCreated,
            BeforeSerializeElementToClient                        = input.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

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

        return new ComponentRenderInfo { ComponentResponse = componentResponse };
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

    static void Inject(ComponentResponse componentResponse, Element component)
    {
        if (component is IPageLayout mainLayout)
        {
            var app = mainLayout.RenderInfo.ComponentResponse.ElementAsJson;

            var isUpdated = false;

            var isCurrent = false;

            void tryUpdate(JsonMap jsonMap)
            {
                jsonMap.Foreach(tryReplace);
                if (isCurrent)
                {
                    jsonMap.Add("$children", new[] { app });
                    componentResponse.DynamicStyles = mainLayout.RenderInfo.ComponentResponse.DynamicStyles;

                    isUpdated = true;
                }
            }

            void tryReplace(string key, object value)
            {
                if (key == "id" && value is string stringValue && stringValue == mainLayout.ContainerDomElementId)
                {
                    isCurrent = true;
                    return;
                }

                if (isUpdated)
                {
                    return;
                }

                if (value is JsonMap jsonMap)
                {
                    tryUpdate(jsonMap);
                }

                if (value is IList jsonMaps)
                {
                    foreach (var item in jsonMaps)
                    {
                        if (item is JsonMap map)
                        {
                            tryUpdate(map);

                            if (isUpdated)
                            {
                                return;
                            }
                        }
                    }
                }
            }

            {
                if (componentResponse.ElementAsJson is JsonMap jsonMap)
                {
                    tryUpdate(jsonMap);
                }
            }
        }
    }
}

public sealed class CalculateComponentHtmlTextInput
{
    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }

    public Element Component { get; init; }

    public HttpContext HttpContext { get; init; }

    public Func<HttpContext, ReactContext, Task> OnReactContextCreated { get; init; }

    public string QueryString { get; init; }
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

public sealed class ComponentRenderInfo
{
    internal ComponentResponse ComponentResponse;

    public string ToJsonString => ComponentResponse.ToJson();
}

public interface IPageLayout
{
    string ContainerDomElementId { get; }
    ComponentRenderInfo RenderInfo { get; set; }
}