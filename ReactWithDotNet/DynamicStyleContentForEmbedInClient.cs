using System.Runtime.InteropServices;
using System.Text;

namespace ReactWithDotNet;

class DynamicStyleContentForEmbedInClient
{
    internal readonly List<CssClassInfo> ListOfClasses = [];
    internal readonly Dictionary<int, List<CssClassInfo>> Map = [];

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

    public static void ReadFrom(object[] arr, ref string name, ref string body, ref string[] mediaQueries, ref string[] pseudos)
    {
        name         = (string)arr[0];
        body         = (string)arr[1];
        mediaQueries = (string[])arr[2];
        pseudos      = (string[])arr[3];
    }

    public static object[] ToArray(CssClassInfo cssClassInfo)
    {
        return
        [
            /*0*/cssClassInfo.Name,
            /*1*/cssClassInfo.Body,
            /*2*/cssClassInfo.MediaQueries?.Select(x => new[] { x.mediaRule, x.cssBody }),
            /*3*/cssClassInfo.Pseudos?.Select(x => new[] { x.Name, x.BodyOfCss })
        ];
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

    public static void WriteAsHtmlStyleNodeContent_2(StringBuilder sb, JsonMap dynamicStylesMap)
    {
        dynamicStylesMap?.Foreach((_, value) =>
        {
            var items = (object[][])value;

            foreach (var arr in items)
            {
                string name = null;
                string body = null;
                string[] mediaQueries = null;
                string[] pseudos = null;

                ReadFrom(arr, ref name, ref body, ref mediaQueries, ref pseudos);

                const string padding = "    ";

                sb.Append(padding);
                sb.Append(".");
                sb.Append(name);
                sb.AppendLine(" {");

                sb.Append(padding);
                sb.AppendLine(body);

                if (mediaQueries is not null)
                {
                    var length = mediaQueries.Length;
                    for (var i = 0; i < length; i++)
                    {
                        var mediaRule = mediaQueries[0];
                        var cssBody = mediaQueries[1];

                        sb.AppendLine();
                        sb.Append("@media");
                        sb.Append(mediaRule);
                        sb.Append("{");
                        sb.Append(".");
                        sb.Append(name);
                        sb.Append(" ");
                        sb.Append(cssBody);
                        sb.Append("}");
                    }
                }

                if (pseudos is not null)
                {
                    foreach (var item in pseudos)
                    {
                        var pseudoName = item[0];
                        var pseudoBody = item[1];

                        sb.AppendLine();
                        sb.Append(padding);
                        sb.Append(".");
                        sb.Append(name);
                        sb.Append(":");
                        sb.Append(pseudoName);
                        sb.Append(" {");
                        sb.Append(pseudoBody);
                        sb.Append("}");
                    }
                }

                sb.AppendLine();
                sb.AppendLine("}");
            }
        });
    }

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
            WriteTo(cssClassInfo, jsonMap);
        }

        return jsonMap;

        static void WriteTo(CssClassInfo cssClassInfo, JsonMap jsonMap)
        {
            if (cssClassInfo.Body is not null)
            {
                var cssSelector = $".{cssClassInfo.Name}";

                jsonMap.Add(cssSelector, cssClassInfo.Body);
            }

            if (cssClassInfo.Pseudos is not null)
            {
                foreach (var pseudoCodeInfo in cssClassInfo.Pseudos)
                {
                    var cssSelector = $".{cssClassInfo.Name}:{pseudoCodeInfo.Name}";
                    var cssBody = pseudoCodeInfo.BodyOfCss;

                    jsonMap.Add(cssSelector, cssBody);
                }
            }

            if (cssClassInfo.MediaQueries != null)
            {
                foreach (var (mediaRule, cssBody) in cssClassInfo.MediaQueries)
                {
                    var cssSelector = $"@media {mediaRule} {{ .{cssClassInfo.Name}";

                    jsonMap.Add(cssSelector, cssBody);
                }
            }
        }
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
                    if (IsEquals(cssClassInfo, cursor[i]))
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
}

sealed class CssPseudoCodeInfo
{
    // @formatter:off
    public string Name; //  { get; init; }
    public string BodyOfCss; //  { get; init; }
    // @formatter:on
}

sealed class CssClassInfo
{
    // @formatter:off
    public required int ComponentUniqueIdentifier  { get; init; }
    public required string Name  { get; init; }
    public required string Body  { get; init; }
    public required IReadOnlyList<(string mediaRule, string cssBody)> MediaQueries  { get; init; }
    public required IReadOnlyList<CssPseudoCodeInfo> Pseudos  { get; init; }
    // @formatter:on
}

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