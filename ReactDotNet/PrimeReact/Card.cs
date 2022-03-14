namespace ReactDotNet.PrimeReact;

public class Card : ElementBase
{
    [React]
    public string title { get; set; }
}

public class Dialog: ElementBase
{
    /// <summary>
    /// Specifies the visibility of the dialog.
    /// </summary>
    [React]
    public bool visible { get; set; }

    /// <summary>
    /// Defines if background should be blocked when dialog is displayed.
    /// </summary>
    [React]
    public bool modal { get; set; }

    /// <summary>
    /// Footer content of the dialog.
    /// </summary>
    [React]
    public Element footer { get; set; }
}
