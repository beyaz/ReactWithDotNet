// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public  class Paper : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     Shadow depth, corresponds to `dp` in the spec.
    ///     <br/>
    ///     It accepts values between 0 and 24 inclusive.
    ///     <br/>
    ///     @default 1
    /// </summary>
    [ReactProp]
    public double? elevation { get; set; }
    
    /// <summary>
    ///     If `true`, rounded corners are disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? square { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The variant to use.
    ///     <br/>
    ///     @default 'elevation'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }
    
    [ReactProp]
    public string component { get; set; }
}
