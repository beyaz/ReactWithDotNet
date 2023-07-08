namespace ReactWithDotNet.ThirdPartyLibraries.UIW.ReactTextareaCodeEditor;

public class CodeEditor : ThirdPartyReactComponent
{
    [ReactProp]
    public string language { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    [ReactProp]
    public int? padding { get; set; }

    [ReactProp]
    public string placeholder { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
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