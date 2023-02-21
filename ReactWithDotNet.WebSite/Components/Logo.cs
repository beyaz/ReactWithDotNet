using ReactWithDotNet.Libraries.mui.material;
using ReactWithDotNet.Libraries.react_syntax_highlighter;

namespace ReactWithDotNet.WebSite.Components;

public class Logo : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexColumn(Width(140), AlignItemsCenter)
        {
            new FlexRow(Gap(25))
            {
                new img { Src(Asset("react.svg")), WidthHeight(30) },
                new img { Src(Asset("net_core_logo.svg")), WidthHeight(30) }
            },
            
            new small{"React with DotNet"}
        };
    }
}


class MainContentContainer : ReactComponent
{
    readonly IModifier[] modifiers;
    public MainContentContainer()
    { }

    public MainContentContainer(params IModifier[] modifiers)
    {
        this.modifiers = modifiers;
    }
    protected override Element render()
    {
        var element = new FlexRow
        {
            MaxWidth(1200),
            JustifyContentCenter,

            Children(children),

            MediaQueryOnMobile(MarginLeftRight("5%")),
            MediaQueryOnTablet(MarginLeftRight("10%")),
            MediaQueryOnDesktop(MarginLeftRight("5%")),
            Role(nameof(MainContentContainer))

        };

        element.Apply(modifiers);

        return element;
    }
}
class HeaderMenuItem : ReactPureComponent
{
    public string Text { get; set; }
    
    protected override Element render()
    {
        return new Tooltip
        {
            classes = { { "tooltip",new Style{Background("transparent"),  } } },
            title = new FlexColumn(BorderRadius(5),Border("1px solid #E0E3E7"), Width(400), BoxShadow("rgba(170, 180, 190, 0.3) 0px 4px 20px"))
            {
                new HeaderMenuItemTooltipRow
                {
                    SvgFileName = "doc.svg",
                    Title = "MUI X", 
                    Description = "class HeaderMenuItem : ReactPureComponent"
                },
                new HeaderMenuItemTooltipRow
                {
                    SvgFileName = "doc.svg",
                    Title       = "MUI X",
                    Description = "class HeaderMenuItem : ReactPureComponent"
                },
                new HeaderMenuItemTooltipRow
                {
                    SvgFileName = "doc.svg",
                    Title       = "MUI X",
                    Description = "class HeaderMenuItem : ReactPureComponent"
                }
            },
            children=
            {
                new div
                {
                    Text(Text),
                    Padding(10),
                    Hover(Background("#D5E5F5")),
                    BorderRadius(10),
                    FontSize14,
                    FontWeight700,
                   CursorDefault
                }
            }
        };

    }
}

public class HeaderMenuItemTooltipRow : ReactPureComponent
{
    public string SvgFileName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    protected override Element render()
    {
        return new a(PaddingTopBottom(20), PaddingLeft(20), PaddingRight(30),TextDecorationNone,Hover(Background("#D5E5F5")),CursorDefault)
        {
            Href("/doc"),
            Background("white"),
            new FlexRow(AlignItemsCenter, Gap(20))
            {
                new img { Src(Asset("react.svg")), WidthHeight(36) },
                new FlexColumn
                {
                    new div(FontWeight700, FontSize14,Color(Theme[Context].text_primary)){Title},
                    new div(FontWeight400,FontSize13,Color(Theme[Context].text_primary)){Description}
                }
            }
        };
    }
}

class HeaderMenuBar: ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            new a{ Href("/"), new Logo(), PaddingTopBottom(10), TextDecorationNone},
            new nav(DisplayFlex,AlignItemsCenter)
            {
                new HeaderMenuItem{Text = "What is ReactWithDotNet"},
                new HeaderMenuItem{Text = "Tutorial"},
                new HeaderMenuItem{Text = "Showcase"}
            }
            
        };
    }
}

class MainPageContentDescription : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexColumn(MaxWidth(400), AlignItemsCenter)
        {
            new div(FontSize(50), FontWeight700)
            {
                "Write ", CreateAttractiveText("react.js"), " application in ", CreateAttractiveText("c#"),
                " language"
            },


            Space(20),
            new div
            {
                LineHeight40, 
                FontSize18, 
                Color(Theme[Context].grey_700), 
                FontWeight400, 
                Text("MUI offers a comprehensive suite of UI tools to help you ship new features faster. Start with Material UI, our fully-loaded component library, or bring your own design system to our production-ready components.")
            },
            Space(40),
            
            new FlexRow(AlignItemsFlexStart, WidthMaximized)
            {
                new GetStartedButton()
            }
        };
    }

    span CreateAttractiveText(string text)
    {
        return new span
        {
            text = text,
            style =
            {
                webkitBackgroundClip = "text",

                webkitTextFillColor = "transparent",

                background = $"linear-gradient(to right, {Theme[Context].primary_main}, {Theme[Context].primary_700})"
            }
        };
    }
}



class CSharpCodePanel : ReactPureComponent
{
    public string Code { get; set; }
    
    protected override Element render()
    {
        return new div
        {
            new SyntaxHighlighter
            {
                language = "csharp",
                style    = SyntaxHighlighterStyle.vs,
                children =
                {
                    Code
                }
            }
        };
    }
}

class GetStartedButton : ReactPureComponent
{
    protected override Element render()
    {
        return new div(CursorDefault)
        {
            text = "Get Started",
            style =
            {
                backgroundImage = "linear-gradient(to right, #DA22FF 0%, #9733EE  51%, #DA22FF  100%)",
                margin          = "10px",
                padding         = "15px 45px",
                textAlign       = "center",
                textTransform   = "uppercase",
                transition      = "0.5s",
                backgroundSize  = "200% auto",
                color           = "white",
                boxShadow       = "0 0 20px #eee",
                borderRadius    = "10px",
                display         = "block",
                
                hover =
                {
                    backgroundPosition = "right center",
                    color = "#fff",
                    textDecoration = "none"
                }
            }
        };
    }
}