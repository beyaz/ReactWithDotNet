namespace QuranAnalyzer;

public static class Analyzer
{
    static readonly string[] AlifCombination = { "ٱ", "إ", "أ", "ﺍ" };
    static readonly string[] AlifCombinationWithHamza = { "ٱ", "إ", "أ", "ﺍ", "ء", "ٔ" };

    class AlternativeForm
    {
        public int ArabicLetterIndex { get; set; }
        public string[] Forms { get; set; }
    }

    static readonly AlternativeForm[] AlternativeForms = 
    {
        new(){ ArabicLetterIndex  = ArabicLetterIndex.Alif, Forms = new []{ "ٱ", "إ", "أ", "ﺍ" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Zay, Forms  = new []{ "ز" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Jiim, Forms = new []{ "ج" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Haa_, Forms = new []{ "ة" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Yaa, Forms  = new []{ "ى", "ئ" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Waaw, Forms  = new []{ "ٯ", "ؤ" } }
    };
    
    static readonly AlternativeForm[] AlternativeFormsWithHamza =
    {
        new(){ ArabicLetterIndex  = ArabicLetterIndex.Alif, Forms = new []{ "ٱ", "إ", "أ", "ﺍ", "ء", "ٔ" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Zay, Forms  = new []{ "ز" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Jiim, Forms = new []{ "ج" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Haa_, Forms = new []{ "ة" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Yaa, Forms  = new []{ "ى", "ئ" } },
        new (){ ArabicLetterIndex = ArabicLetterIndex.Waaw, Forms = new []{ "ٯ", "ؤ" } }
    };

    static AlternativeForm[] GetAlternativeForms(bool isHemzeActive) => isHemzeActive ? AlternativeFormsWithHamza : AlternativeForms;

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
        var alternativeForms  = GetAlternativeForms(isHemzeActive);

        var line = verse._bismillah + verse._text;


        MatchInfo tryMatch(string searchCharacter, int arabicCharacterIndex)
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
                    ArabicCharacterIndex  = arabicCharacterIndex,
                    Verse                 = verse,
                    MatchedLetter         = value
                };
            }

            return null;
        }

        for (var i = 0; i < ArabicLetter.AllArabicLetters.Length; i++)
        {
            

            foreach (var alternativeForm in alternativeForms)
            {
                if (alternativeForm.ArabicLetterIndex == i)
                {
                    foreach (var form in alternativeForm.Forms)
                    {
                        var matchInfo = tryMatch(form, i);
                        if (matchInfo != null)
                        {
                            return matchInfo;
                        }
                    }
                }
                
            }

            // normal match
            {
                var matchInfo = tryMatch(ArabicLetter.AllArabicLetters[i], i);
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }
        }

        return null;
    }
}