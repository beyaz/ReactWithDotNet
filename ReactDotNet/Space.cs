namespace ReactDotNet;


/// <summary>
///     Verticle Space
/// </summary>
public class VSpace : div
{
    public VSpace(double height)
    {
        style.height = $"{height}px";
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
        style.width = $"{width}px";
    }

    public HSpace(string width)
    {
        style.width = width;
    }
}

