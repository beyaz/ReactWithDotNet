using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesignerComponentPreview : ReactComponent<ReactWithDotNetDesignerModel>
{
    public void Refresh()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        Client.GotoMethod(700, Refresh);
        Client.RunJavascript("window.parent.ReactWithDotNet.DispatchEvent('ComponentPreviewRefreshed',[])");
    }

    protected override Task componentDidMount()
    {
        Client.GotoMethod(700, Refresh);

        return Task.CompletedTask;
    }

    protected override Task constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();
        
        return Task.CompletedTask;
    }

    protected override Element render()
    {
        var (successfullyRead, queryString) = ReadPropertyValueInJsonText<string>(state?.JsonTextForDotNetMethodParameters, "$QueryString");
        if (successfullyRead)
        {
            Context.Query = HttpUtility.ParseQueryString(queryString);
        }
        else
        {
            (successfullyRead, queryString) = ReadPropertyValueInJsonText<string>(state?.JsonTextForDotNetInstanceProperties, "$QueryString");
            if (successfullyRead)
            {
                Context.Query = HttpUtility.ParseQueryString(queryString);
            }
            
        }
        
        return createElement()+Border("0.5px dotted #f27070");
    }

    static Type FindType(string typeReference)
    {
        if (!string.IsNullOrWhiteSpace(typeReference))
        {
            return Type.GetType(typeReference, false);
        }

        return null;
    }

    Element createElement()
    {
        try
        {
            // try invoke as static function

            var fullAssemblyPath = state.SelectedAssemblyFilePath;
            if (File.Exists(fullAssemblyPath))
            {
                var assembly = MetadataHelper.LoadAssembly(fullAssemblyPath);
                if (state.SelectedMethod is not null)
                {
                    var methodInfo = assembly.TryLoadFrom(state.SelectedMethod);
                    if (methodInfo != null)
                    {
                        var invocationParameters = new List<object>();

                        var methodParameters = methodInfo.GetParameters();

                        var jsObject = (JObject)DeserializeJson(state.JsonTextForDotNetMethodParameters.HasValue() ? state.JsonTextForDotNetMethodParameters : "{}", typeof(JObject));
                        foreach (var parameterInfo in methodParameters)
                        {
                            var parameterName = parameterInfo.Name;
                            var parameterType = parameterInfo.ParameterType;

                            if (parameterName is not null)
                            {
                                var parameterValueAsJsonObject = jsObject[parameterName];
                                if (parameterValueAsJsonObject is not null)
                                {
                                    invocationParameters.Add(parameterValueAsJsonObject.ToObject(parameterType));
                                    continue;
                                }

                                return new div { text = $"Missing parameter {parameterName}" };
                            }

                            return new div { text = "parameterName not be evaluated" };
                        }

                        if (methodInfo.IsStatic)
                        {
                            return (Element)methodInfo.Invoke(null, invocationParameters.ToArray());
                        }

                        // invoke as instance
                        {
                            var declaringType = methodInfo.DeclaringType;
                            if (declaringType is null)
                            {
                                return new div { text = "Method declaring type is null." };
                            }

                            var instance = (Element)DeserializeJson(state.JsonTextForDotNetInstanceProperties.HasValue() ? state.JsonTextForDotNetInstanceProperties : "{}", declaringType);

                            if (instance is ReactComponentBase component)
                            {
                                component.key     = "0";
                                component.Context = Context;

                                if (component.IsStateNull)
                                {
                                    component.InvokeConstructor().GetAwaiter().GetResult();
                                }
                                component.DesignerCustomizedRender = () => (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());
                                
                                return component;
                            }

                            if (instance is ReactPureComponent reactPureComponent)
                            {
                                reactPureComponent.key     = "0";
                                reactPureComponent.Context = Context;

                                reactPureComponent.DesignerCustomizedRender = () => (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());
                                
                                return reactPureComponent;
                            }

                            return (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());
                        }
                    }
                }

                if (state.SelectedType is not null)
                {
                    var type = assembly.TryLoadFrom(state.SelectedType);
                    if (type == null)
                    {
                        return "type not found.@" + state.SelectedType.FullName;
                    }

                    var instance = (Element)DeserializeJson(state.JsonTextForDotNetInstanceProperties.HasValue() ? state.JsonTextForDotNetInstanceProperties : "{}", type);

                    if (instance is ReactComponentBase component)
                    {
                        component.key     = "0";
                        component.Context = Context;
                        
                        if (component.IsStateNull)
                        {
                            component.InvokeConstructor().GetAwaiter().GetResult();
                        }

                        return component;
                    }

                    if (instance is ReactPureComponent reactPureComponent)
                    {
                        reactPureComponent.key     = "0";
                        reactPureComponent.Context = Context;

                        return reactPureComponent;
                    }

                    return instance.ToString();
                }
            }
        }
        catch (Exception exception)
        {
            if (exception is JsonReaderException)
            {
                return new div(exception.Message);
            }
            return new div(exception.ToString());
        }

        return "Element not created. Select type or method from left panel";
    }

    static (bool successfullyRead, TValue value) ReadPropertyValueInJsonText<TValue>(string json, string propertyNameInJson)
    {
        JObject jObject;
        
        try
        {
            jObject = (JObject)DeserializeJson(json.HasValue() ? json : "{}", typeof(JObject));
        }
        catch (Exception)
        {
            return default;
        }

        if (jObject.TryGetValue(propertyNameInJson, out var jToken))
        {
            return (successfullyRead: true, jToken.ToObject<TValue>());
        }

        return default;

    }
}