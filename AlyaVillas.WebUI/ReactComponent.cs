namespace AlyaVillas.WebUI;

public abstract class ReactComponent<TState> : ReactWithDotNet.ReactComponent<TState> where TState : new()
{
    protected Primary Primary { get; } = new();
    protected Neutral Neutral { get; } = new();
}

public abstract class ReactComponent : ReactWithDotNet.ReactComponent
{
    protected Primary Primary { get; } = new();
    protected Neutral Neutral { get; } = new();
}

public sealed class Primary
{
    public string W50 { get; set; } = "#F6F1E4";
    public string W400 { get; set; } = "#B19045";
    public string W500 { get; set; } = "#A08139";
    public string W600 { get; set; } = "#987931";
}

public sealed class Neutral
{
    public string W0 { get; set; } = "#FFFFFF";
    public string W50 { get; set; } = "#F6F6F6";
    public string W75 { get; set; } = "#EDEDED";
    public string W100 { get; set; } = "#DBDBDB";
    public string W200 { get; set; } = "#C9C9C8";
    public string W500 { get; set; } = "#88898C";
    public string W600 { get; set; } = "#79797B";
    public string W900 { get; set; } = "#4A4A49";
    public string W1000 { get; set; } = "#000";
}