namespace ReactWithDotNet.WebSite.Components;

class MainPageContentDescription : PureComponent
{
    protected override Element render()
    {
        return new FlexColumn(AlignItemsCenter)
        {
            SpaceY(60),
            new div(FontFamily_PlusJakartaSans_ExtraBold, FontSize(56), FontWeight800, WhenMediaSizeLessThan(MD,TextAlignCenter))
            {
                LineHeight(62),
                
                new HighlightedText{Text = "Write [react.js]  application in [c#]  language"}
            },

            SpaceY(20),
            new div
            {
                LineHeight30,
                Color(Theme.grey_700),
                FontWeight400,
                Text("MUI offers a comprehensive suite of UI tools to help you ship new features faster. Start with Material UI, our fully-loaded component library, or bring your own design system to our production-ready components.")
            },
            SpaceY(40),

            new FlexRow(JustifyContentFlexStart, WidthFull, FlexWrap, Gap(32))
            {
                new GetStartedButton{ Text = "Documentation", Href = Page.Doc.Url } + WidthFull+ SM(Width(auto)),
                new GetStartedButton{ Text = "Showcase", Href      = Page.Doc.Url } + WidthFull + SM(Width(auto))
            }
        };
    }

   

    
}