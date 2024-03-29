namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Slider : ElementBase
{
   
    [ReactProp]
    public double? value { get; set; }
    
    [ReactProp]
    public double? max { get; set; }
    
    [ReactProp]
    public double? min { get; set; }
    
    [ReactProp]
    public string valueLabelDisplay { get; set; }
    
    
    [ReactProp]
    public dynamic[] marks { get; set; }
    
    [ReactProp]
    public double? step { get; set; }
     
    
    
    /// <summary>
    /// Callback function that is fired when the mouseup is triggered.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("mui_slider_onChangeCommitted")]
    public Func<double, Task> onChangeCommitted { get; set; }
    
    /// <summary>
    /// Callback function that is fired when the slider's value changed.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("mui_slider_onChange")]
    public Func<double,Task> onChange { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    
    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<double>> valueBind { get; set; }
    
    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public Func<Task> valueBindDebounceHandler { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }
}
