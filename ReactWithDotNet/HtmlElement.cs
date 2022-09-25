using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class HtmlElement : Element
{
    protected HtmlElement()
    {
        
    }

    protected HtmlElement(params Modifier[] modifiers)
    {
        this.Apply(modifiers);
    }
    
    

    protected HtmlElement(Style style)
    {
        this.style.Import(style);
    }
    

    /// <summary>
    ///     Gets the style.
    /// </summary>
    [React]
    public Style style { get; internal set; } = new();

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style
    {
        set => style.Import(value);
    }

    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [React]
    public Action<MouseEvent> onClick { get; set; }

    [React]
    public Action<MouseEvent> onMouseEnter { get; set; }
    
    [React]
    public Action<MouseEvent> onMouseLeave { get; set; }
    
    [React]
    [ReactTransformValueInClient("ReactWithDotNet.GetExternalJsObject")]
    public string onScroll { get; set; }
    

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

    /// <summary>
    ///     'innerText' property of element.
    /// </summary>
    public string text
    {
        set => innerText = value;
    }

    [React]
    public virtual string id { get; set; }

    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [React]
    public string className { get; set; }

    [React]
    public string title { get; set; }

    [React]
    public string tabIndex { get; set; }

    internal void AddClass(string cssClassName)
    {
        if (string.IsNullOrWhiteSpace(className))
        {
            className = cssClassName;
            return;
        }

        className += " " + cssClassName;
    }
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