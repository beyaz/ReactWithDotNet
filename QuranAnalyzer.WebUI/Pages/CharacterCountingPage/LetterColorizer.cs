using System.Text;
using static QuranAnalyzer.ArabicLetterIndex;
using static QuranAnalyzer.LetterColor;
using static QuranAnalyzer.QuranAnalyzerMixin;

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

        if (arabicLetterIndex == Alif)
        {
            if (MushafOption.UseElifReferencesFromTanzil == false)
            {
                if (MushafTotalCountPerVerseDifference[Alif].TryGetValue(GetDifferencesKeyForRK(Verse.Id), out var count))
                {
                    if (MushafTotalCountPerVerseDifference[Alif].TryGetValue(GetDifferencesKeyForTanzil(Verse.Id), out var countAccordingToTanzil))
                    {
                        if (count > countAccordingToTanzil)
                        {
                            return new div { text = "+" + (count - countAccordingToTanzil) };
                        }

                        return new div { text = "-" + (countAccordingToTanzil - count) };
                    }
                }
            }
        }

        if (arabicLetterIndex == Laam)
        {
            if (MushafOption.Use_Laam_SpecifiedByTanzil == false)
            {
                if (MushafTotalCountPerVerseDifference[Laam].TryGetValue(GetDifferencesKeyForRK(Verse.Id), out var count))
                {
                    if (MushafTotalCountPerVerseDifference[Laam].TryGetValue(GetDifferencesKeyForTanzil(Verse.Id), out var countAccordingToTanzil))
                    {
                        if (count > countAccordingToTanzil)
                        {
                            return new div { text = "+" + (count - countAccordingToTanzil) };
                        }

                        return new div { text = "-" + (countAccordingToTanzil - count) };
                    }
                }
            }
        }

        if (arabicLetterIndex == Saad)
        {
            if (MushafOption.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten == false)
            {
                if (MushafTotalCountPerVerseDifference[Saad].TryGetValue(GetDifferencesKeyForRK(Verse.Id), out var count))
                {
                    if (MushafTotalCountPerVerseDifference[Saad].TryGetValue(GetDifferencesKeyForTanzil(Verse.Id), out var countAccordingToTanzil))
                    {
                        if (count > countAccordingToTanzil)
                        {
                            return new div { text = "+" + (count - countAccordingToTanzil) };
                        }

                        return new div { text = "-" + (countAccordingToTanzil - count) };
                    }
                }
            }
        }


        if (arabicLetterIndex == Nun)
        {
            if (MushafOption.Chapter_68_Should_Single_Nun == false)
            {
                if (MushafTotalCountPerVerseDifference[Nun].TryGetValue(GetDifferencesKeyForRK(Verse.Id), out var count))
                {
                    if (MushafTotalCountPerVerseDifference[Nun].TryGetValue(GetDifferencesKeyForTanzil(Verse.Id), out var countAccordingToTanzil))
                    {
                        if (count > countAccordingToTanzil)
                        {
                            return new div { text = "+" + (count - countAccordingToTanzil) };
                        }

                        return new div { text = "-" + (countAccordingToTanzil - count) };
                    }
                }
            }
        }

        if (arabicLetterIndex == Waaw)
        {
            if (MushafOption.Chapter_68_Should_Single_Nun == false)
            {
                if (MushafTotalCountPerVerseDifference[Waaw].TryGetValue(GetDifferencesKeyForRK(Verse.Id), out var count))
                {
                    if (MushafTotalCountPerVerseDifference[Waaw].TryGetValue(GetDifferencesKeyForTanzil(Verse.Id), out var countAccordingToTanzil))
                    {
                        if (count > countAccordingToTanzil)
                        {
                            return new div { text = "+" + (count - countAccordingToTanzil) };
                        }

                        return new div { text = "-" + (countAccordingToTanzil - count) };
                    }
                }
            }
        }

        return null;
    }
}