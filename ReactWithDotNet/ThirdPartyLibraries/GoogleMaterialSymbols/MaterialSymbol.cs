namespace ReactWithDotNet.ThirdPartyLibraries.GoogleMaterialSymbols;


public enum MaterialSymbolVariant
{
    outlined,
    rounded,
    sharp,
}

public sealed class MaterialSymbol : ThirdPartyReactComponent
{
    [ReactProp]
    public required string name { get; set; }
    
    [ReactProp]
    public MaterialSymbolVariant styleVariant { get; set; }
    
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
