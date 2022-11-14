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
class CountsSummaryView : ReactComponent
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
                border       = "1px dashed rgb(218, 220, 224)",
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


            var mean = new div
            {
                style =
                {
                    FontSize("0.6rem"),
                    FontWeight700
                    
                    //marginRight = "1px",
                    //marginLeft = "3px"
                }
            };
            
            var pronunciation = GetPronunciationOfArabicLetter(name);
            if (pronunciation is not null)
            {
                mean.text = "("+pronunciation+")";
            }
            else
            {
                mean.text = GetPronunciationOfArabicWord(name)?.trMean;
            }
            
            var countView = new HPanel
            {
                children =
                {
                    new FlexColumn(AlignItemsCenter)
                    {
                        new div { text = name, style = { color = LetterColorPalette.GetColor(j) } },
                        mean
                    },
                    new div { text = ":", style = { marginLeftRight = "4px" } },

                    new div { text = counts[j].Count.ToString(), id = "subTotal-" + j }
                },
                style = { marginLeft = "10px" }
            };

            countsView.appendChild(countView);

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
                    new div("19"), (small)$"x {total / 19}"|MarginLeftRight(3)
                },
                new div { innerText = ")" }
            },
            style = { display = "flex", flexDirection = "row" },
            id    = "GrandTotal"
        };
    }
}