namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Button : ElementBase
{
    [React]
    public string variant { get; set; }

    [React]
    public bool disabled { get; set; }
    

        [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onClick { get; set; }
}