using System.Collections.Generic;

namespace QuranAnalyzer;

public static class QuranQuery
{
    public static IEnumerable<aya> ListOfVerseEndsWith(string value)
    {
        var valueList = WordSearcher.AsClearArabicCharacterList(value);

        foreach (var sura in DataAccess.AllSura)
        {
            foreach (var aya in sura.aya)
            {
                var list = WordSearcher.AsClearArabicCharacterList(aya._text);
                if (list.EndsWith(valueList))
                {
                    yield return aya;
                }
            }
        }
    }


    public static IEnumerable<(aya verse, int count)> ListOfVerseContains(string value)
    {
        var valueList = WordSearcher.AsClearArabicCharacterList(value);

        foreach (var sura in DataAccess.AllSura)
        {
            foreach (var aya in sura.aya)
            {
                var list = WordSearcher.AsClearArabicCharacterList(aya._text);

                var count = Mixin.Contains(list, valueList);
                if ( count> 0)
                {
                    yield return (aya,count);
                }
            }
        }
    }
}