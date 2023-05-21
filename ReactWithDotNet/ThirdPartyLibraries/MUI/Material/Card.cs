// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Card : Paper
{
    /// <summary>
    ///     If `true`, the card will use raised styling.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? raised { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
}
