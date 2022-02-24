using System;
using System.Collections.Generic;

namespace QuranAnalyzer.WebUI;

[Serializable]
public sealed class MainMenuModel
{
    public string Text { get; set; }
    public string Id { get; set; }
}

[Serializable]
public sealed class CommonDataModel
{
    public string MainTitle { get; set; }
    public IReadOnlyList<MainMenuModel> MainMenuItems{ get; set; }

}