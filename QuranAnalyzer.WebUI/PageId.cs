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
    public const string PageIdOfMushafOptionsDetail = "7";
    public const string WordSearchingPage = "8";
}

static class QueryKey
{
    public static string Page = "p";
    public static string SearchQuery = "q";
}

static class ResourceAccess
{
    public static string Img(string fileName) => ReactWithDotNetIntegration.RootFolderName + "/img/" +fileName;
}

class ApplicationEventName
{

    public static ClientEventInfo<string> ArabicKeyboardPressed = new(nameof(ArabicKeyboardPressed));
    public static ClientEventInfo<double> MainContentDivScrollChanged = new(nameof(MainContentDivScrollChanged));

    public static ClientEventInfo<MushafOption> MushafOptionChanged = new(nameof(MushafOptionChanged));

    public static ClientEventInfo OnHamburgerMenuClosed = new(nameof(OnHamburgerMenuClosed));
    public static ClientEventInfo OnHamburgerMenuOpened = new(nameof(OnHamburgerMenuOpened));
}