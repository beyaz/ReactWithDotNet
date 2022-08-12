using System.Text;
using static QuranAnalyzer.LetterColor;

namespace QuranAnalyzer;

public class LetterColorizer : ReactComponent
{
    public string ChapterNumber { get; set; }
    public string LettersForColorize { get; set; }
    public IReadOnlyList<LetterInfo> LettersForColorizeNodes { get; set; }
    public MushafOption MushafOption { get; set; }

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

        foreach (var letterInfo in verseText)
        {
            for (var j = 0; j < lettersForColorize.Count; j++)
            {
                if (letterInfo.ArabicLetterIndex == lettersForColorize[j].ArabicLetterIndex)
                {
                    var len = letterInfo.MatchedLetter.Length;

                    html.Append(VerseText.Substring(cursor, letterInfo.StartIndex - cursor));

                    var span = new span
                    {
                        innerText = letterInfo.MatchedLetter,
                        style =
                        {
                            color        = GetColor(j),
                            border       = "1px dashed rgb(218, 220, 224)",
                            borderRadius = "4px",
                            fontWeight   = "bold"
                        }
                    };

                    html.Append(span);

                    cursor = letterInfo.StartIndex + len;

                    counts[j]++;

                    break;
                }
            }
        }

        if (cursor < VerseText.Length - 1)
        {
            html.Append(VerseText.Substring(cursor));
        }

        var countsView = new HPanel
        {
            style =
            {
                padding        = "5px",
                justifyContent = "center",
                flexWrap       = "wrap"
            },
        };

        for (var j = 0; j < lettersForColorize.Count; j++)
        {
            var countView = new HPanel
            {
                children =
                {
                    new div { text = lettersForColorize[j].MatchedLetter, style = { color = GetColor(j), fontWeight = "bold"} },
                    
                    new div { text = ":", style = { marginLeftRight = "4px" } },
                    
                    new div { text = counts[j].ToString(), style = { fontSize = "0.78rem" }},
                    
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

        var verseId = new div
        {
            text = $"{ChapterNumber}:{VerseNumber}",
            style =
            {
                fontSize   = "0.8rem",
                fontWeight = "bold",
                marginLeft = "2px"
            }
        };
        
        var topLegend = new legend
        {
            style = { display = "flex", flexDirection = "row", alignItems = "center" },
            children =
            {
                verseId,
                countsView
            }
        };

        return new fieldset
        {
            children = { topLegend, new VSpace(5), textView },
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

        if (MushafOption == null)
        {
            return null;
        }

        if (arabicLetterIndex == ArabicLetterIndex.Alif &&
            MushafOption.UseElifReferencesFromTanzil == false &&
            SpecifiedByRK.RealElifCounts.ContainsKey(Verse.Id))
        {
            var alifCount = SpecifiedByRK.RealElifCounts[Verse.Id];

            var alifCountAccordingToTanzil = SpecifiedByRK.TanzilElifCounts[Verse.Id];

            if (alifCountAccordingToTanzil > alifCount)
            {
                return new div { text = "+" + (alifCountAccordingToTanzil - alifCount) };
            }

            return new div { text = "-" + (alifCount - alifCountAccordingToTanzil) };
        }

        return null;
    }
}