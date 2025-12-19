namespace ReactWithDotNet.ThirdPartyLibraries.xyflow_react;

public sealed class ReactFlow : ThirdPartyReactComponent
{    
    [ReactProp]
    public dynamic nodes { get; set; }
    
    [ReactProp]
    public dynamic edges { get; set; }
}
