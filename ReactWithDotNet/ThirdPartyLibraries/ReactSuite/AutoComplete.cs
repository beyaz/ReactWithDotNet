
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

    /// <summary>
    ///    callback function after successful upload
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateRemoteMethodArguments")]
    public onSuccessHandler onSuccess { get; set; }

    public delegate Task onSuccessHandler(object response, FileType fileType, object evnt, object request);
}

public sealed class FileType
{
    public object blobFile { get; set; }
    public string name { get; set; }
    public string fileKey { get; set; }
    public string status { get; set; }
    public int? progress { get; set; }
    public string url { get; set; }
}