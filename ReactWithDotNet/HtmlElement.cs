using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Mixin
{
    public static HtmlElementModifier Aria(string ariaName, string ariaValue)
    {
        return CreateHtmlElementModifier<HtmlElement>(el => el.aria.Add(ariaName, ariaValue));
    }

    public static HtmlElementModifier Data(string dataName, string dataValue)
    {
        return CreateHtmlElementModifier<HtmlElement>(el => el.data.Add(dataName, dataValue));
    }
    
    /// <summary>
    /// Automatically generates a css class then adds class name to element.
    /// <br/>
    /// css class will be automatically remove when component destroyed.
    /// <br/>
    /// Example:
    /// <code>
    /// arrowDown.WithClass(new Style
    /// {
    ///    Transform("rotate(-180deg)")
    /// })
    /// </code>
    /// </summary>
    public static TElement WithClass<TElement>(this TElement element, Style cssBody) where TElement : HtmlElement
    {
        (element.classNameList ??= new List<Style>()).Add(cssBody);

        return element;
    }
    
    /// <summary>
    /// Automatically generates a css class then adds class name to element.
    /// <br/>
    /// css class will be automatically remove when component destroyed.
    /// <br/>
    /// Example:
    /// <code>
    /// arrowDown.WithClass(new []
    /// {
    ///    Transform("rotate(-180deg)")
    /// })
    /// </code>
    /// </summary>
    public static TElement WithClass<TElement>(this TElement element, StyleModifier[] styleModifiers) where TElement : HtmlElement
    {
        return element.WithClass(new Style(styleModifiers));
    }
}

public abstract class HtmlElement : Element
{
    internal Dictionary<string, string> _aria;
    internal Dictionary<string, string> _data;

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
    ///     Provides a hint for generating a keyboard shortcut for the current element. This attribute consists of a
    ///     space-separated list of characters. The browser should use the first one that exists on the computer keyboard
    ///     layout.
    /// </summary>
    [ReactProp]
    public string accesskey { get; set; }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Dictionary<string, string> aria
    {
        get
        {
            _aria ??= new Dictionary<string, string>();

            return _aria;
        }
    }

    /// <summary>
    /// Specifies whether an element is draggable or not.
    /// <br/>
    /// Tip: Links and images are draggable by default.
    /// </summary>
    [ReactProp]
    public string draggable { get; set; }
    
    
    /// <summary>
    ///     Specifies whether the content of an element is editable or not.
    /// <br/>
    ///     true|false
    /// </summary>
    [ReactProp]
    public string contenteditable { get; set; }
    
    
    
    
    [ReactProp]
    public bool? autofocus { get; set; }

    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [ReactProp]
    public string className { get; set; }

    [ReactProp]
    public dangerouslySetInnerHTML dangerouslySetInnerHTML { get; set; }

    /// <summary>
    ///     The data-* attribute is used to store custom data private to the page or application.
    ///     <br />
    ///     The data-* attribute gives us the ability to embed custom data attributes on all HTML elements.
    ///     <br />
    ///     The stored (custom) data can then be used in the page's JavaScript to create a more engaging user experience
    ///     (without any Ajax calls or server-side database queries).
    ///     <br />
    ///     The data-* attribute consist of two parts:
    ///     <br />
    ///     The attribute name should not contain any uppercase letters, and must be at least one character long after the
    ///     prefix "data-"<br />
    ///     The attribute value can be any string<br />
    ///     Note: Custom attributes prefixed with "data-" will be completely ignored by the user agent.<br />
    /// </summary>
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Dictionary<string, string> data
    {
        get
        {
            _data ??= new Dictionary<string, string>();

            return _data;
        }
    }

    /// <summary>
    ///     Specifies the text direction for the content in an element
    /// </summary>
    [ReactProp]
    public string dir { get; set; }

    [ReactProp]
    public virtual string id { get; set; }

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
    ///     Helps define the language of an element: the language that non-editable elements are in, or the language that
    ///     editable elements should be written in by the user. The attribute contains one "language tag" (made of
    ///     hyphen-separated "language subtags") in the format defined in RFC 5646: Tags for Identifying Languages (also known
    ///     as BCP 47). xml:lang has priority over it.
    /// </summary>
    [ReactProp]
    public string lang { get; set; }

    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onClick { get; set; }
    
    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [ReactProp]
    [JsonPropertyName("$onClickPreview")]
    public Action onClickPreview { get; set; }
    

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onMouseEnter { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onMouseLeave { get; set; }

    /// <summary>
    ///     Handler <paramref name="value" /> should be in client js codes.<br />
    ///     <br />
    ///     Sample Usage:<br />
    ///     <br />
    ///     ReactWithDotNet.RegisterExternalJsObject(<paramref name="value" />, function(e){<br />
    ///     ...<br />
    ///     ...<br />
    ///     });
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient("ReactWithDotNet.GetExternalJsObject")]
    public string onScroll { get; set; }

    /// <summary>
    ///     A space-separated list of the part names of the element. Part names allows CSS to select and style specific
    ///     elements in a shadow tree via the ::part pseudo-element.
    /// </summary>
    [ReactProp]
    public string part { get; set; }

    /// <summary>
    ///     Roles define the semantic meaning of content, allowing screen readers and other tools to present and support
    ///     interaction with an object in a way that is consistent with user expectations of that type of object.
    /// </summary>
    [ReactProp]
    public string role { get; set; }

    /// <summary>
    ///     An enumerated attribute defines whether the element may be checked for spelling errors. It may have the following
    ///     values:<br />
    ///     empty string or true, which indicates that the element should be, if possible, checked for spelling errors;<br />
    ///     false, which indicates that the element should not be checked for spelling errors.
    /// </summary>
    [ReactProp]
    public string spellcheck { get; set; }

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

    [ReactProp]
    public string tabIndex { get; set; }

    /// <summary>
    ///     'innerText' property of element.
    /// </summary>
    public string text
    {
        set => innerText = value;
        get => innerText;
    }

    [ReactProp]
    public string title { get; set; }

    /// <summary>
    ///     An enumerated attribute that is used to specify whether an element's attribute values and the values of its Text
    ///     node children are to be translated when the page is localized, or whether to leave them unchanged. It can have the
    ///     following values:
    ///     <br />
    ///     empty string or yes, which indicates that the element will be translated.
    ///     <br />
    ///     no, which indicates that the element will not be translated.
    /// </summary>
    [ReactProp]
    public string translate { get; set; }

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
    
    

    internal List<Style> classNameList;
    
    
    protected static HtmlElementModifier Modify<THtmlElement>(Action<THtmlElement> modifyAction)
        where THtmlElement : HtmlElement
    {
        return CreateHtmlElementModifier(modifyAction);
    }

    #region Operators

    public static HtmlElement operator +(HtmlElement element, Style style)
    {
        element.style.Import(style);

        return element;
    }
    
    public static HtmlElement operator +(HtmlElement element, StyleModifier[] styleModifiers)
    {
        element.Apply(styleModifiers);

        return element;
    }

    public static HtmlElement operator +(HtmlElement htmlElement, HtmlElementModifier htmlElementModifier)
    {
        htmlElementModifier?.Process(htmlElement);

        return htmlElement;
    }
    
    public static HtmlElement operator +(HtmlElement htmlElement, IModifier modifier)
    {
        ModifyHelper.ProcessModifier(htmlElement, modifier);

        return htmlElement;
    }

    public static HtmlElement operator +(HtmlElement htmlElement, StyleModifier styleModifier)
    {
        styleModifier?.ModifyStyle(htmlElement.style);

        return htmlElement;
    }

    public void Add(StyleModifier styleModifier)
    {
        styleModifier?.ModifyStyle(style);
    }

    public void Add(HtmlElementModifier htmlElementModifier)
    {
        htmlElementModifier?.Process(this);
    }
    
    public void Add(Action<HtmlElement> modifyHtmlElement)
    {
        if (modifyHtmlElement == null)
        {
            return;
        }
        
        modifyHtmlElement(this);
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