using System.Collections.Concurrent;
using System.Runtime.Loader;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace ReactWithDotNet.WebSite.HelperApps;

record CacheItem
{
    public string RenderPartOfCSharpCode { get; init; }
    public AssemblyLoadContext AssemblyLoadContext { get; init; }
    public Type Type { get; init; }
    public DateTime CreationTime { get; } = DateTime.Now;
}

sealed class ScriptManager
{
    readonly ConcurrentDictionary<string, CacheItem> Map = [];

    public static ScriptManager Instance { get; set; } = new();

    public CacheItem this[string key]
    {
        get => Map[key];
        set => Update(key, value);
    }
    
    
    // ReSharper disable once UnusedMember.Local
    public void RemoveOlderItems(TimeSpan duration)
    {
        foreach (var key in Map.Keys)
        {
            tryRemove(key);
        }

        return;

        bool shouldRemove(CacheItem cacheItem)
        {
            return cacheItem.CreationTime - DateTime.Now > duration;
        }

        void tryRemove(string key)
        {
            if (shouldRemove(Map[key]))
            {
                if (Map.TryRemove(key, out var cacheItem))
                {
                    cacheItem.AssemblyLoadContext?.Unload();
                }
            }
        }
    }

    void Update(string key, CacheItem newCacheItem)
    {
        if (Map.TryRemove(key, out var cacheItem))
        {
            cacheItem.AssemblyLoadContext?.Unload();
        }

        if (cacheItem is null)
        {
            return;
        }

        Map.TryAdd(key, newCacheItem);
    }
}


class ScriptCleanerBackgroundService: BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (true)
        {
            await Task.Delay(10, stoppingToken);
                    
            ScriptManager.Instance.RemoveOlderItems(TimeSpan.FromMinutes(10));
        }
        // ReSharper disable once FunctionNeverReturns
    }
}