﻿using System.Reflection;
using System.Reflection.Metadata;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.UIDesigner;

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

    StyleModifier ScaleStyle => TransformOrigin("0 0") + Transform($"scale({state.Scale / (double)100})");

    /// <summary>
    ///     Indicates component is in design mode.
    /// </summary>
    public static bool IsInDesignMode(HttpContext httpContext)
    {
        if (httpContext is null)
        {
            return false;
        }
        
        const string key = nameof(ReactWithDotNetDesigner) + nameof(IsInDesignMode);
        
        if (httpContext.Items.TryGetValue(key, out var value))
        {
            return (bool)value!;
        }
        
        var referer = httpContext.Request.Headers[HeaderNames.Referer].FirstOrDefault();
        if (referer is not null)
        {
            var uri = new Uri(referer);
                
            value =  uri.AbsolutePath == UrlPath;
        }
        else
        {
            value = false;
        }
        
        httpContext.Items[key] =  value;
        
        return (bool)value;
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

        Client.ListenEvent("RefreshComponentPreviewCompleted", OnRefreshComponentPreviewCompleted);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        if (Preview)
        {
            return new ReactWithDotNetDesignerComponentPreview();
        }

        var propertyPanelContent = new FlexColumn(SizeFull, FontSize15, Gap(4))
        {
            new link { href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet" },

            new FlexColumn(PaddingLeftRight(3), PaddingTop(4))
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
            new FlexColumn(PaddingLeftRight(3), PaddingTopBottom(3))
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

            new FlexColumn(WidthFull, Flex(1, 1, 0), PaddingLeftRight(3), OverflowAuto)
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

            new FlexRow(WidthFull, PaddingLeftRight(3))
            {
                new fieldset(WidthFull)
                {
                    Border(1, solid, "#d9d9d9"),
                    BorderRadius(4),

                    new legend(MarginLeft(8), DisplayFlexRowCentered)
                    {
                        new FlexRow(AlignItemsCenter, Gap(5), PaddingLeftRight(1))
                        {
                            createLabel($"Media Size: W: {state.ScreenWidth}px"),
                            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                            {
                                OnClick(OnMediaSizeMinusClicked),
                                new IconMinus()
                            },
                            new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                            {
                                OnClick(OnMediaSizePlusClicked),
                                new IconPlus()
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

            new FlexRow(WidthFull, PaddingLeftRight(3), AlignItemsCenter, Gap(5))
            {
                createLabel($"Scale: %{state.Scale}"),
                new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                {
                    OnClick(async _ =>
                    {
                        if (state.Scale <= 20)
                        {
                            return;
                        }

                        state = state with { Scale = state.Scale - 10 };

                        await SaveState();
                    }),
                    new IconMinus()
                },
                new FlexRowCentered(BorderRadius(100), Padding(3), Background(Blue200), Hover(Background(Blue300)))
                {
                    OnClick(async _ =>
                    {
                        if (state.Scale >= 100)
                        {
                            return;
                        }

                        state = state with { Scale = state.Scale + 10 };

                        await SaveState();
                    }),
                    new IconPlus()
                }
            }
        };

        var propertyPanel = new div(BorderRight(1, dotted, "#d9d9d9"), Width(300), PositionRelative, Transition(Width, 300, "ease-in"), LineHeight24)
        {
            new div(PositionAbsolute, Top(0), state.PropertyPanelIsClosed ? MarginLeft(7) : Right(-8), Size(16), When(state.PropertyPanelIsClosed, PositionSticky), CursorDefault)
            {
                OnClick(state.PropertyPanelIsClosed ? OpenPropertyPanel : ClosePropertyPanel),

                UpdatingProgress is > 0 and <= 100 ?
                    new IconLoading
                    {
                        Color = "#afafaf", style =
                        {
                            Size(16), MarginTop(3)
                        }
                    } :
                    new IconLeft
                    {
                        Color = "#afafaf",
                        style =
                        {
                            When(state.PropertyPanelIsClosed, Rotate("180deg"))
                        }
                    }
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

        return new FlexRow(Width100vw, Height100vh, FontFamily("system-ui"))
        {
            new style
            {
                """
                body {
                  background: rgb(249, 249, 249);
                }
                """
            },
            new HotReloadListener(),
            propertyPanel,
            new FlexColumn(AlignItemsCenter, FlexGrow(1), Padding(7), MarginLeft(40), ScaleStyle)
            {
                createHorizontalRuler() + Width(state.ScreenWidth) + MarginTop(5),
                outputPanel
            }
        };

        static Element createVerticleRuler()
        {
            const int maxHeight = 5000;

            const int step = 50;
            const int max = maxHeight / step + 1;

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
            return new small(Text(text), Color(rgb(73, 86, 193)), FontWeight600, UserSelect(none), WhiteSpaceNoWrap);
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

        var referer = httpContext.Request.Headers[HeaderNames.Referer];
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

    Task OnRefreshComponentPreviewCompleted()
    {
        // refresh element tree
        return Task.CompletedTask;
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

    sealed class IconLeft : PureComponent
    {
        public string Color { get; init; } = "#c5d7e8";

        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 50, 50), svg.Size(16))
            {
                new path { fill = Color, d = "M25 1C11.767 1 1 11.767 1 25s10.767 24 24 24 24-10.767 24-24S38.233 1 25 1zm0 46C12.869 47 3 37.131 3 25S12.869 3 25 3s22 9.869 22 22-9.869 22-22 22z" },
                new path { fill = Color, d = "M29.293 10.293 14.586 25l14.707 14.707 1.414-1.414L17.414 25l13.293-13.293z" }
            };
        }
    }

    // Taken from https://www.w3schools.com/howto/tryit.asp?filename=tryhow_css_loader
    class IconLoading : PureComponent
    {
        public string Color { get; init; } = "#afafaf";

        protected override Element render()
        {
            return new div
            {
                new style
                {
                    """
                    .loader-designer-react-with-dot-net {
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

                new div
                {
                    className = "loader-designer-react-with-dot-net", style =
                    {
                        SizeFull,
                        BorderTop(1, solid, Color),
                        BorderRadius("50%")
                    }
                }
            };
        }
    }

    sealed class IconMinus : PureComponent
    {
        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
            {
                new path { fill = "currentColor", d = "M12 8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h8c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
            };
        }
    }

    sealed class IconPlus : PureComponent
    {
        protected override Element render()
        {
            return new svg(ViewBox(0, 0, 16, 16), svg.Size(16), Color(Blue800))
            {
                new path { fill = "currentColor", d = "M12 8.667H8.667V12c0 .367-.3.667-.667.667A.669.669 0 0 1 7.333 12V8.667H4A.669.669 0 0 1 3.333 8c0-.367.3-.667.667-.667h3.333V4c0-.366.3-.667.667-.667.367 0 .667.3.667.667v3.333H12c.367 0 .667.3.667.667 0 .367-.3.667-.667.667Z" }
            };
        }
    }
}