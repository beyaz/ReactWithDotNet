using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer;

public static class CharachterSearchResultColorizer
{
    
    public static Element ColorizeCharachterSearchResults(IReadOnlyList<MatchInfo> matchRecords, IReadOnlyList<string> searchCharachterList_)
    {
       

        var backgroundColors = new[] { "rgb(217 217 206)", "rgb(235 230 90)", "#8fe4ec", "#f2bef1", "#f0e91a" };

        var searchCharachterList = searchCharachterList_.ToImmutableList();

        var container = new VPanel
        {
            style =
            {
                fontFamily = "Lateef, cursive", fontSize = "6.5rem",
                direction = Direction.rtl
            }
        };


        foreach (var itemsInSurah in from m in matchRecords group m by m.verse.SurahNumber into mgroup select mgroup.ToList())
        {
            foreach (var items in from m in itemsInSurah group m by m.verse._index into mgroup select mgroup.ToList())
            {
                var el = ColorizeCharachterSearchResult(items[0].verse._bismillah + items[0].verse._text, items, getBackgroundColor);

                container.Add(new HPanel
                {
                    new div
                    {
                        text = $"[{items[0].verse.SurahNumber}:{items[0].verse._index}]",
                        style =
                        {
                            marginLeft = px(5),
                            color      = "green",
                            fontSize   = "1rem",
                            direction  = Direction.ltr
                        }
                    },
                    el
                });
            }
        }

        return container;


        

        string getBackgroundColor(string charachter)
        {
            var index = searchCharachterList.IndexOf(charachter);
            if (index >=0 && index < backgroundColors.Length)
            {
                return backgroundColors[index];
            }

            return "yellow";
        }


    }

    static Element ColorizeCharachterSearchResult(string ayahText, IReadOnlyList<MatchInfo> matchRecords, Func<string,string> getBackgroundColor)
    {
        var element = new HPanel
        {
            style =
            {
                verticalAlign = VerticalAlign.top,
                fontSize = "1.4rem"
            }
        };

        var startPosition    = 0;
        
        

        foreach (var matchRecord in matchRecords)
        {
            var text = ayahText.Substring(startPosition, matchRecord.StartIndex - startPosition);

            element.Add(new div {text = text});

            var colorizedText = matchRecord.ToString();

            element.Add(new span {text = colorizedText, style = {color = "red", marginLeft = "3px", marginRight = "3px", background = getBackgroundColor(colorizedText)}});

            startPosition = matchRecord.StartIndex + colorizedText.Length;
        }

        if (startPosition < ayahText.Length-1)
        {
            element.Add(new div {text = ayahText.Substring(startPosition)});
        }

        return element;
    }
}