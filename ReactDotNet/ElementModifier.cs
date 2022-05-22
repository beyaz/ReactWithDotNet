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

    public static implicit operator ElementModifier(Style style)
    {
        return new ElementModifier(element => element.style.Import(style));

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

    public static implicit operator ElementModifier(FlexDirection flexDirection)
    {
        return new ElementModifier(element => element.style.flexDirection = flexDirection);
    }

    public static implicit operator ElementModifier(JustifyContent justifyContent)
    {
        return new ElementModifier(element => element.style.justifyContent = justifyContent);
    }

    public static implicit operator ElementModifier(WhiteSpace whiteSpace)
    {
        return new ElementModifier(element => element.style.whiteSpace= whiteSpace);
    }

    public static implicit operator ElementModifier(AlignContent alignContent)
    {
        return new ElementModifier(element => element.style.alignContent = alignContent);
    }

    public static implicit operator ElementModifier(TextAlign textAlign)
    {
        return new ElementModifier(element => element.style.textAlign = textAlign);
    }

    public static ElementModifier operator +(ElementModifier a, ElementModifier b)
    {
        return new ElementModifier(element =>
        {
            a.Modify(element);
            b.Modify(element);
        });
    }


    public static ElementModifier operator |(ElementModifier a, ElementModifier b)
    {
        return new ElementModifier(element =>
        {
            a.Modify(element);
            b.Modify(element);
        });
    }

}