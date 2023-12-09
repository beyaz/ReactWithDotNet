namespace ReactWithDotNet;

partial class Style
{
    public IReadOnlyDictionary<string, string> ToDictionary()
    {
        var map = new Dictionary<string, string>();

        VisitNotNullValues(map.Add);

        return map;
    }
}