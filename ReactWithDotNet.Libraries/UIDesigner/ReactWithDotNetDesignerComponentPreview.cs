using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesignerComponentPreview : ReactComponent<ReactWithDotNetDesignerModel>
{
    public void Refresh()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        Client.GotoMethod(700, Refresh);
    }

    protected override Task componentDidMount()
    {
        Client.GotoMethod(700, Refresh);

        return Task.CompletedTask;
    }

    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();
    }

    protected override Element render()
    {
        return createElement();
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
                                component.ComponentUniqueIdentifier = 1000;
                                component.key                       = "0";
                                component.Context                   = Context;
                                component.InvokeConstructor();
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

                        if (type.GetProperty("state", BindingFlags.Instance | BindingFlags.Public)?.GetValue(instance) is null)
                        {
                            component.InvokeConstructor();
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
            return new div(exception.ToString());
        }

        return "Element not created. Select type or method from left panel";
    }
}