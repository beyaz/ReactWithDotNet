
namespace QuranAnalyzer;

public static class QuranQuery
{
    static bool IsValidForWordSearch(LetterInfo info)
    {
        if (info.ArabicLetterIndex >= 0 && info.MatchedLetter != Analyzer.HamzaAbove)
        {
            return true;
        }

        return false;
    }

    public static IReadOnlyList<IReadOnlyList<LetterInfo>> GetWords(this IReadOnlyList<LetterInfo> text)
    {
        var returnList = new List<IReadOnlyList<LetterInfo>>();
        
        var length = text.Count;

        var currentWord = new List<LetterInfo>();
        
        for (var i = 0; i < length; i++)
        {
            if (text[i].MatchedLetter == " ")
            {
                if (currentWord.Count == 0)
                {
                    continue;
                }
                
                returnList.Add(currentWord);

                currentWord = new List<LetterInfo>();
                
                continue;
            }

            currentWord.Add(text[i]);
        }

        if (currentWord.Count>0)
        {
            returnList.Add(currentWord);
        }

        return returnList;
    }
    
    public static IReadOnlyList<(LetterInfo start, LetterInfo end)> GetStartAndEndPointsOfSameWords(this Verse verse, IReadOnlyList<LetterInfo> searchWord)
    {
        var returnList = new List<(LetterInfo start, LetterInfo end)>();

        foreach (var word in verse.TextWordList)
        {
            if (word.HasValueAndSame(searchWord))
            {
                returnList.Add((word.First(), word.Last()));
            }
        }

        return returnList;
    }

    public static bool HasValueAndSame(this IReadOnlyList<LetterInfo> a, IReadOnlyList<LetterInfo> b)
    {
        if (a == null || b == null)
        {
            return false;
        }

        a = a.Where(IsValidForWordSearch).ToList();
        b = b.Where(IsValidForWordSearch).ToList();

        if (a.Count != b.Count)
        {
            return false;
        }

        var length = a.Count;

        for (var i = 0; i < length; i++)
        {
            if (!a[i].HasValueAndSameAs(b[i]))
            {
                return false;
            }
        }

        return true;
    }

    public static bool EndsWith(this IReadOnlyList<LetterInfo> source, IReadOnlyList<LetterInfo> search)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (search is null)
        {
            throw new ArgumentNullException(nameof(search));
        }

        source = source.Where(IsValidForWordSearch).ToList();
        search = search.Where(IsValidForWordSearch).ToList();

        if (search.Count > source.Count)
        {
            return false;
        }

        var sourceIndex = source.Count - search.Count;

        for (var i = 0; i < search.Count; i++)
        {
            if (source[sourceIndex + i].ArabicLetterIndex != search[i].ArabicLetterIndex)
            {
                return false;
            }
        }

        return true;
    }

    public static int Contains(this IReadOnlyList<LetterInfo> source, IReadOnlyList<LetterInfo> search)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (search is null)
        {
            throw new ArgumentNullException(nameof(search));
        }

        source = source.Where(IsValidForWordSearch).ToList();
        search = search.Where(IsValidForWordSearch).ToList();

        if (search.Count > source.Count)
        {
            return 0;
        }

        var count = 0;
        for (var i = 0; i < source.Count; i++)
        {
            if (i + search.Count >= source.Count)
            {
                return count;
            }

            var difference = i;

            var isMatch = true;
            for (var j = 0; j < search.Count; j++)
            {
                if (source[difference + j].ArabicLetterIndex != search[j].ArabicLetterIndex)
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
}