namespace ReactWithDotNet;

public class a : HtmlElement
{
    public a()
    {
    }

    public a(params IModifier[] modifiers) : base(modifiers)
    {
    }

    /// <summary>
    ///     Download file when clicking on the link (instead of navigating to the file):
    /// </summary>
    [React]
    public string download { get; set; }

    [React]
    public string href { get; set; }

    [React]
    public string target { get; set; }

    #region Modifiers
    /// <summary>
    ///     a.target = '_blank'
    /// </summary>
    public static HtmlElementModifier TargetBlank => Target("_blank");

    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => CreateHtmlElementModifier<a>(element => element.href = href);

    /// <summary>
    ///     a.target = <paramref name="target" />
    /// </summary>
    public static HtmlElementModifier Target(string target) => CreateHtmlElementModifier<a>(element => element.target = target);
    #endregion
}

partial class Mixin
{
    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => a.Href(href);
}