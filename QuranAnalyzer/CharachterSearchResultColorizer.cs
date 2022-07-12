using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using ReactDotNet;

namespace QuranAnalyzer;

public static class CharachterSearchResultColorizer
{
    
    public static HtmlElement ColorizeCharachterSearchResults(IReadOnlyList<MatchInfo> matchRecords, IReadOnlyList<string> searchCharachterList_)
    {
       

        var colors = new[] { "blue", "red", "#d32f2f", "#0d89ec", "#fbc02d" };

        var searchCharachterList = searchCharachterList_.ToImmutableList();

        var container = new VPanel
        {
            style =
            {
                fontFamily = "Lateef, cursive", fontSize = "6.5rem",
                direction = Direction.rtl
            }
        };


        foreach (var itemsInChapter in from m in matchRecords group m by m.Verse.ChapterNumber into mgroup select mgroup.ToList())
        {
            foreach (var items in from m in itemsInChapter group m by m.Verse._index into mgroup select mgroup.ToList())
            {
                var el = ColorizeCharachterSearchResult(items[0].Verse._bismillah + items[0].Verse._text, items, getColor);

                container.Add(new HPanel
                {
                    new div
                    {
                        innerText = $"[{items[0].Verse.ChapterNumber}:{items[0].Verse._index}]",
                        style =
                        {
                            marginLeft = "5px",
                            color      = "green",
                            fontSize   = "1rem",
                            direction  = "ltr"
                        }
                    },
                    el
                });
            }
        }

        return container;


        

        string getColor(string charachter)
        {
            var index = searchCharachterList.IndexOf(charachter);
            if (index >=0 && index < colors.Length)
            {
                return colors[index];
            }

            return "yellow";
        }
    }

    static Element ColorizeCharachterSearchResult(string ayahText, IReadOnlyList<MatchInfo> matchRecords, Func<string,string> getColor)
    {
        var startPosition    = 0;

        var html = new StringBuilder();

        foreach (var matchRecord in matchRecords)
        {
            var text = ayahText.Substring(startPosition, matchRecord.StartIndexInVerseText - startPosition);

            var colorizedText = matchRecord.ToString();

            var span = new span
            {
                innerText = colorizedText,
                style =
                {
                    color        = getColor(colorizedText),
                    marginLeft   = "3px",
                    marginRight  = "3px",
                    border       = "1px solid rgb(218, 220, 224)",
                    borderRadius = "4px"
                }
            };
            
            startPosition = matchRecord.StartIndexInVerseText + colorizedText.Length;

            html.Append(text);
            html.Append(span);
        }

        if (startPosition < ayahText.Length-1)
        {
            html.Append(ayahText.Substring(startPosition));
        }

        return new div
        {
            innerHTML = html.ToString(),
            style =
            {
                fontSize = "1.4rem"
            }
        };
    }
}