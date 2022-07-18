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