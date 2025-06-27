using System.Diagnostics;

namespace ReactWithDotNet;

public class BaseSyntheticEvent;

public class SyntheticEvent : BaseSyntheticEvent;

public class UIEvent : BaseSyntheticEvent
{
    public ShadowHtmlElement currentTarget { get; init; }
    public ShadowHtmlElement target { get; init; }
}

[DebuggerDisplay("{tagName}")]
public sealed class ShadowHtmlElement
{
    public string id { get; init; }

    public int offsetHeight { get; init; }
    public int offsetLeft { get; init; }
    public int offsetTop { get; init; }
    public int offsetWidth { get; init; }

    public int? selectedIndex { get; init; }

    public int? selectionStart { get; init; }

    public string tagName { get; init; }

    public string value { get; init; }
    
    public string textContent { get; init; }
    
    public IReadOnlyDictionary<string,string> data { get;  set; }

    internal static void Fix(ShadowHtmlElement shadowHtmlElement)
    {
        if (shadowHtmlElement?.data is not null)
        {
            shadowHtmlElement.data = new Dictionary<string, string>(shadowHtmlElement.data, StringComparer.OrdinalIgnoreCase);
        }
    }
    
    public double scrollHeight { get; init; }
    public double scrollLeft { get; init; }
    public double scrollTop { get; init; }
    public double scrollWidth { get; init; }
    
    public DOMRect boundingClientRect { get; init; }
}

public sealed record DOMRect
{
    public double x { get; init; }

    public double y { get; init; }

    public double width { get; init; }

    public double height { get; init; }

    public double left { get; init; }

    public double right { get; init; }

    public double top { get; init; }

    public double bottom { get; init; }
}

public sealed class ScrollEvent : UIEvent
{
    public double timeStamp { get; init; }

    public string type { get; init; }
}
public delegate Task ScrollEventHandler(ScrollEvent e);

public sealed class KeyboardEvent : UIEvent
{
    public int    keyCode { get; init; }
    public string key { get; init; }
    public bool   shiftKey { get; init; }
    public bool   ctrlKey { get; init; }
    public bool   altKey { get; init; }
    public int    which { get; init; }
    public double timeStamp { get; set; }
    public string type { get; set; }
}

public delegate Task KeyboardEventHandler(KeyboardEvent e);


    

public sealed class MouseEvent : UIEvent
{
    public bool altKey { get; init; }
    public bool bubbles { get; init; }
    public int clientX { get; init; }
    public int clientY { get; init; }
    public bool ctrlKey { get; init; }
    
    public bool metaKey { get; init; }

    public double movementX { get; init; }
    public double movementY { get; init; }

    public int pageX { get; init; }
    public int pageY { get; init; }

    public int screenX { get; init; }

    public int screenY { get; init; }

    public bool shiftKey { get; init; }


    public double timeStamp { get; init; }

    public string type { get; init; }

}

public delegate Task MouseEventHandler(MouseEvent e);

public sealed class ChangeEvent : UIEvent
{
    public bool bubbles { get; init; }

    public double timeStamp { get; init; }

    public string type { get; init; }
}

public delegate Task ChangeEventHandler(ChangeEvent e);


public sealed class FocusEvent
{
    public bool bubbles { get; init; }

    public bool cancelable { get; init; }

    public ShadowHtmlElement currentTarget { get; init; }

    public bool defaultPrevented { get; init; }

    public int detail { get; init; }

    public int eventPhase { get; init; }

    public bool isTrusted { get; init; }
    
    public ShadowHtmlElement target { get; init; }
    
    public ShadowHtmlElement relatedTarget { get; init; }

    public double timeStamp { get; init; }

    public string type { get; init; }
}

public delegate Task FocusEventHandler(FocusEvent e);



public sealed class DragEvent
{
    public bool bubbles { get; init; }

    public bool cancelable { get; init; }

    public ShadowHtmlElement currentTarget { get; init; }

    public bool defaultPrevented { get; init; }

    public int detail { get; init; }

    public int eventPhase { get; init; }

    public bool isTrusted { get; init; }
    
    public ShadowHtmlElement target { get; init; }
    
    public ShadowHtmlElement relatedTarget { get; init; }

    public double timeStamp { get; init; }

    public string type { get; init; }
}

public delegate Task DragEventHandler(DragEvent e);


public sealed class ClipboardEvent : UIEvent
{
    public bool bubbles { get; init; }

    public double timeStamp { get; init; }

    public string type { get; init; }
}

public delegate Task ClipboardEventHandler(ClipboardEvent e);
