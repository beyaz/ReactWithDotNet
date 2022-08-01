using System.Text;
using ReactWithDotNet;

namespace QuranAnalyzer;

class LetterColorizer : ReactComponent
{
    #region Static Fields
    static readonly string[] Colors = { "blue", "red", "#E0B4E8", "#D4D925", "#159E09" };
    #endregion

    #region Public Properties
    public string LettersForColorize { get; set; }
    public IReadOnlyList<LetterMatchInfo> LettersForColorizeNodes { get; set; }
    public string VerseText { get; set; }
    public IReadOnlyList<LetterMatchInfo> VerseTextNodes { get; set; }

    public string ChapterNumber { get; set; }
    public string VerseNumber { get; set; }
    #endregion

    #region Public Methods
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
                            color        = getColor(j),
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

        var countsView = new HPanel
        {
            style =
            {
                border = "1px dashed rgb(218, 220, 224)",
                padding = "5px"
            },
            children = { new div($"Ayet: {ChapterNumber} - {VerseNumber}"){style = { marginLeftRight = "10px"}} }
        };
        
        for (var j = 0; j < lettersForColorize.Count; j++)
        {
            var letter = new div(lettersForColorize[j].MatchedLetter) { style = { color = getColor(j) } };

            var countView = new HPanel
            {
                children =
                {
                    letter,
                    new div(":") { style = { marginLeftRight = "4px" } },
                    new div(counts[j].ToString())
                },
                style = { marginLeftRight = "10px" }
            };


            countsView.appendChild(countView);
        }

        var textView = new div
        {
            innerHTML = html.ToString(),
            style =
            {
                fontSize = "1.4rem",
                border   = "1px dashed rgb(218, 220, 224)",
                padding = "5px"
            }
        };

        return new VStack
        {
            textView, countsView
        };
    }
    #endregion

    #region Methods
    static string getColor(int index)
    {
        if (index >= 0 && index < Colors.Length)
        {
            return Colors[index];
        }

        return "red";
    }
    #endregion
}