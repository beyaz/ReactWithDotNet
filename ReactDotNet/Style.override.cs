using System.Text.Json.Serialization;
using static ReactDotNet.Mixin;
namespace ReactDotNet
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