namespace ReactWithDotNet;

public sealed class img : HtmlElement
{
    public img()
    {
    }

    public img(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public string alt { get; set; }

    [ReactProp]
    public string height { get; set; }

    [ReactProp]
    public string loading { get; set; }

    [ReactProp]
    public string src { get; set; }

    [ReactProp]
    public string width { get; set; }

    public static HtmlElementModifier Alt(string alt) => CreateHtmlElementModifier<img>(element => element.alt = alt);

    public static HtmlElementModifier Src(string src) => CreateHtmlElementModifier<img>(element => element.src = src);
}

partial class Mixin
{
    /// <summary>
    ///     img.alt = <paramref name="alt" />
    /// </summary>
    public static HtmlElementModifier Alt(string alt) => CreateHtmlElementModifier<img>(element => element.alt = alt);

    /// <summary>
    ///     img.src = <paramref name="src" />
    /// </summary>
    public static HtmlElementModifier Src(string src) => CreateHtmlElementModifier<img>(element => element.src = src);
}