namespace ReactWithDotNet.Libraries.uiw.react_codemirror;

public class CodeMirror : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.Libraries.uiw.react_codemirror.";
    
    [React]
    public string? value { get; set; }
    
    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public CodeMirrorBasicSetup basicSetup { get; } = new();

    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(CodeMirror) + "::OnChange")]
    public Action<string> onChange { get; set; }

    [React]
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

    [React]
    //[ReactTransformValueInClient(Prefix + nameof(CodeMirror) + "::ConvertToExtension")]
    public List<string> extensions { get; } = new List<string>();


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