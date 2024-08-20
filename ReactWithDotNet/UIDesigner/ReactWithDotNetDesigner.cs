using System.Reflection;
using System.Reflection.Metadata;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;
using static ReactWithDotNet.UIDesigner.Extensions;

[assembly: MetadataUpdateHandler(typeof(HotReloadListener))]

namespace ReactWithDotNet.UIDesigner;

sealed class HotReloadListener : Component<HotReloadListener.State>
{
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

    internal record State
    {
        public int ChangeCount { get; init; }
    }
}

public sealed class ReactWithDotNetDesigner : Component<ReactWithDotNetDesignerModel>
{
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
        
        Client.ListenEvent("RefreshComponentPreviewCompleted",OnRefreshComponentPreviewCompleted);

        return Task.CompletedTask;
    }

    Task OnRefreshComponentPreviewCompleted()
    {
        // refresh element tree
        return Task.CompletedTask;
    }
    
    protected override Element render()
    {
        if (Preview)
        {
            return new ReactWithDotNetDesignerComponentPreview();
        }

        var propertyPanelContent = new FlexColumn(SizeFull, FontSize15)
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
            new FlexColumn(WidthFull, Flex(1, 1, 0))
            {
                new MethodSelectionView
                {
                    ClassFilter               = state.ClassFilter,
                    MethodFilter              = state.MethodFilter,
                    SelectedMethodTreeNodeKey = state.SelectedTreeNodeKey,
                    SelectionChanged          = OnElementSelected,
                    AssemblyFilePath          = state.SelectedAssemblyFilePath
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
            }
        };

        var propertyPanel = new div(BorderRight("1px dotted #d9d9d9"), Width(300), PositionRelative, Transition("width", 300, "ease-in"))
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

            state.PropertyPanelIsClosed ? null : propertyPanelContent,

            When(state.PropertyPanelIsClosed, Width(15))
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

        var stylerPanel = new div(Border("1px dotted #d9d9d9"), PositionRelative, Transition("width", 300, "ease-in"))
        {
            PositionFixed,
            Bottom(0),
            Left(300),
            BorderTopRightRadius(8),
            MinHeight(200),
            Background(rgba(255,255,255,0.6)),
            FontFamily("consolas, sans-serif"), FontSize11, Padding(5)
        };
        
        return new FlexRow(Width100vw, Height100vh, PrimaryBackground, FontFamily("system-ui"))
        {
            new HotReloadListener(),
            propertyPanel,
            new FlexColumn(AlignItemsCenter, FlexGrow(1), Padding(7), MarginLeft(40))
            {
                createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(5),
                outputPanel
            },
            stylerPanel
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

        state = state with
        {
            SelectedType = null,
            SelectedMethod = null,
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

        state = state with
        {
            ClassFilter = classFilter,
            MethodFilter = methodFileter
        };

        await SaveState();

        Client.RefreshComponentPreview();
    }

    Task OnFilterChanged()
    {
        return SaveState();
    }

    async Task OnMediaSizeChanged()
    {
        await SaveState();
    }

    Task OnMediaSizeMinusClicked(MouseEvent e)
    {
        state = state with { ScreenWidth = state.ScreenWidth - 10 };

        return OnMediaSizeChanged();
    }

    Task OnMediaSizePlusClicked(MouseEvent e)
    {
        state = state with { ScreenWidth = state.ScreenWidth + 10 };

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