namespace ReactWithDotNet.UIDesigner;

[Serializable]
public class UIDesignerModel
{
    public int? MetadataToken { get; set; }

    public int ScreenWidth { get; set; } = 100;

    public string SelectedAssemblyFilePath { get; set; }

    public string SelectedComponentTypeReference { get; set; }

    public DotNetMemberSpecification SelectedDotNetMemberSpecification { get; set; } = new();

    public string SelectedMethodName { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    public string SelectedMethodTreeFilter{ get; set; }
    public bool IsInstanceEditorActive { get; set; }
}

[Serializable]
public class DotNetMemberSpecification
{
    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }
}