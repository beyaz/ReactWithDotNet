using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    public UIDesignerView()
    {
        state = StateCache.ReadState();
    }

    public void ComponentDidMount()
    {
        Refresh();
    }

    public override Element render()
    {
        var dataPanel = new div
        {
            children =
            {
                createElement()
            },
            style =
            {
                border = "1px dashed #e0e0e0",
                width  = state.ScreenWidth + "%",
                height = "100%"
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

                if (MetadataHelper.IsReactStatefulComponent(type))
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

                    return ((IReactStatefulComponent)instance).___RootNode___;
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
            layout = SplitterLayoutType.horizontal,
            style =
            {
                width  = "100%",
                height = "100%"
            },

            children =
            {
                new SplitterPanel
                {
                    size = 30,
                    children =
                    {
                        new VPanel
                        {
                            //style = {  display = "flex", flexDirection = "column"},
                            children =
                            {
                                new FolderSelectionView
                                {
                                    SelectedFolder = state.SelectedFolder,
                                    LastQuery      = state.SelectedFolderLastQuery,
                                    Suggestions    = state.SelectedFolderSuggestions,
                                    OnChange       = e => { state.SelectedFolder          = e.GetValue<string>(); },
                                    CompleteMethod = e => { state.SelectedFolderLastQuery = e.query; }
                                },
                                new VSpace(10),
                                new AssemblySelectionView
                                {
                                    SelectedFolder   = state.SelectedFolder,
                                    SelectedAssembly = state.SelectedAssembly,
                                    LastQuery        = state.SelectedAssemblyLastQuery,
                                    OnChange         = e => { state.SelectedAssembly          = e.GetValue<string>(); },
                                    CompleteMethod   = e => { state.SelectedAssemblyLastQuery = e.query; }
                                },
                                new MethodSelectionView
                                {
                                    SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                                    OnSelectionChange = e =>
                                    {
                                        state.SelectedMethodTreeNodeKey = e.value;

                                        state.SelectedComponentTypeReference = $"{getFullClassName()},{Path.GetFileNameWithoutExtension(state.SelectedAssembly)}";
                                        SaveState();
                                    },
                                    AssemblyFilePath = Path.Combine(state.SelectedFolder, state.SelectedAssembly)
                                },
                                new Slider { value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

                                new div { text = state.SelectedComponentTypeReference },
                                new InputTextarea
                                {
                                    valueBind = () => state.ReactWithDotnetComponentAsJson,
                                    style =
                                    {
                                        width = "100%", height = "100%"
                                    }
                                }
                            }
                        }
                    }
                },
                new SplitterPanel
                {
                    size = 70,
                    children =
                    {
                        dataPanel
                    }
                }
            }
        };

        string getAssemblyPath() => Path.Combine(state.SelectedFolder, state.SelectedAssembly);

        bool isAssemblyExists() => File.Exists(getAssemblyPath());

        string getFullClassName()
        {
            var node = MethodSelectionView.FindTreeNode(Path.Combine(state.SelectedFolder, state.SelectedAssembly), state.SelectedMethodTreeNodeKey);
            if (node is not null && node.IsClass)
            {
                if (isAssemblyExists())
                {
                    return $"{node.NamespaceName}.{node.Name}";
                }
            }

            return null;
        }

        return new div
        {
            children =
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

    

    public void Refresh()
    {
        SaveState();

        Context.ClientTasks = new[] { new ClientTaskGotoMethod { Timeout = 100000, MethodName = nameof(Refresh) } };
    }

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    void SaveState() => StateCache.SaveState(state);
    #endregion
}