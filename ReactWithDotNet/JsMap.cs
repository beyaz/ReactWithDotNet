namespace ReactWithDotNet;

interface IReadOnlyJsMap
{
    int Count { get; }
    void Foreach(Action<string, object> action);
}

sealed class JsMap : IReadOnlyJsMap
{
    int count;

    Node head;
    Node tail;

    public int Count => count;

    public void Add(string key, object value)
    {
        count++;

        var node = new Node { key = key, value = value };

        if (head == null)
        {
            tail = head = node;
            return;
        }

        tail.next = node;

        tail = node;
    }

    public void Foreach(Action<string, object> action)
    {
        if (head == null)
        {
            return;
        }

        var node = head;

        while (node is not null)
        {
            action(node.key, node.value);

            node = node.next;
        }
    }

    class Node
    {
        public string key;
        public Node next;
        public object value;
    }
}