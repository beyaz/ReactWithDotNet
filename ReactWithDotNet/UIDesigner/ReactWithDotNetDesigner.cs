using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.UIDesigner;
using static ReactWithDotNet.UIDesigner.Extensions;

[assembly: MetadataUpdateHandler(typeof(HotReloadListener))]

namespace ReactWithDotNet.UIDesigner;

class HotReloadListener : Component
{
    public static int StaticChangeCount { get; private set; }

    public int ChangeCount { get; set; }

    public static void UpdateApplication(Type[] _)
    {
        StaticChangeCount++;
    }

    public Task Refresh()
    {
        if (ChangeCount != StaticChangeCount)
        {
            Client.RefreshComponentPreview();
        }

        ChangeCount = StaticChangeCount;

        Client.GotoMethod(1000, Refresh);

        return Task.CompletedTask;
    }

    protected override Task componentDidMount()
    {
        Client.GotoMethod(700, Refresh);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return null;
    }
}

public class ReactWithDotNetDesigner : Component<ReactWithDotNetDesignerModel>
{
    public static bool IsAttached { get; set; }

    public int UpdatingProgress { get; set; }

    protected override Task constructor()
    {
        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        state.SelectedAssemblyFilePath = Assembly.GetEntryAssembly()?.Location;

        Client.ListenEvent("ComponentPreviewRefreshed", OnComponentPreviewRefreshed);

        return Task.CompletedTask;
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

            return new Fragment
            {
                new link { href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet" },

                new Editor
                {
                    defaultLanguage          = "json",
                    valueBind                = valueBind,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = OnKeypressFinished,
                    options =
                    {
                        renderLineHighlight = "none",
                        fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                        fontSize            = 11,
                        minimap             = new { enabled = false },
                        lineNumbers         = "off"
                    }
                }
            };
        }

        Element createLabel(string text)
        {
            return new small(Text(text), Color("rgb(73 86 193)"), FontWeight600);
        }

        var propertyPanel = new FlexColumn(Height("100%"), Width("100%"), FontSize15)
        {
            new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet" },

            new FlexColumn(MarginLeftRight(3))
            {
                createLabel("Filter by class name"),

                new input
                {
                    type                     = "text",
                    valueBind                = () => state.ClassFilter,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12, Padding(8), Border(Solid(1, "#ced4da")), Focus(OutlineNone), BorderRadius(3), Color("#495057") }
                }
            },
            new FlexColumn(MarginLeftRight(3), MarginTopBottom(3))
            {
                createLabel("Filter by method name"),

                new input
                {
                    type                     = "text",
                    valueBind                = () => state.MethodFilter,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnFilterChanged,
                    style                    = { FontSize12, Padding(8), Border(Solid(1, "#ced4da")), Focus(OutlineNone), BorderRadius(3), Color("#495057") }
                }
            },

            SpaceY(5),
            new MethodSelectionView
            {
                ClassFilter               = state.ClassFilter,
                MethodFilter              = state.MethodFilter,
                SelectedMethodTreeNodeKey = state.SelectedMethodTreeNodeKey,
                SelectionChanged          = OnElementSelected,
                AssemblyFilePath          = state.SelectedAssemblyFilePath
            },

            SpaceY(5),

            new fieldset
            {
                Border("1px solid #d9d9d9"),
                BorderRadius(4),
                MarginLeftRight(3),

                new legend(MarginLeft(8))
                {
                    createLabel($"Media Size: {state.ScreenWidth}px") + MarginTop(-2)
                },
                new FlexRowCentered(ClassName("reactwithdotnet_designer_slider"), PaddingLeftRight(5), PaddingTop(4),  PaddingBottom(10))
                {
                    new style
                    {
                        """
                        .reactwithdotnet_designer_slider input[type="range"] {
                          -webkit-appearance:none !important;
                          width:200px;
                          height:1px;
                          background:#00acee;
                          border:none;
                          outline:none;
                        }
                        .reactwithdotnet_designer_slider input[type="range"]::-webkit-slider-thumb {
                          -webkit-appearance:none !important;
                          width:20px;
                          height:20px;
                          background:#f5f5f5;
                          border:2px solid #00acee;
                          border-radius:50%;
                          cursor:pointer;
                        }
                        """
                    },
                    new input
                    {
                        step                     = 10,
                        type                     = "range",
                        min                      = 300,
                        max                      = 1600,
                        valueBind                = () => state.ScreenWidth,
                        valueBindDebounceTimeout = 500,
                        valueBindDebounceHandler = OnMediaSizeChanged,
                        style                    = { Height(10), WidthMaximized, BorderRadius(38) }
                    }
                }
                
            },

            new FlexColumn(FlexGrow(1))
            {
                // h e a d e r
                new FlexRow(Color("#6c757d"), CursorPointer, TextAlignCenter)
                {
                    When(canShowInstanceEditor(), new div(Text("Instance json"))
                    {
                        OnClick(_ => Task.FromResult(state.IsInstanceEditorActive = true)),
                        When(state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    }),

                    When(canShowParametersEditor(), new div(Text("Parameters json"))
                    {
                        OnClick(_ => Task.FromResult(state.IsInstanceEditorActive = false)),
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
            var maxHeight = 600;

            var step = 50;
            var max = maxHeight / step + 1;

            IReadOnlyList<Element> createTenPoints()
            {
                var returnList = new List<Element>();

                var miniStep = 10;

                var cursor = 0;
                var distance = miniStep;
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
                var distance = miniStep;
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

            return new FlexRow(PositionRelative, WidthMaximized, Height(20))
            {
                Enumerable.Range(0, (int)max).Select(number => new div(PositionAbsolute)
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

        var outputPanel = new div(PositionRelative)
        {
            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
            BackgroundSize("10px 10px"),

            createHorizontalRuler() + PositionAbsolute,
            new div(PositionAbsolute, Top(18), WidthMaximized, Height("calc(100% - 20px)"))
            {
                new div(PositionRelative)
                {
                    WidthHeightMaximized,
                    createElement(),

                    new div(PositionAbsolute, Top(0), Left(0))
                    {
                        createVerticleRuler
                    }
                }
            },

            Width(state.ScreenWidth <= 100 ? state.ScreenWidth + "%" : state.ScreenWidth + "px"),

            HeightMaximized,
            BoxShadow(0, 4, 12, 0, rgba(0, 0, 0, 0.1))
        };

        Element createElement()
        {
            return new iframe
            {
                id    = "ComponentPreview",
                src   = "/ReactWithDotNetDesignerComponentPreview",
                style = { BorderNone, WidthMaximized, HeightMaximized },
                title = "Component Preview"
            };
        }

        return new FlexRow(WidthMaximized, Height100vh, PrimaryBackground, FontFamily("system-ui"))
        {
            new HotReloadListener(),
            new div(BorderRight("1px dotted #d9d9d9"), Width(300), PositionRelative)
            {
                When(UpdatingProgress is > 0 and <= 100, () => new div(PositionAbsolute, TopRight(5))
                {
                    When(state.PropertyPanelIsClosed, PositionStatic),

                    new LoadingIcon() + Size(12, 12)
                }),

                new div
                {
                    state.PropertyPanelIsClosed ? "→" : "←",
                    OnClick(state.PropertyPanelIsClosed ? OpenPropertyPanel : ClosePropertyPanel),
                    PositionAbsolute,
                    TopRight(0),
                    FontSize14,
                    FontWeight500,
                    Color("#c5d7e8"),
                    CursorPointer,
                    Hover(FontSize17, Color("#9090f2")),
                    When(state.PropertyPanelIsClosed, PositionSticky),

                    Size(12, 12),
                    When(UpdatingProgress is > 0 and <= 100, DisplayNone)
                },

                When(state.PropertyPanelIsClosed == false, propertyPanel),
                When(state.PropertyPanelIsClosed, Width(15))
            },
            new div(DisplayFlex, JustifyContentCenter, FlexGrow(1), Padding(7), MarginLeft(40))
            {
                outputPanel
            },
            ComponentInspector
        };
    }

    async Task<Element> ComponentInspector()
    {
        Element rootNode;
        
        try
        {
            rootNode = ReactWithDotNetDesignerComponentPreview.CreateElement(state, Context);
            if (rootNode is PureComponent pureComponent)
            {
                rootNode = await pureComponent.InvokeRender();
            }
        }
        catch (Exception exception)
        {
            return exception.ToString();
        }
        
        return new FlexColumn(BorderLeft("1px dotted #d9d9d9"), Width(300), PositionFixed, Right(0), Top(0), Height100vh,
                              FontFamily("consolas, sans-serif"), FontSize11)
        {
            CreateElementTree(rootNode) + FlexGrow(1),
            CreateStyleEditor() + FlexGrow(3)
        };
    }

    Element CreateStyleEditor()
    {
        return new FlexColumn(BorderTop("1px dotted #d9d9d9"))
        {
            new FlexColumn(Gap(5), JustifyContentFlexStart)
            {
                new input{type = "text", value = "abc"},
                new input{type = "text", value = "abc2"},
                
                new StyleSearchInput()
            }
        };
    }
    Element CreateElementTree(Element rootNode)
    {
        if (rootNode is null)
        {
            return "-";
        }
        
        var tree = new FlexColumn
        {
             CursorDefault, FontWeight500, FontStyleItalic
        };

        addNode(rootNode,"0",2);
        
        return tree;
        
        void addNode(Element node, string path, int leftIndent)
        {
            if (node is null)
            {
                tree.Add(new div(PaddingLeft(leftIndent)){"null"});
                return;
            }

            var name = node.GetType().Name;

            var treeNode = createNewTreeNode(name);

            treeNode.Add(Data("path", path));
            treeNode.onClick = OnComponentElementTreeNodeClicked;

            if (path == state.ComponentElementTreeSelectedNodePath)
            {
                treeNode.Add(BackgroundColor("#e7eaff"));
            }
            
            treeNode.Add(PaddingLeft(leftIndent));
            
            tree.Add(treeNode);

            if (node._children is null)
            {
                return;
            }

            var childIndex = 0;
            foreach (var child in node._children)
            {
                var newPath = path + "," + childIndex;
                
                addNode(child, newPath, leftIndent + 8);
                
                childIndex++;
            }

            static HtmlElement createNewTreeNode(string label)
            {
                return new div
                {
                    label,
                    Hover(BackgroundColor("#f4f5fe"))
                };
            }
            

        }
    }

    async Task OnComponentElementTreeNodeClicked(MouseEvent e)
    {
        state.ComponentElementTreeSelectedNodePath = e.currentTarget.data["path"];

        var path = @"C:\github\ReactWithDotNet\ReactWithDotNet.WebSite\$.cs";
        
        await File.WriteAllTextAsync(path, """
                                     namespace ReactWithDotNet.__designer__; // ReSharper disable All
                                     
                                     static class Designer
                                     {
                                         public static List<(List<int> treePath, List<StyleModifier> modifiers)> GetStyle(Type type)
                                         {
                                             if (type == typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent))
                                             {
                                                 return 
                                                 [
                                                     ([0],
                                                     [
                                                         Background("yellow")
                                                     ])
                                                 ];
                                             }
                                             
                                             return null;
                                         }
                                     }
                                     """);
        
        
        SaveState();
        
        Client.RefreshComponentPreview();
        
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

    Task ClosePropertyPanel(MouseEvent _)
    {
        state.PropertyPanelIsClosed = true;
        SaveState();

        return Task.CompletedTask;
    }

    Task OnComponentPreviewRefreshed()
    {
        UpdatingProgress = 25;
        Client.GotoMethod(UpdateProgress, UpdatingProgress + 25);
        return Task.CompletedTask;
    }

    Task OnElementSelected(string keyOfSelectedTreeNode)
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
        
        Client.RefreshComponentPreview();

        return Task.CompletedTask;

        void initializeInstanceJson()
        {
            var typeOfInstance = state.SelectedType ?? state.SelectedMethod?.DeclaringType;

            if (typeOfInstance == null)
            {
                return;
            }

            var map = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(state.JsonTextForDotNetInstanceProperties ?? string.Empty) ?? new Dictionary<string, object>();

            var instanceType = MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(typeOfInstance);
            
            if (instanceType is not null)
            {
                var instance = Activator.CreateInstance(instanceType);
                
                foreach (var propertyInfo in instanceType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
                {
                    var name = propertyInfo.Name;
                    var propertyType = propertyInfo.PropertyType;

                    if (propertyType.GetInterfaces().Any(x => x == typeof(IModifier)))
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
                            map.Add(name, ReflectionHelper.CreateDummyValue(propertyType));
                            continue;
                        }
                    }

                    if (propertyInfo.DeclaringType == typeof(Element) ||
                        propertyInfo.DeclaringType == typeof(ReactComponentBase) ||
                        propertyInfo.DeclaringType == typeof(PureComponent))
                    {
                        continue;
                    }

                    if (propertyInfo.DeclaringType?.IsGenericType == true &&
                        propertyInfo.DeclaringType.GetGenericTypeDefinition() == typeof(Component<>))
                    {
                        continue;
                    }

                    if (propertyType.BaseType == typeof(MulticastDelegate))
                    {
                        continue;
                    }

                    if (propertyType.IsAbstract && propertyType.IsClass)
                    {
                        continue;
                    }

                    if (isNumberType(propertyInfo.PropertyType))
                    {
                        var existingValue = propertyInfo.GetValue(instance);
                        var defaultValue = Activator.CreateInstance(propertyInfo.PropertyType);

                        var hasDefaultValue = defaultValue!.Equals(existingValue);
                        if (!hasDefaultValue)
                        {
                            map.Add(name, existingValue);
                            continue;
                        }
                    }
                
                    if (!map.ContainsKey(name))
                    {
                        map.Add(name, ReflectionHelper.CreateDummyValue(propertyType));
                    }
                }    
            }
            
            

            state.JsonTextForDotNetInstanceProperties = JsonSerializer.Serialize(map, new JsonSerializerOptions
            {
                WriteIndented          = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
            
            static bool isNumberType(Type type) =>
                type == typeof(int) ||
                type == typeof(long) ||
                type == typeof(decimal) ||
                type == typeof(byte) ||
                type == typeof(short) ||
                type == typeof(decimal) ||
                type == typeof(double) ||
                type == typeof(float);
        }

        void initializeParametersJson()
        {
            var map = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters ?? string.Empty) ?? new Dictionary<string, object>();

            foreach (var parameterInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedMethod)?.GetParameters() ?? new ParameterInfo[] { })
            {
                var name = parameterInfo.Name;
                if (name == null || map.ContainsKey(name))
                {
                    continue;
                }

                map.Add(name, ReflectionHelper.CreateDummyValue(parameterInfo.ParameterType));
            }

            state.JsonTextForDotNetMethodParameters = JsonSerializer.Serialize(map, new JsonSerializerOptions
            {
                WriteIndented          = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
    }

    Task OnFilterChanged()
    {
        return Task.CompletedTask;
    }

    Task OnKeypressFinished()
    {
        SaveState();

        Client.RefreshComponentPreview();

        return Task.CompletedTask;
    }

    Task OnMediaSizeChanged()
    {
        SaveState();

        return Task.CompletedTask;
    }

    Task OpenPropertyPanel(MouseEvent _)
    {
        state.PropertyPanelIsClosed = false;
        SaveState();
        return Task.CompletedTask;
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

    Task UpdateProgress(int newValue)
    {
        UpdatingProgress = newValue;
        if (UpdatingProgress <= 100)
        {
            Client.GotoMethod(500, UpdateProgress, UpdatingProgress + 25);
        }

        return Task.CompletedTask;
    }

    // Taken from https://www.w3schools.com/howto/tryit.asp?filename=tryhow_css_loader
    class LoadingIcon : PureComponent
    {
        protected override Element render()
        {
            return new div
            {
                new style
                {
                    """
                    .loader-designer-react-with-dot-net {
                      border: 1px solid #f3f3f3;
                      border-radius: 50%;
                      border-top: 1px solid #c3beb7;
                    
                      -webkit-animation: spin 1s linear infinite; /* Safari */
                      animation: spin 1s linear infinite;
                    }

                    /* Safari */
                    @-webkit-keyframes spin {
                      0% { -webkit-transform: rotate(0deg); }
                      100% { -webkit-transform: rotate(360deg); }
                    }

                    @keyframes spin {
                      0% { transform: rotate(0deg); }
                      100% { transform: rotate(360deg); }
                    }
                    """
                },

                new div { className = "loader-designer-react-with-dot-net", style = { WidthHeightMaximized } }
            };
        }
    }


    class StyleSearchInput : Component
    {
        Element IconSearch => new svg(svg.Size(18), ViewBox(0, 0, 18, 18))
        {
            new path { d = "M14.8539503,14.1467096 C15.0478453,14.3412138 15.0475893,14.6560006 14.8533783,14.8501892 C14.6592498,15.0442953 14.3445263,15.0442862 14.1504091,14.8501689 L12.020126,12.7261364 C11.066294,13.5214883 9.8390282,14 8.5,14 C5.46243388,14 3,11.5375661 3,8.5 C3,5.46243388 5.46243388,3 8.5,3 C11.5375661,3 14,5.46243388 14,8.5 C14,9.83874333 13.5216919,11.0657718 12.726644,12.0195172 L14.8539503,14.1467096 Z M8.5,13 C10.9852814,13 13,10.9852814 13,8.5 C13,6.01471863 10.9852814,4 8.5,4 C6.01471863,4 4,6.01471863 4,8.5 C4,10.9852814 6.01471863,13 8.5,13 Z" }
        };

        Element IconClose => new svg(svg.Size(18), ViewBox(0, 0, 18, 18))
        {
            new path { d = "M8.44 9.5L6 7.06A.75.75 0 1 1 7.06 6L9.5 8.44 11.94 6A.75.75 0 0 1 13 7.06L10.56 9.5 13 11.94A.75.75 0 0 1 11.94 13L9.5 10.56 7.06 13A.75.75 0 0 1 6 11.94L8.44 9.5z" }
        };
        protected override Element render()
        {
            return new FlexColumn
            {
                new style
                {
                    """
                    input:focus { outline:none; }
                    """
                },
                new FlexRow(BackgroundWhite, Border(Solid(1, "#dbdde5")), Padding(3), BorderRadius(3))
                {

                    IconSearch,
                    new input
                    {
                        type                     = "text",
                        valueBind                = () => Value,
                        valueBindDebounceTimeout = 700,
                        valueBindDebounceHandler = OnTypingFinished,
                        onKeyDown = OnKeyDown,
                        style                    = { BorderNone, FlexGrow(1) }
                    },
                    IconClose,


                },
                new FlexColumn
                {
                    typeof(Mixin).GetProperties(BindingFlags. Static | BindingFlags.Public)
                        .Where(p=>p.Name.Contains(Value+"",StringComparison.OrdinalIgnoreCase))
                        .Select(p=>new div{p.Name})
                        .Take(5)
                }
            };
        }

        [ReactKeyboardEventCallOnly("ESC")]
        Task OnKeyDown(KeyboardEvent e)
        {
            return Task.CompletedTask;
        }

        Task OnTypingFinished()
        {
            
            return Task.CompletedTask;
        }

        public string Value { get; set; }
    }
}