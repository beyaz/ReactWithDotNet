namespace ReactWithDotNet.ThirdPartyLibraries.NextUI;

public abstract class ComponentBase : ThirdPartyReactComponent
{
        
}
public sealed class Avatar : ComponentBase
{

    [ReactProp]
    public bool? bordered { get; set; }

    [ReactProp]
    public bool? squared { get; set; }

    [ReactProp]
    public string color { get; set; }


    [ReactProp]
    public string size { get; set; }


    [ReactProp]
    public string src { get; set; }
}