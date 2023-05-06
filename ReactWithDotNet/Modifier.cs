namespace ReactWithDotNet;

public interface IModifier
{
}

public sealed class StyleModifier : IModifier
{
    internal readonly Action<Style> ModifyStyle;

    internal StyleModifier(Action<Style> modifyStyle)
    {
        this.ModifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    public static StyleModifier operator +(StyleModifier a, StyleModifier b)
    {
        void modify(Style style)
        {
            a.ModifyStyle(style);
            b.ModifyStyle(style);
        }

        return new StyleModifier(modify);
    }
}

public sealed class ElementModifier : IModifier
{
    internal readonly bool IsModifyReactKey;
    
    internal readonly Action<Element> ModifyElement;

    internal ElementModifier(Action<Element> modifyElement):this(modifyElement, isModifyReactKey: false)
    {
    }

    internal ElementModifier(Action<Element> modifyElement, bool isModifyReactKey)
    {
        ModifyElement    = modifyElement ?? throw new ArgumentNullException(nameof(modifyElement));
        IsModifyReactKey = isModifyReactKey;
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

public abstract class HtmlElementModifier : IModifier
{
    internal abstract void Process(HtmlElement htmlElement);
}

class HtmlElementModifier<THtmlElement> : HtmlElementModifier where THtmlElement : HtmlElement
{
    internal Action<THtmlElement> ModifyHtmlElement;

    internal override void Process(HtmlElement htmlElement)
    {
        if (htmlElement is null)
        {
            return;
        }

        ModifyHtmlElement((THtmlElement)htmlElement);
    }
}

abstract class ReactComponentModifier : IModifier
{
    internal abstract void Modify(ReactComponentBase reactComponent);
}

sealed class ReactComponentModifier<TComponent> : ReactComponentModifier where TComponent : ReactComponent
{
    internal readonly Action<TComponent> modify;

    public ReactComponentModifier(Action<TComponent> modifyComponent)
    {
        modify = modifyComponent ?? throw new ArgumentNullException(nameof(modifyComponent));
    }

    internal override void Modify(ReactComponentBase pureComponent)
    {
        if (pureComponent == null)
        {
            return;
        }

        modify((TComponent)pureComponent);
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
    public static IModifier CreateComponentModifier<TComponent>(Action<TComponent> modifyAction) where TComponent : ReactComponent
    {
        return new ReactComponentModifier<TComponent>(modifyAction);
    }

    public static HtmlElementModifier CreateHtmlElementModifier<THtmlElement>(Action<THtmlElement> modifyAction) where THtmlElement : HtmlElement
    {
        return new HtmlElementModifier<THtmlElement> { ModifyHtmlElement = modifyAction };
    }

    public static IModifier CreatePureComponentModifier<TPureComponent>(Action<TPureComponent> modifyAction) 
        where TPureComponent : ReactPureComponent
    {
        return new ReactPureComponentModifier<TPureComponent>(modifyAction);
    }

    public static StyleModifier CreateStyleModifier(Action<Style> modifyAction)
    {
        return new StyleModifier(modifyAction);
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
                styleModifier.ModifyStyle(thirdPartyReactComponent.style);
                return;
            }

            if (modifier is ElementModifier elementModifier)
            {
                elementModifier.ModifyElement(thirdPartyReactComponent);
                return;
            }
        }

        if (element is HtmlElement htmlElement)
        {
            if (modifier is StyleModifier styleModifier)
            {
                styleModifier.ModifyStyle(htmlElement.style);
                return;
            }

            if (modifier is HtmlElementModifier htmlElementModifier)
            {
                htmlElementModifier.Process(htmlElement);
                return;
            }

            if (modifier is ElementModifier elementModifier)
            {
                elementModifier.ModifyElement(htmlElement);
                return;
            }
        }

        if (element is ReactPureComponent reactPureComponent)
        {
            (reactPureComponent.Modifiers ??= new List<IModifier>()).Add(modifier);
            return;
        }

        if (element is Fragment fragment)
        {
            fragment.Modifiers ??= new List<IModifier>();

            fragment.Modifiers.Add(modifier);
            return;
        }

        if (element is FakeChild)
        {
            throw new DeveloperException("Fake child cannot modify. Because fake child is in client.");
        }

        if (element is ReactComponentBase reactStatefulComponent)
        {
            if (modifier is ReactComponentModifier componentModifier)
            {
                componentModifier.Modify(reactStatefulComponent);
                return;
            }

            reactStatefulComponent.Modifiers ??= new List<IModifier>();

            reactStatefulComponent.Modifiers.Add(modifier);

            return;
        }

        throw new DeveloperException("Modifier is not suitable for element. Element is " + element.GetType().FullName);
    }
}