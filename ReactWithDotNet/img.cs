namespace ReactWithDotNet;

public class img : HtmlElement
{
    public img()
    {
    }

    public img(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string alt { get; set; }

    [React]
    public int height { get; set; }

    [React]
    public string loading { get; set; }

    [React]
    public string src { get; set; }

    [React]
    public int width { get; set; }

    public static HtmlElementModifier Alt(string alt) => HtmlElementModifier.Create(el => ((img)el).alt = alt);

    public static HtmlElementModifier Src(string src) => HtmlElementModifier.Create(el => ((img)el).src = src);
}

partial class Mixin
{
    /// <summary>
    ///     img.alt = <paramref name="alt" />
    /// </summary>
    public static HtmlElementModifier Alt(string alt) => img.Alt(alt);

    /// <summary>
    ///     img.src = <paramref name="src" />
    /// </summary>
    public static HtmlElementModifier Src(string src) => img.Src(src);
}