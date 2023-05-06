namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Button : ElementBase
{
    [ReactProp]
    public string size { get; set; }

    [ReactProp]
    public string variant { get; set; }

    [ReactProp]
    public bool disabled { get; set; }
    

        [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onClick { get; set; }
}