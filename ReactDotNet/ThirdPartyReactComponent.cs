using System.Text.Json.Serialization;

namespace ReactDotNet;

public abstract class ThirdPartyReactComponent : Element
{
    [JsonPropertyName("$type")]
    public virtual string Type => GetType().FullName;

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
}