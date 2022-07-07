using System.Text.Json.Serialization;
using static ReactDotNet.Html5.Mixin;
namespace ReactDotNet.Html5
{
    partial class Style
    {
        [JsonIgnore]
        public string marginLeftRight
        {
            set
            {
                marginLeft = value;
                marginRight = value;
            }
        }
        
        [JsonIgnore]
        public string marginTopBottom
        {
            set
            {
                marginTop  = value;
                marginBottom = value;
            }
        }

    }
}