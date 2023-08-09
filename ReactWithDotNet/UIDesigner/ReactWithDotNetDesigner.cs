using System.Reflection;
using Newtonsoft.Json;
using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;
using static ReactWithDotNet.UIDesigner.Extensions;
using Slider = ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Slider;

namespace ReactWithDotNet.UIDesigner;

public class ReactWithDotNetDesigner : ReactComponent<ReactWithDotNetDesignerModel>
{
    public static bool IsAttached { get; set; }

    public int UpdatingProgress { get; set; }
    
    
    protected override Task constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        state.SelectedAssemblyFilePath ??= Assembly.GetEntryAssembly()?.Location;

        Client.ListenEvent("ComponentPreviewRefreshed",OnComponentPreviewRefreshed);
        
        return Task.CompletedTask;
    }

    void OnComponentPreviewRefreshed()
    {
        UpdatingProgress = 25;
        Client.GotoMethod(UpdateProgress,UpdatingProgress+25);
    }
    
    void UpdateProgress(int newValue)
    {
        UpdatingProgress = newValue;
        if (UpdatingProgress <= 100)
        {
            Client.GotoMethod(500,UpdateProgress,UpdatingProgress+25);    
        }
        
    }
    
    protected override Element render()
    {
        Element createJsonEditor()
        {
            Expression<Func<string>> valueBind = () => state.JsonTextForDotNetMethodParameters;

            if (state.IsInstanceEditorActive)
            {
                valueBind = () => state.JsonTextForDotNetInstanceProperties;
            }

            return new CodeMirror
            {
                extensions               = { "json", "githubLight" },
                valueBind                = valueBind,
                valueBindDebounceTimeout = 700,
                valueBindDebounceHandler = OnKeypressFinished,
                basicSetup =
                {
                    highlightActiveLine       = false,
                    highlightActiveLineGutter = false,
                }
            };
        }

        Element createLabel(string text)
        {
            return new small(Text(text), Color("rgb(73 86 193)"), FontWeight600);
        }

        var propertyPanel = new FlexColumn(PaddingLeftRight(5), Height("100%"), Width("100%"), FontSize15)
        {
            new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet" },

            new FlexColumn(MarginLeftRight(3))
            {
                createLabel("Filter by class name"),

                new InputText
                {
                    valueBind                = () => state.ClassFilter,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12 }
                }
            },
            new FlexColumn(MarginLeftRight(3), MarginTopBottom(3))
            {
                createLabel("Filter by method name"),

                new InputText
                {
                    valueBind                = () => state.MethodFilter,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12 }
                }
            },

          
            new style
            {
                
                @"

.cm-editor {
  height: calc(100% - 10px);
}
.cm-theme-light {
  height: calc(100% - 2px);
  font-size: 12px;
}

.ͼ1.cm-editor.cm-focused {
    outline: none;
}

/* left-side-key */
.ͼ18{
    color: #c0bcc8;
    font-weight: bold;
}


/* string */
.ͼ1b{
    color: #f44336;
    font-weight: bold;
}
/* number */
.ͼ19 {
    color: #141413;
    font-weight: bold;
}
/* boolean */
.ͼ1g {
    color: #2c1aeb;
    font-weight: bold;
}
"
            },
            
            Space(5),
            new MethodSelectionView
            {
                ClassFilter               = state.ClassFilter,
                MethodFilter              = state.MethodFilter,
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                SelectionChanged          = OnElementSelected,
                AssemblyFilePath          = state.SelectedAssemblyFilePath
            },

            Space(5),

            new fieldset
            {
                Border("1px solid #d9d9d9"),
                BorderRadius(4),
                PaddingTop(15),
                new legend
                {
                    createLabel("Media Size")
                },
                //new FlexRow(JustifyContentSpaceAround)
                //{
                //    MediaSizeButton(320),
                //    MediaSizeButton(480),
                //    MediaSizeButton(600),
                //    MediaSizeButton(768),
                //    MediaSizeButton(900),
                //    MediaSizeButton(1024),
                //    MediaSizeButton(1200)
                //},
                new Slider
                {
                    min=300,
                    max = 1400,
                    value = state.ScreenWidth,
                    onChangeCommitted = OnMediaSizeChanged,
                    valueLabelDisplay = "on",
                    classes =
                    {
                        { "thumb", new Style
                        {
                           WidthHeight(28),
                           BackgroundWhite
                        }},
                        {"valueLabel",new Style {
                            
                            FontSize12,
                            FontWeightNormal,
                            Top(-1),
                            BackgroundColor("unset"),
                            Color("#1976d2")
                        }}
                    }
                }
            },

            new FlexColumn(HeightMaximized)
            {
                // h e a d e r
                new FlexRow(Color("#6c757d"), CursorPointer, TextAlignCenter)
                {
                    When(canShowInstanceEditor(), new div(Text("Instance json"))
                    {
                        OnClick(_ => state.IsInstanceEditorActive = true),
                        When(state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    }),

                    When(canShowParametersEditor(), new div(Text("Parameters json"))
                    {
                        OnClick(_ => state.IsInstanceEditorActive = false),
                        When(!state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    })
                },

                // c o n t e n t
                createJsonEditor()
            }
        };

        Element createVerticleRuler()
        {
            var maxHeight = 500;
            
            var step = 50;
            var max = maxHeight / step + 1;
            
            
            IReadOnlyList<Element> createTenPoints()
            {
                var returnList = new List<Element>();

                var miniStep = 10;
                
                var cursor = 0;
                var distance =  miniStep;
                while (distance <= maxHeight)
                {
                    cursor++;

                    distance = cursor * miniStep;

                    if (distance % step == 0 || distance > maxHeight)
                    {
                        continue;
                    }
                    
                    returnList.Add(new div(PositionAbsolute)
                    {
                        Right(3), 
                        Top(distance),
                        
                        Height(0.5),
                        Width(4),
                        Background("green")
                    });
                }
                

                return returnList;
            }
            
            return new div(WidthHeightMaximized, PositionRelative)
            {
                Enumerable.Range(0, max).Select(number => new div(PositionAbsolute)
                {
                    Right(3), Top(number * step),
                    new FlexRow(FontSize8, LineHeight6, FontWeight500, Gap(4))
                    {
                        new div(MarginTop(-3))
                        {
                            (number * step).ToString()
                        },
                        new div
                        {
                            Height(0.5),
                            Width(7),

                            Background("green")
                        }
                    }

                }),
                
                createTenPoints()
            };
        }

        Element createHorizontalRuler()
        {
            var step = 50;
            var max = state.ScreenWidth / step + 1;

            double calculateMarginForCenterizeLabel(int stepNumber)
            {
                var label = stepNumber * step;

                if (label < 10)
                {
                    return -2;
                }
                
                if (label < 100)
                {
                    return -4.5;
                }
                
                if (label < 1000)
                {
                    return -7;
                }

                return -9;
            }

            IReadOnlyList<Element> createTenPoints()
            {
                var returnList = new List<Element>();

                var miniStep = 10;
                
                var cursor = 0;
                var distance =  miniStep;
                while (distance <= state.ScreenWidth)
                {
                    cursor++;

                    distance = cursor * miniStep;

                    if (distance % step == 0 || distance > state.ScreenWidth)
                    {
                        continue;
                    }
                    
                    returnList.Add(new div(PositionAbsolute)
                    {
                        Bottom(3), 
                        Left(distance),
                        
                        Width(0.5),
                        Height(4),
                        Background("green")
                    });
                }
                

                return returnList;
            }
            
            return new FlexRow(PositionRelative,WidthMaximized,Height(20))
            {
                Enumerable.Range(0, max).Select(number => new div(PositionAbsolute)
                {
                    Bottom(3), Left(number * step),
                    new FlexColumn(FontSize8, LineHeight6, FontWeight500, Gap(4))
                    {
                        new div(MarginLeft(calculateMarginForCenterizeLabel(number)))
                        {
                            (number * step).ToString()
                        },
                        new div(BorderRadius(3))
                        {
                            Width(0.5),
                            Height(7),

                            Background("green")
                        }
                    }
                }),
                createTenPoints()
                
            };
        }
        
        var outputPanel = new div
        {
            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
            BackgroundSize("10px 10px"),
            
            createHorizontalRuler,
            new div(PositionRelative)
            {
                WidthHeightMaximized,
                createElement(),
                
                new div(PositionAbsolute, Top(0), Left(0))
                {
                    createVerticleRuler
                }
            },

            Width(state.ScreenWidth <= 100 ? state.ScreenWidth + "%" : state.ScreenWidth + "px"),
            
            HeightMaximized,
            BoxShadow(0,4, 12,0,rgba(0, 0, 0, 0.1))
        };

        Element createElement()
        {
            return new iframe { src = "/ReactWithDotNetDesignerComponentPreview", style = { Border("none"), WidthMaximized, HeightMaximized } };
        }
        
        return new FlexRow(WidthHeightMaximized, PrimaryBackground)
        {
            new div(BorderRight("1px dotted #d9d9d9"), Width(400), PositionRelative)
            {
                new div
                { 
                    (state.PropertyPanelIsClosed ? "→" : "←"),
                    OnClick(state.PropertyPanelIsClosed ? OpenPropertyPanel: ClosePropertyPanel),
                    PositionAbsolute, 
                    TopRight(0), 
                    FontSize14, 
                    FontWeight500,
                    Color("#c5d7e8"),
                    CursorPointer,
                    Hover(FontSize17, Color("#9090f2"))
                },
                When(UpdatingProgress is > 0 and <= 100,()=>new div(PositionAbsolute,Right(20))
                { 
                    new CircularProgress
                    { 
                        variant = "determinate",
                        value   = UpdatingProgress,
                        size    = 10
                    }
                }),
                
                When(state.PropertyPanelIsClosed == false, propertyPanel),
                When(state.PropertyPanelIsClosed, Width(15))
            },
            new div(DisplayFlex, JustifyContentCenter, FlexGrow(1), Padding(7))
            {
                outputPanel
            }
        };
    }

    void OnMediaSizeChanged(double newSize)
    {
        state.ScreenWidth = (int)newSize;
        SaveState();
    }

    void ClosePropertyPanel(MouseEvent _)
    {
        state.PropertyPanelIsClosed = true;
        SaveState();
    }
    void OpenPropertyPanel(MouseEvent _)
    {
        state.PropertyPanelIsClosed = false;
        SaveState();
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

    Element MediaSizeButton(int width)
    {
        const string BluePrimary = "#1976d2";

        var isSelected = width == state.ScreenWidth;

        return new FlexRowCentered
        {
            Id(width),
            $"{width}px",
            Color(BluePrimary),
            Border($"1px solid {BluePrimary}"),
            Background(isSelected ? "#dbdbe7" : "transparent"),
            BorderRadius(5),
            CursorPointer,
            OnClick(MediaSizeButtonClicked),
            Height(25),
            Width(50),
            FontSize12
        };
    }

    void MediaSizeButtonClicked(MouseEvent e)
    {
        state.ScreenWidth = int.Parse(e.FirstNotEmptyId);
        SaveState();
    }

    void OnElementSelected(string keyOfSelectedTreeNode)
    {
        var classFilter = state.ClassFilter;
        var methodFileter = state.MethodFilter;
        
        state.SelectedType   = null;
        state.SelectedMethod = null;

        state.JsonTextForDotNetInstanceProperties = null;
        state.JsonTextForDotNetMethodParameters   = null;

        state.SelectedMethodTreeNodeKey = keyOfSelectedTreeNode;

        var fullAssemblyPath = state.SelectedAssemblyFilePath;

        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedMethodTreeNodeKey, state.ClassFilter, state.MethodFilter);
        if (node is not null)
        {
            if (node.IsClass)
            {
                state.SelectedType = node.TypeReference;

                state = StateCache.TryRead(state.SelectedType) ?? state;
            }

            if (node.IsMethod)
            {
                state.SelectedMethod = node.MethodReference;

                state = StateCache.TryRead(state.SelectedMethod) ?? state;
            }
        }
        
        state.ClassFilter  = classFilter;
        state.MethodFilter = methodFileter;

        if (canShowInstanceEditor() && canShowParametersEditor() == false)
        {
            state.IsInstanceEditorActive = true;
        }

        if (canShowParametersEditor() && canShowInstanceEditor() == false)
        {
            state.IsInstanceEditorActive = false;
        }

        if (canShowInstanceEditor())
        {
            IgnoreException(initializeInstanceJson);
        }

        if (canShowParametersEditor())
        {
            IgnoreException(initializeParametersJson);
        }

        SaveState();
        
        void initializeInstanceJson()
        {
            var typeOfInstance = state.SelectedType ?? state.SelectedMethod?.DeclaringType;

            if (typeOfInstance == null)
            {
                return;
            }

            var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetInstanceProperties ?? string.Empty) ?? new Dictionary<string, object>();

            foreach (var propertyInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(typeOfInstance)?.GetProperties(BindingFlags.Instance | BindingFlags.Public) ?? new PropertyInfo[] { })
            {
                var name         = propertyInfo.Name;
                var propertyType = propertyInfo.PropertyType;

                if (propertyType.GetInterfaces().Any(x=>x==typeof(IModifier)))
                {
                    continue;
                }

                if (name is "state")
                {
                    if (propertyType == typeof(EmptyState))
                    {
                        continue;
                    }

                    if (!map.ContainsKey(name))
                    {
                        map.Add(name, ReflectionHelper.CreateDefaultValue(propertyType));
                        continue;
                    }
                }

                if (propertyInfo.DeclaringType == typeof(Element) ||
                    propertyInfo.DeclaringType == typeof(ReactComponentBase)||
                    propertyInfo.DeclaringType == typeof(ReactPureComponent))
                {
                    continue;
                }

                if (propertyInfo.DeclaringType?.IsGenericType == true &&
                    propertyInfo.DeclaringType.GetGenericTypeDefinition() == typeof(ReactComponent<>))
                {
                    continue;
                }

                if (propertyType.BaseType == typeof(MulticastDelegate))
                {
                    continue;
                }

                if (propertyType.IsAbstract)
                {
                    continue;
                }

                if (!map.ContainsKey(name))
                {
                    map.Add(name, ReflectionHelper.CreateDefaultValue(propertyType));
                }
            }

            state.JsonTextForDotNetInstanceProperties = JsonConvert.SerializeObject(map, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting           = Formatting.Indented
            });
        }

        void initializeParametersJson()
        {
            var map = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters ?? string.Empty) ?? new Dictionary<string, object>();

            foreach (var parameterInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedMethod)?.GetParameters() ?? new ParameterInfo[] { })
            {
                var name = parameterInfo.Name;
                if (name == null || map.ContainsKey(name))
                {
                    continue;
                }

                map.Add(name, ReflectionHelper.CreateDefaultValue(parameterInfo.ParameterType));
            }

            state.JsonTextForDotNetMethodParameters = JsonConvert.SerializeObject(map, new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Include,
                Formatting           = Formatting.Indented
            });
        }
    }

    void OnKeypressFinished()
    {
        SaveState();
    }

    void OnFilterChanged()
    {
    }

    void SaveState()
    {
        if (state.SelectedMethod is not null)
        {
            StateCache.Save(state.SelectedMethod, state);
        }

        if (state.SelectedType is not null)
        {
            StateCache.Save(state.SelectedType, state);
        }

        StateCache.Save(state);

        OnComponentPreviewRefreshed();
    }
}