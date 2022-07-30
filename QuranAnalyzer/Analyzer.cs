namespace QuranAnalyzer;

public static class Analyzer
{
    static readonly string[] AlifCombination = { "ٱ", "إ", "أ", "ﺍ" };
    static readonly string[] AlifCombinationWithHamza = { "ٱ", "إ", "أ", "ﺍ", "ء", "ٔ" };
    
    public static IReadOnlyList<MatchInfo> AnalyzeVerse(Verse verse, bool isHemzeActive = true)
    {
        var line = verse._bismillah + verse._text;

        var items = new List<MatchInfo>();

        var cursor = 0;

        while (cursor < line.Length)
        {
            var item = TryRead(verse, cursor, isHemzeActive);
            if (item != null)
            {
                items.Add(item);
                cursor += ArabicLetter.AllArabicLetters[item.ArabicCharacterIndex].Length;
                continue;
            }

            items.Add(new MatchInfo { ArabicCharacterIndex = -1, StartIndexInVerseText = cursor, Verse = verse });

            cursor++;
        }

        return items;
    }
    
    static MatchInfo TryRead(Verse verse, int startIndex, bool isHemzeActive)
    {
        var line = verse._bismillah + verse._text;

        for (var i = 0; i < ArabicLetter.AllArabicLetters.Length; i++)
        {
            MatchInfo tryMatch(string searchCharacter)
            {
                if (startIndex + searchCharacter.Length > line.Length)
                {
                    return null;
                }

                var value = line.Substring(startIndex, searchCharacter.Length);

                var isMatch = value == searchCharacter;
                if (isMatch)
                {
                    return new MatchInfo
                    {
                        StartIndexInVerseText = startIndex,
                        ArabicCharacterIndex  = i,
                        Verse                 = verse,
                        MatchedLetter      = value
                    };
                }

                return null;
            }

            //Elif harfi için ayrıca tarama (+)
            if (i == ArabicLetterIndex.Alif)
            {
                foreach (var item in isHemzeActive ? AlifCombinationWithHamza : AlifCombination)
                {
                    var matchInfo = tryMatch(item);
                    if (matchInfo != null)
                    {
                        return matchInfo;
                    }
                }
            }

            //harfi için ayrıca tarama (+)"ز"
            if (i == ArabicLetterIndex.Zay)
            {
                var matchInfo = tryMatch("ز");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ج"
            if (i == ArabicLetterIndex.Jiim)
            {
                var matchInfo = tryMatch("ج");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ة"
            if (i == ArabicLetterIndex.Haa_)
            {
                var matchInfo = tryMatch("ة");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ى" ve "ئ"
            if (i == ArabicLetterIndex.Yaa)
            {
                var matchInfo = tryMatch("ى");
                if (matchInfo != null)
                {
                    return matchInfo;
                }

                matchInfo = tryMatch("ئ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ٯ" ve "ؤ"
            if (i == ArabicLetterIndex.Waaw)
            {
                var matchInfo = tryMatch("ٯ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }

                matchInfo = tryMatch("ؤ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // normal match
            {
                var matchInfo = tryMatch(ArabicLetter.AllArabicLetters[i]);
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }
        }

        return null;
    }
}