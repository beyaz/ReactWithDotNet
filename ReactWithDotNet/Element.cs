using System.Collections;
using Newtonsoft.Json;

namespace ReactWithDotNet;

public sealed class ElementCollection : List<Element>
{
    public void Add(ElementCollection elements)
    {
        if (elements is not null)
        {
            AddRange(elements);
        }
    }
}

/// <summary>
///     The element
/// </summary>
[JsonObject]
public abstract class Element : IEnumerable<Element>, IEnumerable<IModifier>
{
    internal ElementCollection _children;

    /// <summary>
    ///     The children
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    public ElementCollection children
    {
        get
        {
            _children ??= new ElementCollection();

            return _children;
        }
    }

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    internal string key { get; set; }

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

    public static Element operator +(Element element, Style style)
    {
        ModifyHelper.ProcessModifier(element, CreateStyleModifier(s => s?.Import(style)));

        return element;
    }

    public static Element operator +(Element element, IModifier modifier)
    {
        ModifyHelper.ProcessModifier(element, modifier);

        return element;
    }

    public static implicit operator Element(string text)
    {
        return new HtmlTextNode { text = text };
    }

    /// <summary>
    ///     Adds the specified element.
    /// </summary>
    public void Add(Element element)
    {
        children.Add(element);
    }

    public void Add(params IModifier[] modifiers)
    {
        this.Apply(modifiers);
    }

    public void Add(IEnumerable<Element> elements)
    {
        if (elements is not null)
        {
            children.AddRange(elements);
        }
    }

    public void Add(IModifier modifier)
    {
        ModifyHelper.ProcessModifier(this, modifier);
    }

    /// <summary>
    ///     Invokes <paramref name="elementCreatorFunc" /> then adds return value to children.
    /// </summary>
    public void Add(Func<Element> elementCreatorFunc)
    {
        Add(elementCreatorFunc?.Invoke());
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
        return CalculateComponentHtmlText(new CalculateComponentHtmlTextInput
        {
            Component = new ToStringHandlerComponent(this)
        }).GetAwaiter().GetResult();
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return children.GetEnumerator();
    }

    IEnumerator<IModifier> IEnumerable<IModifier>.GetEnumerator()
    {
        return Enumerable.Empty<IModifier>().GetEnumerator();
    }

    class ToStringHandlerComponent : ReactPureComponent
    {
        Element _element;

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