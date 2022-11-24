namespace ReactWithDotNet;

public interface IModifier
{
    internal void Modify(Style style);
    internal void Modify(Element htmlElement);
}

public sealed class StyleModifier : IModifier
{
    readonly Action<Style> modifyStyle;

    public StyleModifier(Action<Style> modifyStyle)
    {
        this.modifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    void IModifier.Modify(Element instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance is HtmlElement htmlElement)
        {
            modifyStyle(htmlElement.style);
            return;
        }

        if (instance is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            modifyStyle(thirdPartyReactComponent.style);
            return;
        }

        throw new ArgumentException($"Style modifier cannot operate on {instance.GetType().FullName}");
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

public sealed class ElementModifier : IModifier
{
    readonly Action<Element> modifyHtmlElement;

    public ElementModifier(Action<Element> modifyHtmlElement)
    {
        this.modifyHtmlElement = modifyHtmlElement ?? throw new ArgumentNullException(nameof(modifyHtmlElement));
    }

    void IModifier.Modify(Element htmlElement)
    {
        Modify(htmlElement);
    }

    internal void Modify(Element htmlElement)
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

    public static ElementModifier operator |(ElementModifier a, ElementModifier b)
    {
        void modify(Element htmlElement)
        {
            ((IModifier)a).Modify(htmlElement);
            ((IModifier)b).Modify(htmlElement);
        }

        return new ElementModifier(modify);
    }
}