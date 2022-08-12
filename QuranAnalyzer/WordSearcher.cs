namespace QuranAnalyzer;

static class WordSearcher
{
    public static IReadOnlyList<int> Contains(IReadOnlyList<LetterInfo> sourceWord, IReadOnlyList<LetterInfo> searchWord)
    {
        if (sourceWord is null)
        {
            throw new ArgumentNullException(nameof(sourceWord));
        }

        if (searchWord is null)
        {
            throw new ArgumentNullException(nameof(searchWord));
        }

        sourceWord = sourceWord.Where(x => x.ArabicLetterIndex >= 0).ToList();
        searchWord = searchWord.Where(x => x.ArabicLetterIndex >= 0).ToList();

        if (searchWord.Count > sourceWord.Count)
        {
            return Array.Empty<int>();
        }

        var matchStartIndexList = new List<int>();

        for (var i = 0; i < sourceWord.Count; i++)
        {
            if (i + searchWord.Count >= sourceWord.Count)
            {
                return matchStartIndexList;
            }

            var isMatch = true;
            for (var j = 0; j < searchWord.Count; j++)
            {
                if (sourceWord[i + j].ArabicLetterIndex != searchWord[j].ArabicLetterIndex)
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                matchStartIndexList.Add(i);
            }
        }

        return matchStartIndexList;
    }
}