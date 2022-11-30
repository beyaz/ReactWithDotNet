using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class HtmlElement : Element
{
    internal Style _style;

    protected HtmlElement()
    {
    }

    protected HtmlElement(params IModifier[] modifiers)
    {
        this.Apply(modifiers);
    }

    protected HtmlElement(string innerText)
    {
        text = innerText;
    }

    protected HtmlElement(Style style)
    {
        this.style.Import(style);
    }

    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [React]
    public string className { get; set; }

    [React]
    public dangerouslySetInnerHTML dangerouslySetInnerHTML { get; set; }

    [React]
    public virtual string id { get; set; }

    [React]
    public string role { get; set; }

    /// <summary>
    /// Provides a hint for generating a keyboard shortcut for the current element. This attribute consists of a space-separated list of characters. The browser should use the first one that exists on the computer keyboard layout.
    /// </summary>
    [React]
    public string accesskey { get; set; }
    
    
    /// <summary>
    /// An enumerated attribute that is used to specify whether an element's attribute values and the values of its Text node children are to be translated when the page is localized, or whether to leave them unchanged. It can have the following values:
    /// <br/>
    /// empty string or yes, which indicates that the element will be translated.
    /// <br/>
    /// no, which indicates that the element will not be translated.
    /// </summary>
    [React]
    public string translate { get; set; }

    [React]
    public bool? autofocus { get; set; }

    /// <summary>
    /// A space-separated list of the part names of the element. Part names allows CSS to select and style specific elements in a shadow tree via the ::part pseudo-element.
    /// </summary>
    [React]
    public string part { get; set; }

    /// <summary>
    /// Helps define the language of an element: the language that non-editable elements are in, or the language that editable elements should be written in by the user. The attribute contains one "language tag" (made of hyphen-separated "language subtags") in the format defined in RFC 5646: Tags for Identifying Languages (also known as BCP 47). xml:lang has priority over it.
    /// </summary>
    [React]
    public string lang { get; set; }

    /// <summary>
    /// An enumerated attribute defines whether the element may be checked for spelling errors. It may have the following values:<br/>
    /// empty string or true, which indicates that the element should be, if possible, checked for spelling errors;<br/>
    /// false, which indicates that the element should not be checked for spelling errors.
    /// </summary>
    [React]
    public string spellcheck { get; set; }

    [JsonIgnore]
    public string innerHTML
    {
        set => dangerouslySetInnerHTML = value;
    }

    /// <summary>
    ///     'innerText' property of element.
    /// </summary>
    public string innerText { get; set; }

    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onClick { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onMouseEnter { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onMouseLeave { get; set; }

    [React]
    [ReactTransformValueInClient("ReactWithDotNet.GetExternalJsObject")]
    public string onScroll { get; set; }

    /// <summary>
    ///     Gets the style.
    /// </summary>
    [JsonIgnore]
    public Style style
    {
        get
        {
            _style ??= new Style();

            return _style;
        }
    }

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style
    {
        set => style.Import(value);
    }

    [React]
    public string tabIndex { get; set; }

    /// <summary>
    ///     'innerText' property of element.
    /// </summary>
    public string text
    {
        set => innerText = value;
    }

    [React]
    public string title { get; set; }

    [JsonPropertyName("$type")]
    public virtual string Type => GetType().Name.ToLower();

    /// <summary>
    ///     Adds given cssClassName ot class attribute of html element
    /// </summary>
    public void AddClass(string cssClassName)
    {
        if (string.IsNullOrWhiteSpace(className))
        {
            className = cssClassName;
            return;
        }

        className += " " + cssClassName;
    }

    internal virtual void BeforeSerialize(HtmlElement parent)
    {
    }

    #region Operators

    public static HtmlElement operator +(HtmlElement element, Style style)
    {
        element.style.Import(style);

        return element;
    }
    
    public static HtmlElement operator +(HtmlElement htmlElement, HtmlElementModifier htmlElementModifier)
    {
        htmlElementModifier.modifyHtmlElement(htmlElement);

        return htmlElement;
    }



    public static HtmlElement operator +(HtmlElement htmlElement, StyleModifier styleModifier)
    {
        styleModifier.modifyStyle(htmlElement.style);

        return htmlElement;
    }

    public void Add(StyleModifier styleModifier)
    {
        styleModifier?.modifyStyle(style);
    }

    public void Add(HtmlElementModifier htmlElementModifier)
    {
        htmlElementModifier?.modifyHtmlElement(this);
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public void Add(Style style)
    {
        this.style.Import(style);
    }


    #endregion
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
        return new dangerouslySetInnerHTML { __html = html };
    }
}