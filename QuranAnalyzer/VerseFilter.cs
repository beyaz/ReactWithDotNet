using System;
using System.Collections.Generic;
using System.Linq;
using static QuranAnalyzer.DataAccess;
using static QuranAnalyzer.FpExtensions;

namespace QuranAnalyzer;

public static class VerseFilter
{
    #region Public Methods
    public static Response<IReadOnlyList<Verse>> GetVerseList(string searchScript)
    {
        if (string.IsNullOrWhiteSpace(searchScript))
        {
            return "Arama kriteri boş olamaz";
        }

        if (searchScript.Trim() == "*")
        {
            return AllSurahs.SelectMany(x => x.Verses).ToList();
        }
        
        var returnList = new List<Verse>();

        var items = searchScript.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var item in items)
        {
            var response = process(item);
            if (response.IsFail)
            {
                return response;
            }

            returnList.AddRange(response.Value);
        }

        return returnList;

        Response<IReadOnlyList<Verse>> process(string searchItem)
        {
            var arr = searchItem.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 2)
            {
                return (Error) $"arama kriterlerinde hata var.{searchItem}";
            }

            return parseSureNumber()
                  .Then(findSurahByNumber)
                  .Then(sura => collectVerseList(sura, arr[1]));

            Response<int> parseSureNumber()
            {
                return ParseInt(arr[0]);
            }

            Response<Surah> findSurahByNumber(int surahNumber)
            {
                if (surahNumber <= 0 || surahNumber > AllSurahs.Length)
                {
                    return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                }

                return AllSurahs[--surahNumber];
            }

            Response<IReadOnlyList<Verse>> collectVerseList(Surah sura, string ayaFilter)
            {
                foreach (var aya in sura.Verses)
                {
                    aya.ChapterNumber = sura.Index;
                }

                var filters = ayaFilter.Split("-".ToCharArray()).Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x=>x.Trim()).ToArray();


                if (filters.Length == 1)
                {
                    if (filters[0] == "*")
                    {
                        return sura.Verses.ToArray();
                    }

                    return ParseInt(filters[0]).Then(selectOne);
                }

                if (filters.Length == 2)
                {
                    // return Apply(selectMultipe, Apply(ParseInt,filters[0]), Apply(ParseInt,filters[1]));
                    return Apply(selectMultipe, ParseInt(filters[0]), ParseInt(filters[1]));
                }
                
                return (Error)$"Sure seçiminde yanlışlık var.{searchItem}";


                Response<IReadOnlyList<Verse>> selectOne(int ayahIndex)
                {
                    if (ayahIndex <= 0 || ayahIndex > sura.Verses.Count)
                    {
                        return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    return new[] {sura.Verses[--ayahIndex]};
                }

                Response<IReadOnlyList<Verse>> selectMultipe(int verseStartIndex, int verseEndIndex)
                {
                    if (verseStartIndex <= 0 || verseStartIndex > sura.Verses.Count)
                    {
                        return (Error)$"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    if (verseEndIndex <= 0 || verseEndIndex > sura.Verses.Count)
                    {
                        return (Error)$"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    if (verseStartIndex > verseEndIndex)
                    {
                        return (Error)$"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    return sura.Verses.ToList().GetRange(verseStartIndex - 1, verseEndIndex - verseStartIndex + 1);
                }
            }
        }
    }
    #endregion
}