namespace ReactWithDotNet.UIDesigner;

[Serializable]
public sealed record ReactWithDotNetDesignerModel
{
    public string ClassFilter { get; init; }

    public bool IsInstanceEditorActive { get; set; }

    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }

    public string MethodFilter { get; init; }

    public int ScreenWidth { get; set; } = 900;

    public string SelectedAssemblyFilePath { get; set; }

    public MethodReference SelectedMethod { get; set; }

    public string SelectedTreeNodeKey { get; set; }

    public TypeReference SelectedType { get; set; }
    
    public bool PropertyPanelIsClosed { get; set; }

    public int ScreenHeight { get; set; } = 100;
    
    public bool IsMethodSelectionViewCollapsed { get; init; }
}

