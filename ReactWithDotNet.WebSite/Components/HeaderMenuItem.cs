using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.Components;

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
                    Title       = "MUI X", 
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
            children =
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