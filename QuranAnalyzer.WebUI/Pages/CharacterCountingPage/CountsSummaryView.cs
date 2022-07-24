using System;
using System.Collections.Generic;
using System.Linq;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

public class SummaryInfo
{
    public string Name { get; set; }
    public int Count { get; set; }
}


[Serializable]
class CountsSummaryView: ReactComponent
{
    public IReadOnlyList<SummaryInfo> Counts { get; set; } = new List<SummaryInfo>();

    public override Element render()
    {
        static Element toElement(SummaryInfo x)
        {
            return new div
            {
                innerHTML = $"<strong>{x.Count}</strong> adet <strong>{x.Name}</strong> harfi bulundu."
            };
        }
        
        var returnDiv = new div
        {
            new div
            {
                Children = Counts?.Select(toElement)
            }
        };

        var total = Counts.Select(x => x.Count).Sum();

        if (total % 19 == 0)
        {
            returnDiv.appendChild(new div
            {
                children =
                {
                    new div {innerHTML = $"Toplam: <strong>{total}</strong> ("},
                    new div {innerText = "19 x " + total / 19, style = {color = "red", marginLeftRight = "5px"}},
                    new div {innerText = ")"}
                },
                style = { display = "flex", flexDirection = "row"},
            });
        }
        else
        {
            returnDiv.appendChild(new div
            {
                new div{innerHTML = $"Toplam: <strong>{total}</strong>"}
            });
        }

        return returnDiv;
    }
}