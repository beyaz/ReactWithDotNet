using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using ReactDotNet.PrimeReact;

namespace ReactDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    #region Static Fields
    static readonly object fileLock = new();
    #endregion

    public UIDesignerView()
    {
        state = ReadState();
    }
    
    public void ComponentDidMount()
    {
        Refresh();
    }

    public override Element render()
    {
        var componentSelector = new ListBox
        {
            options     = GetComponents(Assembly.Load(state.ComponentsLocatedAssemblyName)),
            optionLabel = nameof(ReactComponentInfo.Name),
            optionValue = nameof(ReactComponentInfo.Value),
            value       = state.SelectedComponentTypeReference,
            onChange    = OnSelectedComponentChanged,
            filter      = true,
            listStyle   = {maxHeight = "400px" },
            style = { height = "100%" },
            itemTemplate = new ItemTemplates<ReactComponentInfo>
            {
                Items = GetComponents(Assembly.Load(state.ComponentsLocatedAssemblyName)),
                Template = item => new HPanel
                {
                    style    = { alignItems = "center"},
                    children = { new img { src = "img/Class.svg", width = 15, height = 15 }, new div(item.Name) { style = { marginLeft = "5px" } } }
                }
            },
        };

        var propertyList = new ListBox
        {
            options     = (state.Properties ?? Enumerable.Empty<DotNetObjectPropertyValue>()).OrderBy(x => string.IsNullOrWhiteSpace(x.Value)).Select(x => new Pair {Key = x.Path, Value = x.Path}),
            optionLabel = nameof(Pair.Key),
            optionValue = nameof(Pair.Value),
            value       = state.SelectedPropertyName,
            onChange    = OnSelectedPropertyChanged,
            filter      = true,
            listStyle   = {maxHeight = "400px" }
        };

        var dataPanel = new Splitter
        {
            children =
            {
                new SplitterPanel
                {
                    propertyList
                },
                new SplitterPanel
                {
                    new InputTextarea
                    {
                        valueBind = () => state.SelectedPropertyValue,
                        style =
                        {
                            width = "100%", height = "100%"
                        }
                    }
                }
            },
            style =
            {
                height = "100%",
                width  = "100%"
            }
        };

        Element createElement()
        {
            try
            {
                var type = FindType(state.SelectedComponentTypeReference);
                if (type == null)
                {
                    return new div("type not found.");
                }

                var instance = Activator.CreateInstance(type);
                if (instance == null)
                {
                    return new div("instance is null.");
                }

                

                foreach (var dotNetObjectPropertyValue in state.Properties.Where(x => !string.IsNullOrWhiteSpace(x.Value)))
                {
                    var path      = dotNetObjectPropertyValue.Path;
                    var jsonValue = dotNetObjectPropertyValue.Value;

                    var propertyInfo = type.GetProperty(path);

                    if (propertyInfo is not null)
                    {
                        object propertyValue = null;
                        if (propertyInfo.PropertyType == typeof(string))
                        {
                            propertyValue = jsonValue;
                        }
                        else
                        {
                            propertyValue = JsonSerializer.Deserialize(jsonValue, propertyInfo.PropertyType);
                        }

                        UIDesignerViewExtension.SaveValueToPropertyPath(propertyValue, instance, path);
                    }
                }

                if (IsReactStatefulComponent(type))
                {
                    var statePropertyInfo = type.GetProperty("state");
                    if (statePropertyInfo is not null)
                    {
                        var stateInstance = statePropertyInfo.GetValue(instance);
                        if (stateInstance == null)
                        {
                            stateInstance = Activator.CreateInstance(statePropertyInfo.PropertyType);

                            statePropertyInfo.SetValue(instance, stateInstance);
                        }

                        UIDesignerViewExtension.OpenNullProperties(stateInstance);
                    }

                    return ((IReactStatefulComponent) instance).___RootNode___;
                }

                return instance as Element;
            }
            catch (Exception exception)
            {
                return new div(exception.ToString());
            }
        }

        

        var mainPanel = new Splitter
        {
            layout = SplitterLayoutType.vertical,
            style =
            {
                width  = "100%",
                height = "100%"
            },

            children =
            {
                new SplitterPanel
                {
                    size = 70,
                    children =
                    {
                        new div
                        {
                           children=
                           {
                               createElement()
                           },
                           style =
                           {
                               border = "1px dashed #e0e0e0",
                               width = state.ScreenWidth + "%",
                               height = "100%"
                           }
                        }
                    }
                },

                new SplitterPanel
                {
                    size = 30,
                    children =
                    {
                        new div
                        {
                            style={ display = "flex", flexDirection = "column", width = "100%"},
                            children=
                            {
                                new Slider {value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px"}},
                                new Splitter
                                {
                                    new SplitterPanel
                                    {
                                        size = 2,
                                        children =
                                        {
                                            new EnvironmentSelectorView(),
                                            new FolderSelectionView(),
                                            new AssemblySelectionView(),
                                            componentSelector
                                        }
                                    },

                                    new SplitterPanel
                                    {
                                        size = 6,
                                        children =
                                        {
                                            dataPanel
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        return new div
        {
            children=
            {
                mainPanel
            },
            style =
            {
                width = "100%", height = "100%", padding = "10px"
            }
        };


        
    }

    #region Methods
    static Type FindType(string typeReference)
    {
        if (!string.IsNullOrWhiteSpace(typeReference))
        {
            return Type.GetType(typeReference, false);
        }

        return null;
    }

    static IEnumerable<ReactComponentInfo> GetComponents(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract)
            {
                continue;
            }

            if (IsReactComponent(type))
            {
                yield return new ReactComponentInfo {Name = type.GetFullName(), Value = type.GetFullName()};
            }
        }
    }

    static IEnumerable<DotNetObjectPropertyValue> GetProperties(Type type)
    {
        foreach (var propertyInfo in type.GetProperties())
        {
            var propertyType = propertyInfo.PropertyType;
            
            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(IReadOnlyList<>))
            {
                yield return new DotNetObjectPropertyValue { Path = propertyInfo.Name };
            }
            
            if (propertyType.IsAbstract)
            {
                continue;
            }

            yield return new DotNetObjectPropertyValue {Path = propertyInfo.Name};
        }
    }

    static bool IsReactComponent(Type type)
    {
        if (type.IsSubclassOf(typeof(ReactComponent)))
        {
            return true;
        }

        return IsReactStatefulComponent(type);
    }

    static bool IsReactStatefulComponent(Type type)
    {
        type = type.BaseType;

        if (type?.IsGenericType == true)
        {
            var typeDefinition = type.GetGenericTypeDefinition();
            if (typeDefinition == typeof(ReactComponent<>) || typeDefinition.IsSubclassOf(typeof(ReactComponent<>)))
            {
                return true;
            }
        }

        return false;
    }

   

    string GetCacheFilePath(string typeReference) => state.SaveDirectoryPath + typeReference + ".json";

    void OnSelectedComponentChanged(ListBoxChangeParams e)
    {
        if (!string.IsNullOrWhiteSpace(state.SelectedComponentTypeReference) && state.Properties?.Count > 0)
        {
            SaveProperties(state.SelectedComponentTypeReference, state.Properties);
        }

        state.SelectedComponentTypeReference = e.value;

        state.Properties = null;

        var type = FindType(state.SelectedComponentTypeReference);
        if (type != null)
        {
            state.Properties = GetProperties(type).ToList();

            foreach (var item in ReadProperties(state.SelectedComponentTypeReference))
            {
                var entry = state.Properties.FirstOrDefault(x => x.Path == item.Path);
                if (entry != null)
                {
                    entry.Value = item.Value;
                }
            }
        }

        SaveState();
    }

    public void Refresh()
    {
        TransferPropertyValueToPropertyMap();

        SaveState();

        Context.ClientTasks = new[] { new ClientTaskGotoMethod { Timeout = 100000, MethodName = nameof(Refresh) } };
    }
    void OnSelectedPropertyChanged(ListBoxChangeParams e)
    {
        state.SelectedPropertyName = e.value;

        state.SelectedPropertyValue = state.Properties.FirstOrDefault(x => x.Path == state.SelectedPropertyName)?.Value;

        SaveState();
    }

    
   

    void TransferPropertyValueToPropertyMap()
    {
        if (state.SelectedPropertyName is not null)
        {
            state.Properties.TryUpdateFirst(x => x.Path == state.SelectedPropertyName, x => x.Value = state.SelectedPropertyValue);
        }
    }
    

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    IEnumerable<DotNetObjectPropertyValue> ReadProperties(string typeReference)
    {
        var filePath = GetCacheFilePath(typeReference);

        if (!File.Exists(filePath))
        {
            return Enumerable.Empty<DotNetObjectPropertyValue>();
        }

        var json = File.ReadAllText(filePath);

        return JsonSerializer.Deserialize<IEnumerable<DotNetObjectPropertyValue>>(json);
    }

    UIDesignerModel ReadState()
    {
        if (File.Exists(@"d:\\temp\\UIDesignerModel.json"))
        {
            var json = File.ReadAllText(@"d:\\temp\\UIDesignerModel.json");

            try
            {
                return JsonSerializer.Deserialize<UIDesignerModel>(json);
            }
            catch (Exception)
            {
                return new UIDesignerModel();
            }
        }

        return new UIDesignerModel();
    }

    void SaveProperties(string typeReference, IEnumerable<DotNetObjectPropertyValue> items)
    {
        var filePath = GetCacheFilePath(typeReference);

        File.WriteAllText(filePath, JsonSerializer.Serialize(items, new JsonSerializerOptions {WriteIndented = true}));
    }

    void SaveState()
    {
        lock (fileLock)
        {
            File.WriteAllText(@"d:\\temp\\UIDesignerModel.json", JsonSerializer.Serialize(state));
        }
    }
    #endregion
}