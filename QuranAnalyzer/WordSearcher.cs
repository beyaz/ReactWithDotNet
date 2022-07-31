namespace QuranAnalyzer;

static class WordSearcher
{
    public static int Contains(IReadOnlyList<LetterMatchInfo> source, int startIndex, IReadOnlyList<LetterMatchInfo> search)
    {
        if (source is null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (search is null)
        {
            throw new ArgumentNullException(nameof(search));
        }

        source = source.Where(x => x.ArabicLetterIndex >= 0).ToList();
        search = search.Where(x => x.ArabicLetterIndex >= 0).ToList();

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