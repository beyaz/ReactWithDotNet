
namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static string GetPageLink(string pageId) => App.Settings.RootLocation + $"?{QueryKey.Page}=" + pageId;
    
    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

   

   

    public static string GetLetterCountingScript(string chapterFilter, params string[] arabicLetters)
    {
        return chapterFilter + "|" + string.Join(string.Empty, arabicLetters);
    }
}