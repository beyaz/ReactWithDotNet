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
    protected internal virtual void BeforeSerialize()
    {
        children.RemoveAll(x => x is null);
        InitializeKeyIfNotExists(children);
    }

    #region Fields
    /// <summary>
    ///     The children
    /// </summary>
    public readonly List<Element> children = new();

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public IEnumerable<Element> Children
    {
        set => children.AddRange(value);
    }
    
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
    ///     Gets or sets the name of the class.
    /// </summary>
    [React]
    public string className { get; set; }

    

    /// <summary>
    ///     Gets or sets the key.
    /// </summary>
    [React]
    public string key { get; set; }

    [React]
    public string width { get; set; }

    [React]
    public string height { get; set; }

  





    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [React]
    public Action<string> onClick { get; set; }


   

  



    /// <summary>
    ///     Gets the style.
    /// </summary>
    [React]
    public Style style { get; private set; } = new Style();

    
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





    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style 
    {
        set => style.Import(value);
    }

    
    

        
}

public abstract class HtmlElement : Element
{

    [JsonPropertyName("$type")]
    public virtual string Type => GetType().Name.ToLower();
    
    [React]
    public dangerouslySetInnerHTML dangerouslySetInnerHTML { get; set; }

    [JsonIgnore]
    public string innerHTML
    {
        set => dangerouslySetInnerHTML = value;
    }

    /// <summary>
    ///     'innerText' property of element.
    /// </summary>
    public string innerText { get; set; }

    [React]
    public virtual string id { get; set; }
}

[Serializable]
public sealed class dangerouslySetInnerHTML
{
    public dangerouslySetInnerHTML(string html)
    {
        __html = html;
    }

    public dangerouslySetInnerHTML()
    {
    }
    public string __html { get; set; }

    public static implicit operator dangerouslySetInnerHTML(string html)
    {
        return new dangerouslySetInnerHTML {__html = html};
    }
}

public abstract class ThirdPartyComponent: Element
{
    [JsonPropertyName("$type")]
    public virtual string Type => GetType().FullName;
}