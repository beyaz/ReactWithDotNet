namespace QuranAnalyzer.WebUI.Components;

class MainContentContainer : ReactComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            JustifyContentCenter,
            
            Children(children),

            PaddingLeftRight(5),
            Width("100%"),
            Height("100%"),

            MediaQueryOnSmartphone(MarginLeftRight("5%")),
            MediaQueryOnTablet(MarginLeftRight("10%")),
            MediaQueryOnDesktop(MarginLeftRight("15%")),
            
           
        };
    }
}