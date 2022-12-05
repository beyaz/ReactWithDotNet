namespace ReactWithDotNet.UIDesigner;

[Serializable]
public class UIDesignerModel
{
    public bool IsInstanceEditorActive { get; set; }

    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }

    public int ScreenWidth { get; set; } = 100;

    public string SelectedAssemblyFilePath { get; set; }

    public string SelectedComponentTypeReference { get; set; }

    public DotNetMemberSpecification SelectedDotNetMemberSpecification { get; set; } = new();
    
    public TypeReference SelectedType{ get; set; }
    
    public MethodReference SelectedMethod { get; set; }

    public string SelectedMethodTreeFilter { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }
}

[Serializable]
public class DotNetMemberSpecification
{
    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }
}