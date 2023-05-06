namespace ReactWithDotNet.Libraries.PrimeReact;

/// <summary>
/// Splitter is utilized to separate and resize panels.
/// </summary>
public class Splitter: ElementBase
{
    /// <summary>
    /// Orientation of the panels, valid values are "horizontal" and "vertical".
    /// <para>Default: horizontal</para>
    /// </summary>
    [ReactProp]
    public SplitterLayoutType? layout { get; set; }

    /// <summary>
    /// 	Size of the divider in pixels.
    /// <para>Default: 4</para>
    /// </summary>
    [ReactProp]
    public int? gutterSize { get; set; }
}


public class SplitterPanel : ElementBase
{
    [ReactProp]
    public int? size { get; set; }

    [ReactProp]
    public int? minSize { get; set; }

    public SplitterPanel()
    {
        
    }

    public SplitterPanel(params StyleModifier[] modifiers) : base(modifiers) { }
}




public enum SplitterLayoutType
{
    vertical,
    horizontal
}
