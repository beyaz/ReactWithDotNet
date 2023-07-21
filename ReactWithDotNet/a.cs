namespace ReactWithDotNet;

public sealed class a : HtmlElement
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
    [ReactProp]
    public string download { get; set; }

    [ReactProp]
    public string href { get; set; }

    [ReactProp]
    public string target { get; set; }

    #region Modifiers
    /// <summary>
    ///     a.target = '_blank'
    /// </summary>
    public static HtmlElementModifier TargetBlank => Target("_blank");

    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => Modify<a>(element => element.href = href);

    /// <summary>
    ///     a.target = <paramref name="target" />
    /// </summary>
    public static HtmlElementModifier Target(string target) => Modify<a>(element => element.target = target);
    #endregion
}

partial class Mixin
{
    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => a.Href(href);

    /// <summary>
    ///     a.target = '_blank'
    /// </summary>
    public static HtmlElementModifier TargetBlank => a.TargetBlank;
}