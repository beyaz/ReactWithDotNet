namespace ReactWithDotNet.WebSite.Components;

class MainPageContentDescription : ReactPureComponent
{
    protected override Element render()
    {
        return new FlexColumn(MaxWidth(400), AlignItemsCenter)
        {
            new div(FontSize(50), FontWeight700)
            {
                "Write ", CreateAttractiveText("react.js"), " application in ", CreateAttractiveText("c#"),
                " language"
            },


            Space(20),
            new div
            {
                LineHeight40, 
                FontSize18, 
                Color(Theme[Context].grey_700), 
                FontWeight400, 
                Text("MUI offers a comprehensive suite of UI tools to help you ship new features faster. Start with Material UI, our fully-loaded component library, or bring your own design system to our production-ready components.")
            },
            Space(40),
            
            new FlexRow(AlignItemsFlexStart, WidthMaximized)
            {
                new GetStartedButton()
            }
        };
    }

    span CreateAttractiveText(string text)
    {
        return new span
        {
            text = text,
            style =
            {
                webkitBackgroundClip = "text",

                webkitTextFillColor = "transparent",

                background = $"linear-gradient(to right, {Theme[Context].primary_main}, {Theme[Context].primary_700})"
            }
        };
    }
}