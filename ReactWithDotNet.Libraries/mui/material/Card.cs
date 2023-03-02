namespace ReactWithDotNet.Libraries.mui.material;

partial class Card
{
    /// <summary>
    ///    Override or extend the styles applied to the component.
    /// </summary>
    [React]
    public string classes {get; set; }
    
    /// <summary>
    ///    If `true`, the card will use raised styling.
    /// <br/>
    ///    @default false
    /// </summary>
    [React]
    public bool raised {get; set; }
    
    /// <summary>
    ///    The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public dynamic sx { get; } = new ExpandoObject();
}
