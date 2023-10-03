namespace ReactWithDotNet.ThirdPartyLibraries.ReactSplitPane;


public sealed class SplitPane : ThirdPartyReactComponent
{

    [ReactProp]
    public double[] sizes { get; set; }

    [ReactProp]
    public double? minSize { get; set; }

    [ReactProp]
    public bool? expandToMin { get; set; }


    [ReactProp]
    public double? gutterSize { get; set; }

    /// <summary>
    /// 'center' | 'start' | 'end'
    /// </summary>
    [ReactProp]
    public string gutterAlign { get; set; }
    
    
    [ReactProp]
    public double? snapOffset { get; set; }
    
    
    [ReactProp]
    public double? dragInterval { get; set; }
    
    /// <summary>
    /// 'horizontal' | 'vertical'
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
    
    [ReactProp]
    public string cursor { get; set; }
}