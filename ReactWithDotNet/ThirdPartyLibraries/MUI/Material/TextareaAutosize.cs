// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class TextareaAutosize : ElementBase
{
    /// <summary>
    /// Maximum number of rows to display.
    /// </summary>
    [ReactProp]
    public int? maxRows { get; set; }

    /// <summary>
    ///  Minimum number of rows to display.
    /// <br/>
    /// Default: 1
    /// </summary>
    [ReactProp]
    public int? minRows { get; set; }
    
    
    [ReactProp]
    public string value { get; set; }
    
    /// <summary>
    ///     The default value. Use when the component is not controlled.
    /// </summary>
    [ReactProp]
    public string defaultValue { get; set; }
    
    /// <summary>
    ///     The short hint displayed in the `input` before the user enters a value.
    /// </summary>
    [ReactProp]
    public string placeholder { get; set; }
    
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public ChangeEventHandler onChange { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    
    
    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.currentTarget.value", eventName = "onChange")]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public Expression<Func<string>> valueBind { get; set; }

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
