namespace ReactWithDotNet;

public class a : HtmlElement
{
    public a()
    {
    }

    public a(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string href { get; set; }

    [React]
    public string target { get; set; }

    /// <summary>
    /// Download file when clicking on the link (instead of navigating to the file):
    /// </summary>
    [React]
    public string download { get; set; }

    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => CreateHtmlElementModifier<a>(element => element.href = href);

}

partial class Mixin
{
    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => a.Href(href);
}