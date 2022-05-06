using System;
using System.Collections.Generic;
using System.Linq;

namespace QuranAnalyzer;

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
        var charachterList = WordSearcher.ToList(searchCharachters);

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