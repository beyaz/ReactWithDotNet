namespace ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor;

public class Editor : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.";

    [ReactProp]
    [ReactTransformValueInClient(Prefix + "GetHighlightFunction")]
    public string highlight { get; set; }

    [ReactProp]
    public int? padding { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = "onValueChange")]
    public Expression<Func<string>> valueBind { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public Action valueBindDebounceHandler { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }

    protected override Element GetSuspenseFallbackElement()
    {
        return base.GetSuspenseFallbackElement() + MinHeight(300) + MinWidth(400);
    }
}