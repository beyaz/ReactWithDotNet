namespace QuranAnalyzer.WebUI;



static class PageId
{
    public const string MainPage = "1";
    public const string SecuringDataWithCurrentTechnology = "2";
    public const string PreInformation = "3";
    public const string InitialLetters = "4";
    public const string QuestionAnswerPage = "5";
    public const string ContactPage = "6";
    public const string CharacterCounting = "7";
    public const string WordSearchingPage = "8";
    public const string AlternativeSystems = "9";
    public const string Definition = "10";
    public const string PageIdOfMushafOptionsDetail = "11";
    public const string WhoIsReshadKhalifePage = "12";
    public const string PageVerseListContainsAllInitialLetters = "13";

    public const string WhyFamousPeopleAreSilentPage = "14"; 
}

static class QueryKey
{
    public static string Page = "p";
    public static string SearchQuery = "q";
    public static string FactIndex = "f";
    public static string ShowNumbers = "n";
}

static class ResourceAccess
{
    public static string Img(string fileName) => "wwwroot/img/" + fileName;
}

static class ContextKey
{
    public static ReactContextKey<MushafOption> MushafOptionKey = new(nameof(MushafOptionKey));
    public static ReactContextKey<bool> HamburgerMenuIsOpen = new(nameof(HamburgerMenuIsOpen));
}

class SettingsFile
{
    public string RootLocation { get; set; }
    public string LogFilePath { get; set; }
    public bool IsLogEnabled { get; set; }
}

static class App
{
    public static StyleModifier FontFamily_Lateef => FontFamily("Lateef, cursive");

    public static StyleModifier ComponentBorder => Border($"1px solid {BorderColor}");

    public static string BluePrimary => "#1976d2";

    public static string BorderColor = "#dee2e6";
}

public abstract class ReactComponent : ReactWithDotNet.ReactComponent
{
    protected string GetPronunciationOfArabicLetter(string arabicLetter)
    {
        return GetTurkishPronunciationOfArabicLetter(arabicLetter);
    }

    protected (string reading, string trMean)? GetPronunciationOfArabicWord(string arabicWord)
    {
        return GetTurkishPronunciationOfArabicWord(arabicWord);
    }

    protected IEnumerable<Element> AsLetter(string arabicLetter)
    {
        string pronunciation = GetPronunciationOfArabicLetter(arabicLetter);

        return new Element[] { (strong)pronunciation, "(", (strong)arabicLetter, ")" };
    }
}

public abstract class ReactComponent<TState> : ReactWithDotNet.ReactComponent<TState> where TState : new()
{
    protected string GetPronunciationOfArabicLetter(string arabicLetter)
    {
        return GetTurkishPronunciationOfArabicLetter(arabicLetter);
    }

    protected (string reading, string trMean)? GetPronunciationOfArabicWord(string arabicWord)
    {
        return GetTurkishPronunciationOfArabicWord(arabicWord);
    }

    protected IEnumerable<Element> AsLetter(string arabicLetter)
    {
        string pronunciation = GetPronunciationOfArabicLetter(arabicLetter);

        return new Element[] { (strong)pronunciation, "(", (strong)arabicLetter, ")" };
    }
}