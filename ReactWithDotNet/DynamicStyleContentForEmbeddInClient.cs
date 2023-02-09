namespace ReactWithDotNet;

class DynamicStyleContentForEmbeddInClient
{
    internal readonly List<CssClassInfo> listOfClasses = new();

    public string GetClassName(CssClassInfo cssClassInfo)
    {
        // change name until is unique
        { 
            var suffix = 0;
            while (true)
            {
                if (listOfClasses.Any(x => x.Name == cssClassInfo.Name))
                {
                    cssClassInfo = new CssClassInfo
                    {
                        Name         = cssClassInfo.Name + suffix++,
                        Pseudos      = cssClassInfo.Pseudos,
                        MediaQueries = cssClassInfo.MediaQueries
                    };

                    continue;
                }
                break;
            }
        }
        
        listOfClasses.Add(cssClassInfo);

        return cssClassInfo.Name;
    }

    public JsonMap CalculateCssClassList()
    {
        if (!listOfClasses.Any())
        {
            return null;
        }

        var jsonMap = new JsonMap();

        foreach (var cssClassInfo in listOfClasses)
        {
            cssClassInfo.WriteTo(jsonMap);
        }

        return jsonMap;
    }
}

class CssPseudoCodeInfo
{

    public string Name { get; init; }
    public string BodyOfCss { get; init; }

    public override bool Equals(object other)
    {
        if (other is CssPseudoCodeInfo info)
        {
            return info.Name == Name && info.BodyOfCss == BodyOfCss;
        }

        return false;
    }

    protected bool Equals(CssPseudoCodeInfo other)
    {
        return Name == other.Name && BodyOfCss == other.BodyOfCss;
    }

    public override int GetHashCode()
    {
        return (Name + ":" + BodyOfCss).GetHashCode();
    }
}

class CssClassInfo
{
    public string Name { get; init; }
    public IReadOnlyList<CssPseudoCodeInfo> Pseudos { get; init; }
    public IReadOnlyList<(string mediaRule, string cssBody)> MediaQueries { get; set; }
    public int? ComponentUniqueIdentifier { get; init; }

    public void WriteTo(JsonMap jsonMap)
    {
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