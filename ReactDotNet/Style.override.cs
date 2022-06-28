using System.Text.Json.Serialization;
using static ReactDotNet.Mixin;
namespace ReactDotNet
{
    partial class Style
    {
        public string marginLeftRight
        {
            set
            {
                marginLeft = value;
                marginRight = value;
            }
        }

        
    }
}