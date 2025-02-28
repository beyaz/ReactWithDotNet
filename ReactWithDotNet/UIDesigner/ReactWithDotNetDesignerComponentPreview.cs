﻿using System.Collections;
using System.Collections.Immutable;
using System.Reflection;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

sealed class ReactWithDotNetDesignerComponentPreview : Component<ReactWithDotNetDesignerModel>
{
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

                object calculateDummyParameter(ParameterInfo parameterInfo)
                {
                    var parameterName = parameterInfo.Name;
                    var parameterType = parameterInfo.ParameterType;

                    var dummyValue = tryGetDummyValue(assembly, parameterName, parameterType);
                    if (dummyValue is not null)
                    {
                        return dummyValue;
                    }

                    if (parameterType.IsValueType)
                    {
                        return Activator.CreateInstance(parameterType);
                    }

                    // try create empty array for collection types
                    if (parameterType.IsGenericType)
                    {
                        var genericTypeDefinition = parameterType.GetGenericTypeDefinition();

                        if (genericTypeDefinition == typeof(IReadOnlyList<>) ||
                            genericTypeDefinition == typeof(IReadOnlyCollection<>) ||
                            genericTypeDefinition == typeof(IList<>) ||
                            genericTypeDefinition == typeof(IReadOnlyList<>))
                        {
                            var genericArguments = parameterType.GetGenericArguments();

                            return Activator.CreateInstance(genericArguments[0].MakeArrayType(), 0);
                        }

                        if (genericTypeDefinition == typeof(ImmutableArray<>) ||
                            genericTypeDefinition == typeof(ImmutableList<>))
                        {
                            var genericArguments = parameterType.GetGenericArguments();

                            return genericTypeDefinition
                                .MakeGenericType(genericArguments[0])
                                .GetField("Empty", BindingFlags.Public | BindingFlags.Static)!
                                .GetValue(null);
                        }
                    }

                    return null;
                }

                if (state.SelectedMethod is not null)
                {
                    var methodInfo = assembly.TryLoadFrom(state.SelectedMethod);
                    if (methodInfo != null)
                    {
                        var invocationParameters = new List<object>();

                        var methodParameters = methodInfo.GetParameters();

                        foreach (var parameterInfo in methodParameters)
                        {
                            invocationParameters.Add(calculateDummyParameter(parameterInfo));
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

                                tryInitializeProperties(component);

                                return component;
                            }

                            if (instance is PureComponent reactPureComponent)
                            {
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

                                tryInitializeProperties(reactPureComponent);

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

                    if (type.IsStaticClass())
                    {
                        return new div { "Selected type is static. Please select component inherited class." };
                    }

                    var instance = createInstance(type);

                    Element resultElement = null;

                    if (instance is ReactComponentBase component)
                    {
                        if (component.IsStateNull)
                        {
                            tryUpdateStateFromStateTree(component, Context);
                        }

                        if (component.IsStateNull)
                        {
                            
                            // invoke constructor
                            {
                                var construnctorTask = component.InvokeConstructor();
                                if (construnctorTask is null)
                                {
                                    throw new DeveloperException($"{component.GetType().FullName} constructor should return task but null value returned.");
                                }

                                await construnctorTask;
                            }
                        }

                        resultElement = component;
                    }
                    else if (instance is PureComponent reactPureComponent)
                    {
                        resultElement = reactPureComponent;
                    }
                    else if (instance is Element instanceAsElement)
                    {
                        resultElement = instanceAsElement;
                    }

                    if (resultElement is null)
                    {
                        return new div { "Please select component inherited class." };
                    }

                    tryInitializeProperties(resultElement);

                    return resultElement;
                }
            }
        }
        catch (Exception exception)
        {
            if (exception is JsonException)
            {
                return new pre { exception.Message };
            }

            return new pre { exception.ToString() };
        }

        return "Element not created. Select type or method from left panel";

        object createInstance(Type type)
        {
            
            object[] constructorArguments = [];
            {
                var constructorInfoList = type.GetConstructors();
                if (constructorInfoList.Length == 1)
                {
                    var parameterInfoList = constructorInfoList[0].GetParameters();
                    
                    if (parameterInfoList.Length == 1 && parameterInfoList[0].ParameterType == typeof(Modifier[]))
                    {
                        constructorArguments = [new Modifier[] { }];
                    }
                }
            }
           
            var instance = Activator.CreateInstance(type,constructorArguments);
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

                        IgnoreException(() =>
                        {
                            var val = DeserializeJsonBySystemTextJson(stateAsJson, propertyType);

                            stateProperty.SetValue(component, val);
                        });
                    }
                }
            }
        }

        static void IgnoreException(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        static object tryGetDummyValue(Assembly assembly, string targetLocationName, Type targetLocationType)
        {
            var dummyValueProviderClass = assembly.GetTypes().FirstOrDefault(t => t.Name == "Dummy");
            if (dummyValueProviderClass is null)
            {
                return null;
            }

            var properties = dummyValueProviderClass.GetProperties();

            return firstOrDefault(properties, hasMatchNameAndType, hasMatchWithPropertyType)?.GetValue(null, []);

            bool hasMatchNameAndType(PropertyInfo propertyInfo)
            {
                if (propertyInfo.PropertyType == targetLocationType)
                {
                    if (propertyInfo.Name.Equals(targetLocationName, StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }
                }

                return false;
            }

            bool hasMatchWithPropertyType(PropertyInfo propertyInfo)
            {
                if (isSimpleCommonPrimitiveType(targetLocationType))
                {
                    return false;
                }
                
                if (propertyInfo.PropertyType == targetLocationType)
                {
                    return true;
                }

                return false;

                static bool isSimpleCommonPrimitiveType(Type type)
                {
                    return type == typeof(string) ||
                           type == typeof(sbyte) ||
                           type == typeof(byte) ||
                           type == typeof(short) ||
                           type == typeof(int) ||
                           type == typeof(double) ||
                           type == typeof(float) ||
                           type == typeof(long) ||
                           type == typeof(ulong) ||
                           type == typeof(decimal);
                }
            }

            static TSource firstOrDefault<TSource>(IReadOnlyCollection<TSource> source, params Func<TSource, bool>[] predicates) where TSource : class
            {
                foreach (var predicate in predicates)
                {
                    var result = source.FirstOrDefault(predicate);
                    if (result != null)
                    {
                        return result;
                    }
                }

                return default;
            }
        }

        static void tryInitializeProperties(object instance)
        {
            foreach (var propertyInfo in instance.GetType().GetProperties())
            {
                if (propertyInfo.SetMethod is null && propertyInfo.GetMethod is null)
                {
                    continue;
                }

                var propertyValue = propertyInfo.GetValue(instance);
                if (propertyValue != null)
                {
                    if (propertyInfo.PropertyType.IsClass)
                    {
                        continue;
                    }

                    if (propertyValue is IEnumerable)
                    {
                        continue;
                    }

                    if (!propertyValue.Equals(Activator.CreateInstance(propertyInfo.PropertyType)))
                    {
                        continue;
                    }
                }

                propertyValue = tryGetDummyValue(instance.GetType().Assembly, propertyInfo.Name, propertyInfo.PropertyType);
                if (propertyValue is null)
                {
                    continue;
                }

                propertyInfo.SetValue(instance, propertyValue);
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
        var element = await CreateElement(state, Context);
        
        Client.RefreshComponentPreviewCompleted();

        var hoverEffect = new Fragment
        {
            new style
            {
                """
                .react-with-dotnet-designer-hover-effect
                {
                    transition: outline 0.3s ease;
                    outline: 1px dashed #bfdbfe;
                    background-image: repeating-linear-gradient(45deg, #fde047 0, #fde047 1px, transparent 0, transparent 50%);
                    background-size: 5px 5px;
                }
                """
            },
            new script
            {
                src = HelperJsFileResourceName
            }
        };

        return new Fragment
        {
            hoverEffect,
            element
        };
    }

    public static readonly string HelperJsFileResourceName = "ReactWithDotNet.UIDesigner.Resources.ComponentIndicator.js";
}