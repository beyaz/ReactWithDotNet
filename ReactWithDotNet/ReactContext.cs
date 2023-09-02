using System.Collections.Specialized;
using System.Web;

namespace ReactWithDotNet;

// ReSharper disable once UnusedTypeParameter
public sealed class ReactContextKey<TValue>
{
    public readonly string Key;

    public ReactContextKey(string key)
    {
        Key = key;
    }

    public TValue this[ReactContext reactContext]
    {
        get => reactContext.TryGetValue(this);
        set => reactContext.Set(Key,value);
    }
}

public sealed class ReactContext
{
    internal ReactContext()
    {
        
    }
    readonly Dictionary<string, object> map = new();

    public double? ClientHeight { get; internal set; }

    public double? ClientWidth { get; internal set; }

    public NameValueCollection Query { get; internal set; } = new();

    public string QueryAsString => string.Join("&", Query.AllKeys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(Query[key])}"));
    
    internal IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; init; }

    public bool Contains<TValue>(ReactContextKey<TValue> key)
    {
        return map.ContainsKey(key.Key);
    }

    public void Set<TValue>(ReactContextKey<TValue> key, TValue value)
    {
        Set(key.Key, value);
    }

    public void Set<TValue>(string key, TValue value)
    {
        if (map.ContainsKey(key))
        {
            map[key] = value;
            return;
        }

        map.Add(key, value);
    }

    public TValue TryGetValue<TValue>(ReactContextKey<TValue> key)
    {
        if (map.TryGetValue(key.Key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }

    public TValue TryGetValue<TValue>(string key)
    {
        if (map.TryGetValue(key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }
}