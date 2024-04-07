using System.Collections;
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
        
        s.border ??= $"0.5px dotted {Blue500}";
    });

    public Task Refresh()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        return Task.CompletedTask;
    }

    internal static async Task<Element> CreateElement(ReactWithDotNetDesignerModel state, ReactContext Context)
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

                        var dictionary = new Dictionary<string, object>();
                        
                        if (state.JsonTextForDotNetMethodParameters.HasValue())
                        {
                            dictionary = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters);
                        }
                        
                        dictionary ??= new();
                        
                        foreach (var parameterInfo in methodParameters)
                        {
                            var parameterName = parameterInfo.Name;
                            var parameterType = parameterInfo.ParameterType;

                            object parameterValue;
                                
                            if (parameterName is not null && dictionary.TryGetValue(parameterName, out var parameterValueAsJsonObject))
                            {
                                parameterValue = ArrangeValueForTargetType(parameterValueAsJsonObject, parameterType);
                            }
                            else
                            {
                                parameterValue = parameterType.IsClass ? null : Activator.CreateInstance(parameterType);    
                            }

                            invocationParameters.Add(parameterValue);
                        }

                        if (methodInfo.IsStatic)
                        {
                            var invocationResponse = methodInfo.Invoke(null, invocationParameters.ToArray());

                            if (invocationResponse is null)
                            {
                                return null;
                            }
                            
                            if (invocationResponse is Task task)
                            {
                                await task;
                                        
                                invocationResponse = task.GetType()
                                    .GetProperty("Result", BindingFlags.Instance | BindingFlags.Public)!
                                    .GetValue(task);
                            }
                            
                            
                            if (invocationResponse is Element invocationResultAsElement)
                            {
                                return invocationResultAsElement;
                            }

                            if (invocationResponse is IEnumerable enumerable)
                            {
                                return new Fragment
                                {
                                    enumerable.ToReadOnlyListOf<object, Element>(x => x as Element)
                                };
                            }
                            
                            return new div { text = $"Method should return Element or FC but returned {invocationResponse?.GetType().FullName}" };
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

                                ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);

                                if (component.IsStateNull)
                                {
                                    tryUpdateStateFromStateTree(component, Context);
                                }

                                if (component.IsStateNull)
                                {
                                    component.InvokeConstructor().GetAwaiter().GetResult();
                                }

                                component.DesignerCustomizedRender = async () =>
                                {
                                     var invocationResponse = methodInfo.Invoke(instance, invocationParameters.ToArray());

                                     if (invocationResponse is null)
                                     {
                                         return null;
                                     }
                                    
                                     if (invocationResponse is Task task)
                                     {
                                         await task;
                                        
                                         invocationResponse = task.GetType()
                                             .GetProperty("Result", BindingFlags.Instance | BindingFlags.Public)!
                                             .GetValue(task);
                                     }

                                     return (Element)invocationResponse;
                                };

                                return component;
                            }

                            if (instance is PureComponent reactPureComponent)
                            {
                                ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);

                                reactPureComponent.DesignerCustomizedRender = async () =>
                                {
                                    var invocationResponse = methodInfo.Invoke(instance, invocationParameters.ToArray());

                                    if (invocationResponse is null)
                                    {
                                        return null;
                                    }
                                    
                                    if (invocationResponse is Task task)
                                    {
                                        await task;
                                        
                                        invocationResponse = task.GetType()
                                            .GetProperty("Result", BindingFlags.Instance | BindingFlags.Public)!
                                            .GetValue(task);
                                    }

                                    return (Element)invocationResponse;
                                };

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

                    var propertyInfoList = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                    var map = JsonSerializer.Deserialize<Dictionary<string, object>>(json.HasValue() ? json : "{}");
                    foreach (var (propertyName, propertyValue) in map)
                    {
                        var propertyInfo = propertyInfoList.FirstOrDefault(p => p.Name == propertyName);
                        if (propertyInfo is null)
                        {
                            propertyInfo = propertyInfoList.FirstOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));
                        }

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

                        ModifyElementByJson(state.JsonTextForDotNetInstanceProperties, instance);

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

    protected override Task constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        Client.ListenEvent("RefreshComponentPreview", Refresh);

        return Task.CompletedTask;
    }

    protected override async Task<Element> renderAsync()
    {
        return (await CreateElement(state, Context)) + ComponentIndicatorStyle;
    }
}