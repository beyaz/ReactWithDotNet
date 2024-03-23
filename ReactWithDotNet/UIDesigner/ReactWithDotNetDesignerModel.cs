namespace ReactWithDotNet.UIDesigner;

[Serializable]
public class ReactWithDotNetDesignerModel
{
    public string ClassFilter { get; set; }

    public bool IsInstanceEditorActive { get; set; }

    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }

    public string MethodFilter { get; set; }

    public int ScreenWidth { get; set; } = 900;

    public string SelectedAssemblyFilePath { get; set; }

    public MethodReference SelectedMethod { get; set; }

    public string SelectedTreeNodeKey { get; set; }

    public TypeReference SelectedType { get; set; }
    
    public bool PropertyPanelIsClosed { get; set; }

    public int ScreenHeight { get; set; } = 100;
}

