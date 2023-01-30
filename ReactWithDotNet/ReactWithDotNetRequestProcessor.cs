using System.Text;
using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

sealed class ProcessReactWithDotNetRequestInput
{
    public ComponentRequest ComponentRequest { get; set; }

    internal Func<string, Type> findType;

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; set; }

    public HttpContext HttpContext { get; set; }

    public Action<ReactContext> OnReactContextCreated { get; set; }
    
    public Element Instance { get; set; }

    public bool CalculateSuspenseFallbackForThirdPartyReactComponents { get; set; }
}

static class ReactWithDotNetRequestProcessor
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
    public static string CalculateHtmlText(CalculateHtmlTextInput calculateHtmlTextInput)
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
            findType                       = Type.GetType,
            Instance                       = element,
            OnReactContextCreated          = calculateHtmlTextInput.OnReactContextCreated,
            BeforeSerializeElementToClient = calculateHtmlTextInput.BeforeSerializeElementToClient,
            CalculateSuspenseFallbackForThirdPartyReactComponents = true,

            ComponentRequest = new ComponentRequest
            {
                MethodName                        = "FetchComponent",
                FullName                          = element.GetType().GetFullName(),
                LastUsedComponentUniqueIdentifier = 1,
                ComponentUniqueIdentifier         = 1,
                QueryString                   = calculateHtmlTextInput.QueryString

            }
        };

        var componentResponse = ComponentRequestHandler.HandleRequest(input);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return HtmlTextGenerator.ToHtml(componentResponse);
    }

    public static async Task<string> CalculateJsonText(CalculateJsonTextInput calculateJsonTextInput)
    {
        if (calculateJsonTextInput is null)
        {
            throw new ArgumentNullException(nameof(calculateJsonTextInput));
        }

        if (calculateJsonTextInput.HttpContext is null)
        {
            throw new ArgumentNullException(string.Join('.', nameof(calculateJsonTextInput), nameof(calculateJsonTextInput.HttpContext)));
        }

        var input = new ProcessReactWithDotNetRequestInput
        {
            HttpContext                    = calculateJsonTextInput.HttpContext,
            OnReactContextCreated          = calculateJsonTextInput.OnReactContextCreated,
            BeforeSerializeElementToClient = calculateJsonTextInput.BeforeSerializeElementToClient,
        };
        
        var componentResponse = await ReactWithDotNetRequestProcessor.ProcessReactWithDotNetRequest(input);


        return componentResponse.ToJson();
    }

    public static string CalculateJsonText(Element component, string queryString, 
                                           Action<ReactContext> onReactContextCreated = null,
                                           Action<Element, ReactContext> beforeSerializeElementToClient = null)
    {
        if (component is null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        var input = new ProcessReactWithDotNetRequestInput
        {
            findType                       = Type.GetType,
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

        var componentResponse = ComponentRequestHandler.HandleRequest(input);

        if (componentResponse.ErrorMessage is not null)
        {
            throw DeveloperException(componentResponse.ErrorMessage);
        }

        return componentResponse.ToJson();
    }
}

public sealed class CalculateHtmlTextInput
{
    public ReactStatefulComponent ReactComponent  { get; init; }
    
    public string QueryString { get; init; }

    public Action<ReactContext> OnReactContextCreated { get; init; }

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }
}

public sealed class CalculateJsonTextInput
{
    public HttpContext HttpContext  { get; init; }

    public Action<ReactContext> OnReactContextCreated { get; init; }

    public Action<Element, ReactContext> BeforeSerializeElementToClient { get; init; }
}