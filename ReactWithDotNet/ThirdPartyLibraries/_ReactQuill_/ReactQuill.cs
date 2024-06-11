namespace ReactWithDotNet.ThirdPartyLibraries._ReactQuill_;

public sealed class ReactQuill : ThirdPartyReactComponent
{
    [ReactProp]
    public string theme { get; set; }

    [ReactProp]
    public string value { get; set; }
    
    [ReactProp]
    public Func<string,Task> onChange { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }

    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public Func<Task> valueBindDebounceHandler { get; set; }


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }

    [ReactProp]
    public Dictionary<string,dynamic> modules  { get; set; }
    
    
    [ReactProp]
    public string[] formats  { get; set; }
    
    [ReactProp]
    public string placeholder  { get; set; }
    
}
