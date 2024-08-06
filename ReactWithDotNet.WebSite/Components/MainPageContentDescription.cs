namespace ReactWithDotNet.WebSite.Components;

class MainPageContentDescription : PureComponent
{
    protected override Element render()
    {
        return new FlexColumn(AlignItemsCenter)
        {
            SpaceY(32),
            new div(FontFamily_PlusJakartaSans_ExtraBold, FontSize(56), FontWeight800, WhenMediaSizeLessThan(MD, TextAlignCenter))
            {
                LineHeight(62),

                new HighlightedText { Text = "Write [react.js]  application in [c#]  language" }
            },

            SpaceY(20),
            new div
            {
                LineHeight30,
                Color(Gray700),
                FontWeight400,
                Text("ReactWithDotNet is a new way to build web applications. Build component tree in server side on .Net Core (c#) then sends component tree to react client side. When any component has any action then React client communicates serverside only required parts of application. Does not hold any state at .Net Core server side. Thousend of users can be handle at sime time. In summary, combines power of c#, .net and react.js")
            },
            SpaceY(40),

            new FlexRow(JustifyContentFlexStart, WidthFull, FlexWrap, Gap(32))
            {
                new PrimaryLinkButton { Text = "Documentation", Href = Page.Doc.Url } + WidthFull + SM(Width(auto)),
                new PrimaryLinkButton { Text = "Showcase", Href      = Page.Doc.Url } + WidthFull + SM(Width(auto))
            }
        };
    }

    sealed class PrimaryLinkButton : PureComponent
    {
        public string Href { get; init; }
        public string Text { get; init; }

        protected override Element render()
        {
            return new a(Href(Href), PrimaryButtonStyle)
            {
                text = Text
            };
        }
    }
}