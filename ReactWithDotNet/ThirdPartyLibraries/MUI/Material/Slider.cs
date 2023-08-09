namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Slider : ElementBase
{
   
    [ReactProp]
    public double? value { get; set; }
    
    [ReactProp]
    public double? max { get; set; }
    
    [ReactProp]
    public double? min { get; set; }
    
    /// <summary>
    /// Callback function that is fired when the mouseup is triggered.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("mui_slider_onChangeCommitted")]
    public Action<double> onChangeCommitted { get; set; }
}
