using System.Collections.Specialized;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;









public sealed class ReactContext
{
    readonly Dictionary<string, object> map = new();

    public TValue TryGetValue<TValue>(ReactContextKey<TValue> key)
    {
        if (map.TryGetValue(key.Key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }

    public void Insert<TValue>(ReactContextKey<TValue> key, TValue value)
    {
        Insert(key.Key, value);
    }

    public void Insert<TValue>(string key, TValue value)
    {
        if (map.ContainsKey(key))
        {
            map[key] = value;
            return;
        }

        map.Add(key, value);
    }

    public double ClientWidth { get; internal set; }
    public double ClientHeight { get; internal set; }
    public NameValueCollection Query { get; internal set; }

}




// ReSharper disable once UnusedTypeParameter
public sealed class ReactContextKey<TValue>
{
    public readonly string Key;

    public ReactContextKey(string key)
    {
        Key = key;
    }
}

public class ClientEventInfo
{
    public readonly string Name;

    public ClientEventInfo(string name)
    {
        Name = name;
    }
}

public sealed class ClientEventInfo<EventArgument1> : ClientEventInfo
{
    public ClientEventInfo(string name): base(name)
    {
    }
}

public abstract class ReactStatefulComponent : Element
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; set; }

    public readonly  ClientTaskCollection ClientTask  = new();

    public abstract Element render();

    protected  abstract void constructor();

    internal void InvokeConstructor() => constructor();

    protected virtual void componentDidMount()
    {
        
    }
}



public abstract class ReactComponent : ReactComponent<EmptyState>
{
}