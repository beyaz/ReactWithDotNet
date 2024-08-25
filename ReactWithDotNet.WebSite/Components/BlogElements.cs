namespace ReactWithDotNet.WebSite.Components;

sealed class BlogH1 : PureComponent
{
    protected override Element render()
    {
        return new h1
        {
            children,
            FontFamily("'General Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'"),
            FontSize36,
            LineHeight(44),
            LetterSpacing(-0.2),
            FontWeight600,
            Color(rgb(15, 18, 20)),
        };
    }
}

sealed class BlogP : PureComponent
{
    protected override Element render()
    {
        return new p
        {
            children,
            FontSize14
        };
    }
}