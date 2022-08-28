namespace QuranAnalyzer.WebUI;

static class PageId
{
    public const string MainPage = "1";
    public const string SecuringDataWithCurrentTechnology = "2";
    public const string InitialLetters = "3";
    public const string QuestionAnswerPage = "4";
    public const string ContactPage = "5";
    public const string CharacterCounting = "6";
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
    public const string BackdropClicked = nameof(BackdropClicked);
    public const string OnHamburgerMenuClicked = nameof(OnHamburgerMenuClicked);
    public const string ArabicKeyboardPressed = nameof(ArabicKeyboardPressed);
    public const string MainContentDivScrollChanged = nameof(MainContentDivScrollChanged);

    public const string MushafOptionChanged = nameof(MushafOptionChanged);
}