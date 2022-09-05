namespace ReactWithDotNet;


/// <summary>
///     Verticle Space
/// </summary>
public class VSpace : HtmlElement
{
    public override string Type => nameof(div);
    
    public VSpace(double height)
    {
        style.height = $"{height}px";
    }

    public VSpace(string height)
    {
        style.height = height;
    }
}


public class HSpace : HtmlElement
{
    public override string Type => nameof(div);
    public HSpace(double width)
    {
        style.width = $"{width}px";
    }

    public HSpace(string width)
    {
        style.width = width;
    }
}

