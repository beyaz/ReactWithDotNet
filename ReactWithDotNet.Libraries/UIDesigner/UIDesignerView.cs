using System.IO;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    static ClientEventInfo OnBrowserInactive = new(nameof(OnBrowserInactive));

    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
    }

    public void ComponentDidMount()
    {
        ClientTask.ListenEvent(OnBrowserInactive, Refresh);
        ClientTask.CallJsFunction("InitializeUIDesignerEvents", 1000);
    }

    public override Element render()
    {
        var propertyPanel = new VStack
        {
            style = { margin = "5px"},
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
                new VSpace(10),
                new MethodSelectionView
                {
                    SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                    OnSelectionChange = e =>
                    {
                        SaveState();

                        state.SelectedMethodName = null;
                        state.MetadataToken      = null;

                        state.SelectedMethodTreeNodeKey = e.value;

                        string typeReference = null;
                        
                        var fullAssemblyPath = Path.Combine(state.SelectedFolder, state.SelectedAssembly);

                        string fullClassName = null;
                        
                        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey);
                        if (node is not null)
                        {
                            if (node.IsClass)
                            {
                                fullClassName = $"{node.NamespaceName}.{node.Name}";
                            }

                            if (node.IsMethod)
                            {
                                fullClassName = $"{node.DeclaringTypeFullName}";
                                
                                state.MetadataToken      = node.MetadataToken;
                                state.SelectedMethodName = node.Name;
                            }
                        }

                        if (fullClassName is not null)
                        {
                            typeReference = $"{fullClassName},{Path.GetFileNameWithoutExtension(state.SelectedAssembly)}";
                        }

                        state.SelectedComponentTypeReference = typeReference;

                        if (typeReference != null)
                        {
                            state.JsonText = StateCache.ReadFromCache(typeReference + state.MetadataToken);
                        }

                        SaveState();
                    },
                    AssemblyFilePath = state.SelectedFolder.HasValue() && state.SelectedAssembly.HasValue() ? Path.Combine(state.SelectedFolder, state.SelectedAssembly) : null
                },
                new VSpace(10),
                new Slider { max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

                new div { text = state.SelectedComponentTypeReference + (state.SelectedMethodName.HasValue() ? $"::{state.SelectedMethodName}" : null) },
                
                new Editor
                {
                    valueBind=()=> state.JsonText,
                    highlight= "json",
                    style = { minHeight = "200px", border = "1px dashed blue",fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
                }
            }
        };


        var outputPanel = new div
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
            return new iframe { src = "/ReactWithDotNetDesigner.ComponentPreview", style = { width_height = "100%", border = "none"}};
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
                    size = 40,
                    children =
                    {
                       propertyPanel
                    }
                },
                new SplitterPanel
                {
                    size = 60,
                    style = { display = "flex", justifyContent = "center", alignItems = "center"},
                    children =
                    {
                        outputPanel
                    }
                }
            }
        };


      

        return new div
        {
            children =
            {
                mainPanel
            },
            style =
            {
                width = "100%", height = "100%", padding = "7px"
            }
        };
    }

    #region Methods
   
    public void Refresh()
    {
        SaveState();
    }

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    void SaveState()
    {
        if (state.SelectedComponentTypeReference.HasValue())
        {
            StateCache.SaveToCache(state.SelectedComponentTypeReference+ state.MetadataToken, state.JsonText);
        }
        
        StateCache.SaveState(state);
    }
    #endregion
}