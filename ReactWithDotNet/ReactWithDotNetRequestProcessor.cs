using System.Collections;
using System.Text;
using System.Text.Json;
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
    
    public OnReactContextDisposed OnReactContextDisposed { get; init; }
    
    
    public ReactContext ReactContext { get; init; }
}

static class ReactWithDotNetRequestProcessor
{
    public static async Task<ComponentResponse> ProcessReactWithDotNetRequest(ProcessReactWithDotNetRequestInput input)
    {
        var httpContext = input.HttpContext;

        input.ComponentRequest ??= await JsonSerializer.DeserializeAsync<ComponentRequest>(httpContext.Request.Body, JsonSerializerOptionsInstance);

        return await ComponentRequestHandler.HandleRequest(input);
    }
}

partial class Mixin
{
    public static async Task ProcessReactWithDotNetPageRequest(ProcessReactWithDotNetPageRequestInput input)
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
        
        var instance = ReflectionHelper.CreateNewInstance(layoutType);

        if (instance is not IPageLayout layoutInstance)
        {
            throw DeveloperException($"{layoutType} should be support interface: {typeof(IPageLayout)}");
        }
        
        if (instance is not PureComponent layoutInstanceAsPureComponent)
        {
            throw DeveloperException($"{layoutType} should be inherit from {typeof(PureComponent)}");
        }

        var component = (Element)ReflectionHelper.CreateNewInstance(input.MainContentType);

        var httpContext = input.HttpContext;
        
        layoutInstance.RenderInfo = await CalculateComponentRenderInfo(new()
        {
            Component                      = component,
            HttpContext                    = httpContext,
            OnReactContextCreated          = input.OnReactContextCreated,
            OnReactContextDisposed         = input.OnReactContextDisposed,
            BeforeSerializeElementToClient = input.BeforeSerializeElementToClient
        });

        var reactContext = layoutInstance.RenderInfo.ComponentResponse.ReactContext;

        var sb = await CalculateComponentHtmlText(new()
        {
            HttpContext                    = httpContext,
            Component                      = layoutInstanceAsPureComponent,
            QueryString                    = httpContext.Request.QueryString.ToString(),
            OnReactContextCreated          = input.OnReactContextCreated,
            OnReactContextDisposed         = input.OnReactContextDisposed,
            ReactContext                   = reactContext,
            BeforeSerializeElementToClient = input.BeforeSerializeElementToClient
        });

        await httpContext.Response.WriteAsync(sb.ToString());
        
        await httpContext.Response.BodyWriter.FlushAsync();

        await httpContext.Response.WriteAsync("<script type='module'>");
        await httpContext.Response.WriteAsync(layoutInstance.InitialScript);
        await httpContext.Response.WriteAsync("</script>");
    }
    
    public static async Task ProcessReactWithDotNetComponentRequest(ProcessReactWithDotNetComponentRequestInput processReactWithDotNetComponentRequestInput)
    {
        if (processReactWithDotNetComponentRequestInput is null)
        {
            throw new ArgumentNullException(nameof(processReactWithDotNetComponentRequestInput));
        }

        if (processReactWithDotNetComponentRequestInput.HttpContext is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(processReactWithDotNetComponentRequestInput), nameof(processReactWithDotNetComponentRequestInput.HttpContext)));
        }

        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                    = processReactWithDotNetComponentRequestInput.HttpContext,
            OnReactContextCreated          = processReactWithDotNetComponentRequestInput.OnReactContextCreated,
            OnReactContextDisposed         = processReactWithDotNetComponentRequestInput.OnReactContextDisposed,
            BeforeSerializeElementToClient = processReactWithDotNetComponentRequestInput.BeforeSerializeElementToClient
        };

        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);
        
        await componentResponse.ToJson(processReactWithDotNetComponentRequestInput.HttpContext.Response.Body);
    }

    internal static async Task<StringBuilder> CalculateComponentHtmlText(CalculateComponentHtmlTextInput input)
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
            OnReactContextDisposed                                = input.OnReactContextDisposed,
            ReactContext                                          = input.ReactContext,
            BeforeSerializeElementToClient                        = input.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new()
            {
                MethodName                        = "FetchComponent",
                FullName                          = input.Component.GetType().SerializeToString(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1
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
            OnReactContextDisposed                                = input.OnReactContextDisposed,
            BeforeSerializeElementToClient                        = input.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new()
            {
                MethodName                        = "FetchComponent",
                FullName                          = input.Component.GetType().SerializeToString(),
                LastUsedComponentUniqueIdentifier = 1000,
                ComponentUniqueIdentifier         = 1000
            }
        };

        var componentResponse = await ComponentRequestHandler.HandleRequest(request);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return new() { ComponentResponse = componentResponse };
    }

    static void Inject(ComponentResponse componentResponse, Element component)
    {
        if (component is IPageLayout mainLayout)
        {
            var app = mainLayout.RenderInfo.ComponentResponse.ElementAsJson;

            {
                if (componentResponse.ElementAsJson is JsonMap jsonMap)
                {
                    tryUpdate(new(), jsonMap);
                }
            }

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
        }
    }

    class InjectContext
    {
        public bool isUpdated, isCurrent;
    }
}

public sealed class ProcessReactWithDotNetPageRequestInput
{
    public HttpContext HttpContext { get; init; }
    
    public Type LayoutType { get; init; }
    
    public Type MainContentType { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }
    public OnReactContextDisposed OnReactContextDisposed { get; init; }
    
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }
}

sealed class CalculateComponentHtmlTextInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public Element Component { get; init; }

    public HttpContext HttpContext { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }
    public OnReactContextDisposed OnReactContextDisposed { get; init; }

    public string QueryString { get; init; }
    
    public ReactContext ReactContext { get; init; }
}

public sealed class ProcessReactWithDotNetComponentRequestInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }

    public HttpContext HttpContext { get; init; }

    public OnReactContextCreated OnReactContextCreated { get; init; }
    public OnReactContextDisposed OnReactContextDisposed { get; init; }
}

sealed class CalculateComponentRenderInfoInput
{
    public BeforeSerializeElementToClient BeforeSerializeElementToClient { get; init; }
    public Element Component { get; init; }
    public HttpContext HttpContext { get; init; }
    public OnReactContextCreated OnReactContextCreated { get; init; }
    public OnReactContextDisposed OnReactContextDisposed { get; init; }
}

public delegate Task BeforeSerializeElementToClient(ReactContext reactContext, Element element, Element parent);

public delegate Task OnReactContextCreated(ReactContext reactContext);
public delegate Task OnReactContextDisposed(ReactContext reactContext, Exception exception);

public sealed class ComponentRenderInfo
{
    internal ComponentResponse ComponentResponse;

    public ReadOnlySpan<char> ToJsonString()
    {
        return ComponentResponse.ToJson();
    }
}

public interface IPageLayout
{
    string ContainerDomElementId { get; }
    ComponentRenderInfo RenderInfo { get; set; }
    string InitialScript { get; }
}