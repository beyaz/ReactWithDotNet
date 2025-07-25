namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Box : ElementBase
{
    ///// <summary>
    /////     The system prop that allows defining system overrides as well as additional CSS styles.
    ///// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
}