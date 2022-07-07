using System;
using ReactDotNet.Html5;

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

    /// <summary>
    /// Title content of the dialog.
    /// </summary>
    [React]
    public object header { get; set; }


    /// <summary>
    /// Adds a close icon to the header to hide the dialog.
    /// </summary>
    [React]
    public bool closable { get; set; }


    /// <summary>
    /// Callback to invoke when dialog is showed.
    /// </summary>
    [React]
    public Action onShow { get; set; }

    /// <summary>
    /// Callback to invoke when dialog is hidden (Required).
    /// </summary>
    [React]
    public Action onHide { get; set; }
}


