namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class MainPageFooter : PureComponent
{
    protected override Element render()
    {
        return new footer(BorderTop(1, solid, Gray100), Height(50), WidthFull, Background("white"))
        {
            DisplayFlexRowCentered,
            new HighlightedText
            {
                Text = "React [\u2665] .Net"
            }
        };
    }
}