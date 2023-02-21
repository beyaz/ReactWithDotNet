using ReactWithDotNet.Libraries.react_syntax_highlighter;

namespace ReactWithDotNet.WebSite.Components;

class DemoPanel : ReactPureComponent
{
    public string CSharpCode { get; set; }

    public Element Element { get; set; }

    protected override Element render()
    {
        return new FlexRow
        {
            new CSharpCodePanel{ Code = CSharpCode},
            new FlexRowCentered{Element}
        };
    }
}