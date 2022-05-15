using System;
using System.Collections.Generic;
using System.Linq;
using static  QuranAnalyzer.FpExtensions;

namespace QuranAnalyzer;

public sealed class CountingOption
{
    public bool UseElifCountsSpecifiedByRK { get; set; }
}

public static class ArabicCharacters
{
    public static string Sin = "س";
    public static string Ya = "ي";
}



public static class Mixin
{
    #region Public Methods

   
    public static bool EndsWith(this IReadOnlyList<string> source, IReadOnlyList<string> search)
    {
        if (search.Count > source.Count)
        {
            return false;
        }

        var sourceIndex = source.Count - search.Count;

        for (var i = 0; i < search.Count; i++)
        {
            if (source[sourceIndex + i] != search[i])
            {
                return false;
            }
        }

        return true;
    }

    public static int Contains(IReadOnlyList<string> source, IReadOnlyList<string> search)
    {
        if (search.Count > source.Count)
        {
            return 0;
        }

        var count = 0;
        for (int i = 0; i < source.Count; i++)
        {
            if (i + search.Count >= source.Count)
            {
                return count;
            }

            var difference = i;

            var isMatch = true;
            for (int j = 0; j < search.Count; j++)
            {
                if (source[difference + j] != search[j])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                count++;
            }
        }

        return count;
    }

    public static int GetCountOfCharacter(int characterIndex, int[] chapterNumbers)
    {
        var count = 0;

        foreach (var chapterNumber in chapterNumbers)
        {
            foreach (var aya in DataAccess.AllSura[chapterNumber - 1].aya)
            {
                count += DataAccess.Analyze(aya).Count(x => x.HarfIndex == characterIndex);
            }
        }

        return count;
    }

    static string IdOf(Verse verse)
    {
        return $"{verse.SurahNumber}:{verse._index}";
    }
    public static Response<int> GetCountOfCharacter(IReadOnlyList<Verse> verseList, string character , CountingOption option = null)
    {
        option ??= new CountingOption();

        Response<int> calculateCount(Verse verse)
        {
            if (character == "ا" && option.UseElifCountsSpecifiedByRK && SpecifiedByRK.RealElifCounts.ContainsKey(IdOf(verse)))
            {
                return SpecifiedByRK.RealElifCounts[IdOf(verse)];
            }

            return DataAccess.Analyze(verse).GetCountOf(character);
        }

        return verseList.Sum(calculateCount);
    }

    public static Response<int> Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Response<int>> selector)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return source.Aggregate(0, selector, (total, value) => total + value);
    }

    public static Response<TAccumulate> Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, Response<TAccumulate>> func, Func<TAccumulate, TAccumulate, TAccumulate> acumulate)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        TAccumulate result = seed;
        foreach (TSource element in source)
        {
            var response = func(element);
            if (response.IsFail)
            {
                return response.Errors.ToArray();
            }

            result = acumulate(result, response.Value);
        }

        return result;
    }

    public static Response<int> GetCountOf(this IReadOnlyList<MatchInfo> matchList, string character)
    {
        return Pipe(character.AsArabicCharacterIndex(), arabicCharacterIndex => matchList.Count(x => x.HarfIndex == arabicCharacterIndex));
    }

    static Response<int> AsArabicCharacterIndex(this string character)
    {
        return DataAccess.harfler.GetIndex(character);
    }

    public static int GetCountOfCharacter(string character, int[] chapterNumbers)
    {
        return GetCountOfCharacter(Array.IndexOf(DataAccess.harfler, character), chapterNumbers);
    }

    public static bool HasValueAndSame(this IReadOnlyList<string> a, IReadOnlyList<string> b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        if (a.Count != b.Count)
        {
            return false;
        }

        var length = a.Count;

        for (int i = 0; i < length; i++)
        {
            if (a[i] != b[i])
            {
                return false;
            }
        }

        return true;
    }

    public static Response<IReadOnlyList<MatchInfo>> SearchCharachters(string searchScript, string searchCharachters)
    {
        // var charachterList = searchCharachters.Split(", ", StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim()).ToArray();
        var charachterList = searchCharachters.AsClearArabicCharacterList();

        var indexList = charachterList.Select(x => Array.IndexOf(DataAccess.harfler, x)).ToList();

        var items = new List<MatchInfo>();

        foreach (var aya in AyaFilter.Filter(searchScript).Value)
        {
            items.AddRange(DataAccess.Analyze(aya).Where(x => indexList.Contains(x.HarfIndex)));
        }

        return items;
    }


    
    
    


    public static Response<IReadOnlyList<MatchInfo>> SearchCharachtersWithCache(string searchScript, string searchCharachters)
    {
        var key = searchScript + "|" + searchCharachters;

        return CachedAccess.AccessValue(key, ()=>SearchCharachters(searchScript, searchCharachters));
    }




    #endregion



}