using System.IO;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

[Serializable]
class UIDesignerModel
{
    public DotNetMemberSpecification SelectedDotNetMemberSpecification { get; set; } = new();

    public int ScreenWidth { get; set; } = 100;

    public string SelectedAssembly { get; set; }
    public string SelectedAssemblyLastQuery { get; set; }
    public string SelectedComponentTypeReference { get; set; }
    public string SelectedFolder { get; set; }

    public string SelectedFolderLastQuery { get; set; }

    public IReadOnlyList<string> SelectedFolderSuggestions { get; set; } = new[]
    {
        Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + Path.DirectorySeparatorChar
    };

    public string SelectedMethodTreeNodeKey { get; set; }

    public int? MetadataToken { get; set; }
    
    public string SelectedMethodName { get; set; }
}

[Serializable]
class DotNetMemberSpecification
{
    public string JsonTextForDotNetInstanceProperties { get; set; }

    public string JsonTextForDotNetMethodParameters { get; set; }
}