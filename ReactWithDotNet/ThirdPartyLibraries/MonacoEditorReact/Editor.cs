namespace ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

public class Editor : ThirdPartyReactComponent
{
    public Editor()
    {
    }

    public Editor(params StyleModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public string width { get; set; }

    [ReactProp]
    public string height { get; set; }

    [ReactProp]
    public string defaultLanguage { get; set; }


    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact::OnChange")]
    public Func<string,Task> onChange { get; set; }
    

    [ReactProp]
    public string defaultValue { get; set; }
    
    [ReactProp]
    public string value { get; set; }
    
    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = "onChange")]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public Expression<Func<string>> valueBind { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public Func<Task> valueBindDebounceHandler { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }
    
    
    /// <summary>
    /// https://microsoft.github.io/monaco-editor/typedoc/variables/editor.EditorOptions.html
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic options { get; } = new ExpandoObject();
    
    
    
    
}
