namespace ReactWithDotNet;

interface IReadOnlyJsonMap
{
    int Count { get; }
    void Foreach(Action<string, object> action);
}

sealed class JsonMap : IReadOnlyJsonMap
{
    internal Node Head;
    internal Node Tail;

    public int Count { get; private set; }

    public void Add(string key, object value)
    {
        Count++;

        var node = new Node { Key = key, Value = value };

        if (Head == null)
        {
            Tail = Head = node;
            return;
        }

        Tail.Next = node;

        Tail = node;
    }

    public void Foreach(Action<string, object> action)
    {
        if (Head == null)
        {
            return;
        }

        var node = Head;

        while (node is not null)
        {
            action(node.Key, node.Value);

            node = node.Next;
        }
    }

    public void Foreach<TContext>(TContext context, Action<TContext, string, object> action)
    {
        if (Head == null)
        {
            return;
        }

        var node = Head;

        while (node is not null)
        {
            action(context, node.Key, node.Value);

            node = node.Next;
        }
    }

    internal class Node
    {
        public string Key;
        public Node Next;
        public object Value;
    }
}