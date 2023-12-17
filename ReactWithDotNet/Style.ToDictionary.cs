namespace ReactWithDotNet;

partial class Style
{
    public IReadOnlyDictionary<string, string> ToDictionary()
    {
        var map = new Dictionary<string, string>();

        var node = headNode;

        while (node != null)
        {
            map.Add(node.NameInfo.NameInCamelCase, node.Value);

            node = node.Next;
        }

        return map;
    }
}