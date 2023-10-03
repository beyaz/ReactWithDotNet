namespace ReactWithDotNet.ThirdPartyLibraries.ReactSuite;

public sealed class AutoComplete : ElementBase
{
    [ReactProp]
    public IEnumerable<string> data { get; set; }

    [ReactProp]
    public string id { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(AutoComplete) + "::OnChange")]
    public Action<string> onChange { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    public string placeholder { get; set; }
}



public sealed class Modal : ElementBase
{
    [ReactProp]
    public bool? open  { get; set; }
    
    [ReactProp]
    public Action onClose { get; set; }
    
    
    public sealed class Header : ElementBase;
    
    public sealed class Body : ElementBase;
}