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
    public string height { get; set; }

    [ReactProp]
    public string defaultLanguage { get; set; }


    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact::OnChange")]
    public Action<string> onChange { get; set; }
    

    [ReactProp]
    public string defaultValue { get; set; }
    
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic options { get; } = new ExpandoObject();
    
    
    
    
}
