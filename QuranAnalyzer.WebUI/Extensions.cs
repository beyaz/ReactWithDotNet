namespace QuranAnalyzer.WebUI;

static class Extensions
{
    public static string BorderColor = "#dee2e6";

    public static string BluePrimary => "#1976d2";

    public static StyleModifier BorderRadiusForPanels => BorderRadius(5);

    public static StyleModifier ComponentBorder => Border($"1px solid {BorderColor}");
    public static StyleModifier FontFamily_Lateef => FontFamily("Lateef, cursive");

    public static int AsNumber(this bool value) =>
        value ? 1 : 0;

    public static string AsText(this IReadOnlyList<LetterInfo> letters)
    {
        return string.Join(string.Empty, letters);
    }

    public static string FileAtImgFolder(string fileName) => "wwwroot/img/" + fileName;

    public static string GetLetterCountingScript(string chapterFilter, params string[] arabicLetters)
    {
        return chapterFilter + "~" + string.Join(string.Empty, arabicLetters);
    }

    public static string GetPageLink(string pageId) => $"/?{QueryKey.Page}=" + pageId;

    public static string GetTurkishPronunciationOfArabicLetter(string arabicLetter)
    {
        if (ArabicLetter.Alif.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Alif;
        if (ArabicLetter.Baa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Baa;
        if (ArabicLetter.Taa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Taa;
        if (ArabicLetter.Thaa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Thaa;
        if (ArabicLetter.Jiim.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Jiim;
        if (ArabicLetter.Haa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Haa;
        if (ArabicLetter.Khaa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Khaa;
        if (ArabicLetter.Daal.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Daal;
        if (ArabicLetter.Dhaal.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Dhaal;
        if (ArabicLetter.Raa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Raa;
        if (ArabicLetter.Zay.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Zay;
        if (ArabicLetter.Siin.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Siin;
        if (ArabicLetter.Shiin.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Shiin;
        if (ArabicLetter.Saad.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Saad;
        if (ArabicLetter.Daad.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Daad;
        if (ArabicLetter.Taa_.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Taa_;
        if (ArabicLetter.Zaa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Zaa;
        if (ArabicLetter.Ayn.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Ayn;
        if (ArabicLetter.Ghayn.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Ghayn;
        if (ArabicLetter.Faa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Faa;
        if (ArabicLetter.Qaaf.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Qaaf;
        if (ArabicLetter.Kaaf.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Kaaf;
        if (ArabicLetter.Laam.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Laam;
        if (ArabicLetter.Miim.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Miim;
        if (ArabicLetter.Nun.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Nun;
        if (ArabicLetter.Haa_.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Haa_;
        if (ArabicLetter.Waaw.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Waaw;
        if (ArabicLetter.Yaa.EqualsArabicIgnoreCase(arabicLetter)) return ArabicLetterTurkishPronunciation.Yaa;

        return arabicLetter;
    }

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


        // i s a
        const string isa    = "عيسي";
        const string ve_isa = "وعيسي";
        const string ya_isa = "يعيسي";
        const string bi_isa = "بعيسي";

        return arabicWord switch
        {
            isa => ("isa", "isa"),
            ve_isa => ("ve isa", "ve isa"),
            ya_isa => ("ya isa", "ya isa"),
            bi_isa => ("bi isa", "bi isa"),
            
            _ =>null
        };

        return null;
    }

    public static bool HasNoValue(this string value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

    public static void MainContentDivScrollChangedOverZero(this Client client, double mainDivScrollY)
    {
        client.DispatchEvent(nameof(MainContentDivScrollChangedOverZero));
    }

    public static void OnMainContentDivScrollChangedOverZero(this Client client, Action<double> handlerAction)
    {
        client.ListenEvent(MainContentDivScrollChangedOverZero, handlerAction);
    }
}