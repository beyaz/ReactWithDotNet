namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static (string reading, string trMean)? GetTurkishPronunciationOfArabicWord(string arabicWord)
    {
        if (arabicWord == "ايام")
        {
            return ("eyyam", "günler");
        }
        if (arabicWord == "يومين")
        {
            return ("yevmeyn", "2 gün");
        }
        if (arabicWord == "الايام")
        {
            return ("el-eyyam", "günler");
        }
        if (arabicWord == "اياما")
        {
            return ("eyyamen", "günler");
        }
        if (arabicWord == "واياما")
        {
            return ("ve eyyamen", "günler");
        }
        if (arabicWord == "بايىم")
        {
            return ("bi-eyyam", "günler");
        }
        
        return null;
    }

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

   

    public static string GetTurkishPronunciationOfArabicLetter(string arabicLetter)
    {
        if (arabicLetter == ArabicLetter.Yaa)
        {
            return "Ye";
        }

        if (arabicLetter == ArabicLetter.Siin)
        {
            return "Sin";
        }

        if (arabicLetter == ArabicLetter.Qaaf)
        {
            return "Kaf";
        }

        if (arabicLetter == ArabicLetter.Ayn)
        {
            return "Ayn";
        }

        if (arabicLetter == ArabicLetter.Kaaf)
        {
            return "Kef";
        }

        if (arabicLetter == ArabicLetter.Haa)
        {
            return "Ha";
        }

        if (arabicLetter == ArabicLetter.Saad)
        {
            return "Sad";
        }

        if (arabicLetter == ArabicLetter.Haa_)
        {
            return "Ha";
        }

        if (arabicLetter == ArabicLetter.Alif)
        {
            return "Elif";
        }

        if (arabicLetter == ArabicLetter.Laam)
        {
            return "Lam";
        }

        if (arabicLetter == ArabicLetter.Miim)
        {
            return "Mim";
        }

        if (arabicLetter == ArabicLetter.Raa)
        {
            return "Ra";
        }

        if (arabicLetter == ArabicLetter.Taa_)
        {
            return "Ta";
        }

        if (arabicLetter == ArabicLetter.Nun)
        {
            return "Nun";
        }

        return null;
    }
}

