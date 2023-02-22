namespace ReactWithDotNet.WebSite.HeaderComponents;

public class HeaderMenuItemTooltipRow : ReactPureComponent
{
    public string SvgFileName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    protected override Element render()
    {
        return new a(PaddingTopBottom(20), PaddingLeft(20), PaddingRight(30), TextDecorationNone, Hover(Background("#D5E5F5")), CursorDefault)
        {
            Href("/?p=HelperApps"),
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