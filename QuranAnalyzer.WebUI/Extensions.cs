namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static IReadOnlyDictionary<string, (string reading, string tr)> TranslateMap = new Dictionary<string, (string reading, string tr)>
    {
        { "ايام", ("eyyam", "günler") },
        { "يومين", ("yevmeyn", "2 gün") },
        { "الايام", ("el-eyyam", "günler") },
        { "اياما", ("eyyamen", "günler") },
        { "واياما", ("ve eyyamen", "günler") },
        { "بايىم", ("bi-eyyam", "günler") }
    };

    public static string PageUrlOfDays30 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*|ايام;*|يومين;*|الايام;*|اياما;*|واياما;*|بايىم";

    public static string PageUrlOfDays365 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*|يوم;*|ويوم;*|اليوم;*|واليوم;*|يوما;*|ليوم;*|فاليوم;*|بيوم;*|باليوم;*|وباليوم";

    public static void ArabicKeyboardPressed(this Client client, string arabicLetter)
    {
        client.DispatchEvent(nameof(ArabicKeyboardPressed), arabicLetter);
    }

    public static string AsText(this IReadOnlyList<LetterInfo> letters)
    {
        return string.Join(string.Empty, letters);
    }

    public static string GetLetterCountingScript(string chapterFilter, params string[] arabicLetters)
    {
        return chapterFilter + "|" + string.Join(string.Empty, arabicLetters);
    }

    public static string GetPageLink(string pageId) => Settings.RootLocation + $"?{QueryKey.Page}=" + pageId;

    public static void HamburgerMenuClosed(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuClosed));
    }

    public static void HamburgerMenuOpened(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuOpened));
    }

    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

    public static void MainContentDivScrollChanged(this Client client, double mainDivScrollY)
    {
        client.DispatchEvent(nameof(MainContentDivScrollChanged));
    }

    public static void MushafOptionChanged(this Client client, MushafOption mushafOption)
    {
        client.DispatchEvent(nameof(MushafOptionChanged), mushafOption);
    }

    public static void OnArabicKeyboardPressed(this Client client, Action<string> handlerAction)
    {
        client.ListenEvent(ArabicKeyboardPressed, handlerAction);
    }

    public static void OnHamburgerMenuClosed(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuClosed, handler);
    }

    public static void OnHandleHamburgerMenuOpened(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuOpened, handler);
    }

    public static void OnMainContentDivScrollChanged(this Client client, Action<double> handlerAction)
    {
        client.ListenEvent(MainContentDivScrollChanged, handlerAction);
    }

    public static void OnMushafOptionChanged(this Client client, Action<MushafOption> handler)
    {
        client.ListenEvent(MushafOptionChanged, handler);
    }
}