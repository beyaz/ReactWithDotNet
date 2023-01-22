using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ReactWithDotNet;

sealed class FakeChild : Element
{
    public int Index { get; set; }
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

    public static Element operator +(Element element, StyleModifier styleModifier)
    {
        ModifyHelper.ProcessModifier(element, styleModifier);

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

    /// <summary>
    ///     Represents element as html text as possible.
    /// </summary>
    public override string ToString()
    {
        ReactContext reactContext = null;
        
        if (this is ReactStatefulComponent reactStatefulComponent)
        {
            reactContext = reactStatefulComponent.Context;

        }
        return this.ToString( reactContext ?? ReactContext.Create(null,700,800));
    }

    /// <summary>
    ///     Represents element as html text as possible.
    /// </summary>
    public  string ToString(ReactContext reactContext)
    {
        return HtmlTextGenerator.ToHtml(this, reactContext);
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
}