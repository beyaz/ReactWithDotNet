namespace ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;

public class CodeMirror : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror.";
    
    [ReactProp]
    public string value { get; set; }
    
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public CodeMirrorBasicSetup basicSetup { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(CodeMirror) + "::OnChange")]
    public Action<string> onChange { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }

    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public Action valueBindDebounceHandler { get; set; }


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }

    [ReactProp]
    //[ReactTransformValueInClient(Prefix + nameof(CodeMirror) + "::ConvertToExtension")]
    public List<string> extensions { get; } = new();


    protected  override Element GetSuspenseFallbackElement()
    {
        return base.GetSuspenseFallbackElement() + MinHeight(300) + MinWidth(400);
    }
}


// https://uiwjs.github.io/react-codemirror/#/extensions/basic-setup
public sealed class CodeMirrorBasicSetup
{
    public bool? highlightActiveLine { get; set; }

    public bool? highlightActiveLineGutter { get; set; }
}