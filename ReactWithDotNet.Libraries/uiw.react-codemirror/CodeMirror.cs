namespace ReactWithDotNet.Libraries.uiw.react_codemirror;

public class CodeMirror : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.Libraries.uiw.react_codemirror.";
    
    [React]
    public string? value { get; set; }

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public CodeMirrorOption options { get; } = new();

    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(CodeMirror) + "::OnChange")]
    public Action<string> onChange { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    [ReactTransformValueInClient(Prefix + nameof(CodeMirror) + "::ConvertToExtension")]
    public List<string> extensions { get; } = new List<string>();
}


public sealed class CodeMirrorOption
{
    public string? mode { get; set; }
    
    public string? placeholder { get; set; }

    public bool? highlightActiveLine { get; set; }
}