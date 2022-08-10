using System;
using System.Collections.Generic;
using System.Linq;
using ReactWithDotNet;
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
    public override Element render()
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

        if (total > 0)
        {
            if (total % 19 == 0)
            {
                returnDiv.appendChild(MultipleOf(total));
            }
            else
            {
                returnDiv.appendChild(new legend
                {
                    style = { display = "flex", flexDirection = "row"},
                    children =
                    {
                        new div { text = "Toplam:"},
                        new HSpace(4),
                        new strong { text = total.ToString(), id ="GrandTotal" }
                    }
                });
            }
        }

     

        var countsView = new HPanel
        {
            style =
            {
                padding        = "5px",
                justifyContent = "space-between",
                fontSize       = "0.9rem",
                flexWrap       = "wrap",
                marginTop = "20px"
            },
        };

        for (var j = 0; j < counts.Count; j++)
        {
            var countView = new HPanel
            {
                children =
                {
                    new div { text = counts[j].Name ,style = { color           = LetterColorizer.GetColor(j) } },
                    new div { text = ":", style            = { marginLeftRight = "4px" } },
                    new div{text   = counts[j].Count.ToString(), id = "subTotal-"+j}
                },
                style = { marginLeft = "10px" }
            };


            countsView.appendChild(countView);


            returnDiv.appendChild(new Xarrow
            {
                start = "GrandTotal",
                end = "subTotal-" + j,
                path = "smooth",
                color = "rgb(218, 220, 224)",
                strokeWidth = 1,
                startAnchor = "bottom",
                dashness = true,
                curveness = 1,
                endAnchor = "top"

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
                new div { innerText = "19 x " + total / 19, style = { color = "red", marginLeftRight = "5px" } },
                new div { innerText = ")" }
            },
            style = { display = "flex", flexDirection = "row" },
            id = "GrandTotal"
        };
    }
    #region Methods
    static Element ToElement(SummaryInfo x)
    {
        return new div
        {
            innerHTML = $"<strong>{x.Count}</strong> adet <strong>{x.Name}</strong>",
            style = { marginLeftRight = "15px"}
        };
    }
    #endregion
}