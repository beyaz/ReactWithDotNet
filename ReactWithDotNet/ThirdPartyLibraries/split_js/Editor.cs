namespace ReactWithDotNet.ThirdPartyLibraries.split_js;

public sealed class Split : ThirdPartyReactComponent
{
    [ReactProp]
    public int? minSize { get; set; }
    
    [ReactProp]
    public int? maxSize { get; set; }
    
    [ReactProp]
    public int? gutterSize { get; set; }

    [ReactProp]
    public string gutterAlign { get; set; }

    [ReactProp]
    public int? snapOffset { get; set; }
    
    [ReactProp]
    public int? dragInterval { get; set; }
}
