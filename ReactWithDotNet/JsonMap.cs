using System.Text.Json;

namespace ReactWithDotNet;

interface IReadOnlyJsonMap
{
    int Count { get; }
    void Foreach(Action<string, object> action);
    void Write(Utf8JsonWriter writer, JsonSerializerOptions options);
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
    
    public void Write(Utf8JsonWriter writer, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        var node = Head;
        
        while (node is not null)
        {
            writer.WritePropertyName(node.Key);

            JsonSerializer.Serialize(writer, node.Value, options);

            node = node.Next;
        }

        writer.WriteEndObject();
    }

    internal sealed class Node
    {
        public string Key;
        public Node Next;
        public object Value;
    }
}