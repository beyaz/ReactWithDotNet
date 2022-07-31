namespace ReactWithDotNet;

public sealed class HStack : div
{
    public HStack()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "stretch";
        style.width         = "100%";
    }
}

public sealed class HPanel : div
{
    public HPanel()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "center";
    }
}

public sealed class VPanel : div
{
    public VPanel()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "center";
    }
}

public sealed class VStack : div
{
    public VStack()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "stretch";
        style.height        = "100%";
    }
}