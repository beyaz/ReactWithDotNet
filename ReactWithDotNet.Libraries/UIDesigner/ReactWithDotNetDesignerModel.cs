namespace ReactWithDotNet.UIDesigner;

[Serializable]
public class ReactWithDotNetDesignerModel
{
    public bool IsInstanceEditorActive { get; set; }

    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }

    public int ScreenWidth { get; set; } = 100;

    public string SelectedAssemblyFilePath { get; set; }

    public MethodReference SelectedMethod { get; set; }

    public string SelectedMethodTreeFilter { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    public TypeReference SelectedType { get; set; }
}

