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