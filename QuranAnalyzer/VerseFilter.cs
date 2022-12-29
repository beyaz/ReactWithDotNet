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

        var items = searchScript.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(x=>x.Trim());

        foreach (var item in items)
        {
            var shouldExtract = item[0]=='-';
            
            var response   = process(item.RemoveFromStart("-"));
            if (response.IsFail)
            {
                return response;
            }

            if (shouldExtract)
            {
                returnList.RemoveAll(x=>response.Value.Any(y=>y.Id==x.Id));
            }
            else
            {
                returnList.AddRange(response.Value);
            }
        }

        return returnList;

        Response<IReadOnlyList<Verse>> process(string searchItem)
        {
            var arr = searchItem.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (arr.Length != 2)
            {
                return (Error) $"arama kriterlerinde hata var.{searchItem}";
            }

            return parseChapterNumber()
                  .Then(findChapterByNumber)
                  .Then(sura => collectVerseList(sura, arr[1]));

            Response<int> parseChapterNumber()
            {
                return ParseInt(arr[0]);
            }

            Response<Surah> findChapterByNumber(int surahNumber)
            {
                if (surahNumber <= 0 || surahNumber > AllSurahs.Count)
                {
                    return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                }

                return AllSurahs[--surahNumber];
            }

            Response<IReadOnlyList<Verse>> collectVerseList(Surah sura, string verseFilter)
            {
                var filters = verseFilter.Split("-".ToCharArray()).Where(x=>!string.IsNullOrWhiteSpace(x)).Select(x=>x.Trim()).ToArray();


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
                    return Apply(selectMultipe, ParseInt(filters[0]), ParseInt(filters[1]));
                }
                
                return (Error)$"Sure seçiminde yanlışlık var.{searchItem}";


                Response<IReadOnlyList<Verse>> selectOne(int verseIndex)
                {
                    if (verseIndex <= 0 || verseIndex > sura.Verses.Count)
                    {
                        return (Error) $"Sure seçiminde yanlışlık var.{searchItem}";
                    }

                    return new[] {sura.Verses[--verseIndex]};
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

    public static Verse GetVerseById(string verseId)
    {
        var arr = verseId.Split(':');

        var chapterNumber = int.Parse(arr[0]);
        var verseNumber = int.Parse(arr[1]);

        return AllSurahs[chapterNumber - 1].Verses[verseNumber - 1];
    }
    
}

public class VerseNumberComparer : IComparer<string>
{
    public int Compare(string verseIdA, string verseIdB)
    {
        if (verseIdA == null)
        {
            throw new ArgumentNullException(nameof(verseIdA));
        }

        if (verseIdB == null)
        {
            throw new ArgumentNullException(nameof(verseIdB));
        }

        if (verseIdA == verseIdB)
        {
            return 0;
        }
        
        var a = verseIdA.Split(':');
        var b = verseIdB.Split(':');

        if (int.Parse(a[0]) > int.Parse(b[0]))
        {
            return 1;
        }

        if (int.Parse(a[0]) == int.Parse(b[0]))
        {
            if (int.Parse(a[1]) > int.Parse(b[1]))
            {
                return 1;
            }
        }

        return -1;
    }
}