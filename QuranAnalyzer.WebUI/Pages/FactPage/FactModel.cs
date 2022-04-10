using System;

namespace QuranAnalyzer.WebUI.Pages.FactPage;

[Serializable]
public sealed class FactModel
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string SearchScript { get; set; }
    public string SearchCharacters { get; set; }
}