using System.Globalization;

namespace QuranAnalyzer;

public static class Analyzer
{
    static readonly AlternativeForm[] AlternativeForms =
    {
        new() { ArabicLetterIndex = ArabicLetterIndex.Alif, Forms = new[] { "ٱ", "إ", "أ", "ﺍ" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Zay, Forms  = new[] { "ز" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Jiim, Forms = new[] { "ج" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Haa_, Forms = new[] { "ة" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Yaa, Forms  = new[] { "ى", "ئ" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Waaw, Forms = new[] { "ٯ", "ؤ" } }
    };

    static readonly AlternativeForm[] AlternativeFormsWithHamza =
    {
        new() { ArabicLetterIndex = ArabicLetterIndex.Alif, Forms = new[] { "ٱ", "إ", "أ", "ﺍ", "ء" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Zay, Forms  = new[] { "ز" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Jiim, Forms = new[] { "ج" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Haa_, Forms = new[] { "ة" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Yaa, Forms  = new[] { "ى", "ئ" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Waaw, Forms = new[] { "ٯ", "ؤ" } }
    };

    public static bool HasValueAndSameAs(this LetterMatchInfo a, LetterMatchInfo b)
    {
        if (a is null || b is null)
        {
            return false;
        }

        if (a.ArabicLetterIndex >= 0 && a.ArabicLetterIndex == b.ArabicLetterIndex)
        {
            return true;
        }

        return false;
    }
    
    public static IReadOnlyList<LetterMatchInfo> AnalyzeText(string line, bool isHemzeActive = true)
    {
        var items = new List<LetterMatchInfo>();

        var cursor = 0;

        while (cursor < line.Length)
        {
            var item = TryRead(line, cursor, isHemzeActive);
            if (item != null)
            {
                items.Add(item);
                cursor += ArabicLetter.AllArabicLetters[item.ArabicLetterIndex].Length;
                continue;
            }

            // check is special char like space or
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(line[cursor]);

                if (unicodeCategory == UnicodeCategory.NonSpacingMark ||
                    unicodeCategory == UnicodeCategory.SpaceSeparator ||
                    unicodeCategory == UnicodeCategory.ModifierLetter ||
                    line[cursor] == '۩')
                {
                    items.Add(new LetterMatchInfo { ArabicLetterIndex = -1, StartIndex = cursor, MatchedLetter = line[cursor].ToString() });

                    cursor++;

                    continue;
                }
            }

            throw new InvalidOperationException("Arabic letter is not recognized.");
        }

        return items;
    }

    public static IReadOnlyList<MatchInfo> AnalyzeVerse(Verse verse, bool isHemzeActive = true)
    {
        var line = verse._bismillah + verse._text;

        return AnalyzeText(line, isHemzeActive).Select(asMatchInfo).ToList();

        MatchInfo asMatchInfo(LetterMatchInfo x)
        {
            return new MatchInfo
            {
                ArabicLetterIndex     = x.ArabicLetterIndex,
                MatchedLetter         = x.MatchedLetter,
                StartIndexInVerseText = x.StartIndex,
                Verse                 = verse
            };
        }
    }
    

    static LetterMatchInfo TryMatch(string line, int startIndex, string searchLetter, int arabicLetterIndex)
    {
        if (startIndex + searchLetter.Length > line.Length)
        {
            return null;
        }

        var value = line.Substring(startIndex, searchLetter.Length);

        var isMatch = value == searchLetter;
        if (isMatch)
        {
            return new LetterMatchInfo
            {
                ArabicLetterIndex = arabicLetterIndex,
                MatchedLetter     = value,
                StartIndex        = startIndex,
            };
        }

        return null;
    }

    static LetterMatchInfo TryRead(string line, int startIndex, bool isHemzeActive)
    {
        var alternativeForms = getAlternativeForms(isHemzeActive);

        for (var arabicLetterIndex = 0; arabicLetterIndex < ArabicLetter.AllArabicLetters.Length; arabicLetterIndex++)
        {
            foreach (var alternativeForm in alternativeForms)
            {
                if (alternativeForm.ArabicLetterIndex == arabicLetterIndex)
                {
                    foreach (var form in alternativeForm.Forms)
                    {
                        var matchInfo = tryMatch(form, arabicLetterIndex);
                        if (matchInfo != null)
                        {
                            return matchInfo;
                        }
                    }
                }
            }

            // normal match
            {
                var matchInfo = tryMatch(ArabicLetter.AllArabicLetters[arabicLetterIndex], arabicLetterIndex);
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }
        }

        return null;

        LetterMatchInfo tryMatch(string searchCharacter, int arabicCharacterIndex) => TryMatch(line, startIndex, searchCharacter, arabicCharacterIndex);
        static AlternativeForm[] getAlternativeForms(bool isHemzeActive) => isHemzeActive ? AlternativeFormsWithHamza : AlternativeForms;
    }
    
    class AlternativeForm
    {
        public int ArabicLetterIndex { get; init; }
        public string[] Forms { get; init; }
    }
}

public sealed class LetterMatchInfo
{
    public int ArabicLetterIndex { get; init; }
    public string MatchedLetter { get; init; }
    public int StartIndex { get; init; }
    
    public override string ToString()
    {
        return MatchedLetter;
    }
}