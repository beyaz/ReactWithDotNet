
namespace ReactWithDotNet;

public class div : HtmlElement
{
    public override string Type => nameof(div);

    public div()
    {
    }

    public div(string innerText)
    {
        this.innerText = innerText;
    }

    public div(IEnumerable<Element> children)
    {
        this.children.AddRange(children);
    }

    public override string ToString() => this.ToHTML();
}