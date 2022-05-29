using System;
using System.Collections.Generic;
using System.Linq;
using static QuranAnalyzer.DataAccess;
using static QuranAnalyzer.FpExtensions;
using static QuranAnalyzer.VerseFilter;

namespace QuranAnalyzer;

[Serializable]
public class MatchInfo
{
    #region Public Properties
    public int HarfIndex { get; set; }

    public bool HasNoMatch => HarfIndex == -1;
    public int StartIndex { get; set; }

    public Verse verse { get; set; }
    #endregion

    #region Public Methods
    public override string ToString()
    {
        if (verse != null)
        {
            if (HarfIndex >= 0)
            {
                return harfler[HarfIndex];
            }

            return verse._text[StartIndex].ToString();
        }

        return string.Empty;
    }
    #endregion
}

public sealed class CountingOption
{
    #region Public Properties
    public bool UseElifCountsSpecifiedByRK { get; set; }
    public bool Use_Sad_in_Surah_7_Verse_69_in_word_bestaten { get; set; }
    public bool Use_Lam_SpecifiedByRK { get; set; }

    #endregion
}

public static class ArabicCharacters
{
    #region Static Fields
    public static string Kaf = "ق";
    public static string Sad = "ص";
    public static string Sin = "س";
    public static string Ya = "ي";
    public static string Elif = "ا";
    public static string Mim = "م";
    public static string Lam = "ل";
    public static string Ra = "ر";
    #endregion
}

public static class QuranAnalyzerMixin
{
    #region Public Methods
    static Response<int> GetCountOf(this IReadOnlyList<MatchInfo> matchList, string character)
    {
        return Pipe(character.AsArabicCharacterIndex(), arabicCharacterIndex => matchList.Count(x => x.HarfIndex == arabicCharacterIndex));
    }

    public static Response<int> GetCountOfCharacter(IReadOnlyList<Verse> verseList, string character, CountingOption option = null)
    {
        option ??= new CountingOption();

        Response<int> calculateCount(Verse verse)
        {
            if (character == ArabicCharacters.Elif && option.UseElifCountsSpecifiedByRK && SpecifiedByRK.RealElifCounts.ContainsKey(IdOf(verse)))
            {
                return SpecifiedByRK.RealElifCounts[IdOf(verse)];
            }

            if (character == ArabicCharacters.Sad && option.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten && IdOf(verse) == "7:69")
            {
                return SpecifiedByRK.SS[IdOf(verse)];
            }

            if (character == ArabicCharacters.Lam && option.Use_Lam_SpecifiedByRK && SpecifiedByRK.Lam.ContainsKey(IdOf(verse)))
            {
                return SpecifiedByRK.Lam[IdOf(verse)];
            }

            return AnalyzeVerse(verse).GetCountOf(character);
        }

        return verseList.Sum(calculateCount);
    }

    public static Response<IReadOnlyList<MatchInfo>> SearchCharachters(string searchScript, string searchCharachters)
    {
        var charachterList = searchCharachters.AsClearArabicCharacterList();

        var indexList = charachterList.Select(x => Array.IndexOf(harfler, x)).ToList();

        var items = new List<MatchInfo>();

        foreach (var verse in GetVerseList(searchScript).Value)
        {
            items.AddRange(AnalyzeVerse(verse).Where(x => indexList.Contains(x.HarfIndex)));
        }

        return items;
    }

    public static Response<IReadOnlyList<MatchInfo>> SearchCharachtersWithCache(string searchScript, string searchCharachters)
    {
        var key = searchScript + "|" + searchCharachters;

        return CachedAccess.AccessValue(key, () => SearchCharachters(searchScript, searchCharachters));
    }
    #endregion

    #region Methods
    static Response<int> AsArabicCharacterIndex(this string character)
    {
        return harfler.GetIndex(character);
    }

    static string IdOf(Verse verse)
    {
        return $"{verse.SurahNumber}:{verse._index}";
    }
    #endregion
}