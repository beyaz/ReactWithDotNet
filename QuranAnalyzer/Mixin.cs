using System;
using System.Collections.Generic;
using System.Linq;
using static QuranAnalyzer.FpExtensions;

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
                return DataAccess.harfler[HarfIndex];
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
    #endregion
}

public static class Mixin
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

            return DataAccess.Analyze(verse).GetCountOf(character);
        }

        return verseList.Sum(calculateCount);
    }

    public static Response<IReadOnlyList<MatchInfo>> SearchCharachters(string searchScript, string searchCharachters)
    {
        var charachterList = searchCharachters.AsClearArabicCharacterList();

        var indexList = charachterList.Select(x => Array.IndexOf(DataAccess.harfler, x)).ToList();

        var items = new List<MatchInfo>();

        foreach (var aya in VerseFilter.GetVerseList(searchScript).Value)
        {
            items.AddRange(DataAccess.Analyze(aya).Where(x => indexList.Contains(x.HarfIndex)));
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
        return DataAccess.harfler.GetIndex(character);
    }

    static string IdOf(Verse verse)
    {
        return $"{verse.SurahNumber}:{verse._index}";
    }
    #endregion
}