using System.Collections.Generic;
using System.Linq;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer;

public static class CharachterSearchResultColorizer
{
    public static Element ColorizeCharachterSearchResults(IReadOnlyList<MatchInfo> matchRecords)
    {
        var container = new VPanel
        {
            style =
            {
                fontFamily = "Lateef, cursive", fontSize = px(25),
                direction = Direction.rtl
            }
        };


        foreach (var itemsInSurah in from m in matchRecords group m by m.aya.SurahNumber into mgroup select mgroup.ToList())
        {
            foreach (var items in from m in itemsInSurah group m by m.aya._index into mgroup select mgroup.ToList())
            {
                var el = ColorizeCharachterSearchResult(items[0].aya._bismillah + items[0].aya._text, items);

                container.Add(new HPanel
                {
                    new div
                    {
                        text = $"[{items[0].aya.SurahNumber}:{items[0].aya._index}]",
                        style =
                        {
                            marginLeft = px(5),
                            color      = "green",
                            fontSize   = px(13),
                            direction  = Direction.ltr
                        }
                    },
                    el
                });
            }
        }

        return container;
    }

    static Element ColorizeCharachterSearchResult(string ayahText, IReadOnlyList<MatchInfo> matchRecords)
    {
        var element = new HPanel
        {
            style =
            {
                
               verticalAlign = VerticalAlign.top
            }
        };

        var startPosition = 0;

        foreach (var matchRecord in matchRecords)
        {
            var text = ayahText.Substring(startPosition, matchRecord.StartIndex - startPosition);

            element.Add(new div {text = text});

            element.Add(new span {text = matchRecord.ToString(), style = {color = "red", marginLeft = "3px", marginRight = "3px"}});

            startPosition = matchRecord.StartIndex + matchRecord.ToString().Length;
        }

        if (startPosition < ayahText.Length-1)
        {
            element.Add(new div {text = ayahText.Substring(startPosition)});
        }

        return element;
    }
}