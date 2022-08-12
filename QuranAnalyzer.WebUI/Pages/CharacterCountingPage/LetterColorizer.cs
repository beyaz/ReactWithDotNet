using System.Text;
using static QuranAnalyzer.LetterColor;

namespace QuranAnalyzer;

public class LetterColorizer : ReactComponent
{
    public string ChapterNumber { get; set; }
    public string LettersForColorize { get; set; }
    public IReadOnlyList<LetterInfo> LettersForColorizeNodes { get; set; }
    public MushafOption option { get; set; }

    public Verse Verse { get; set; }
    public string VerseNumber { get; set; }
    public string VerseText { get; set; }
    public IReadOnlyList<LetterInfo> VerseTextNodes { get; set; }
    public override Element render()
    {
        var verseText = VerseTextNodes ??= Analyzer.AnalyzeText(VerseText).Where(Analyzer.IsArabicLetter).ToList();

        var lettersForColorize = LettersForColorizeNodes ??= Analyzer.AnalyzeText(LettersForColorize).Where(Analyzer.IsArabicLetter).ToList();

        var cursor = 0;

        var counts = new int[lettersForColorize.Count];

        var html = new StringBuilder();

        for (var i = 0; i < verseText.Count; i++)
        {
            for (var j = 0; j < lettersForColorize.Count; j++)
            {
                if (verseText[i].ArabicLetterIndex == lettersForColorize[j].ArabicLetterIndex)
                {
                    var len = verseText[i].MatchedLetter.Length;

                    html.Append(VerseText.Substring(cursor, verseText[i].StartIndex - cursor));

                    var span = new span
                    {
                        innerText = verseText[i].MatchedLetter,
                        style =
                        {
                            color        = GetColor(j),
                            border       = "1px dashed rgb(218, 220, 224)",
                            borderRadius = "4px",
                            fontWeight   = "bold"
                        }
                    };

                    html.Append(span);

                    cursor = verseText[i].StartIndex + len;

                    counts[j]++;

                    break;
                }
            }
        }

        if (cursor < VerseText.Length - 1)
        {
            html.Append(VerseText.Substring(cursor));
        }

        Element verseId = new legend
        {
            text = $"{ChapterNumber}:{VerseNumber}",
            style =
            {
                //marginLeft       = "10px", 
                //marginTop        = "-8px",
                //background       = "white",
                //paddingLeftRight = "5px",
                fontSize   = "0.9rem",
                marginLeft = "1px"
            }
        };

        var countsView = new HPanel
        {
            style =
            {
                padding        = "5px",
                justifyContent = "space-between",
                fontSize       = "0.9rem",
                flexWrap       = "wrap"
            },
        };

        for (var j = 0; j < lettersForColorize.Count; j++)
        {
            var countView = new HPanel
            {
                children =
                {
                    new div { text = lettersForColorize[j].MatchedLetter, style = { color           = GetColor(j) } },
                    new div { text = ":", style                                 = { marginLeftRight = "4px" } },
                    new div { text = counts[j].ToString() },
                    GetExtra(lettersForColorize[j].ArabicLetterIndex)
                },
                style = { marginLeft = "10px" }
            };

            countsView.appendChild(countView);
        }

        var textView = new div
        {
            innerHTML = html.ToString(),
            style =
            {
                fontSize    = "1.4rem",
                padding     = "5px",
                fontFamily  = "Lateef, cursive",
                direction   = "rtl",
                marginRight = "auto"
            }
        };

        verseId = new legend
        {
            style = { display = "flex", flexDirection = "row", alignItems = "center" },
            children =
            {
                new div
                {
                    text = $"{ChapterNumber}:{VerseNumber}",
                    style =
                    {
                        fontSize   = "0.8rem",
                        marginLeft = "1px"
                    }
                },
                countsView
            }
        };

        return new fieldset
        {
            children = { verseId, new VSpace(5), textView },
            style =
            {
                marginTop    = "5px",
                border       = "1px dashed rgb(218, 220, 224)",
                borderRadius = "4px",

                display       = "flex",
                flexDirection = "column",
                alignItems    = "flex-start"
            }
        };
    }
    Element GetExtra(int arabicLetterIndex)
    {
        if (Verse == null)
        {
            return null;
        }

        if (option == null)
        {
            return null;
        }

        if (arabicLetterIndex == ArabicLetterIndex.Alif &&
            option.UseElifReferencesFromTanzil == false &&
            SpecifiedByRK.RealElifCounts.ContainsKey(Verse.Id))
        {
            var r = SpecifiedByRK.RealElifCounts[Verse.Id];

            var tanzil = SpecifiedByRK.TanzilElifCounts[Verse.Id];

            if (tanzil > r)
            {
                return new div { text = "+" + (tanzil - r) };
            }

            return new div { text = "-" + (r - tanzil) };
        }

        return null;
    }
}