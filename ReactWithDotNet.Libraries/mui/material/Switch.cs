namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Switch: ElementBase
{
    [React]
    public bool? defaultChecked { get; set; }

    [React]
    public bool? disabled { get; set; }
}
