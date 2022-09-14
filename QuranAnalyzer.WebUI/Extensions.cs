
namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static string GetPageLink(string pageId) => App.Settings.RootLocation + $"?{QueryKey.Page}=" + pageId;
    
    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);


    public static string PageUrlOfDays365 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*|يوم;*|ويوم;*|اليوم;*|واليوم;*|يوما;*|ليوم;*|فاليوم;*|بيوم;*|باليوم;*|وباليوم";
    public static string PageUrlOfDays30 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*|ايام;*|يومين;*|الايام;*|اياما;*|واياما;*|بايىم";

    public static string AsText(this IReadOnlyList<LetterInfo> letters)
    {
        return string.Join(string.Empty, letters);
    }

    public static string GetLetterCountingScript(string chapterFilter, params string[] arabicLetters)
    {
        return chapterFilter + "|" + string.Join(string.Empty, arabicLetters);
    }

    public static IReadOnlyDictionary<string, (string reading, string tr)> TranslateMap = new Dictionary<string, (string reading, string tr)>
    {
        {"ايام",("eyyam","günler")},
        {"يومين",("yevmeyn","2 gün")},
        {"الايام",("el-eyyam","günler")},
        {"اياما",("eyyamen","günler")},
        {"واياما",("ve eyyamen","günler")},
        {"بايىم",("bi-eyyam","günler")}
    };

}