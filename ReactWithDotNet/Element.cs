using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

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

        static void InitializeKeyIfNotExists(IReadOnlyList<Element> siblings)
        {
            var key = 0;

            foreach (var sibling in siblings)
            {
                if (sibling.key == null)
                {
                    sibling.key = key++.ToString();
                }
            }
        }
    }
    #endregion
}