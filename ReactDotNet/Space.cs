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
                    style.Height = value.Value.AsPixel();
                }
                else
                {
                    style.Height = null;
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
                    style.Width = value.Value.AsPixel();
                }
                else
                {
                    style.Width = null;
                }
            }
        }

        
    }
}