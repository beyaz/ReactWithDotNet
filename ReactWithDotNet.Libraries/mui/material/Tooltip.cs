namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Tooltip : ElementBase
{
    [React]
    public bool? arrow { get; set; }

    [React]
    public Element title { get; set; }

    [React]
    public string placement { get; set; }

    [React]
    public Dictionary<string, Style> classes { get; set; }
    

}