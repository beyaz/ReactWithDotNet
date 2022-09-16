namespace ReactWithDotNet;

public sealed class HStack : HtmlElement
{
    public override string Type => nameof(div);
    public HStack()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "stretch";
        style.width         = "100%";
    }
}

public sealed class HPanel : HtmlElement
{
    public override string Type => nameof(div);
    public HPanel()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "center";
    }
}

public sealed class VPanel : HtmlElement
{
    public override string Type => nameof(div);
    public VPanel()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "center";
    }
}

public sealed class VStack : HtmlElement
{
    public override string Type => nameof(div);
    public VStack()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "stretch";
        style.height        = "100%";
    }
}

public sealed class divVerticalCentered : HtmlElement
{
    public override string Type => nameof(div);
    
    public divVerticalCentered(int gapAsPixel = 10)
    {
        style.width_height   = "100%";
        style.display        = "flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
        style.textAlign      = "center";
        style.gap            = gapAsPixel + "px";
    }
}

public sealed class divHorizontalCentered : HtmlElement
{
    public override string Type => nameof(div);

    public divHorizontalCentered(int gapAsPixel = 10)
    {
        style.width_height   = "100%";
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
        style.textAlign      = "center";
        style.gap            = gapAsPixel + "px";
    }
}