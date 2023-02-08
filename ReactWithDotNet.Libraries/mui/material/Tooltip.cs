namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Tooltip : ElementBase
{
    [React]
    public bool? arrow { get; set; }

    [React]
    public Element title { get; set; }
}