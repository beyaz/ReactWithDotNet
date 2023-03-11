namespace QuranAnalyzer.WebUI.Pages.PageAllInitialLettersCombined;

public class InitialLetterCountInfo
{
    public string Count { get; set; }

    public List<CountInfo> Details { get; set; }
    public string Text { get; set; }
}

public class CountInfo
{
    public int ChapterNumber { get; set; }
    public string Count { get; set; }
}