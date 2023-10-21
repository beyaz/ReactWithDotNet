using System.Text;

namespace ReactWithDotNet.WebSite.Components;

class HighlightedText : PureComponent
{
    public string Text { get; set; }
    
    protected override Element render()
    {
        return new Fragment
        {
            ConvertToHighlighted(Text)
        };
    }

    Element CreateAttractiveText(string text)
    {
        return new span
        {
            text = text,
            style =
            {
                PaddingLeftRight(3),
                WebkitBackgroundClipText,
                WebkitTextFillColor(Transparent),
                Background(linear_gradientTo("right", Theme.Blue400, Theme.Blue700))
            }
        };
    }
    
    IEnumerable<Element> ConvertToHighlighted(string str)
    {
        return ParseSpecialString("[]", str).Select(x => (x.isSpecial ? CreateAttractiveText(x.value) : x.value) as Element);
    }

    static IReadOnlyList<(string value, bool isSpecial)> ParseSpecialString(string specialBeginEndChars, string value)
    {

        var items = new List<(string value, bool isSpecial)>();


        if (value == null)
        {
            return items;
        }

        var currentText = new StringBuilder();
        var isSpecial   = false;

        foreach (var c in value)
        {
            if (c == specialBeginEndChars[0])
            {
                if (currentText.Length > 0)
                {
                    items.Add((currentText.ToString(), isSpecial));
                }

                isSpecial = true;
                currentText.Clear();

                continue;
            }

            if (c == specialBeginEndChars[1])
            {
                items.Add((currentText.ToString(), isSpecial));

                isSpecial = false;
                currentText.Clear();

                continue;
            }

            currentText.Append(c);
        }

        if (currentText.Length > 0)
        {
            items.Add((currentText.ToString(), isSpecial));
        }

        return items;
    }
}