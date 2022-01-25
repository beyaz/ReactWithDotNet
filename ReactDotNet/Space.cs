using System.Text.Json.Serialization;

namespace ReactDotNet
{
    public class Space : div
    {
        [JsonIgnore]
        public double? Height
        {
            set
            {
                if (value.HasValue)
                {
                    style.height = value.Value.AsPixel();
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
                    style.width = value.Value.AsPixel();
                }
                else
                {
                    style.width = null;
                }
            }
        }

        
    }
}