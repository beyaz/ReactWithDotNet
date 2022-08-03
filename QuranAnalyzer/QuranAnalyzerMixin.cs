namespace QuranAnalyzer;

public static class QuranAnalyzerMixin
{
    #region Public Methods
    public static Response<int> GetCountOfCharacter(IReadOnlyList<Verse> verseList, string character, MushafOptions option = null)
    {
        option ??= new MushafOptions();

        Response<int> calculateCount(Verse verse)
        {
            if (character == ArabicLetter.Alif && option.UseElifCountsSpecifiedByRK && SpecifiedByRK.RealElifCounts.ContainsKey(verse.Id))
            {
                return SpecifiedByRK.RealElifCounts[verse.Id];
            }

            if (character == ArabicLetter.Saad && option.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten && verse.Id == "7:69")
            {
                return SpecifiedByRK.SS[verse.Id];
            }

            if (character == ArabicLetter.Laam && option.Use_Lam_SpecifiedByRK && SpecifiedByRK.Lam.ContainsKey(verse.Id))
            {
                return SpecifiedByRK.Lam[verse.Id];
            }

            return Analyzer.AnalyzeVerse(verse).GetCountOf(character);
        }

        return verseList.Sum(calculateCount);
    }

    public static Response<int> GetCountOfCharacter(IReadOnlyList<Verse> verseList, int arabicLetterIndex, MushafOptions option = null)
    {
        option ??= new MushafOptions();

        Response<int> calculateCount(Verse verse)
        {
            if (arabicLetterIndex == ArabicLetterIndex.Alif && option.UseElifCountsSpecifiedByRK && SpecifiedByRK.RealElifCounts.ContainsKey(verse.Id))
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

        return verseList.Sum(calculateCount);
    }
    #endregion

    #region Methods
    static Response<int> AsArabicCharacterIndex(this string character)
    {
        return ArabicLetter.AllArabicLetters.GetIndex(character);
    }

    static Response<int> GetCountOf(this IReadOnlyList<MatchInfo> matchList, string character)
    {
        return character.AsArabicCharacterIndex().Then(arabicCharacterIndex => matchList.Count(x => x.ArabicLetterIndex == arabicCharacterIndex));
    }
    #endregion
}