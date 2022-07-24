using System;
using System.Collections.Generic;
using System.Linq;
using ReactWithDotNet;

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

        var returnDiv = new div
        {
            new div
            {
                Children = counts.Select(ToElement)
            }
        };

        var total = counts.Select(x => x.Count).Sum();

        if (total > 0)
        {
            if (total % 19 == 0)
            {
                returnDiv.appendChild(new div
                {
                    children =
                    {
                        new div { innerHTML = $"Toplam: <strong>{total}</strong> (" },
                        new div { innerText = "19 x " + total / 19, style = { color = "red", marginLeftRight = "5px" } },
                        new div { innerText = ")" }
                    },
                    style = { display = "flex", flexDirection = "row" },
                });
            }
            else
            {
                returnDiv.appendChild(new div
                {
                    new div { innerHTML = $"Toplam: <strong>{total}</strong>" }
                });
            }
        }

        return returnDiv;
    }
    #endregion

    #region Methods
    static Element ToElement(SummaryInfo x)
    {
        return new div
        {
            innerHTML = $"<strong>{x.Count}</strong> adet <strong>{x.Name}</strong> harfi bulundu."
        };
    }
    #endregion
}