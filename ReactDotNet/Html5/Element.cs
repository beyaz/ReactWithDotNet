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
    protected Element(params ElementModifier[] modifiers)
    {
        Mixin.Apply(this, modifiers);
    }

    protected internal virtual void BeforeSerialize()
    {
        children.RemoveAll(x => x is null);
        InitializeKeyIfNotExists(children);
    }

    #region Fields
    /// <summary>
    ///     The children
    /// </summary>
    public readonly List<Element> children = new List<Element>();

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnoreAttribute]
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
        Margin  = new MarginThickness(style);
        Padding = new PaddingThickness(style);
    }
    #endregion

    #region Public Properties
    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [React]
    public string className { get; set; }

    /// <summary>
    ///     Gets or sets the gravity.
    /// </summary>
    public int? gravity { get; set; }

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


    #region Margin
    [JsonIgnore]
    public MarginThickness Margin { get; }

    [JsonIgnore]
    public double? MarginAll
    {
        set
        {
            Margin.Left   = value;
            Margin.Right  = value;
            Margin.Top    = value;
            Margin.Bottom = value;
        }
    }

    [JsonIgnore]
    public double? MarginLeftRight
    {
        set
        {
            Margin.Left  = value;
            Margin.Right = value;
        }
    }

    [JsonIgnore]
    public double? MarginTopBottom
    {
        set
        {
            Margin.Top    = value;
            Margin.Bottom = value;
        }
    }

    #endregion

    #region Padding
    [JsonIgnore]
    public PaddingThickness Padding { get; }

    [JsonIgnore]
    public double? PaddingAll
    {
        set
        {
            Padding.Left   = value;
            Padding.Right  = value;
            Padding.Top    = value;
            Padding.Bottom = value;
        }
    }

    [JsonIgnore]
    public double? PaddingLeftRight
    {
        set
        {
            Padding.Left  = value;
            Padding.Right = value;
        }
    }

    [JsonIgnore]
    public double? PaddingTopBottom
    {
        set
        {
            Padding.Top    = value;
            Padding.Bottom = value;
        }
    }
    #endregion



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
    [JsonIgnoreAttribute]
    public Style Style 
    {
        set => style.Import(value);
    }


    public static Element operator +(Element element, Style style)
    {
        element.style.Import(style);

        return element;  
    }

    public static Element operator +(Element element, ElementModifier elementModifier)
    {
        return element | elementModifier;
    }
        
    public static Element operator |(Element element, ElementModifier elementModifier)
    {
        if (element is ReactComponent reactComponent)
        {
            if (reactComponent.modifier == null)
            {
                reactComponent.modifier = elementModifier;
            }
            else
            {
                reactComponent.modifier |= elementModifier;
            }

            return element;
        }
        elementModifier?.Modify(element);

        return element;
    }

        
}

public abstract class HtmlElement : Element
{

    [JsonPropertyName("$type")]
    public virtual string Type => GetType().Name.ToLower();
    

    protected HtmlElement()
    {
            
    }
    protected HtmlElement(params ElementModifier[] modifiers):base(modifiers)
    {
    }

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

    protected ThirdPartyComponent(params ElementModifier[] modifiers) : base(modifiers)
    {
    }

    public ThirdPartyComponent()
    {
            
    }
}