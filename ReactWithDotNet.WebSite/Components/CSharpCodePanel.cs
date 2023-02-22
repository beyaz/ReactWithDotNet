using ReactWithDotNet.Libraries.react_free_scrollbar;
using ReactWithDotNet.Libraries.react_syntax_highlighter;

namespace ReactWithDotNet.WebSite.Components;

class CSharpCodePanel : ReactPureComponent
{
    public string Code { get; set; }
    
    protected override Element render()
    {
        return new FreeScrollBar
        {
            WidthMaximized,
            Height(400),
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