using System.Runtime.InteropServices;
using System.Text;

namespace ReactWithDotNet;

static partial class Mixin
{
    internal static void AddRange(this Dictionary<int, List<CssClassInfo>> map, Dictionary<int, List<CssClassInfo>> insertValues)
    {
        foreach (var (key, value) in insertValues)
        {
            map.Add(key, value);
        }
    }
}

class DynamicStyleContentForEmbedInClient
{
    internal readonly Dictionary<int,List<CssClassInfo>> Map = [];
    
    internal readonly List<CssClassInfo> ListOfClasses = [];

    public JsonMap CalculateCssClassList()
    {
        var cssClassInfoList = CollectionsMarshal.AsSpan(ListOfClasses);
        if (cssClassInfoList.Length == 0)
        {
            return null;
        }

        var jsonMap = new JsonMap();

        foreach (var cssClassInfo in cssClassInfoList)
        {
            cssClassInfo.WriteTo(jsonMap);
        }

        return jsonMap;
    }

    public string GetClassName(CssClassInfo cssClassInfo)
    {
        // change name until is unique
        {
            var firstName = cssClassInfo.Name;

            var suffix = 0;

            while (true)
            {
                cssClassInfo = new()
                {
                    Name                      = firstName + suffix++,
                    Pseudos                   = cssClassInfo.Pseudos,
                    MediaQueries              = cssClassInfo.MediaQueries,
                    ComponentUniqueIdentifier = cssClassInfo.ComponentUniqueIdentifier,
                    Body                      = cssClassInfo.Body
                };

                var cursor = CollectionsMarshal.AsSpan(ListOfClasses);
                var length = cursor.Length;

                // if everything is equal then no need to reExport return existing record
                for (var i = 0; i < length; i++)
                {
                    if (CssClassInfo.IsEquals(cssClassInfo, cursor[i]))
                    {
                        return cssClassInfo.Name;
                    }
                }

                // check has already same name give another name
                var hasAlreadyExistsSameName = false;
                {
                    for (var i = 0; i < length; i++)
                    {
                        if (cursor[i].Name == cssClassInfo.Name)
                        {
                            hasAlreadyExistsSameName = true;
                            break;
                        }
                    }
                }

                if (hasAlreadyExistsSameName)
                {
                    continue;
                }

                break;
            }
        }

        ListOfClasses.Add(cssClassInfo);

        return cssClassInfo.Name;
    }

    public static void WriteAsHtmlStyleNodeContent(StringBuilder sb, JsonMap dynamicStylesMap)
    {
        dynamicStylesMap?.Foreach((cssSelector, cssBody) =>
        {
            const string padding = "    ";

            sb.Append(padding);
            sb.Append(cssSelector);
            sb.Append(padding);
            sb.AppendLine("{");

            sb.Append(padding);
            sb.Append(padding);
            sb.Append(cssBody);

            if (cssSelector.IndexOf("@media ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                sb.AppendLine();

                sb.Append(padding);
                sb.AppendLine("}");
            }

            sb.AppendLine();

            sb.Append(padding);
            sb.AppendLine("}");
        });
    }
}

sealed class CssPseudoCodeInfo
{
    public string BodyOfCss; //  { get; init; }
    public string Name; //  { get; init; }
}

sealed class CssClassInfo
{
    public required string Body  { get; init; }
    public required int ComponentUniqueIdentifier  { get; init; }
    public required IReadOnlyList<(string mediaRule, string cssBody)> MediaQueries  { get; init; }
    public required string Name  { get; init; }
    public required IReadOnlyList<CssPseudoCodeInfo> Pseudos  { get; init; }

    public static bool IsEquals(CssClassInfo a, CssClassInfo b)
    {
        if (a.ComponentUniqueIdentifier != b.ComponentUniqueIdentifier)
        {
            return false;
        }
        
        if (a.Name != b.Name)
        {
            return false;
        }
        
        if (a.Body != b.Body)
        {
            return false;
        }

        // compare Pseudos
        {
            var aPseudos = a.Pseudos;
            var bPseudos = b.Pseudos;

            if (aPseudos is not null && bPseudos is null)
            {
                return false;
            }

            if (aPseudos is null && bPseudos is not null)
            {
                return false;
            }

            if (aPseudos is not null)
            {
                if (aPseudos.Count != bPseudos.Count)
                {
                    return false;
                }

                for (var i = 0; i < aPseudos.Count; i++)
                {
                    if (aPseudos[i].Name != bPseudos[i].Name)
                    {
                        return false;
                    }

                    if (aPseudos[i].BodyOfCss != bPseudos[i].BodyOfCss)
                    {
                        return false;
                    }
                }
            }
        }

        // compare MediaQueries
        {
            var aMediaQueries = a.MediaQueries;
            var bMediaQueries = b.MediaQueries;

            if (aMediaQueries is not null && bMediaQueries is null)
            {
                return false;
            }

            if (aMediaQueries is null && bMediaQueries is not null)
            {
                return false;
            }

            if (aMediaQueries is not null)
            {
                if (aMediaQueries.Count != bMediaQueries.Count)
                {
                    return false;
                }

                for (var i = 0; i < aMediaQueries.Count; i++)
                {
                    if (aMediaQueries[i].mediaRule != bMediaQueries[i].mediaRule)
                    {
                        return false;
                    }

                    if (aMediaQueries[i].cssBody != bMediaQueries[i].cssBody)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public void WriteTo(JsonMap jsonMap)
    {
        if (Body is not null)
        {
            var cssSelector = $".{Name}";

            jsonMap.Add(cssSelector, Body);
        }

        if (Pseudos is not null)
        {
            foreach (var pseudoCodeInfo in Pseudos)
            {
                var cssSelector = $".{Name}:{pseudoCodeInfo.Name}";
                var cssBody = pseudoCodeInfo.BodyOfCss;

                jsonMap.Add(cssSelector, cssBody);
                jsonMap.Add(ComponentUniqueIdentifier.ToString(), ToArray());
            }
        }

        if (MediaQueries != null)
        {
            foreach (var (mediaRule, cssBody) in MediaQueries)
            {
                var cssSelector = $"@media {mediaRule} {{ .{Name}";

                jsonMap.Add(cssSelector, cssBody);
            }
        }
    }
    
    public object[] ToArray()
    {
        return
        [
            /*0*/Name,
            /*1*/Body,
            /*2*/MediaQueries?.Select(x => new[] { x.mediaRule, x.cssBody }),
            /*3*/Pseudos?.Select(x => new[] { x.Name, x.BodyOfCss })
        ];
    }
}