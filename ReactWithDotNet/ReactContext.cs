using Microsoft.AspNetCore.Http;

namespace ReactWithDotNet;

// ReSharper disable once UnusedTypeParameter
public sealed class ReactContextKey<TValue>
{
    public readonly string Key;

    internal Func<ReactContext, TValue> AccessFunc;

    public ReactContextKey(string key) // todo: fixme
    {
        Key = key;
    }

    public ReactContextKey(string key, Func<ReactContext, TValue> accessFunc)
    {
        Key = key;
        AccessFunc = accessFunc;
    }

    public TValue this[ReactContext reactContext]
    {
        get => reactContext.TryGetValue(this);
        set => reactContext.Set(Key, value);
    }
}

public sealed class ReactContext
{
    readonly Dictionary<string, object> map = new();

    internal ReactContext()
    {
    }

    public required HttpContext HttpContext { get; init; }

    public (string Path, IQueryCollection Query) Request { get; init; }

    public string wwwroot { get; init; }

    internal IReadOnlyDictionary<string, ClientStateInfo> CapturedStateTree { get; init; }
    internal double? ClientHeight { get; init; }

    internal double? ClientWidth { get; init; }
    

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
        
        if (key.AccessFunc is not null)
        {
            var newValue = key.AccessFunc(this);
            
            map[key.Key] = newValue;

            return newValue;
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