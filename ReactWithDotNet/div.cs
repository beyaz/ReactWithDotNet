namespace ReactWithDotNet;

public sealed class div : HtmlElement
{
    public div()
    {
    }

    public div(params IModifier[] modifiers) : base(modifiers)
    {
    }

 

    [React]
    public string role { get; set; }
}