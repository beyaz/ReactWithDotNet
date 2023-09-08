using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

sealed class ProcessReactWithDotNetRequestInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; init; }
    
    public ComponentRequest ComponentRequest { get; set; }

    public HttpContext HttpContext { get; init; }

    public Element Instance { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }
    
    public ReactContext ReactContext { get; init; }
}

static class ReactWithDotNetRequestProcessor
{
    public static async Task<ComponentResponse> ProcessReactWithDotNetRequest(ProcessReactWithDotNetRequestInput input)
    {
        var httpContext = input.HttpContext;

        input.ComponentRequest ??= await readJson();

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
    public static async Task<string> CalculateFirstRender(CalculateFirstRenderInput input)
    {
        if (input is null)
        {
            throw new ArgumentNullException(nameof(input));
        }

        var layoutType = input.LayoutType;
        if (layoutType is null)
        {
            throw new ArgumentNullException(nameof(input.LayoutType));
        }
        
        var instance = Activator.CreateInstance(layoutType);

        var layoutInstance = instance as IPageLayout;
        if (layoutInstance == null)
        {
            throw DeveloperException($"{layoutType} should be support interface: {typeof(IPageLayout)}");
        }

        var component = (Element)Activator.CreateInstance(input.MainContentType);

        layoutInstance.RenderInfo = await CalculateComponentRenderInfo(new CalculateComponentRenderInfoInput
        {
            Component                      = component,
            HttpContext                    = input.HttpContext,
            QueryString                    = input.HttpContext.Request.QueryString.ToString(),
            OnReactContextCreated          = input.OnReactContextCreated,
            BeforeSerializeElementToClient = input.BeforeSerializeElementToClient
        });

        var reactContext = layoutInstance.RenderInfo.ComponentResponse.ReactContext;

        return await CalculateComponentHtmlText(new CalculateComponentHtmlTextInput
        {
            HttpContext                    = input.HttpContext,
            Component                      = (Element)layoutInstance,
            QueryString                    = input.HttpContext.Request.QueryString.ToString(),
            OnReactContextCreated          = input.OnReactContextCreated,
            ReactContext = reactContext,
            BeforeSerializeElementToClient = input.BeforeSerializeElementToClient
        });
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

    internal static async Task<string> CalculateComponentHtmlText(CalculateComponentHtmlTextInput input)
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
            Instance                                              = input.Component,
            OnReactContextCreated                                 = input.OnReactContextCreated,
            ReactContext                                          = input.ReactContext,
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

    static async Task<ComponentRenderInfo> CalculateComponentRenderInfo(CalculateComponentRenderInfoInput input)
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
            Instance                                              = input.Component,
            OnReactContextCreated                                 = input.OnReactContextCreated,
            BeforeSerializeElementToClient                        = input.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = input.Component.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1000,
                ComponentUniqueIdentifier         = 1000,
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

    static void Inject(ComponentResponse componentResponse, Element component)
    {
        if (component is IPageLayout mainLayout)
        {
            var app = mainLayout.RenderInfo.ComponentResponse.ElementAsJson;

            void tryUpdate(InjectContext context, JsonMap jsonMap)
            {
                jsonMap.Foreach(context, tryReplace);
                if (context.isCurrent)
                {
                    context.isCurrent = false;

                    jsonMap.Add("$children", new[] { app });
                    componentResponse.DynamicStyles                     = mainLayout.RenderInfo.ComponentResponse.DynamicStyles;
                    componentResponse.CallFunctionId                    = mainLayout.RenderInfo.ComponentResponse.CallFunctionId;
                    componentResponse.LastUsedComponentUniqueIdentifier = mainLayout.RenderInfo.ComponentResponse.LastUsedComponentUniqueIdentifier;
                    componentResponse.Trace                             = mainLayout.RenderInfo.ComponentResponse.Trace;

                    context.isUpdated = true;
                }
            }

            void tryReplace(InjectContext context, string key, object value)
            {
                if (context.isUpdated)
                {
                    return;
                }

                if (key == "id" && value is string stringValue && stringValue == mainLayout.ContainerDomElementId)
                {
                    context.isCurrent = true;
                    return;
                }

                if (value is JsonMap jsonMap)
                {
                    tryUpdate(context, jsonMap);
                }

                if (value is IList jsonMaps)
                {
                    foreach (var item in jsonMaps)
                    {
                        if (item is JsonMap map)
                        {
                            tryUpdate(context, map);

                            if (context.isUpdated)
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
                    tryUpdate(new InjectContext(), jsonMap);
                }
            }
        }
    }

    class InjectContext
    {
        public bool isUpdated, isCurrent;
    }
}

public sealed class CalculateFirstRenderInput
{
    public Type LayoutType, MainContentType;
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public HttpContext HttpContext { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }

    public string QueryString { get; init; }
}

sealed class CalculateComponentHtmlTextInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public Element Component { get; init; }

    public HttpContext HttpContext { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }

    public string QueryString { get; init; }
    
    public ReactContext ReactContext { get; init; }
}

public sealed class CalculateRenderInfoInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public HttpContext HttpContext { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }
}

public sealed class CalculateComponentRenderInfoInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }
    public Element Component { get; init; }
    public HttpContext HttpContext { get; init; }
    public OnReactContextCreated OnReactContextCreated { get; init; }
    public string QueryString { get; init; }
}

public delegate void BeforeSerializeElementToClient(ReactContext reactContext, Element element, Element parent);

public delegate Task OnReactContextCreated(HttpContext httpContext, ReactContext reactContext);

public sealed class ComponentRenderInfo
{
    internal ComponentResponse ComponentResponse;

    public string ToJsonString()
    {
        return ComponentResponse.ToJson();
    }
}

public interface IPageLayout
{
    string ContainerDomElementId { get; }
    ComponentRenderInfo RenderInfo { get; set; }
}