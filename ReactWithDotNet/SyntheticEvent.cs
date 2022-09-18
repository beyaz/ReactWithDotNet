namespace ReactWithDotNet;

public class BaseSyntheticEvent
{
}

public class SyntheticEvent : BaseSyntheticEvent
{
}

public class UIEvent : BaseSyntheticEvent
{
}

public class ShadowHtmlElement
{
    #region Public Properties
    public string id { get; set; }
    public string tagName { get; set; }
    #endregion

    //public int selectionStart { get; set; }
    //public string value { get; set; }
}

public sealed class MouseEvent : UIEvent
{
    public bool altKey { get; set; }
    public bool bubbles { get; set; }
    public int clientX { get; set; }
    public int clientY { get; set; }
    public bool ctrlKey { get; set; }

    /// <summary>
    ///     this is the id of first element id which is 'has id value'
    /// </summary>
    public string FirstNotEmptyId { get; set; }

    public bool metaKey { get; set; }

    public double movementX { get; set; }
    public double movementY { get; set; }

    public int pageX { get; set; }
    public int pageY { get; set; }

    public int screenX { get; set; }

    public int screenY { get; set; }

    public bool shiftKey { get; set; }

    public ShadowHtmlElement target { get; set; }

    public double timeStamp { get; set; }

    public string type { get; set; }
}