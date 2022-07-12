using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

public abstract class ThirdPartyComponent: Element
{
    [JsonPropertyName("$type")]
    public virtual string Type => GetType().FullName;
}