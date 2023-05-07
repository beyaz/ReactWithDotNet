namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class Tooltip : ElementBase
{
    /// <summary>
    /// Position of the tooltip, valid values are right, left, top and bottom.
    /// </summary>
    [ReactProp]
    public string position { get; set; }

    /// <summary>
    /// Target element on global tooltip option.
    /// </summary>
    [ReactProp]
    public string target { get; set; }


    [ReactProp]
    public string content { get; set; }

    /// <summary>
    /// Delay to show the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [ReactProp]
    public double showDelay { get; set; }



    /// <summary>
    /// 	Delay to update the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [ReactProp]
    public double updateDelay { get; set; }



    /// <summary>
    /// Delay to hide the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [ReactProp]
    public double hideDelay { get; set; }

    protected override Element GetSuspenseFallbackElement()
    {
        return new div { DisplayNone };
    }
}

