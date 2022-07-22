namespace ReactWithDotNet;

public class SyntheticEvent
{
    public ShadowHtmlElement target { get; set; }
}

public class ShadowHtmlElement
{
    public int selectionStart { get; set; }
    public string value { get; set; }
}

public sealed class MouseEvent
{
    public int clientX { get; set; }
    public int clientY { get; set; }

    public int pageX { get; set; }
    public int pageY { get; set; }


    public int screenX { get; set; }

    public int screenY { get; set; }

    public double timeStamp { get; set; }
    
    public string type { get; set; }
}