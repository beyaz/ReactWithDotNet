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

    public static Element operator |(Element element, IModifier modifier)
    {
        element.ProcessModifier(modifier);

        return element;
    }

    public static Element operator +(Element element, IModifier modifier)
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