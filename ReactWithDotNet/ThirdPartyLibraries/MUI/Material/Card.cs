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
    ///     If `true`, the card will use raised styling.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static Modifier Raised(bool? value) => CreateThirdPartyReactComponentModifier<Card>(x => x.raised = value);
}
