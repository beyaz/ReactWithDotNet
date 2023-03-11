namespace QuranAnalyzer.WebUI.Pages.PageInitialLetters;

static class Extensions
{
    public static IEnumerable<Element> AsMultipleOf19(this int total)
    {
        var detail = new small { "(", (b)"19", "x", (total / 19).ToString(), ")" };

        return new Element[] { total.ToString(), detail };
    }

    public static string GetUrlOfLetterCountingSearchScript(string searchScriptOfLetterCounting)
    {
        return $"?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={searchScriptOfLetterCounting}";
    }
}