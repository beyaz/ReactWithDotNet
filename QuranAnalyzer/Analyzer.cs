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
        string line = verse._bismillah + verse._text;

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
                        Verse                 = verse
                    };
                }

                return null;
            }

            //Elif harfi için ayrıca tarama (+)
            if (i == 0)
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
            if (i == 10)
            {
                var matchInfo = tryMatch("ز");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ج"
            if (i == 4)
            {
                var matchInfo = tryMatch("ج");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ة"
            if (i == 25)
            {
                var matchInfo = tryMatch("ة");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ى" ve "ئ"
            if (i == 27)
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
            if (i == 26)
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