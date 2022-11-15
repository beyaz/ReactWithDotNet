using System.Collections;
using Newtonsoft.Json;

namespace ReactWithDotNet;

sealed class FakeChild : Element
{
    public int Index { get; set; }

    protected internal override void ProcessModifier(IModifier modifier)
    {
        throw new NotImplementedException("Fake childs cannot modify");
    }
}

/// <summary>
///     The element
/// </summary>
[JsonObject]
public abstract class Element : IEnumerable<Element>, IEnumerable<IModifier>
{
    internal List<Element> _children;

    /// <summary>
    ///     The children
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    public List<Element> children
    {
        get
        {
            if (_children == null)
            {
                _children = new List<Element>();
            }

            return _children;
        }
    }

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    [React]
    public string key { get; set; }

    public static Element operator +(Element element, StyleModifier modifier)
    {
        element.ProcessModifier(modifier);

        return element;
    }

    public static Element operator |(Element element, StyleModifier modifier)
    {
        element.ProcessModifier(modifier);

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
    
    public void Add(IEnumerable<Element> elements)
    {
        if (elements is not null)
        {
            children.AddRange(elements);
        }
    }
    
    public void Add(IModifier modifier)
    {
        ProcessModifier(modifier);
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    public IEnumerator<Element> GetEnumerator()
    {
        return children.GetEnumerator();
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

    protected internal abstract void ProcessModifier(IModifier modifier);
}


public sealed class Fragment : Element
{
    internal readonly Style style;
    internal List<IModifier> modifiers;

    protected internal override void ProcessModifier(IModifier modifier)
    {
        if (modifier is null)
        {
            return;
        }

        modifiers ??= new List<IModifier>();

        modifiers.Add(modifier);
    }

    public Fragment()
    {
        
    }
    
    public Fragment(params IModifier[] modifiers)
    {
        if (modifiers is null || modifiers.Length == 0)
        {
            return;
        }

        this.modifiers ??= new List<IModifier>();

        this.modifiers.AddRange(modifiers);
    }

    public Fragment(Style style)
    {
        this.style = style;
    }

    internal void ArrangeChildren()
    {
        if (modifiers is not null)
        {
            foreach (var modifier in modifiers)
            {
                foreach (var child in children)
                {
                    child?.ProcessModifier(modifier);
                }
            }
        }

        if (style is not null)
        {
            foreach (var child in children)
            {
                if (child is null)
                {
                    continue;
                }
                
                if (child is HtmlElement htmlElement)
                {
                    htmlElement.style.Import(style);
                    continue;
                }

                if (child is ThirdPartyReactComponent thirdPartyReactComponent)
                {
                    thirdPartyReactComponent.style.Import(style);
                    continue;
                }

                //if (child is ReactStatefulComponent reactStatefulComponent)
                //{
                //    reactStatefulComponent.style.Import(style);
                //    continue;
                //}
            }
        }
    }
}