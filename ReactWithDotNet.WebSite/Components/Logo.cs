using ReactWithDotNet.Libraries.mui.material;

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
            new a{ Href("/"), new Logo(), PaddingTopBottom(5), TextDecorationNone},
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
        return new FlexColumn(Width(400))
        {
            
            
           new div(FontSize(50), FontWeight700)
           {
               "Write ",
               new span()
               {
                   Text("react.js"),
                   new Style{webkitBackgroundClip = "text", webkitTextFillColor ="transparent"} + Background($"linear-gradient(to right, {Theme[Context].primary_main}, {Theme[Context].primary_700})")
               },
               " application in ",
               new span(Background($"linear-gradient(to right, {Theme[Context].primary_main}, {Theme[Context].primary_700})"))
               {
                   Text("c#")
               },
               "language"
           },
           
           
           Space(10),
           new div{LineHeight25, Text("MUI offers a comprehensive suite of UI tools to help you ship new features faster. Start with Material UI, our fully-loaded component library, or bring your own design system to our production-ready components.") }

        };
    }
}

class MainPageContentSample: ReactPureComponent
{
    protected override Element render()
    {
        return new div(wh(400),Border("2px solid red"))
        {

        };
    }
}
