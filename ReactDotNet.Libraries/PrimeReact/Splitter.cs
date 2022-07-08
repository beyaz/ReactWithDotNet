namespace ReactDotNet.PrimeReact;

/// <summary>
/// Splitter is utilized to separate and resize panels.
/// </summary>
public class Splitter: ElementBase
{
    /// <summary>
    /// Orientation of the panels, valid values are "horizontal" and "vertical".
    /// <para>Default: horizontal</para>
    /// </summary>
    [React]
    public SplitterLayoutType? layout { get; set; }

    /// <summary>
    /// 	Size of the divider in pixels.
    /// <para>Default: 4</para>
    /// </summary>
    [React]
    public int? gutterSize { get; set; }
}


public class SplitterPanel : ElementBase
{
    [React]
    public int? size { get; set; }

    [React]
    public int? minSize { get; set; }
}




public enum SplitterLayoutType
{
    vertical,
    horizontal
}
