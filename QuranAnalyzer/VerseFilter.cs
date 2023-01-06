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
            return AllChapters.SelectMany(x => x.Verses).ToList();
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

        static Response<IReadOnlyList<Verse>> byRange(string begin, string end)
        {
            var verseBegin = getVerseById(begin);
            var verseEnd = getVerseById(end);

            if (verseBegin.IsFail)
            {
                return verseBegin.FailMessage;
            }

            if (verseEnd.IsFail)
            {
                return verseEnd.FailMessage;
            }

            if (verseBegin.Value.ChapterNumber > verseEnd.Value.ChapterNumber)
            {
                return $"Başlangıç {verseBegin.Value.ChapterNumber} bitişten {verseEnd.Value.ChapterNumber} büyük olamaz.";
            }

            var returnList = new List<Verse>();

            foreach (var chapter in AllChapters)
            {
                if (chapter.Index < verseBegin.Value.ChapterNumber || chapter.Index > verseEnd.Value.ChapterNumber)
                {
                    continue;
                }

                foreach (var verse in chapter.Verses)
                {
                    if (chapter.Index == verseBegin.Value.ChapterNumber && verse.IndexAsNumber < verseBegin.Value.IndexAsNumber )
                    {
                        continue;
                    }

                    if (chapter.Index == verseEnd.Value.ChapterNumber && verse.IndexAsNumber > verseEnd.Value.IndexAsNumber)
                    {
                        continue;
                    }
                    
                    returnList.Add(verse);
                }
            }

            return returnList;
        }

        static Response<Verse> getVerseById(string verseId)
        {
            var arr = verseId.Split(":".ToCharArray(),StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
            if (arr.Length != 2)
            {
                return (Error)$"arama kriterlerinde hata var.{verseId}";
            }

            var chapter = ParseInt(arr[0]).Then(findChapterByNumber);
            if (chapter.IsFail)
            {
                return chapter.FailMessage;
            }
            
            var verseNumber = ParseInt(arr[1]);
            if (verseNumber.IsFail)
            {
                return verseNumber.FailMessage;
            }

            if (verseNumber.Value <= 0 || verseNumber.Value > chapter.Value.Verses.Count)
            {
                return (Error)$"Sure seçiminde yanlışlık var.{verseId}";
            }

            return  chapter.Value.Verses[--verseNumber.Value];
        }
        
        static Response<Chapter> findChapterByNumber(int surahNumber)
        {
            if (surahNumber <= 0 || surahNumber > AllChapters.Count)
            {
                return (Error)$"Sure seçiminde yanlışlık var.{surahNumber}";
            }

            return AllChapters[--surahNumber];
        }

        Response<IReadOnlyList<Verse>> process(string searchItem)
        {
            if (searchItem.Trim() == "*")
            {
                return AllChapters.SelectMany(x => x.Verses).ToList();
            }

            // is range
            if (searchItem.Trim().Contains("-->", StringComparison.OrdinalIgnoreCase))
            {
                var range = searchItem.Split("-->".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                if (range.Length != 2)
                {
                    return (Error)$"arama kriterlerinde hata var.{searchItem}";
                }

                return byRange(range[0], range[1]);
            }

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

            

            Response<IReadOnlyList<Verse>> collectVerseList(Chapter sura, string verseFilter)
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

        return AllChapters[chapterNumber - 1].Verses[verseNumber - 1];
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