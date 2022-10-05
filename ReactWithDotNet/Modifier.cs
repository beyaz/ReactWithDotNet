namespace ReactWithDotNet;

public interface IModifier
{
    internal void Modify(Style style);
    internal void Modify(HtmlElement htmlElement);
}

public sealed class StyleModifier : IModifier
{
    readonly Action<Style> modifyStyle;

    public StyleModifier(Action<Style> modifyStyle)
    {
        this.modifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    void IModifier.Modify(HtmlElement instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        modifyStyle(instance.style);
    }

    void IModifier.Modify(Style style)
    {
        Modify(style);
    }

    internal void Modify(Style style)
    {
        if (style == null)
        {
            throw new ArgumentNullException(nameof(style));
        }

        modifyStyle(style);
    }

    public static StyleModifier operator |(StyleModifier a, StyleModifier b)
    {
        void modify(Style style)
        {
            ((IModifier)a).Modify(style);
            ((IModifier)b).Modify(style);
        }

        return new StyleModifier(modify);
    }
}

public sealed class HtmlElementModifier : IModifier
{
    readonly Action<HtmlElement> modifyHtmlElement;

    public HtmlElementModifier(Action<HtmlElement> modifyHtmlElement)
    {
        this.modifyHtmlElement = modifyHtmlElement ?? throw new ArgumentNullException(nameof(modifyHtmlElement));
    }

    void IModifier.Modify(HtmlElement htmlElement)
    {
        Modify(htmlElement);
    }

    internal void Modify(HtmlElement htmlElement)
    {
        if (htmlElement == null)
        {
            throw new ArgumentNullException(nameof(htmlElement));
        }

        modifyHtmlElement(htmlElement);
    }

    void IModifier.Modify(Style style)
    {
        throw new InvalidOperationException("HtmlElementModifier cannot be use for style");
    }

    //public static implicit operator HtmlElementModifier(string text)
    //{
    //    return Mixin.Text(text);
    //}

    public static HtmlElementModifier operator |(HtmlElementModifier a, HtmlElementModifier b)
    {
        void modify(HtmlElement htmlElement)
        {
            ((IModifier)a).Modify(htmlElement);
            ((IModifier)b).Modify(htmlElement);
        }

        return new HtmlElementModifier(modify);
    }
}