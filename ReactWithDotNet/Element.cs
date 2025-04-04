﻿using System.Collections;
using System.Text;

namespace ReactWithDotNet;

/// <summary>
///     The element
/// </summary>
public abstract class Element : IEnumerable<Element>, IEnumerable<Modifier>
{
    internal ElementCollection _children;

    /// <summary>
    ///     The children
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public ElementCollection children
    {
        get
        {
            _children ??= new ();

            return _children;
        }
    }
    
    public bool HasChildren=>_children == null || _children.Count == 0;

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    internal string key;

    public static Element operator +(Element element, StyleModifier styleModifier)
    {
        ModifyHelper.ProcessModifier(element, styleModifier);

        return element;
    }

    public static Element operator +(Element element, StyleModifier[] styleModifiers)
    {
        element.Apply(styleModifiers);

        return element;
    }
    
    public static Element operator +(Element parent, Element child)
    {
        parent.Add(child);

        return parent;
    }

    public static Element operator +(Element element, Style style)
    {
        ModifyHelper.ProcessModifier(element, CreateStyleModifier(s => s?.Import(style)));

        return element;
    }

    public static Element operator +(Element element, Modifier modifier)
    {
        ModifyHelper.ProcessModifier(element, modifier);

        return element;
    }

    public static implicit operator Element(string text)
    {
        return new HtmlTextNode { text = text };
    }
    
    public static implicit operator Element(StringBuilder stringBuilder)
    {
        return new HtmlTextNode { stringBuilder = stringBuilder };
    }
    
    public static implicit operator Element(int number)
    {
        return new HtmlTextNode { text = number.ToString() };
    }
    public static implicit operator Element(int? number)
    {
        return new HtmlTextNode { text = number?.ToString() };
    }
    public static implicit operator Element(double? number)
    {
        return new HtmlTextNode { text = number?.ToString() };
    }
    
    public static implicit operator Element(Task<Element> task)
    {
        return new ElementAsTask(task);
    }
    
    /// <summary>
    ///     Adds the specified element.
    /// </summary>
    public void Add(Element element)
    {
        children.Add(element);
    }
    
    public void Add(params Modifier[] modifiers)
    {
        this.Apply(modifiers);
    }
    
    public void Add(StyleModifier[] modifiers)
    {
        this.Apply(modifiers);
    }

    public void Add<TElement>(IEnumerable<TElement> elements) where TElement:Element
    {
        if (elements is not null)
        {
            children.AddRange(elements);
        }
    }
    
    public void Add(Func<IEnumerable<Element>> elements)
    {
        if (elements is not null)
        {
            Add(elements());
        }
    }

    public void Add(Modifier modifier)
    {
        ModifyHelper.ProcessModifier(this, modifier);
    }
    
    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Element> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            Add((Element)null);
            return;
                
        }
        Add(elementCreatorFunc.Invoke());
    }
    
    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Task<Element>> elementCreatorFunc)
    {
        if (elementCreatorFunc == null)
        {
            Add((Element)null);
            return;
        }
        
        Add(elementCreatorFunc.Invoke());
    }
    
    public void Add<TElement>(Action<TElement> action) where TElement : Element
    {
        action?.Invoke((TElement)this);
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    public IEnumerator<Element> GetEnumerator()
    {
        return children.GetEnumerator();
    }

    public string ToHtml()
    {
        return CalculateComponentHtmlText(new()
        {
            Component = new ToStringHandlerComponent(this)
        }).GetAwaiter().GetResult().ToString();
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return children.GetEnumerator();
    }

    IEnumerator<Modifier> IEnumerable<Modifier>.GetEnumerator()
    {
        foreach (var item in Enumerable.Empty<Modifier>())
        {
            yield return item;
        }
    }

    class ToStringHandlerComponent : PureComponent
    {
        readonly Element _element;

        public ToStringHandlerComponent(Element element)
        {
            _element = element;
        }

        protected override Element render()
        {
            return _element;
        }
    }
}

sealed class FakeChild : Element
{
    public int Index { get; set; }
}

sealed class ElementAsTask : Element
{
    public readonly Task<Element> Value;

    public ElementAsTask(Task<Element> value)
    {
        Value = value;
    }
    
    internal List<Modifier> Modifiers;
}