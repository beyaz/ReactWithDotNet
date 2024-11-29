namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class MainPageFooter : PureComponent
{
    protected override Element render()
    {
        return new footer
        {
            PaddingY(8),
            DisplayFlexRowCentered,
            WidthFull,
            BorderTop(1, solid, Gray100), 
            Background(White),
            
            "React ", new GradientText { "♥", FontSize20 }, " .Net"
        };
    }
}