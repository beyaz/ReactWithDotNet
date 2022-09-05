namespace ReactWithDotNet;

public sealed class div : HtmlElement
{
    public div()
    {
    }

    public div(string innerText)
    {
        this.innerText = innerText;
    }
}