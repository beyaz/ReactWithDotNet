namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class Card : ElementBase
{
    [ReactProp]
    public string title { get; set; }
}



public class Dialog: ElementBase
{
    /// <summary>
    /// Specifies the visibility of the dialog.
    /// </summary>
    [ReactProp]
    public bool visible { get; set; }

    /// <summary>
    /// Defines if background should be blocked when dialog is displayed.
    /// </summary>
    [ReactProp]
    public bool modal { get; set; }

    /// <summary>
    /// Footer content of the dialog.
    /// </summary>
    [ReactProp]
    public Element footer { get; set; }

    /// <summary>
    /// Title content of the dialog.
    /// </summary>
    [ReactProp]
    public Element header { get; set; }


    /// <summary>
    /// Adds a close icon to the header to hide the dialog.
    /// </summary>
    [ReactProp]
    public bool closable { get; set; }


    /// <summary>
    /// Callback to invoke when dialog is showed.
    /// </summary>
    [ReactProp]
    public Action onShow { get; set; }

    /// <summary>
    /// Callback to invoke when dialog is hidden (Required).
    /// </summary>
    [ReactProp]
    public Action onHide { get; set; }

    protected  override Element GetSuspenseFallbackElement()
    {
        return new div { DisplayNone };
    }
}


