namespace AlyaVillas.WebUI;

static class Extension
{
    public static IReadOnlyList<T> ListOf<T>(IEnumerable<T> enumerable, T newItem)
    {
        var items = new List<T>();

        items.AddRange(enumerable);

        items.Add(newItem);

        return items;
    }

    public static IReadOnlyList<T> ListOf<T>(T newItem, IEnumerable<T> enumerable)
    {
        var items = new List<T>
        {
            newItem
        };

        items.AddRange(enumerable);

       

        return items;
    }
}