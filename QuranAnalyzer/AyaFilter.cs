using System;
using System.Collections.Generic;
using static QuranAnalyzer.DataAccess;
using static QuranAnalyzer.FpExtensions;

namespace QuranAnalyzer;

public static class AyaFilter
{
    public static Response<IReadOnlyList<aya>> Filter(string searchScript)
    {
        var returnList = new List<aya>();

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


        Response<IReadOnlyList<aya>> process(string searchItem)
        {
            var arr = searchItem.Split(": ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 2)
            {
                return (Error) $"arama kriterlerinde hata var.{searchItem}";
            }

            return parseSureNumber()
               .Then(findSurahByNumber)
               .Then(sura=>collectAyaList(sura,arr[1]));

            Response<int> parseSureNumber()
            {
                return Try(() => int.Parse(arr[0]));
            }

            Response<sura> findSurahByNumber(int surahNumber)
            {
                if (surahNumber<=0 || surahNumber > AllSura.Length)
                {
                    return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                }

                return AllSura[--surahNumber];
            }

            

          



            Response<IReadOnlyList<aya>> collectAyaList(sura sura, string ayaFilter)
            {
                foreach (var aya in sura.aya)
                {
                    aya.SurahNumber = sura.Index;
                }

                if (ayaFilter == "*")
                {
                    return sura.aya;
                }

                Response<IReadOnlyList<aya>> selectOne(int ayahIndex)
                {
                    if (ayahIndex<=0 || ayahIndex > sura.aya.Length)
                    {
                        return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    return new []{sura.aya[--ayahIndex]};
                }

                return ayaFilter.Then(int.Parse).Then(selectOne);
            }
        }
    }
}