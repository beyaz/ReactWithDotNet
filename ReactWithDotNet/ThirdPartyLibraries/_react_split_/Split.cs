namespace ReactWithDotNet.ThirdPartyLibraries._react_split_;

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
    
    
    [ReactProp]
    public string direction { get; set; }
    
    [ReactProp]
    public int[] sizes { get; set; }

    
    
    public void Add(Action<Split> modify) => modify?.Invoke(this);
}
