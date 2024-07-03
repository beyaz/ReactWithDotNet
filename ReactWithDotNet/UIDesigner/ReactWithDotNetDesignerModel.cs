namespace ReactWithDotNet.UIDesigner;

[Serializable]
public sealed record ReactWithDotNetDesignerModel
{
    public string ClassFilter { get; init; }

    public string MethodFilter { get; init; }

    public int ScreenWidth { get; init; } = 900;

    public string SelectedAssemblyFilePath { get; init; }

    public MethodReference SelectedMethod { get; init; }

    public string SelectedTreeNodeKey { get; init; }

    public TypeReference SelectedType { get; init; }
    
    public bool PropertyPanelIsClosed { get; init; }

    public int ScreenHeight { get; init; } = 100;
    
    public bool IsMethodSelectionViewCollapsed { get; init; }
}

