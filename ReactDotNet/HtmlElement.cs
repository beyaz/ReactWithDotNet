using ReactDotNet.Html5;
using System;
using System.Text.Json.Serialization;

namespace ReactDotNet;

public abstract class HtmlElement : Element
{

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

    /// <summary>
    ///     Gets or sets the on click.
    /// </summary>
    [React]
    public Action<string> onClick { get; set; }

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

    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [React]
    public string className { get; set; }
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