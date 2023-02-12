using ReactWithDotNet.react_xarrows;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

public class SummaryInfo
{
    #region Public Properties
    public int Count { get; set; }
    public string Name { get; set; }
    #endregion
}

[Serializable]
class CountsSummaryView : ReactPureComponent
{
    #region Public Properties
    public IReadOnlyList<SummaryInfo> Counts { get; set; } = new List<SummaryInfo>();
    #endregion

    #region Public Methods
    protected override Element render()
    {
        var counts = Counts ?? new List<SummaryInfo>();

        var returnDiv = new fieldset
        {
            style =
            {
                marginTop    = "5px",
                border       = "2px solid rgb(218, 220, 224)",
                borderRadius = "4px",

                display       = "flex",
                flexDirection = "column",
                alignItems    = "flex-start"
            }
        };

        var total = counts.Select(x => x.Count).Sum();

        if (total > 0 && total % 19 == 0)
        {
            returnDiv.appendChild(MultipleOf(total));
        }
        else
        {
            returnDiv.appendChild(new legend
            {
                style = { display = "flex", flexDirection = "row" },
                children =
                {
                    new div { text = "Toplam:" },
                    new HSpace(4),
                    new strong { text = total.ToString(), id = "GrandTotal" }
                }
            });
        }

        var countsView = new FlexRow(Gap(20))
        {
            style =
            {
                padding        = "5px",
                justifyContent = "space-between",
                fontSize       = "0.9rem",
                flexWrap       = "wrap",
                marginTop      = "20px"
            }
        };

        
        for (var j = 0; j < counts.Count; j++)
        {
            var name = counts[j].Name;

            countsView.appendChild(CountAsElement(name, LetterColorPalette.GetColor(j), GetPronunciation(name), counts[j].Count, "subTotal-" + j));

            returnDiv.appendChild(new Xarrow
            {
                start       = "GrandTotal",
                end         = "subTotal-" + j,
                path        = "smooth",
                color       = "rgb(218, 220, 224)",
                strokeWidth = 1,
                startAnchor = "bottom",
                dashness    = true,
                endAnchor   = "top"
            });
        }

        returnDiv.appendChild(countsView);

        return returnDiv;
    }

    string GetPronunciation(string name)
    {
        var pronunciation = GetPronunciationOfArabicLetter(name);
        if (pronunciation is not null)
        {
            pronunciation = "(" + pronunciation + ")";
        }
        else
        {
            pronunciation = GetPronunciationOfArabicWord(name)?.trMean;
        }

        return pronunciation;
    }

    static Element CountAsElement(string text, string color, string pronunciation, int count, string id)
    {
        return new FlexRow
        {
            new FlexColumn(AlignItemsCenter)
            {
                new div { text = text, style = { color = color} },
                new div{ Text(pronunciation), FontSize("0.6rem"), FontWeight700}
            },
                       
            new div { text = ":", style = { marginLeftRight = "4px" } },

            new div { text = count.ToString(), id = id},
                       
                      
        };
    }
    #endregion

    static Element MultipleOf(int total)
    {
        return new legend
        {
            children =
            {
                new div { innerHTML = $"Toplam: <strong>{total}</strong> (" },
                new FlexRow(MarginLeftRight(5), AlignItemsCenter,Color("red"))
                {
                    new div("19"), (small)$"x {total / 19}"+MarginLeftRight(3)
                },
                new div { innerText = ")" }
            },
            style = { display = "flex", flexDirection = "row" },
            id    = "GrandTotal"
        };
    }
}