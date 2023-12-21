using System.Reflection;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesignerComponentPreview : Component<ReactWithDotNetDesignerModel>
{
    static StyleModifier ComponentIndicatorStyle => new(s =>
    {
        if (s.border is not null || s.boxShadow is not null)
        {
            return;
        }

        s.border ??= "0.5px dotted blue";
    });

    public Task Refresh()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        return Task.CompletedTask;
    }

    protected override Task constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        Client.ListenEvent("RefreshComponentPreview", Refresh);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return createElement() + ComponentIndicatorStyle;
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

                        if (state.JsonTextForDotNetMethodParameters.HasValue())
                        {
                            var dictionary = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters);
                            foreach (var parameterInfo in methodParameters)
                            {
                                var parameterName = parameterInfo.Name;
                                var parameterType = parameterInfo.ParameterType;

                                if (parameterName is not null && dictionary.TryGetValue(parameterName, out var parameterValueAsJsonObject))
                                {
                                    invocationParameters.Add(ArrangeValueForTargetType(parameterValueAsJsonObject, parameterType));
                                }

                                return new div { text = $"Missing parameter {parameterName}" };
                            }
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

                            var instance = createInstance(declaringType);

                            if (instance is ReactComponentBase component)
                            {
                                tryUpdateStatePropertyFromJson(state.JsonTextForDotNetInstanceProperties, instance);
                                
                                if (component.IsStateNull)
                                {
                                    tryUpdateStateFromStateTree(component, Context);
                                }

                                if (component.IsStateNull)
                                {
                                    component.InvokeConstructor().GetAwaiter().GetResult();
                                }

                                ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);
                                
                                component.DesignerCustomizedRender = () => (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());

                                return component;
                            }

                            if (instance is PureComponent reactPureComponent)
                            {
                                ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);
                                
                                reactPureComponent.DesignerCustomizedRender = () => (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());

                                return reactPureComponent;
                            }

                            return (Element)methodInfo.Invoke(instance, invocationParameters.ToArray());
                        }
                    }
                }

                static void tryUpdateStatePropertyFromJson(string jsonTextForDotNetInstanceProperties, object instance)
                {
                    var type = instance.GetType();

                    if (string.IsNullOrWhiteSpace(jsonTextForDotNetInstanceProperties))
                    {
                        return;
                    }

                    var map = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(jsonTextForDotNetInstanceProperties);

                    if (map.TryGetValue("state", out var stateValue))
                    {
                        var stateProperty = type.GetProperty("state", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (stateProperty is null)
                        {
                            return;
                        }

                        stateProperty.SetValue(instance, ArrangeValueForTargetType(stateValue, stateProperty.PropertyType));
                    }
                }

                static void ModifyElementByJson(string json, object instance)
                {
                    var type = instance.GetType();

                    var map = JsonSerializer.Deserialize<Dictionary<string, object>>(json.HasValue() ? json : "{}");
                    foreach (var (propertyName, propertyValue) in map)
                    {
                        var propertyInfo = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
                        if (propertyInfo is not null && propertyInfo.GetIndexParameters().Length == 0)
                        {
                            propertyInfo.SetValue(instance, ArrangeValueForTargetType(propertyValue, propertyInfo.PropertyType));
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

                    var instance = createInstance(type);
                    
                    if (instance is ReactComponentBase component)
                    {
                        tryUpdateStatePropertyFromJson(state.JsonTextForDotNetInstanceProperties, instance);
                        
                        if (component.IsStateNull)
                        {
                            tryUpdateStateFromStateTree(component, Context);
                        }

                        if (component.IsStateNull)
                        {
                            component.InvokeConstructor().GetAwaiter().GetResult();
                        }

                        ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);
                        
                        return component;
                    }

                    if (instance is PureComponent reactPureComponent)
                    {
                        ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);
                        
                        return reactPureComponent;
                    }

                    return instance.ToString();
                }
            }
        }
        catch (Exception exception)
        {
            if (exception is JsonException)
            {
                return new div(exception.Message);
            }

            return new div(exception.ToString());
        }

        return "Element not created. Select type or method from left panel";

        Element createInstance(Type type)
        {
            var instance = (Element)Activator.CreateInstance(type);
            if (instance is ReactComponentBase component)
            {
                component.key     = "0";
                component.Context = Context;
            }

            if (instance is PureComponent reactPureComponent)
            {
                reactPureComponent.key     = "0";
                reactPureComponent.Context = Context;
            }

            return instance;
        }

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

                        var val = DeserializeJsonBySystemTextJson(stateAsJson, propertyType);

                        stateProperty.SetValue(component, val);
                    }
                }
            }
        }
    }
}