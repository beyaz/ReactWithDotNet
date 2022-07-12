using System;
using ReactDotNet.Html5;

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
    

   

    public static ElementModifier operator +(ElementModifier a, ElementModifier b)
    {
        return new ElementModifier(element =>
        {
            a.Modify(element);
            b.Modify(element);
        });
    }




}