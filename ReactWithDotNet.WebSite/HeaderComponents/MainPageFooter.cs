namespace ReactWithDotNet.WebSite.HeaderComponents;

sealed class MainPageFooter : PureComponent
{
    protected override Element render()
    {
        return new footer
        {
            DisplayFlexRowCentered,
            WidthFull,Height(50),
            BorderTop(1, solid, Gray100), 
            Background(White),
            
            "React ", new GradientText { "♥", FontSize20 }, " .Net"
        };
    }
}