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
    
    /// <summary>
    /// Callback function that is fired when the slider's value changed.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("mui_slider_onChange")]
    public Action<double> onChange { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
}
