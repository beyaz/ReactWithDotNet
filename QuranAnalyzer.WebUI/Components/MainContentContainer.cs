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
            

            MediaQuery("(min-width: 600px)", new Style
            {
                MarginLeftRight("5%")
            }),
            
            MediaQuery("(min-width: 1200px)", new Style
            {
                MarginLeftRight("10%")
            })
        };
    }
}