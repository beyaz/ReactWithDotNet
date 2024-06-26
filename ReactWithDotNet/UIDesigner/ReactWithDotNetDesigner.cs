﻿using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.UIDesigner;
using static ReactWithDotNet.UIDesigner.Extensions;

[assembly: MetadataUpdateHandler(typeof(HotReloadListener))]

namespace ReactWithDotNet.UIDesigner;

sealed class HotReloadListener : Component<HotReloadListener.State>
{
    internal record State
    {
        public int ChangeCount { get; init; }
    }
    
    public static int StaticChangeCount { get; private set; }

    public static void UpdateApplication(Type[] _)
    {
        StaticChangeCount++;
    }

    public Task Refresh()
    {
        if (state.ChangeCount != StaticChangeCount)
        {
            Client.RefreshComponentPreview();
        }

        state = state with { ChangeCount = StaticChangeCount };

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

public sealed class ReactWithDotNetDesigner : Component<ReactWithDotNetDesignerModel>
{
    delegate Task JsonTextChanged(string componentName, string jsonText);

    public static string UrlPath => "/$";

    public int UpdatingProgress { get; set; }

    internal static string UrlPathOfComponentPreview => $"{UrlPath}?preview=true";

    bool Preview => GetQuery("preview") == "true";

    /// <summary>
    ///     Indicates component is in design mode.
    /// </summary>
    public static bool IsInDesignMode(HttpContext httpContext)
    {
        var referer = httpContext?.Request.Headers[HeaderNames.Referer].FirstOrDefault();

        return referer?.EndsWith(UrlPath) == true ||
               referer?.EndsWith(UrlPathOfComponentPreview) == true;
    }

    protected override Task constructor()
    {
        if (Preview)
        {
            return Task.CompletedTask;
        }

        state = StateCache.ReadState() ?? new ReactWithDotNetDesignerModel();

        state = state with { SelectedAssemblyFilePath = Assembly.GetEntryAssembly()?.Location };

        Client.ListenEvent("ComponentPreviewRefreshed", OnComponentPreviewRefreshed);

        Client.ListenEventThenOnlyUpdateState<JsonTextChanged>(OnJsonTextChanged);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        if (Preview)
        {
            return new ReactWithDotNetDesignerComponentPreview();
        }

        var propertyPanel = new FlexColumn(Height("100%"), Width("100%"), FontSize15)
        {
            new link { href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet" },

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
            new FlexColumn(WidthFull, MaxHeight(250))
            {
                new MethodSelectionView
                {
                    ClassFilter               = state.ClassFilter,
                    MethodFilter              = state.MethodFilter,
                    SelectedMethodTreeNodeKey = state.SelectedTreeNodeKey,
                    SelectionChanged          = OnElementSelected,
                    AssemblyFilePath          = state.SelectedAssemblyFilePath,
                    IsCollapsed = state.IsMethodSelectionViewCollapsed,
                    OnCollapseClick = _ => Task.FromResult(state = state with{IsMethodSelectionViewCollapsed = !state.IsMethodSelectionViewCollapsed})
                }
            },

            SpaceY(10),

            new FlexRow(WidthFull, PaddingLeftRight(3))
            {
                new fieldset(WidthFull)
                {
                    Border("1px solid #d9d9d9"),
                    BorderRadius(4),

                    new legend(MarginLeft(8), DisplayFlexRowCentered)
                    {
                        new FlexRow(AlignItemsCenter, Gap(5), PaddingLeftRight(1))
                        {
                            createLabel($"Media Size: W: {state.ScreenWidth}px"),
                            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                            {
                                OnClick(OnMediaSizeMinusClicked),
                                new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
                                {
                                    new path { fill = "currentColor", d = "M12 8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h8c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
                                }
                            },
                            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                            {
                                OnClick(OnMediaSizePlusClicked),
                                new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
                                {
                                    new path { fill = "currentColor", d = "M12 8.667H8.667V12c0 .367-.3.667-.667.667A.669.669 0 0 1 7.333 12V8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h3.333V4c0-.366.3-.667.667-.667.367 0 .667.3.667.667v3.333H12c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
                                }
                            }
                        },

                        SpaceX(10),

                        new FlexRow(AlignItemsCenter, PaddingLeftRight(1))
                        {
                            createLabel($"H: {state.ScreenHeight}%")
                        }
                    },

                    new FlexRow(WidthFull, ClassName("reactwithdotnet_designer_slider"))
                    {
                        new style
                        {
                            """
                            .reactwithdotnet_designer_slider input[type="range"] {
                              -webkit-appearance:none !important;
                              width:200px;
                              height:1px;
                              background:#7cc8e5;
                              border:none;
                              outline:none;
                            }
                            .reactwithdotnet_designer_slider input[type="range"]::-webkit-slider-thumb {
                              -webkit-appearance:none !important;
                              width:20px;
                              height:20px;
                              background:#f5f5f5;
                              border:2px solid #7cc8e5;
                              border-radius:50%;
                              cursor:pointer;
                            }
                            """
                        },

                        new FlexColumn(WidthFull)
                        {
                            new FlexRowCentered(PaddingLeftRight(5), PaddingTop(8), PaddingBottom(10))
                            {
                                new input
                                {
                                    step                     = 10,
                                    type                     = "range",
                                    min                      = 300,
                                    max                      = 1600,
                                    valueBind                = () => state.ScreenWidth,
                                    valueBindDebounceTimeout = 500,
                                    valueBindDebounceHandler = OnMediaSizeChanged,
                                    style                    = { Height(10), WidthFull, BorderRadius(38) }
                                }
                            },

                            new FlexRow(JustifyContentSpaceAround, AlignItemsCenter)
                            {
                                new[] { "M", "SM", "MD", "LG", "XL", "XXL" }.Select(x => new FlexRowCentered
                                {
                                    x,
                                    FontSize16,
                                    FontWeight300,
                                    CursorDefault,
                                    PaddingTopBottom(3),
                                    FlexGrow(1),

                                    Data("value", x),
                                    OnClick(OnCommonSizeClicked),
                                    Hover(Color("#2196f3")),

                                    (x == "M" && state.ScreenWidth == 320) ||
                                    (x == "SM" && state.ScreenWidth == 640) ||
                                    (x == "MD" && state.ScreenWidth == 768) ||
                                    (x == "LG" && state.ScreenWidth == 1024) ||
                                    (x == "XL" && state.ScreenWidth == 1280) ||
                                    (x == "XXL" && state.ScreenWidth == 1536)
                                        ? FontWeight500 + Color("#2196f3")
                                        : null
                                })
                            }
                        },

                        new FlexRow(Width(30))
                        {
                            new input
                            {
                                step                     = 10,
                                type                     = "range",
                                min                      = 10,
                                max                      = 100,
                                valueBind                = () => state.ScreenHeight,
                                valueBindDebounceTimeout = 500,
                                valueBindDebounceHandler = OnMediaSizeChanged,
                                style                    = { Height(7), MarginTop(20), Width(70), BorderRadius(38), Transform("translateY(20px)"), Rotate("90deg") }
                            }
                        }
                    }
                }
            },

            new FlexColumn(Flex(1,1,0))
            {
                // h e a d e r
                new FlexRow(Color("#6c757d"), CursorPointer, TextAlignCenter)
                {
                    When(canShowInstanceEditor(), () => new div(Text("Instance json"))
                    {
                        OnClick(_ => Task.FromResult(state = state with { IsInstanceEditorActive = true })),
                        When(state.IsInstanceEditorActive, BorderBottom("2px solid #2196f3"), Color("#2196f3"), FontWeight600),
                        Padding(10),
                        FlexGrow(1),
                        FontSize13
                    }),

                    When(canShowParametersEditor(), () => new div(Text("Parameters json"))
                    {
                        OnClick(_ => Task.FromResult(state = state with { IsInstanceEditorActive = false })),
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

        var outputPanel = new FlexRow(JustifyContentFlexStart, PositionRelative)
        {
            BackgroundImage("radial-gradient(#a5a8ed 0.5px, #f8f8f8 0.5px)"),
            BackgroundSize("10px 10px"),

            createVerticleRuler,
            createElement(),

            Width(state.ScreenWidth),
            Height(state.ScreenHeight * percent),
            BoxShadow(0, 4, 12, 0, rgba(0, 0, 0, 0.1))
        };

        return new FlexRow(WidthFull, Height100vh, PrimaryBackground, FontFamily("system-ui"))
        {
            new HotReloadListener(),
            new div(BorderRight("1px dotted #d9d9d9"), Width(300), PositionRelative,Transition("width", 300,"ease-in"))
            {
                When(UpdatingProgress is > 0 and <= 100, () => new div(PositionAbsolute, TopRight(4))
                {
                    When(state.PropertyPanelIsClosed, PositionStatic),

                    new LoadingIcon() + Size(12)
                }),

                new div
                {
                    state.PropertyPanelIsClosed ? "→" : "←",
                    OnClick(state.PropertyPanelIsClosed ? OpenPropertyPanel : ClosePropertyPanel),
                    PositionAbsolute,
                    TopRight(0),
                    FontSize16,
                    FontWeight500,
                    Color("#c5d7e8"),
                    CursorPointer,
                    Hover(Color("#9090f2")),
                    When(state.PropertyPanelIsClosed, PositionSticky),

                    Size(16),
                    When(UpdatingProgress is > 0 and <= 100, DisplayNone)
                },

                state.PropertyPanelIsClosed ? null : propertyPanel,

                When(state.PropertyPanelIsClosed, Width(15))
            },
            new FlexColumn(AlignItemsCenter, FlexGrow(1), Padding(7), MarginLeft(40))
            {
                createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(5),
                outputPanel
            }
        };

        static Element createVerticleRuler()
        {
            const int maxHeight = 5000;

            const int step = 50;
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

            return new div(SizeFull, Width(30), MarginLeft(-30), OverflowHidden, PositionRelative)
            {
                Enumerable.Range(0, max).Select(number => new div(PositionAbsolute)
                {
                    Right(3), Top(number * step),
                    new FlexRow(FontSize8, LineHeight6, FontWeight500, Gap(4))
                    {
                        new div(MarginTop(number == 0 ? 0 : -3))
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

        static Element createElement()
        {
            return new iframe
            {
                id    = "ComponentPreview",
                src   = UrlPathOfComponentPreview,
                style = { BorderNone, WidthFull, HeightFull },
                title = "Component Preview"
            };
        }

        Element createJsonEditor()
        {
            if (state.IsInstanceEditorActive)
            {
                return new JsonTextEditor
                {
                    JsonText            = state.JsonTextForDotNetInstanceProperties,
                    Name                = nameof(state.JsonTextForDotNetInstanceProperties),
                    SelectedTreeNodeKey = state.SelectedTreeNodeKey
                };
            }

            return new JsonTextEditor
            {
                JsonText            = state.JsonTextForDotNetMethodParameters,
                Name                = nameof(state.JsonTextForDotNetMethodParameters),
                SelectedTreeNodeKey = state.SelectedTreeNodeKey
            };
        }

        static Element createLabel(string text)
        {
            return new small(Text(text), Color("rgb(73 86 193)"), FontWeight600, UserSelect(none));
        }

        Element createHorizontalRuler()
        {
            const int step = 50;
            var max = state.ScreenWidth / step + 1;

            return new FlexRow(PositionRelative, WidthFull, Height(20))
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
        }
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

    async Task ClosePropertyPanel(MouseEvent _)
    {
        state = state with { PropertyPanelIsClosed = true };
     
        await SaveState();
    }

    string GetQuery(string name)
    {
        var httpContext = Context.HttpContext;

        var value = httpContext.Request.Query[name].FirstOrDefault();
        if (value != null)
        {
            return value;
        }

        var referer = httpContext.Request.Headers["Referer"];
        if (string.IsNullOrWhiteSpace(referer))
        {
            return null;
        }

        var nameValueCollection = HttpUtility.ParseQueryString(new Uri(referer).Query);

        return nameValueCollection[name];
    }

    async Task OnCommonSizeClicked(MouseEvent e)
    {
        state = state with
        {
            ScreenWidth = e.currentTarget.data["value"] switch
            {
                "M"   => 320,
                "SM"  => 640,
                "MD"  => 768,
                "LG"  => 1024,
                "XL"  => 1280,
                "XXL" => 1536,
                _     => throw new ArgumentOutOfRangeException()
            }
        };

        await SaveState();
    }

    Task OnComponentPreviewRefreshed()
    {
        UpdatingProgress = 25;
        Client.GotoMethod(UpdateProgress, UpdatingProgress + 25);
        return Task.CompletedTask;
    }

    async Task OnElementSelected(string keyOfSelectedTreeNode)
    {
        var classFilter = state.ClassFilter;
        var methodFileter = state.MethodFilter;

        state                = state with
        {
            SelectedType = null,
            SelectedMethod = null,
            JsonTextForDotNetInstanceProperties = null,
            JsonTextForDotNetMethodParameters   = null,
            SelectedTreeNodeKey = keyOfSelectedTreeNode
        };
        
        var fullAssemblyPath = state.SelectedAssemblyFilePath;

        var node = MethodSelectionView.FindTreeNode(fullAssemblyPath, state.SelectedTreeNodeKey, state.ClassFilter, state.MethodFilter);
        if (node is not null)
        {
            if (node.IsClass)
            {
                state = state with { SelectedType = node.TypeReference };

                state = StateCache.TryRead(state.SelectedType) ?? state;
            }

            if (node.IsMethod)
            {
                state = state with { SelectedMethod = node.MethodReference };

                state = StateCache.TryRead(state.SelectedMethod) ?? state;
            }
        }

        state              = state with
        {
            ClassFilter = classFilter,
            MethodFilter = methodFileter
        };

        if (canShowInstanceEditor() && canShowParametersEditor() == false)
        {
            state = state with { IsInstanceEditorActive = true };
        }

        if (canShowParametersEditor() && canShowInstanceEditor() == false)
        {
            state = state with { IsInstanceEditorActive = false };
        }

        if (canShowInstanceEditor())
        {
            IgnoreException(initializeInstanceJson);
        }

        if (canShowParametersEditor())
        {
            IgnoreException(initializeParametersJson);
        }

        await SaveState();

        Client.RefreshComponentPreview();

        return;

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
                    if (propertyInfo.CanWrite is false || propertyInfo.CanRead is false)
                    {
                        continue;
                    }

                    var name = propertyInfo.Name;
                    var propertyType = propertyInfo.PropertyType;

                    if (propertyType.GetInterfaces().Any(x => x == typeof(Modifier)))
                    {
                        continue;
                    }

                    if (propertyType.Namespace?.StartsWith("System.Linq.Expressions", StringComparison.OrdinalIgnoreCase) is true)
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

                    var existingValue = propertyInfo.GetValue(instance);
                    if (existingValue is not null)
                    {
                        if (propertyInfo.PropertyType.IsClass)
                        {
                            map.Add(name, existingValue);
                            continue;
                        }

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

            state = state with
            {
                JsonTextForDotNetInstanceProperties = JsonSerializer.Serialize(map, new JsonSerializerOptions
                {
                    WriteIndented          = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                })
            };
        }

        void initializeParametersJson()
        {
            var map = DeserializeJsonBySystemTextJson<Dictionary<string, object>>(state.JsonTextForDotNetMethodParameters ?? string.Empty) ?? new Dictionary<string, object>();

            foreach (var parameterInfo in MetadataHelper.LoadAssembly(fullAssemblyPath).TryLoadFrom(state.SelectedMethod)?.GetParameters() ?? [])
            {
                var name = parameterInfo.Name;
                if (name == null || map.ContainsKey(name))
                {
                    continue;
                }

                map.Add(name, ReflectionHelper.CreateDummyValue(parameterInfo.ParameterType));
            }

            state = state with
            {
                JsonTextForDotNetMethodParameters = JsonSerializer.Serialize(map, new JsonSerializerOptions
                {
                    WriteIndented          = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                })
            };
        }
    }

    Task OnFilterChanged()
    {
        state = state with
        {
            IsMethodSelectionViewCollapsed = false
        };
        
        return SaveState();
        
    }

    async Task OnJsonTextChanged(string componentname, string jsontext)
    {
        if (componentname == nameof(state.JsonTextForDotNetInstanceProperties))
        {
            state = state with { JsonTextForDotNetInstanceProperties = jsontext };
        }
        else
        {
            state = state with { JsonTextForDotNetMethodParameters = jsontext };
        }

        await SaveState();

        Client.RefreshComponentPreview();

    }

    async Task OnMediaSizeChanged()
    {
        await SaveState();
    }

    Task OnMediaSizeMinusClicked(MouseEvent e)
    {
        state = state with { ScreenWidth = 10 };

        return OnMediaSizeChanged();
    }

    Task OnMediaSizePlusClicked(MouseEvent e)
    {
        state = state with { ScreenWidth = 10 };

        return OnMediaSizeChanged();
    }

    async Task OpenPropertyPanel(MouseEvent _)
    {
        state = state with { PropertyPanelIsClosed = false };
        await SaveState();
    }

    async Task SaveState()
    {
        if (state.SelectedMethod is not null)
        {
            await StateCache.Save(state.SelectedMethod, state);
        }

        if (state.SelectedType is not null)
        {
            await StateCache.Save(state.SelectedType, state);
        }

        await StateCache.Save(state);

        await OnComponentPreviewRefreshed();
    }

    Task UpdateProgress(int newValue)
    {
        UpdatingProgress = newValue;
        if (UpdatingProgress <= 100)
        {
            Client.GotoMethod(100, UpdateProgress, UpdatingProgress + 25);
        }

        return Task.CompletedTask;
    }

    class JsonTextEditor : Component<JsonTextEditor.JsonTextEditorState>
    {
        public required string JsonText { get; init; }
        public required string Name { get; init; }
        public required string SelectedTreeNodeKey { get; init; }

        protected internal override Task OverrideStateFromPropsBeforeRender()
        {
            if (state.SelectedTreeNodeKey != SelectedTreeNodeKey)
            {
                state.SelectedTreeNodeKey = SelectedTreeNodeKey;
                state.JsonText            = JsonText;
            }

            return Task.CompletedTask;
        }

        protected override Task constructor()
        {
            state = new()
            {
                JsonText            = JsonText,
                SelectedTreeNodeKey = SelectedTreeNodeKey
            };

            return Task.CompletedTask;
        }

        protected override Element render()
        {
            return new Editor
            {
                defaultLanguage          = "json",
                valueBind                = () => state.JsonText,
                valueBindDebounceTimeout = 800,
                valueBindDebounceHandler = OnKeypressFinished,
                options =
                {
                    renderLineHighlight = "none",
                    fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                    fontSize            = 11,
                    minimap             = new { enabled = false },
                    lineNumbers         = "off",
                    unicodeHighlight    = new { showExcludeOptions = false }
                }
            };
        }

        Task OnKeypressFinished()
        {
            Client.DispatchEvent<JsonTextChanged>([Name, state.JsonText]);

            return Task.CompletedTask;
        }

        internal class JsonTextEditorState
        {
            public string JsonText { get; set; }
            public string SelectedTreeNodeKey { get; set; }
        }
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

                new div { className = "loader-designer-react-with-dot-net", style = { SizeFull } }
            };
        }
    }
}