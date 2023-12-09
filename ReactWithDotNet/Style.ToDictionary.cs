namespace ReactWithDotNet;

partial class Style
{
    public IReadOnlyDictionary<string, string> ToDictionary()
    {
        var map = new Dictionary<string, string>();

        this.VisitNotNullValues(map.Add);

        return map;
    }

}