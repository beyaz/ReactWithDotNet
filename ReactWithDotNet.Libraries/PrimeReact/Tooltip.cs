namespace ReactWithDotNet.Libraries.PrimeReact;

public class Tooltip : ElementBase
{
    /// <summary>
    /// Position of the tooltip, valid values are right, left, top and bottom.
    /// </summary>
    [React]
    public string position { get; set; }

    /// <summary>
    /// Target element on global tooltip option.
    /// </summary>
    [React]
    public string target { get; set; }


    [React]
    public string content { get; set; }

    /// <summary>
    /// Delay to show the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [React]
    public double showDelay { get; set; }



    /// <summary>
    /// 	Delay to update the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [React]
    public double updateDelay { get; set; }



    /// <summary>
    /// Delay to hide the tooltip in milliseconds.
    /// <para>Default: 0</para>
    /// </summary>
    [React]
    public double hideDelay { get; set; }
}

