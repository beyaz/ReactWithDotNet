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

