namespace ReactWithDotNet;

class DynamicStyleContentForEmbedInClient
{
    internal readonly List<CssClassInfo> ListOfClasses = new();

    public JsonMap CalculateCssClassList()
    {
        if (!ListOfClasses.Any())
        {
            return null;
        }

        var jsonMap = new JsonMap();

        foreach (var cssClassInfo in ListOfClasses)
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
                cssClassInfo = new CssClassInfo
                {
                    Name                      = firstName + suffix++,
                    Pseudos                   = cssClassInfo.Pseudos,
                    MediaQueries              = cssClassInfo.MediaQueries,
                    ComponentUniqueIdentifier = cssClassInfo.ComponentUniqueIdentifier,
                    Body                      = cssClassInfo.Body
                };

                // if everything is equal then no need to reExport
                if (ListOfClasses.Any(x => CssClassInfo.IsEquals(cssClassInfo, x)))
                {
                    return cssClassInfo.Name;
                }

                if (ListOfClasses.Any(x => x.Name == cssClassInfo.Name))
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

class CssPseudoCodeInfo
{
    public string BodyOfCss { get; init; }
    public string Name { get; init; }
}

class CssClassInfo
{
    public string Body { get; init; }
    public int? ComponentUniqueIdentifier { get; init; }
    public IReadOnlyList<(string mediaRule, string cssBody)> MediaQueries { get; set; }
    public string Name { get; init; }
    public IReadOnlyList<CssPseudoCodeInfo> Pseudos { get; init; }

    public static bool IsEquals(CssClassInfo a, CssClassInfo b)
    {
        if (a.Name != b.Name)
        {
            return false;
        }

        if (a.ComponentUniqueIdentifier != b.ComponentUniqueIdentifier)
        {
            return false;
        }

        // compare Pseudos
        {
            if (a.Pseudos is not null && b.Pseudos is null)
            {
                return false;
            }

            if (a.Pseudos is null && b.Pseudos is not null)
            {
                return false;
            }

            if (a.Pseudos is not null && b.Pseudos is not null)
            {
                if (a.Pseudos.Count != b.Pseudos.Count)
                {
                    return false;
                }

                for (var i = 0; i < a.Pseudos.Count; i++)
                {
                    if (a.Pseudos[i].Name != b.Pseudos[i].Name)
                    {
                        return false;
                    }

                    if (a.Pseudos[i].BodyOfCss != b.Pseudos[i].BodyOfCss)
                    {
                        return false;
                    }
                }
            }
        }

        // compare MediaQueries
        {
            if (a.MediaQueries is not null && b.MediaQueries is null)
            {
                return false;
            }

            if (a.MediaQueries is null && b.MediaQueries is not null)
            {
                return false;
            }

            if (a.MediaQueries is not null && b.MediaQueries is not null)
            {
                if (a.MediaQueries.Count != b.MediaQueries.Count)
                {
                    return false;
                }

                for (var i = 0; i < a.MediaQueries.Count; i++)
                {
                    if (a.MediaQueries[i].mediaRule != b.MediaQueries[i].mediaRule)
                    {
                        return false;
                    }

                    if (a.MediaQueries[i].cssBody != b.MediaQueries[i].cssBody)
                    {
                        return false;
                    }
                }
            }
        }

        if (a.Body != b.Body)
        {
            return false;
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
                var cssBody     = pseudoCodeInfo.BodyOfCss;

                jsonMap.Add(cssSelector, cssBody);
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
}