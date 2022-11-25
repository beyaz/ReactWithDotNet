namespace ReactWithDotNet;

public interface IModifier
{
    internal void Modify(Style style);
    internal void Modify(Element element);
}

public sealed class StyleModifier : IModifier
{
    readonly Action<Style> modifyStyle;

    public StyleModifier(Action<Style> modifyStyle)
    {
        this.modifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    void IModifier.Modify(Element element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        if (element is HtmlElement htmlElement)
        {
            modifyStyle(htmlElement.style);
            return;
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            modifyStyle(thirdPartyReactComponent.style);
            return;
        }

        throw new ArgumentException($"Style modifier cannot operate on {element.GetType().FullName}");
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
    readonly Action<Element> modifyElement;

    public ElementModifier(Action<Element> modifyElement)
    {
        this.modifyElement = modifyElement ?? throw new ArgumentNullException(nameof(modifyElement));
    }

    void IModifier.Modify(Element element)
    {
        Modify(element);
    }

    internal void Modify(Element element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        modifyElement(element);
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

public sealed class ComponentModifier : IModifier
{

    public void Modify(object instance)
    {
        if (instance is ReactComponent reactComponent)
        {
            modifyElement(reactComponent);
        }

        throw new DeveloperException($"Expected react component but found {instance.GetType().FullName}");
    }
    
    readonly Action<ReactComponent> modifyElement;

    public ComponentModifier(Action<ReactComponent> modifyElement)
    {
        this.modifyElement = modifyElement ?? throw new ArgumentNullException(nameof(modifyElement));
    }

    void IModifier.Modify(Element element)
    {
        Modify(element);
    }

    internal void Modify(Element element)
    {
        
    }

    void IModifier.Modify(Style style)
    {
        throw new InvalidOperationException("HtmlElementModifier cannot be use for style");
    }

    
}