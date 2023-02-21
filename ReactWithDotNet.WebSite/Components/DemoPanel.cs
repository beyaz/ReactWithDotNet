using ReactWithDotNet.Libraries.react_syntax_highlighter;

namespace ReactWithDotNet.WebSite.Components;

class DemoPanel : ReactPureComponent
{
    public string CSharpCode { get; set; }

    public Element Element { get; set; }

    protected override Element render()
    {
        return new div(BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(15), BorderRadius(5), MarginTopBottom(10))
        {
            new SyntaxHighlighter
            {
                language = "csharp",
                style    = SyntaxHighlighterStyle.vs,
                children =
                {
                    Code
                }
            }
        };
    }
}