using System.Text.Json.Serialization;
using static ReactDotNet.Mixin;
namespace ReactDotNet.Html5;


/// <summary>
///     Verticle Space
/// </summary>
public class VSpace : div
{
    public VSpace(double height)
    {
        style.height = px(height);
    }

    public VSpace(string height)
    {
        style.height = height;
    }
}


public class HSpace : div
{
    public HSpace(double width)
    {
        style.width = px(width);
    }

    public HSpace(string width)
    {
        style.width = width;
    }
}


public class Space : div
{
    public Space()
    {
            
    }

    public Space(params ElementModifier[] modifiers) : base(modifiers)
    {
    }


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