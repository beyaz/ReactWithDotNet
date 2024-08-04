using Microsoft.AspNetCore.Http;

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
        set => reactContext.Set(Key, value);
    }
}

public sealed class ReactContext
{
    public required HttpContext HttpContext{ get; init; }
    
    public string wwwroot { get; init; }
    
    internal double? ClientWidth { get; init; }
    internal double? ClientHeight { get; init; }
    
    readonly Dictionary<string, object> map = new();

    internal ReactContext()
    {
    }

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
        map[key] = value;
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