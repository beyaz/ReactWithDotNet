namespace QuranAnalyzer.WebUI.Components;

class MainContentContainer : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexRow
        {
            JustifyContentCenter,

            Children(children),

            WidthMaximized,
            Height("100%"),

            MediaQueryOnMobile(MarginLeftRight("5%")),
            MediaQueryOnTablet(MarginLeftRight("10%")),
            MediaQueryOnDesktop(MarginLeftRight("15%"))
        };
    }
}