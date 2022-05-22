using System.Collections.Generic;

namespace QuranAnalyzer;

public static class QuranQuery
{
    public static IEnumerable<Verse> ListOfVerseEndsWith(string value)
    {
        var valueList = value.AsClearArabicCharacterList();

        foreach (var sura in DataAccess.AllSurahs)
        {
            foreach (var aya in sura.Verses)
            {
                var list = aya._text.AsClearArabicCharacterList();
                if (list.EndsWith(valueList))
                {
                    yield return aya;
                }
            }
        }
    }


    public static IEnumerable<(Verse verse, int count)> ListOfVerseContains(string value)
    {
        var valueList = value.AsClearArabicCharacterList();

        foreach (var sura in DataAccess.AllSurahs)
        {
            foreach (var aya in sura.Verses)
            {
                var list = aya._text.AsClearArabicCharacterList();

                var count = list.Contains(valueList);
                if ( count> 0)
                {
                    yield return (aya,count);
                }
            }
        }
    }
}