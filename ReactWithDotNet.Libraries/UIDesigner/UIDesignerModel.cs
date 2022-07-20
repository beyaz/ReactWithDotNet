using System;
using System.Collections.Generic;

namespace ReactWithDotNet.UIDesigner;

[Serializable]
class UIDesignerModel
{
    public string ReactWithDotnetComponentAsJson { get; set; }

    public int ScreenWidth { get; set; } = 100;

    public string SelectedAssembly { get; set; }
    public string SelectedAssemblyLastQuery { get; set; }
    public string SelectedComponentTypeReference { get; set; }
    public string SelectedFolder { get; set; }

    public string SelectedFolderLastQuery { get; set; }

    public IReadOnlyList<string> SelectedFolderSuggestions { get; set; } = new[] { @"d:\boa\server\bin\", @"d:\boa\client\bin\" };

    public string SelectedMethodTreeNodeKey { get; set; }
}