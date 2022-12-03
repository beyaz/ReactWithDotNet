using System.IO;
using System.Reflection;
using System.Text.Json;
using Newtonsoft.Json;
using ReactWithDotNet.Libraries.uiw.react_codemirror;
using ReactWithDotNet.PrimeReact;
using static ReactWithDotNet.UIDesigner.Extensions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesigner : ReactComponent<UIDesignerModel>
{
    protected override void constructor()
    {
        state = StateCache.ReadState() ?? new UIDesignerModel();

        state.SelectedAssemblyFilePath ??= Assembly.GetEntryAssembly()?.Location;

        if (state.SelectedMethodTreeNodeKey.HasValue())
        {
            OnElementSelected((state.SelectedMethodTreeNodeKey, state.SelectedMethodTreeFilter));
        }
    }

    public void Refresh()
    {
        SaveState();
    }

    protected override void componentDidMount()
    {
        Client.OnBrowserInactive(Refresh);
        Client.CallJsFunction("InitializeUIDesignerEvents", 1000);
    }
    bool canShowInstanceEditor()
    {
        if (state.SelectedMethod?.IsStatic == true)
        {
            return false;
        }

        return true;
    }

    bool canShowParametersEditor()
    {
        if (state.SelectedMethod?.Parameters.Count > 0)
        {
            return true;
        }

        return false;
    }
    protected override Element render()
    {
        const int width = 500;

        Element createJsonEditor()
        {
            Expression<Func<string>> valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetMethodParameters;
            
            if (state.IsInstanceEditorActive)
            {
                valueBind = () => state.SelectedDotNetMemberSpecification.JsonTextForDotNetInstanceProperties;
            }

            return new CodeMirror
            {
                extensions = { "json", "githubLight" },
                valueBind  = valueBind,
                basicSetup =
                {
                    highlightActiveLine       = false,
                    highlightActiveLineGutter = false,
                },
                style =
                {
                    BorderRadius(3),
                    Border("1px solid #d9d9d9"),
                    FontSize11,
                    MaxWidth(width)
                }
            };
        }
        
        
        var propertyPanel = new FlexColumn(PaddingLeftRight(5), Height("100%"),Width("100%"), FontSize15, PrimaryBackground)
        {
            new link{href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet"},
            
            new style{Text($@"



.token.property{{ {new Style {   Color("#189af6") }.ToCssWithImportant()} }}

.cm-editor{{ {new Style { Height("calc(100% - 2px)"), Color("#DE3163") }.ToCssWithImportant()} }}

.ͼ16 {{ {new Style { FontWeight600, FontFamily("'IBM Plex Mono', monospace") }.ToCssWithImportant()} }}

") },
            new MethodSelectionView
            {
                Filter                    = state.SelectedMethodTreeFilter,
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                SelectionChanged          = OnElementSelected,
                AssemblyFilePath          = state.SelectedAssemblyFilePath,
                Width = width
            },
            Space(10),
            new Slider
            {
                max = 100, min = 0, value = state.ScreenWidth, onChange = OnWidthChanged ,
                style = { Margin(10) , Padding(5) }
            },

            
            
            new FlexColumn(Height("100%"))
            {
                // header
                new FlexRow(Color("#6c757d"),CursorPointer, TextAlignCenter)
                {
                    When(canShowInstanceEditor(), new div(Text("Instance json"))
                    {
                        OnClick(_=>state.IsInstanceEditorActive = true),
                        When(state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"),FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    }),

                    When(canShowParametersEditor(), new div(Text("Parameters json"))
                    {
                        OnClick(_=>state.IsInstanceEditorActive = false),
                        When(!state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"),FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13

                    }),
                    
                    
                },
                // content
                createJsonEditor() + Height("100%")
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
                border = "0.3px dotted #e0e0e0",
                width  = state.ScreenWidth + "%",
                height = "100%"
            }
        };

        Element createElement()
        {
            return new iframe { src = "/ReactWithDotNetDesignerComponentPreview", style = { Border("none"), WidthMaximized, HeightMaximized } };
        }

        return  new FlexRow(WidthHeight("100%"))
        {
            new link{rel = "stylesheet",href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css"},
            new link{rel = "stylesheet",href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css"},
            new link{rel = "stylesheet",href = "https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css"},

            new div(BorderRight("1px dotted #d9d9d9"), Width(width))
            {
                propertyPanel
            },
            new div(DisplayFlex, JustifyContentCenter,FlexGrow(1), Padding(7))
            {
                outputPanel
            }
        };
    }

    void OnElementSelected((string value, string filter) e)
    {
        SaveState();

        state.SelectedMethod     = null;

        state.SelectedMethodTreeNodeKey = e.value;
        state.SelectedMethodTreeFilter  = e.filter;

        string typeReference = null;

        var fullAssemblyPath = state.SelectedAssemblyFilePath;

        string fullClassName = null;

        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey);
        if (node is not null)
        {
            if (node.IsClass)
            {
                fullClassName = $"{node.TypeReference.FullName}";
            }

            if (node.IsMethod)
            {
                fullClassName = $"{node.MethodReference.DeclaringType.FullName}";

                state.SelectedMethod = node.MethodReference;
            }
        }

        if (fullClassName is not null)
        {
            typeReference = $"{fullClassName},{Path.GetFileNameWithoutExtension(state.SelectedAssemblyFilePath)}";
        }

        state.SelectedComponentTypeReference = typeReference;

        if (typeReference != null)
        {
            var json = StateCache.ReadFromCache(typeReference + state.SelectedMethod?.MetadataToken);
            if (json.HasValue())
            {
                state.SelectedDotNetMemberSpecification = JsonConvert.DeserializeObject<DotNetMemberSpecification>(json);
            }
            else
            {
                state.SelectedDotNetMemberSpecification = new DotNetMemberSpecification();
            }
        }

        if (canShowInstanceEditor() && canShowParametersEditor() == false)
        {
            state.IsInstanceEditorActive = true;
        }

        if (canShowParametersEditor() && canShowInstanceEditor() == false)
        {
            state.IsInstanceEditorActive = false;
        }

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
            var selectedDotNetMemberSpecificationAsJson = JsonSerializer.Serialize(state.SelectedDotNetMemberSpecification, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true });

            StateCache.SaveToCache(state.SelectedComponentTypeReference + state.SelectedMethod?.MetadataToken, selectedDotNetMemberSpecificationAsJson);
        }

        StateCache.SaveState(state);
    }
}