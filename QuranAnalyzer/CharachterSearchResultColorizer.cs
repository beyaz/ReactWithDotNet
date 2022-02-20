using System.Collections.Generic;
using System.Linq;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer;

public static class CharachterSearchResultColorizer
{
    public static Element ColorizeCharachterSearchResults(IReadOnlyList<MatchInfo> matchRecords)
    {
        var container = new VPanel();

        foreach (var items in from m in matchRecords group m by m.aya._index into mgroup select mgroup.ToList())
        {
            var el = ColorizeCharachterSearchResult(items[0].aya._bismillah + items[0].aya._text, items);

            container.Add(new HPanel
            {
                new div {text = $"[{items[0].aya.SurahNumber}:{items[0].aya._index}]", style = {marginRight = px(5), color = "green"}},
                el
            });
        }

        return container;
    }

    static Element ColorizeCharachterSearchResult(string ayahText, IReadOnlyList<MatchInfo> matchRecords)
    {
        var element = new HPanel();

        var startPosition = 0;

        foreach (var matchRecord in matchRecords)
        {
            var text = ayahText.Substring(startPosition, matchRecord.StartIndex - startPosition);

            element.Add(new div {text = text});

            element.Add(new span {text = matchRecord.ToString(), style = {color = "red", marginLeft = "2px", marginRight = "2px"}});

            startPosition = matchRecord.StartIndex + matchRecord.ToString().Length;
        }

        if (startPosition < ayahText.Length-1)
        {
            element.Add(new div {text = ayahText.Substring(startPosition)});
        }

        return element;
    }
}