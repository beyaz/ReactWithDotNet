using QuranAnalyzer.WebUI.Components;

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

    public static string PageUrlOfDays30 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*~ايام;*~يومين;*~الايام;*~اياما;*~واياما;*~بايىم";

    public static string PageUrlOfDays365 => GetPageLink(PageId.WordSearchingPage) + "&" + QueryKey.SearchQuery + "=" + "*~يوم;*~ويوم;*~اليوم;*~واليوم;*~يوما;*~ليوم;*~فاليوم;*~بيوم;*~باليوم;*~وباليوم";

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
        return chapterFilter + "~" + string.Join(string.Empty, arabicLetters);
    }

    public static string GetPageLink(string pageId) =>  $"/?{QueryKey.Page}=" + pageId;

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

    public static void MainContentDivScrollChangedOverZero(this Client client, double mainDivScrollY)
    {
        client.DispatchEvent(nameof(MainContentDivScrollChangedOverZero));
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

    public static void OnMainContentDivScrollChangedOverZero(this Client client, Action<double> handlerAction)
    {
        client.ListenEvent(MainContentDivScrollChangedOverZero, handlerAction);
    }

   

    public static string GetTurkishPronunciationOfArabicLetter(string arabicLetter)
    {
        switch (arabicLetter)
        {
            case ArabicLetter.Alif: return ArabicLetterTurkishPronunciation.Alif;
            case ArabicLetter.Baa: return ArabicLetterTurkishPronunciation.Baa;
            case ArabicLetter.Taa: return ArabicLetterTurkishPronunciation.Taa;
            case ArabicLetter.Thaa: return ArabicLetterTurkishPronunciation.Thaa;
            case ArabicLetter.Jiim: return ArabicLetterTurkishPronunciation.Jiim;
            case ArabicLetter.Haa: return ArabicLetterTurkishPronunciation.Haa;
            case ArabicLetter.Khaa: return ArabicLetterTurkishPronunciation.Khaa;
            case ArabicLetter.Daal: return ArabicLetterTurkishPronunciation.Daal;
            case ArabicLetter.Dhaal: return ArabicLetterTurkishPronunciation.Dhaal;
            case ArabicLetter.Raa: return ArabicLetterTurkishPronunciation.Raa;
            case ArabicLetter.Zay: return ArabicLetterTurkishPronunciation.Zay;
            case ArabicLetter.Siin: return ArabicLetterTurkishPronunciation.Siin;
            case ArabicLetter.Shiin: return ArabicLetterTurkishPronunciation.Shiin;
            case ArabicLetter.Saad: return ArabicLetterTurkishPronunciation.Saad;
            case ArabicLetter.Daad: return ArabicLetterTurkishPronunciation.Daad;
            case ArabicLetter.Taa_: return ArabicLetterTurkishPronunciation.Taa_;
            case ArabicLetter.Zaa: return ArabicLetterTurkishPronunciation.Zaa;
            case ArabicLetter.Ayn: return ArabicLetterTurkishPronunciation.Ayn;
            case ArabicLetter.Ghayn: return ArabicLetterTurkishPronunciation.Ghayn;
            case ArabicLetter.Faa: return ArabicLetterTurkishPronunciation.Faa;
            case ArabicLetter.Qaaf: return ArabicLetterTurkishPronunciation.Qaaf;
            case ArabicLetter.Kaaf: return ArabicLetterTurkishPronunciation.Kaaf;
            case ArabicLetter.Laam: return ArabicLetterTurkishPronunciation.Laam;
            case ArabicLetter.Miim: return ArabicLetterTurkishPronunciation.Miim;
            case ArabicLetter.Nun: return ArabicLetterTurkishPronunciation.Nun;
            case ArabicLetter.Haa_: return ArabicLetterTurkishPronunciation.Haa_;
            case ArabicLetter.Waaw: return ArabicLetterTurkishPronunciation.Waaw;
            case ArabicLetter.Yaa: return ArabicLetterTurkishPronunciation.Yaa;
        }
        
        return arabicLetter;
    }

    public static HtmlElementModifier SrcArrowDown =>
        Src("data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/PjwhRE9DVFlQRSBzdmcgIFBVQkxJQyAnLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4nICAnaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkJz48c3ZnIGhlaWdodD0iNTEycHgiIGlkPSJMYXllcl8xIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgdmVyc2lvbj0iMS4xIiB2aWV3Qm94PSIwIDAgNTEyIDUxMiIgd2lkdGg9IjUxMnB4IiB4bWw6c3BhY2U9InByZXNlcnZlIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIj48cG9seWdvbiBwb2ludHM9IjM5Ni42LDE2MCA0MTYsMTgwLjcgMjU2LDM1MiA5NiwxODAuNyAxMTUuMywxNjAgMjU2LDMxMC41ICIvPjwvc3ZnPg==");

    public static HtmlElementModifier SrcArrowUp =>
        Src("data:image/svg+xml;base64,PD94bWwgdmVyc2lvbj0iMS4wIiA/PjwhRE9DVFlQRSBzdmcgIFBVQkxJQyAnLS8vVzNDLy9EVEQgU1ZHIDEuMS8vRU4nICAnaHR0cDovL3d3dy53My5vcmcvR3JhcGhpY3MvU1ZHLzEuMS9EVEQvc3ZnMTEuZHRkJz48c3ZnIGhlaWdodD0iNTEycHgiIGlkPSJMYXllcl8xIiBzdHlsZT0iZW5hYmxlLWJhY2tncm91bmQ6bmV3IDAgMCA1MTIgNTEyOyIgdmVyc2lvbj0iMS4xIiB2aWV3Qm94PSIwIDAgNTEyIDUxMiIgd2lkdGg9IjUxMnB4IiB4bWw6c3BhY2U9InByZXNlcnZlIiB4bWxucz0iaHR0cDovL3d3dy53My5vcmcvMjAwMC9zdmciIHhtbG5zOnhsaW5rPSJodHRwOi8vd3d3LnczLm9yZy8xOTk5L3hsaW5rIj48cG9seWdvbiBwb2ludHM9IjM5Ni42LDM1MiA0MTYsMzMxLjMgMjU2LDE2MCA5NiwzMzEuMyAxMTUuMywzNTIgMjU2LDIwMS41ICIvPjwvc3ZnPg==");

    public static int AsNumber(this bool value) =>
        value ? 1 : 0;
}

