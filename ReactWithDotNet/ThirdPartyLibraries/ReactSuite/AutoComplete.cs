
namespace ReactWithDotNet.ThirdPartyLibraries.ReactSuite;

public abstract class AutoComplete : ElementBase
{
    [ReactProp]
    public string id { get; set; }
    
    [ReactProp]
    public string placeholder { get; set; }
}

[ReactRealType(typeof(AutoComplete))]
public sealed class AutoComplete<TRecord> : AutoComplete
{
    [ReactProp]
    public IEnumerable<TRecord> data { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(AutoComplete) + "::OnChange")]
    public Func<TRecord,Task> onChange { get; set; }

    [ReactProp]
    public TRecord value { get; set; }
    
    [ReactProp]
    [ReactTemplate(nameof(data))]
    public Func<TRecord,Element> renderMenuItem { get; set; }
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

public sealed class Uploader : ElementBase
{
    [ReactProp]
    public string action  { get; set; }
}