namespace ReactWithDotNet;

public interface IModifier
{
}

public sealed class StyleModifier : IModifier
{
    internal readonly Action<Style> modifyStyle;

    internal StyleModifier(Action<Style> modifyStyle)
    {
        this.modifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    public static StyleModifier operator +(StyleModifier a, StyleModifier b)
    {
        void modify(Style style)
        {
            a.modifyStyle(style);
            b.modifyStyle(style);
        }

        return new StyleModifier(modify);
    }
}

public sealed class ElementModifier : IModifier
{
    internal readonly Action<Element> modifyElement;

    internal ElementModifier(Action<Element> modifyElement)
    {
        this.modifyElement = modifyElement ?? throw new ArgumentNullException(nameof(modifyElement));
    }

    public static ElementModifier operator +(ElementModifier a, ElementModifier b)
    {
        void modify(Element element)
        {
            ModifyHelper.ProcessModifier(element, a);
            ModifyHelper.ProcessModifier(element, b);
        }

        return new ElementModifier(modify);
    }
}

public sealed class HtmlElementModifier : IModifier
{
    internal readonly Action<HtmlElement> modifyHtmlElement;

    HtmlElementModifier(Action<HtmlElement> modifyHtmlElement)
    {
        this.modifyHtmlElement = modifyHtmlElement ?? throw new ArgumentNullException(nameof(modifyHtmlElement));
    }

    internal static HtmlElementModifier Create(Action<HtmlElement> modifyAction)
    {
        return new HtmlElementModifier(modifyAction);
    }
}

public sealed class ComponentModifier : IModifier
{
    internal readonly Action<ReactStatefulComponent> modify;

    ComponentModifier(Action<ReactStatefulComponent> modifyComponent)
    {
        modify = modifyComponent ?? throw new ArgumentNullException(nameof(modifyComponent));
    }

    internal static ComponentModifier Create(Action<ReactStatefulComponent> modifyComponent)
    {
        return new ComponentModifier(modifyComponent);
    }
}

abstract class ReactPureComponentModifier : IModifier
{
    internal abstract void Modify(ReactPureComponent pureComponent);
}

sealed class ReactPureComponentModifier<TPureComponent> : ReactPureComponentModifier where TPureComponent : ReactPureComponent
{
    internal readonly Action<TPureComponent> modify;

    public ReactPureComponentModifier(Action<TPureComponent> modifyPureComponent)
    {
        modify = modifyPureComponent ?? throw new ArgumentNullException(nameof(modifyPureComponent));
    }

    internal override void Modify(ReactPureComponent pureComponent)
    {
        if (pureComponent == null)
        {
            return;
        }

        modify((TPureComponent)pureComponent);
    }
}

partial class Mixin
{
    public static IModifier CreateModifier<TPureComponent>(Action<TPureComponent> modifyAction) where TPureComponent : ReactPureComponent
    {
        return new ReactPureComponentModifier<TPureComponent>(modifyAction);
    }

    public static StyleModifier CreateModifier(Action<Style> modifyAction)
    {
        return new StyleModifier(modifyAction);
    }

    internal static HtmlElementModifier CreateModifier(Action<HtmlElement> modifyAction)
    {
        return HtmlElementModifier.Create(modifyAction);
    }

    public static ComponentModifier CreateModifier(Action<ReactStatefulComponent> modifyAction)
    {
        return ComponentModifier.Create(modifyAction);
    }
}

static class ModifyHelper
{
    public static void ProcessModifier(Element element, IModifier modifier)
    {
        if (modifier == null || element is null)
        {
            return;
        }

        if (element is ThirdPartyReactComponent thirdPartyReactComponent)
        {
            if (modifier is StyleModifier styleModifier)
            {
                styleModifier.modifyStyle(thirdPartyReactComponent.style);
                return;
            }

            if (modifier is ElementModifier elementModifier)
            {
                elementModifier.modifyElement(thirdPartyReactComponent);
                return;
            }
        }

        if (element is HtmlElement htmlElement)
        {
            if (modifier is StyleModifier styleModifier)
            {
                styleModifier.modifyStyle(htmlElement.style);
                return;
            }

            if (modifier is HtmlElementModifier htmlElementModifier)
            {
                htmlElementModifier.modifyHtmlElement(htmlElement);
                return;
            }
            
            if (modifier is ElementModifier elementModifier)
            {
                elementModifier.modifyElement(htmlElement);
                return;
            }
        }

        if (element is ReactPureComponent reactPureComponent)
        {
            (reactPureComponent.modifiers ??= new List<IModifier>()).Add(modifier);
            return;
        }
        
        if (element is Fragment fragment)
        {
            fragment.modifiers ??= new List<IModifier>();

            fragment.modifiers.Add(modifier);
            return;
        }

        if (element is FakeChild)
        {
            throw new DeveloperException("Fake child cannot modify. Because fake child is in client.");
        }

        if (element is ReactStatefulComponent reactStatefulComponent)
        {
            if (modifier is ComponentModifier componentModifier)
            {
                componentModifier.modify(reactStatefulComponent);
                return;
            }

            reactStatefulComponent.modifiers ??= new List<IModifier>();

            reactStatefulComponent.modifiers.Add(modifier);

            return;
        }

        throw new DeveloperException("Modifier is not suitable for element. Element is " + element.GetType().FullName);
    }
}