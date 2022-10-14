using System.IO;
using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json;
using ReactWithDotNet.PrimeReact;
using ReactWithDotNet.react_simple_code_editor;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactWithDotNet.UIDesigner;

class UIDesignerView : ReactComponent<UIDesignerModel>
{
    

    protected override void constructor()
    {
        state =   StateCache.ReadState() ?? new UIDesignerModel();
        
        state.SelectedAssemblyFilePath ??= Assembly.GetEntryAssembly()?.Location;
    }

    protected override Element render()
    {
        var propertyPanel = new FlexColumn(Padding(5))
        {
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

                    var fullAssemblyPath = state.SelectedAssemblyFilePath;

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
                        typeReference = $"{fullClassName},{Path.GetFileNameWithoutExtension(state.SelectedAssemblyFilePath)}";
                    }

                    state.SelectedComponentTypeReference = typeReference;

                    if (typeReference != null)
                    {
                        var json = StateCache.ReadFromCache(typeReference + state.MetadataToken);
                        if (json.HasValue())
                        {
                            state.SelectedDotNetMemberSpecification = JsonConvert.DeserializeObject<DotNetMemberSpecification>(json);
                        }
                        else
                        {
                            state.SelectedDotNetMemberSpecification = new DotNetMemberSpecification();
                        }
                    }

                    SaveState();
                },
                AssemblyFilePath = state.SelectedAssemblyFilePath
            }.render(),
            Space(10),
            new Slider { max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged, style = { margin = "10px", padding = "5px" } },

            new TabView
            {
                new TabPanel
                {
                    header = "Instance json",
                    children =
                    {
                        new Editor
                        {
                            valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties,
                            highlight = "json",
                            style     = { minHeight = "200px", border = "1px dashed blue", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
                        }
                    }
                },
                new TabPanel
                {
                    header = "Parameters json",
                    children =
                    {
                        new Editor
                        {
                            valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetMethodParameters,
                            highlight = "json",
                            style     = { minHeight = "200px", border = "1px dashed blue", fontSize = "16px", fontFamily = "ui-monospace,SFMono-Regular,SF Mono,Menlo,Consolas,Liberation Mono,monospace" }
                        }
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
            return new iframe { src = "/ReactWithDotNetDesigner.ComponentPreview", style = { width_height = "100%", border = "none" } };
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
                    size = 25,
                    children =
                    {
                        propertyPanel
                    }
                },
                new SplitterPanel(DisplayFlex, JustifyContentCenter)
                {
                    size = 75,

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

   

   

   

    

    void OnWidthChanged(SliderChangeParams e)
    {
        state.ScreenWidth = e.value;
    }

    void SaveState()
    {
        if (state.SelectedComponentTypeReference.HasValue())
        {
            var selectedDotNetMemberSpecificationAsJson = JsonSerializer.Serialize(state.SelectedDotNetMemberSpecification, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true });

            StateCache.SaveToCache(state.SelectedComponentTypeReference + state.MetadataToken, selectedDotNetMemberSpecificationAsJson);
        }

        StateCache.SaveState(state);
    }
}

class Pair
{
    public string Key { get; set; }
    public string Value { get; set; }
}





