using InvalidCastException = System.InvalidCastException;

namespace ReactWithDotNet;

/// <summary>
///     Designed for modify an element by functional way <br/>
///     Example: <br/>
///     <code>
///     -- normal aproach 
///     var element = new div();
///     element.style.width = "50px";
/// 
///     -- better approach
///     new div { Width(50) } 
///     </code>
///     <br/>
///     Inherited types : <br/>
///     <see cref="StyleModifier"/> <br/>
///     <see cref="ElementModifier"/>
/// </summary>
public abstract class Modifier;

public sealed class StyleModifier : Modifier
{
    internal readonly Action<Style> ModifyStyle;

    internal StyleModifier(Action<Style> modifyStyle)
    {
        ModifyStyle = modifyStyle ?? throw new ArgumentNullException(nameof(modifyStyle));
    }

    public static StyleModifier operator +(StyleModifier a, StyleModifier b)
    {
        return new Style(a, b);
    }

    public static implicit operator StyleModifier(Style style)
    {
        return new(x => x.Import(style));
    }
    
    
    public static Element operator +(StyleModifier styleModifier, Element element)
    {
        ModifyHelper.ProcessModifier(element, styleModifier);

        return element;
    }
}

public sealed class ElementModifier : Modifier
{
    internal readonly bool IsModifyReactKey;

    internal readonly Action<Element> ModifyElement;

    internal ElementModifier(Action<Element> modifyElement) : this(modifyElement, false)
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

        return new(modify);
    }
}

public abstract class HtmlElementModifier : Modifier
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

        THtmlElement element;

        try
        {
            element = (THtmlElement)htmlElement;
        }
        catch (InvalidCastException)
        {
            throw DeveloperException(string.Join(Environment.NewLine, [
                $"InvalidCast: {htmlElement.GetType().Name} -> {typeof(THtmlElement).Name}",
                $"Location:{ModifyHtmlElement.Method}"
            ]));
        }
        
        ModifyHtmlElement(element);
    }
}

abstract class ReactComponentModifier : Modifier
{
    internal abstract void Modify(ReactComponentBase reactComponent);
}

sealed class ReactComponentModifier<TComponent> : ReactComponentModifier where TComponent : Component
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

abstract class ReactPureComponentModifier : Modifier
{
    internal abstract void Modify(PureComponent pureComponent);
}

sealed class ReactPureComponentModifier<TPureComponent> : ReactPureComponentModifier where TPureComponent : PureComponent
{
    internal readonly Action<TPureComponent> modify;

    public ReactPureComponentModifier(Action<TPureComponent> modifyPureComponent)
    {
        modify = modifyPureComponent ?? throw new ArgumentNullException(nameof(modifyPureComponent));
    }

    internal override void Modify(PureComponent pureComponent)
    {
        if (pureComponent == null)
        {
            return;
        }

        modify((TPureComponent)pureComponent);
    }
}

abstract class ThirdPartyReactComponentModifier : Modifier
{
    internal abstract void Modify(ThirdPartyReactComponent thirdPartyReactComponent);
}

sealed class ThirdPartyReactComponentModifier<TComponent> : ThirdPartyReactComponentModifier where TComponent : ThirdPartyReactComponent
{
    internal readonly Action<TComponent> modify;

    public ThirdPartyReactComponentModifier(Action<TComponent> modifyPureComponent)
    {
        modify = modifyPureComponent ?? throw new ArgumentNullException(nameof(modifyPureComponent));
    }

    internal override void Modify(ThirdPartyReactComponent thirdPartyReactComponent)
    {
        if (thirdPartyReactComponent == null)
        {
            return;
        }

        modify((TComponent)thirdPartyReactComponent);
    }
}

partial class Mixin
{
    public static void Apply<TElement>(this TElement[] elements, StyleModifier[] styleModifiers) where TElement : Element
    {
        if (elements is null)
        {
            return;
        }

        foreach (var element in elements)
        {
            if (element is null)
            {
                continue;
            }

            element.Apply(styleModifiers);
        }
    }

    public static Element Apply(this Element element, StyleModifier[] styleModifiers)
    {
        if (styleModifiers is not null)
        {
            foreach (var styleModifier in styleModifiers)
            {
                ModifyHelper.ProcessModifier(element, styleModifier);
            }
        }

        return element;
    }

    public static Modifier CreateComponentModifier<TComponent>(Action<TComponent> modifyAction) where TComponent : Component
    {
        return new ReactComponentModifier<TComponent>(modifyAction);
    }

    public static HtmlElementModifier CreateHtmlElementModifier<THtmlElement>(Action<THtmlElement> modifyAction) where THtmlElement : HtmlElement
    {
        return new HtmlElementModifier<THtmlElement> { ModifyHtmlElement = modifyAction };
    }

    public static Modifier CreatePureComponentModifier<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : PureComponent
    {
        return new ReactPureComponentModifier<TPureComponent>(modifyAction);
    }

    public static StyleModifier CreateStyleModifier(Action<Style> modifyAction)
    {
        return new(modifyAction);
    }

    public static Modifier CreateThirdPartyReactComponentModifier<TComponent>(Action<TComponent> modifyAction)
        where TComponent : ThirdPartyReactComponent
    {
        return new ThirdPartyReactComponentModifier<TComponent>(modifyAction);
    }
}

static class ModifyHelper
{
    public static void ProcessModifier(Element element, Modifier modifier)
    {
        if (modifier == null || element is null)
        {
            return;
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

            if (modifier is Style style)
            {
                htmlElement.style.Import(style);
                return;
            }
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

            if (modifier is ThirdPartyReactComponentModifier thirdPartyReactComponentModifier)
            {
                thirdPartyReactComponentModifier.Modify(thirdPartyReactComponent);
                return;
            }

            if (modifier is Style style)
            {
                thirdPartyReactComponent.style.Import(style);
                return;
            }
        }

        if (element is PureComponent reactPureComponent)
        {
            (reactPureComponent.Modifiers ??= []).Add(modifier);
            return;
        }

        if (element is Fragment fragment)
        {
            fragment.Modifiers ??= [];

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

            reactStatefulComponent.Modifiers ??= [];

            reactStatefulComponent.Modifiers.Add(modifier);

            return;
        }
        
        if (element is ElementAsTask elementAsTask)
        {
            (elementAsTask.Modifiers ??= []).Add(modifier);
            
            return;
        }
        
        throw new DeveloperException("Modifier is not suitable for element. Element is " + element.GetType().FullName);
    }
}