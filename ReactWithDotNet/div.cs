namespace ReactWithDotNet;

public sealed class div : HtmlElement
{
    [React]
    public string role { get; set; }

   
    

    public div()
    {
    }

    public div(Style style)
    {
        style.Import(style);
    }

    public div(string innerText)
    {
        this.innerText = innerText;
    }
}