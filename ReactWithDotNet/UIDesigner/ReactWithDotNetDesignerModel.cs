namespace ReactWithDotNet.UIDesigner;

[Serializable]
public class ReactWithDotNetDesignerModel
{
    public string ClassFilter { get; set; }

    public bool IsInstanceEditorActive { get; set; }

    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }

    public string MethodFilter { get; set; }

    public double ScreenWidth { get; set; } = 900;

    public string SelectedAssemblyFilePath { get; set; }

    public MethodReference SelectedMethod { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    public TypeReference SelectedType { get; set; }
    
    public bool PropertyPanelIsClosed { get; set; }
    
    public string ComponentElementTreeSelectedNodePath { get; set; }
}

