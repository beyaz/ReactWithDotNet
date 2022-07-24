using System;
using System.Collections.Generic;
using System.Linq;
using static QuranAnalyzer.DataAccess;
using static QuranAnalyzer.VerseFilter;

namespace QuranAnalyzer;

[Serializable]
public class MatchInfo
{
    #region Public Properties
    public int ArabicCharacterIndex { get; set; }

    public bool HasNoMatch => ArabicCharacterIndex == -1;
    
    public int StartIndexInVerseText { get; set; }

    public Verse Verse { get; set; }
    #endregion

    #region Public Methods
    public override string ToString()
    {
        if (Verse != null)
        {
            if (ArabicCharacterIndex >= 0)
            {
                return harfler[ArabicCharacterIndex];
            }

            return Verse._text[StartIndexInVerseText].ToString();
        }

        return string.Empty;
    }
    #endregion
}

[Serializable]
public sealed class MushafOptions
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
    public const string Kaf = "ق";
    public const string Sad = "ص";
    public const string Sin = "س";
    public const string Ya = "ي";
    public const string Elif = "ا";
    public const string Mim = "م";
    public const string Lam = "ل";
    public const string Ra = "ر";

    public const string Kef = "ك";
    public const string Ha = "ه";
    public const string Ayn = "ع";

    public const string HH = "ح";

    public const string T = "ط";

    public const string Nun = "ن";
    #endregion
}

public static class QuranAnalyzerMixin
{
    #region Public Methods
    static Response<int> GetCountOf(this IReadOnlyList<MatchInfo> matchList, string character)
    {
        return character.AsArabicCharacterIndex().Then(arabicCharacterIndex => matchList.Count(x => x.ArabicCharacterIndex == arabicCharacterIndex));
    }

    public static Response<int> GetCountOfCharacter(IReadOnlyList<Verse> verseList, string character, MushafOptions option = null)
    {
        option ??= new MushafOptions();

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

    static List<MatchInfo> RecalculateWithOptions(List<MatchInfo> mathInfoList, MushafOptions mushafOptions, int arabicCharacterIndex)
    {
        if (mushafOptions != null)
        {
            if (mushafOptions.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten)
            {
                if (arabicCharacterIndex.AsArabicCharacter().Value == ArabicCharacters.Sad)
                {
                    mathInfoList.RemoveAll(x => x.Verse.Id == "7:69" && x.ArabicCharacterIndex == ArabicCharacters.Sad.AsArabicCharacterIndex().Value);
                }
            }
        }
     

        return mathInfoList;
    }

    public static Response<IReadOnlyList<MatchInfo>> SearchCharachters(string searchScript, string searchCharachters, MushafOptions mushafOptions = null)
    {
        var charachterList = searchCharachters.AsClearArabicCharacterList();

        var indexList = charachterList.Select(x => Array.IndexOf(harfler, x)).ToList();
        if (indexList.Count == 0)
        {
            return "En az bir harf girilmelidir.";
        }
        
        var items = new List<MatchInfo>();

        var verseListResponse = GetVerseList(searchScript);
        if (verseListResponse.IsFail)
        {
            return verseListResponse.FailMessage;
        }
        var verseList = verseListResponse.Value;

        bool canSelect(MatchInfo matchInfo)
        {
            return indexList.Contains(matchInfo.ArabicCharacterIndex);
        }

        foreach (var verse in verseList)
        {
            items.AddRange(AnalyzeVerse(verse).Where(canSelect));
        }

        foreach (var arabicCharacterIndex in indexList)
        {
            items = RecalculateWithOptions(items, mushafOptions, arabicCharacterIndex);
        }

        return items;
    }

   
    #endregion

    #region Methods
    public static Response<int> AsArabicCharacterIndex(this string character)
    {
        return harfler.GetIndex(character);
    }

    public static Response<string> AsArabicCharacter(this int arabicCharacterIndex)
    {
        if (arabicCharacterIndex >=0 && arabicCharacterIndex < harfler.Length)
        {
            return new Response<string>{Value = harfler[arabicCharacterIndex] };
        }

        return (Error)("İndex out of range for arabic letters. @index: "+ arabicCharacterIndex);
    }

    static string IdOf(Verse verse)
    {
        return $"{verse.ChapterNumber}:{verse._index}";
    }
    #endregion
}