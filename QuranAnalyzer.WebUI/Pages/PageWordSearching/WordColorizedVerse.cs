using System.Text;
using static QuranAnalyzer.WebUI.LetterColorPalette;

namespace QuranAnalyzer.WebUI.Pages.PageWordSearching;

class WordColorizedVerse : ReactPureComponent
{
    public IReadOnlyList<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startEndPoints)> MatchList { get; set; }
    public Verse Verse { get; set; }

    public IReadOnlyList<LetterInfo> VerseLetters => Verse.TextAnalyzed;

    protected override Element render()
    {
        // T e s t
        if (Verse == null)
        {
            Verse = VerseFilter.GetVerseById("2:286");
            MatchList = new List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo first, LetterInfo last)> startPoints)>
            {
                (Analyzer.AnalyzeText("واليوم"), new[] { (VerseLetters[6], VerseLetters[9]), (VerseLetters[28], VerseLetters[33]), (VerseLetters[258], VerseLetters[268]) }),
                (Analyzer.AnalyzeText("يوم"), new[] { (VerseLetters[45], VerseLetters[50]), (VerseLetters[68], VerseLetters[78]) }),
                (Analyzer.AnalyzeText("باليوم"), new[] { (VerseLetters[96], VerseLetters[100]), (VerseLetters[178], VerseLetters[184]), (VerseLetters[228], VerseLetters[235]) })
            };
        }

        var verseLetters = VerseLetters.ToList();

        var cursor = 0;

        var html = new StringBuilder();

        while (cursor < verseLetters.Count)
        {
            var letterInfo = verseLetters[cursor];

            var hasAnyMatch = false;

            var searchWordIndex = 0;
            foreach (var (_, startEndPoints) in MatchList)
            {
                foreach (var startEndPoint in startEndPoints)
                {
                    if (startEndPoint.start == letterInfo)
                    {
                        var endIndex = verseLetters.IndexOf(startEndPoint.end, cursor);

                        var span = new span
                        {
                            innerText = verseLetters.GetRange(cursor, endIndex - cursor + 1).AsText(),
                            style =
                            {
                                FontWeightBold,
                                BorderRadiusForPanels,
                                Border("1px dashed rgb(218, 220, 224)"),
                                Color(GetColor(searchWordIndex))
                            }
                        };

                        html.Append(span);

                        cursor = endIndex + 1;

                        hasAnyMatch = true;
                        break;
                    }
                }

                if (hasAnyMatch)
                {
                    break;
                }

                searchWordIndex++;
            }

            if (hasAnyMatch)
            {
                continue;
            }

            html.Append(letterInfo.MatchedLetter);

            cursor++;
        }

        var countsView = new FlexRow(FlexWrap, JustifyContentCenter, Padding(5), Gap(13));

        {
            var searchWordIndex = 0;

            foreach (var (searchWord, startEndPoints) in MatchList)
            {
                var countView = new FlexRow(AlignItemsCenter)
                {
                    new div { string.Join(string.Empty, searchWord), FontWeightBold, Color(GetColor(searchWordIndex)) },

                    new div { ":", MarginLeftRight(4) },

                    new div { startEndPoints.Count.ToString(), FontSize12 }
                };

                countsView.appendChild(countView);

                searchWordIndex++;
            }
        }
        var textView = new div(FontFamily_Lateef)
        {
            innerHTML = html.ToString(),
            style =
            {
                FontSize(38),
                Padding(5),
                DirectionRtl
            }
        };

        var verseId = new div(FontWeightBold, MarginLeft(2), FontSize13)
        {
            $"{Verse.Id}"
        };

        var topLegend = new legend(DisplayFlex, FlexDirectionRow, AlignItemsCenter, Gap(5))
        {
            verseId,
            countsView
        };

        return new fieldset
        {
            children = { topLegend, textView },
            style =
            {
                DisplayFlex,
                FlexDirectionColumn,
                AlignItemsFlexEnd,

                Border("1px dashed rgb(218, 220, 224)"),
                BorderRadiusForPanels
            }
        };
    }
}