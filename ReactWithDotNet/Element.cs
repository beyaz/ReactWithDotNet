using System.Collections;
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
public abstract class Element : IEnumerable<Element>
{
    #region Fields
    /// <summary>
    ///     The children
    /// </summary>
    public readonly List<Element> children = new();
    #endregion

    #region Public Properties
    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public IEnumerable<Element> Children
    {
        set => children.AddRange(value);
    }

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    [React]
    public string key { get; set; }
    #endregion

    #region Public Methods
    /// <summary>
    ///     Adds the specified element.
    /// </summary>
    public void Add(Element element)
    {
        children.Add(element);
    }

    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    public IEnumerator<Element> GetEnumerator()
    {
        return children.GetEnumerator();
    }

    public static implicit operator Element(string text)
    {
        return new HtmlTextNode { text = text};
    }
    
    #endregion

    #region Explicit Interface Methods
    /// <summary>
    ///     Gets the enumerator.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return children.GetEnumerator();
    }
    #endregion
}