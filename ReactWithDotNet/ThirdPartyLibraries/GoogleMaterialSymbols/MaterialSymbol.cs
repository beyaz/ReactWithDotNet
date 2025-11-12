namespace ReactWithDotNet.ThirdPartyLibraries.GoogleMaterialSymbols;

public sealed class MaterialSymbol : PureComponent
{
    [ReactProp]
    public required string name { get; set; }
    
    [ReactProp]
    public string styleVariant { get; set; }
    
    [ReactProp]
    public int? size { get; set; }
    
    [ReactProp]
    public string color { get; set; }

    [ReactProp]
    public int? fill { get; set; }

    [ReactProp]
    public int? weight { get; set; }
    
    [ReactProp]
    public int? grade { get; set; }
    
    [ReactProp]
    public int? opticalSize { get; set; }
}
