using System;
using System.Collections.Generic;
using System.Linq;
using static QuranAnalyzer.DataAccess;
using static QuranAnalyzer.VerseFilter;

namespace QuranAnalyzer;

public static class QuranAnalyzerMixin
{
    #region Public Methods
    public static Response<string> AsArabicCharacter(this int arabicCharacterIndex)
    {
        if (arabicCharacterIndex >= 0 && arabicCharacterIndex < harfler.Length)
        {
            return new Response<string> { Value = harfler[arabicCharacterIndex] };
        }

        return (Error)("İndex out of range for arabic letters. @index: " + arabicCharacterIndex);
    }

    public static Response<int> AsArabicCharacterIndex(this string character)
    {
        return harfler.GetIndex(character);
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
    static Response<int> GetCountOf(this IReadOnlyList<MatchInfo> matchList, string character)
    {
        return character.AsArabicCharacterIndex().Then(arabicCharacterIndex => matchList.Count(x => x.ArabicCharacterIndex == arabicCharacterIndex));
    }

    static string IdOf(Verse verse)
    {
        return $"{verse.ChapterNumber}:{verse._index}";
    }

    static List<MatchInfo> RecalculateWithOptions(List<MatchInfo> mathInfoList, MushafOptions mushafOptions, int arabicCharacterIndex)
    {
        if (mushafOptions != null)
        {
            if (mushafOptions.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten)
            {
                var sadIndex = ArabicCharacters.Sad.AsArabicCharacterIndex().Value;

                if (arabicCharacterIndex == sadIndex)
                {
                    mathInfoList.RemoveAll(x => x.Verse.Id == "7:69" && x.ArabicCharacterIndex == sadIndex);
                }
            }

            if (mushafOptions.UseElifCountsSpecifiedByRK)
            {
                var alif = ArabicCharacters.Elif.AsArabicCharacterIndex().Value;
                if (alif == arabicCharacterIndex)
                {
                    mathInfoList.RemoveAll(x => SpecifiedByRK.RealElifCounts.ContainsKey(x.Verse.Id));
                }
            }
        }

        return mathInfoList;
    }
    #endregion
}