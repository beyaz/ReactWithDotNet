using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using static ReactDotNet.Html5.UniqueKeyInitializer;

namespace ReactDotNet.Html5;

/// <summary>
///     The element
/// </summary>
public abstract class Element : IEnumerable<Element>
{
    #region Fields
    /// <summary>
    ///     The children
    /// </summary>
    public readonly List<Element> children = new();
    #endregion

    #region Constructors
    /// <summary>
    ///     Initializes a new instance of the <see cref="Element" /> class.
    /// </summary>
    protected Element()
    {
    }
    #endregion

    #region Public Properties
    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public IEnumerable<Element> Children
    {
        set => children.AddRange(value);
    }

   

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    [React]
    public string key { get; set; }



    /// <summary>
    ///     Gets the style.
    /// </summary>
    [React]
    public Style style { get; } = new();

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style
    {
        set => style.Import(value);
    }
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

    #region Methods
    protected internal virtual void BeforeSerialize()
    {
        children.RemoveAll(x => x is null);
        InitializeKeyIfNotExists(children);
    }
    #endregion
}