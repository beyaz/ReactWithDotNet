using System;
using System.IO;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    public UIDesignerView()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();
    }

    public void ComponentDidMount()
    {
        Refresh();
    }

    public override Element render()
    {
        var propertyPanel = new VPanel
        {
            style = { margin = "5px" },
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
                        
                        state.SelectedMethodTreeNodeKey = e.value;
                        
                        state.SelectedComponentTypeReference = $"{getFullClassName()},{Path.GetFileNameWithoutExtension(state.SelectedAssembly)}";
                        

                        state.ReactWithDotnetComponentAsJson = StateCache.ReadFromCache(state.SelectedComponentTypeReference);
                    },
                    AssemblyFilePath = state.SelectedFolder.HasValue() && state.SelectedAssembly.HasValue() ? Path.Combine(state.SelectedFolder, state.SelectedAssembly) : null
                },
                new VSpace(10),
                new Slider { max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

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
            try
            {
                var type = FindType(state.SelectedComponentTypeReference);
                if (type == null)
                {
                    return new div("type not found.@"+ state.SelectedComponentTypeReference);
                }

                var instance = (Element)Json.DeserializeJsonByNewtonsoft(state.ReactWithDotnetComponentAsJson.HasValue() ? state.ReactWithDotnetComponentAsJson : "{}", type);
                
                if (instance is ReactComponent reactComponent)
                {
                    reactComponent.Context = new ReactContext();
                    return reactComponent.render();
                }

                if (instance is ReactStatefulComponent reactStatefulComponent)
                {
                    reactStatefulComponent.Context = new ReactContext();

                    return reactStatefulComponent.render();
                }

                return new div(instance.ToString());
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
                    size = 40,
                    children =
                    {
                       propertyPanel
                    }
                },
                new SplitterPanel
                {
                    size = 60,
                    children =
                    {
                        outputPanel
                    }
                }
            }
        };


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


            string getAssemblyPath() => Path.Combine(state.SelectedFolder, state.SelectedAssembly);

            bool isAssemblyExists() => File.Exists(getAssemblyPath());
        }

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

        Context.ClientTasks = new[] { new ClientTaskGotoMethod { Timeout = 1000, MethodName = nameof(Refresh) } };
    }

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    void SaveState()
    {
        if (state.SelectedComponentTypeReference.HasValue())
        {
            StateCache.SaveToCache(state.SelectedComponentTypeReference, state.ReactWithDotnetComponentAsJson);
        }
        
        StateCache.SaveState(state);
    }
    #endregion
}