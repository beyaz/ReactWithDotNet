using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer;

public static class CharachterSearchResultColorizer
{
    static string fontSize(double minSize, double maxSize, double minViewPortWidth, double maxViewPortWidth)
    {
        // https://css-tricks.com/snippets/css/fluid-typography/
        // calc([minimum size] + ([maximum size] - [minimum size]) * ((100vw - [minimum viewport width]) / ([maximum viewport width] - [minimum viewport width])));

        return $"calc({minSize}px + ({maxSize-minSize}) * ((100vw - {minViewPortWidth}px) / ({maxViewPortWidth - minViewPortWidth})))";
    }
    public static Element ColorizeCharachterSearchResults(IReadOnlyList<MatchInfo> matchRecords, IReadOnlyList<string> searchCharachterList_)
    {
        string f(int size)
        {
            return fontSize(size-5, size+5, 300, 1600);
        }

        var backgroundColors = new[] { "rgb(217 217 206)", "rgb(235 230 90)", "#8fe4ec", "#f2bef1", "#f0e91a" };

        var searchCharachterList = searchCharachterList_.ToImmutableList();

        var container = new VPanel
        {
            style =
            {
                fontFamily = "Lateef, cursive", fontSize = f(33),
                direction = Direction.rtl
            }
        };


        foreach (var itemsInSurah in from m in matchRecords group m by m.aya.SurahNumber into mgroup select mgroup.ToList())
        {
            foreach (var items in from m in itemsInSurah group m by m.aya._index into mgroup select mgroup.ToList())
            {
                var el = ColorizeCharachterSearchResult(items[0].aya._bismillah + items[0].aya._text, items, getBackgroundColor);

                container.Add(new HPanel
                {
                    new div
                    {
                        text = $"[{items[0].aya.SurahNumber}:{items[0].aya._index}]",
                        style =
                        {
                            marginLeft = px(5),
                            color      = "green",
                            fontSize   = f(13),
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
                verticalAlign = VerticalAlign.top
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