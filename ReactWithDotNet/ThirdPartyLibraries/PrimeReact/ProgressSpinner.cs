namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class ProgressSpinner : ElementBase
{
    /// <summary>
    ///     Width of the circle stroke.
    ///     <para>default: 2</para>
    /// </summary>
    [ReactProp]
    public string strokeWidth { get; set; }

    /// <summary>
    ///    Color for the background of the circle.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string fill { get; set; }

    /// <summary>
    ///     Duration of the rotate animation.
    ///     <para>default: 2s</para>
    /// </summary>
    [ReactProp]
    public string animationDuration { get; set; }
}