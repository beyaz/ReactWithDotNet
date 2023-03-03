namespace ReactWithDotNet.Libraries.mui.material;

partial class Paper
{
    /// <summary>
    ///    Override or extend the styles applied to the component.
    /// </summary>
    [React]
    public string classes {get; set; }
    
    /// <summary>
    ///    Shadow depth, corresponds to `dp` in the spec.
    /// <br/>
    ///    It accepts values between 0 and 24 inclusive.
    /// <br/>
    ///    @default 1
    /// </summary>
    [React]
    public double elevation {get; set; }
    
    /// <summary>
    ///    If `true`, rounded corners are disabled.
    /// <br/>
    ///    @default false
    /// </summary>
    [React]
    public bool square {get; set; }
    
    /// <summary>
    ///    The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///    The variant to use.
    /// <br/>
    ///    @default 'elevation'
    /// </summary>
    [React]
    public string variant {get; set; }
}
