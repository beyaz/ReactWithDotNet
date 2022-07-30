using System.Globalization;

namespace QuranAnalyzer;

public static class Analyzer
{
    class AlternativeForm
    {
        public int ArabicLetterIndex { get; set; }
        public string[] Forms { get; set; }
    }

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
        new() { ArabicLetterIndex = ArabicLetterIndex.Alif, Forms = new[] { "ٱ", "إ", "أ", "ﺍ", "ء", "ٔ" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Zay, Forms  = new[] { "ز" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Jiim, Forms = new[] { "ج" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Haa_, Forms = new[] { "ة" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Yaa, Forms  = new[] { "ى", "ئ" } },
        new() { ArabicLetterIndex = ArabicLetterIndex.Waaw, Forms = new[] { "ٯ", "ؤ" } }
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
                    items.Add(new MatchInfo { ArabicLetterIndex = -1, StartIndexInVerseText = cursor, Verse = verse });

                    cursor++;

                    continue;
                }
            }

            throw new InvalidOperationException("Arabic letter is not recognized.");
        }

        return items;
    }

    static MatchInfo TryRead(Verse verse, int startIndex, bool isHemzeActive)
    {
        var alternativeForms = GetAlternativeForms(isHemzeActive);

        var line = verse._bismillah + verse._text;

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

        MatchInfo tryMatch(string searchCharacter, int arabicCharacterIndex)
        {
            var matchInfo = TryMatch(line, startIndex, searchCharacter, arabicCharacterIndex);
            if (matchInfo == null)
            {
                return null;
            }
            
            return new MatchInfo
            {
                StartIndexInVerseText = matchInfo.StartIndex,
                ArabicLetterIndex  = matchInfo.ArabicLetterIndex,
                Verse                 = verse,
                MatchedLetter         = matchInfo.Value
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
                StartIndex = startIndex,
                ArabicLetterIndex  = arabicLetterIndex,
                Value         = value
            };
        }

        return null;
    }
}


public sealed class LetterMatchInfo
{
    public int StartIndex { get; init; }
    public int ArabicLetterIndex { get; init; }
    public string Value { get; init; }

    public override string ToString()
    {
        return Value;
    }
}