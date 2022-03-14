using System;
using System.Collections.Concurrent;
using System.Text.Json;

namespace QuranAnalyzer;

static class CachedAccess
{
    #region Static Fields
    static readonly ConcurrentDictionary<string, string> cache = new();
    #endregion

    #region Public Methods
    public static Response<T> AccessValue<T>(string key, Func<Response<T>> func)
    {
        var cachedValue = TryGetFromCache(key);

        if (cachedValue != null)
        {
            return JsonSerializer.Deserialize<T>(cachedValue);
        }

        var response = func();

        if (response.IsFail)
        {
            return response;
        }

        cachedValue = JsonSerializer.Serialize(response.Value, new JsonSerializerOptions {WriteIndented = true});

        TryAddToCache(key, cachedValue);

        return response;
    }
    #endregion

    #region Methods
    static void TryAddToCache(string key, string value)
    {
        cache.TryAdd(key, value);
    }

    static string TryGetFromCache(string key)
    {
        cache.TryGetValue(key, out string value);

        return value;
    }
    #endregion
}