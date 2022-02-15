using System.Text.Json.Serialization;
using static ReactDotNet.Mixin;
namespace ReactDotNet
{
    public class Space : div
    {
        public override string tagName => "div";

        [JsonIgnore]
        public double? Height
        {
            set
            {
                if (value.HasValue)
                {
                    style.height = px(value.Value);
                }
                else
                {
                    style.height = null;
                }
            }
        }

        [JsonIgnore]
        public double? Width
        {
            set
            {
                if (value.HasValue)
                {
                    style.width = px(value.Value);
                }
                else
                {
                    style.width = null;
                }
            }
        }

        
    }
}