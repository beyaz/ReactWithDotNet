using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Type = System.Type;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesignerComponentPreview : Component<ReactWithDotNetDesignerModel>
{
    public DateTime? LastWriteTime { get; set; }

    public Task Refresh()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        Client.GotoMethod(700, Refresh);

        var fullAssemblyPath = state.SelectedAssemblyFilePath;
     
        if (File.Exists(fullAssemblyPath))
        {
            var fileInfo = new FileInfo(fullAssemblyPath);
            if (LastWriteTime != fileInfo.LastWriteTime)
            {
                Client.RunJavascript("window.parent.ReactWithDotNet.DispatchEvent('ComponentPreviewRefreshed',[])");
            }

            LastWriteTime = fileInfo.LastWriteTime;
        }
        
        return Task.CompletedTask;
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
        return createElement() + ComponentIndicatorStyle;
    }

    static StyleModifier ComponentIndicatorStyle => new(s => { s.border ??= "0.5px dotted blue"; });
    
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
                            tryUpdateStatePropertyFromJson(state.JsonTextForDotNetInstanceProperties, instance);
                            if (instance is ReactComponentBase component)
                            {
                                component.key     = "0";
                                component.Context = Context;

                                if (component.IsStateNull)
                                {
                                    tryUpdateStateFromStateTree(component, Context);
                                }

                                if (component.IsStateNull)
                                {
                                    component.InvokeConstructor().GetAwaiter().GetResult();
                                }

                                component.DesignerCustomizedRender = () => (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());

                                return component;
                            }

                            if (instance is PureComponent reactPureComponent)
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

                static void tryUpdateStatePropertyFromJson(string jsonTextForDotNetInstanceProperties, object instance)
                {
                    Type type = instance.GetType();
                    
                    if (string.IsNullOrWhiteSpace(jsonTextForDotNetInstanceProperties))
                    {
                        return;
                    }
                    
                    var jsonForInstance = (JObject)DeserializeJson(jsonTextForDotNetInstanceProperties, typeof(JObject));
                    var jsonForInstanceState = jsonForInstance["state"];
                    if (jsonForInstanceState is null)
                    {
                        return;
                    }
                    var stateProperty = type.GetProperty("state", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (stateProperty is null)
                    {
                        return;
                    }
                    
                    var stateValue = jsonForInstanceState.ToObject(stateProperty.PropertyType);
                    
                    stateProperty.SetValue(instance, stateValue);
                }
                
                if (state.SelectedType is not null)
                {
                    var type = assembly.TryLoadFrom(state.SelectedType);
                    if (type == null)
                    {
                        return "type not found.@" + state.SelectedType.FullName;
                    }

                    var instance = (Element)DeserializeJson(state.JsonTextForDotNetInstanceProperties.HasValue() ? state.JsonTextForDotNetInstanceProperties : "{}", type);

                    tryUpdateStatePropertyFromJson(state.JsonTextForDotNetInstanceProperties, instance);
                    
                    if (instance is ReactComponentBase component)
                    {
                        component.key     = "0";
                        component.Context = Context;

                        if (component.IsStateNull)
                        {
                            tryUpdateStateFromStateTree(component, Context);
                        }
                        

                        if (component.IsStateNull)
                        {
                            component.InvokeConstructor().GetAwaiter().GetResult();
                        }

                        return component;
                    }

                    if (instance is PureComponent reactPureComponent)
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

        static void tryUpdateStateFromStateTree(object component, ReactContext reactContext)
        {
            if (reactContext.CapturedStateTree?.TryGetValue("0,0", out var stateInfo) == true)
            {
                var stateAsJson = stateInfo.StateAsJson;

                if (!string.IsNullOrWhiteSpace(stateAsJson))
                {
                    var stateProperty = component.GetType().GetProperty("state", BindingFlags.Instance | BindingFlags.NonPublic);
                    if (stateProperty is not null)
                    {
                        var propertyType = stateProperty.PropertyType;

                        var val = DeserializeJson(stateAsJson, propertyType);

                        stateProperty.SetValue(component, val);
                    }
                }
            }
        }
    }
}