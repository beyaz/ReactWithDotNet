namespace ReactWithDotNet;

public interface IModifier
{
    internal void Modify(object instance);
}

public sealed class StyleModifier : IModifier
{
    readonly Action<Style> modifyStyle;

    public StyleModifier(Action<Style> modifyStyle)
    {
        this.modifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    public static StyleModifier operator +(StyleModifier a, StyleModifier b)
    {
        void modify(Style style)
        {
            ((IModifier)a).Modify(style);
            ((IModifier)b).Modify(style);
        }

        return new StyleModifier(modify);
    }

    void IModifier.Modify(object instance)
    {
        if (instance == null)
        {
            throw new ArgumentNullException(nameof(instance));
        }

        if (instance is Style style)
        {
            modifyStyle(style);
            return;
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

    internal void Modify(Style style)
    {
        if (style == null)
        {
            throw new ArgumentNullException(nameof(style));
        }

        modifyStyle(style);
    }
}

public sealed class ElementModifier : IModifier
{
    readonly Action<Element> modifyElement;

    public ElementModifier(Action<Element> modifyElement)
    {
        this.modifyElement = modifyElement ?? throw new ArgumentNullException(nameof(modifyElement));
    }

    public static ElementModifier operator +(ElementModifier a, ElementModifier b)
    {
        void modify(Element htmlElement)
        {
            ((IModifier)a).Modify(htmlElement);
            ((IModifier)b).Modify(htmlElement);
        }

        return new ElementModifier(modify);
    }

    void IModifier.Modify(object instance)
    {
        if (instance is Element element)
        {
            Modify(element);
            return;
        }

        throw new ArgumentException($"ElementModifier cannot operate on {instance.GetType().FullName}");
    }

    internal void Modify(Element element)
    {
        if (element == null)
        {
            throw new ArgumentNullException(nameof(element));
        }

        modifyElement(element);
    }
}

public sealed class ComponentModifier : IModifier
{
    readonly Action<ReactStatefulComponent> modify;

    public ComponentModifier(Action<ReactStatefulComponent> modifyComponent)
    {
        modify = modifyComponent ?? throw new ArgumentNullException(nameof(modifyComponent));
    }

    void IModifier.Modify(object instance)
    {
        if (instance is ReactStatefulComponent reactComponent)
        {
            Modify(reactComponent);
        }

        throw new ArgumentException($"ComponentModifier cannot operate on {instance.GetType().FullName}");
    }

    internal void Modify(ReactStatefulComponent reactComponent)
    {
        if (reactComponent == null)
        {
            throw new ArgumentNullException(nameof(reactComponent));
        }

        modify(reactComponent);
    }
}