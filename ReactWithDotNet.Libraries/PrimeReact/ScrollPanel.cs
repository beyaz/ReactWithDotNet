namespace ReactWithDotNet.Libraries.PrimeReact;

/// <summary>
///     ScrollPanel is defined using dimensions for the scrollable viewport.
/// </summary>
public sealed class ScrollPanel : ElementBase
{
    [ReactProp]
    public string className { get; set; }
}