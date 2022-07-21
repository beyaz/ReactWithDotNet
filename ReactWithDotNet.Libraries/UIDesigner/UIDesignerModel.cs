using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

[Serializable]
class UIDesignerModel
{
    public string JsonText { get; set; } = "";

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
}