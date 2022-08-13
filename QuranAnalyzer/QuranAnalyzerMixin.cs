namespace QuranAnalyzer;

public enum MushafId
{
    Tanzil = 1, RK = 2
}

public sealed class MushafTotalCountPerVerseDifference
{
    public string VerseId { get; init; }
    public int TotalCount { get; init; }
    public MushafId MushafId { get; init; }

    public MushafTotalCountPerVerseDifference(string verseId, int count, MushafId mushafId = MushafId.RK)
    {
        VerseId    = verseId;
        TotalCount = count;
        MushafId   = mushafId;
    }
}

public static class QuranAnalyzerMixin
{
    public static readonly IReadOnlyDictionary<int, IReadOnlyList<MushafTotalCountPerVerseDifference>> MushafDifferences = new Dictionary<int, IReadOnlyList<MushafTotalCountPerVerseDifference>>
    {
        [ArabicLetterIndex.Saad] = new[]
        {
            new MushafTotalCountPerVerseDifference("7:69",1,MushafId.Tanzil), // bestaten: *بَسْطَةً*
            new MushafTotalCountPerVerseDifference("7:69",0) // bestaten: *بَصْۜطَةً*
        },
        [ArabicLetterIndex.Laam] = new[]
        {
            new MushafTotalCountPerVerseDifference("11:70",8,MushafId.Tanzil),
            new MushafTotalCountPerVerseDifference("11:70",9)
        }

    };


    #region Public Methods
    public static int GetCountOfLetter(IReadOnlyList<Verse> verseList, int arabicLetterIndex, MushafOption option = null)
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
            return MushafDifferences[ArabicLetterIndex.Saad].First(x => x.MushafId == MushafId.RK && x.VerseId == "7:69").TotalCount;
        }

        if (arabicLetterIndex == ArabicLetterIndex.Laam && option.Use_Lam_SpecifiedByRK && SpecifiedByRK.Lam.ContainsKey(verse.Id))
        {
            return SpecifiedByRK.Lam[verse.Id];
        }

        return verse.AnalyzedFullText.Count(x => x.ArabicLetterIndex == arabicLetterIndex);
    }
    #endregion
}