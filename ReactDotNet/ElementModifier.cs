using System;

namespace ReactDotNet;

public sealed class ElementModifier
{
    readonly Action<Element> modifier;

    public ElementModifier(Action<Element> modifier)
    {
        this.modifier = modifier;
    }

    public void Modify(Element element)
    {
        if (modifier != null)
        {
            modifier(element);
        }
    }

    public static implicit operator ElementModifier(Display display)
    {
        return new ElementModifier(element => element.style.display = display);

    }

    public static implicit operator ElementModifier(AlignItems alignItems)
    {
        return new ElementModifier(element => element.style.alignItems = alignItems);
    }

    public static implicit operator ElementModifier(FlexWrap flexWrap)
    {
        return new ElementModifier(element => element.style.flexWrap = flexWrap);
    }
    public static implicit operator ElementModifier(JustifyContent justifyContent)
    {
        return new ElementModifier(element => element.style.justifyContent = justifyContent);
    }
    



}