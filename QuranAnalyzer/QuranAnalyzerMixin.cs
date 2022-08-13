namespace QuranAnalyzer;

public static class QuranAnalyzerMixin
{
    #region Public Methods
    public static Response<int> GetCountOfLetter(IReadOnlyList<Verse> verseList, int arabicLetterIndex, MushafOption option = null)
    {
        option ??= new MushafOption();

        return verseList.Select(x => GetCountOfLetterInVerse(x, arabicLetterIndex, option)).Sum();
    }

    public static int GetCountOfLetterInVerse(Verse verse, int arabicLetterIndex, MushafOption option)
    {
        if (verse == null)
        {
            throw new ArgumentNullException(nameof(verse));
        }

        if (option == null)
        {
            throw new ArgumentNullException(nameof(option));
        }

        if (arabicLetterIndex == ArabicLetterIndex.Alif &&
            option.UseElifReferencesFromTanzil == false &&
            SpecifiedByRK.RealElifCounts.ContainsKey(verse.Id))
        {
            return SpecifiedByRK.RealElifCounts[verse.Id];
        }

        if (arabicLetterIndex == ArabicLetterIndex.Saad && option.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten && verse.Id == "7:69")
        {
            return SpecifiedByRK.SS[verse.Id];
        }

        if (arabicLetterIndex == ArabicLetterIndex.Laam && option.Use_Lam_SpecifiedByRK && SpecifiedByRK.Lam.ContainsKey(verse.Id))
        {
            return SpecifiedByRK.Lam[verse.Id];
        }

        return verse.AnalyzedFullText.Count(x => x.ArabicLetterIndex == arabicLetterIndex);
    }
    #endregion
}