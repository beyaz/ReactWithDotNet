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
            classes = { { "tooltip",new Style{Background("transparent"), Width(500) } } },
            title = new FlexColumn(Width(200), BorderRadius(5),Border("1px solid #E0E3E7"), BoxShadow("rgba(170, 180, 190, 0.3) 0px 4px 20px"))
            {
                new div(wh(100),Border("1px solid yellow"),BorderRadius(5)),
                new div(wh(100),Border("1px solid green")),
                new div(wh(100),Border("1px solid blue"),Hover(Background("black")))
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
                    CursorPointer
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