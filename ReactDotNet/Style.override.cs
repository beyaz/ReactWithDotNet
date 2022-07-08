using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

partial class Style
{
    [JsonIgnore]
    public string marginLeftRight
    {
        set
        {
            marginLeft  = value;
            marginRight = value;
        }
    }

    [JsonIgnore]
    public string marginTopBottom
    {
        set
        {
            marginTop    = value;
            marginBottom = value;
        }
    }
}