global using static ReactWithDotNet.WebSite.QueryStringStore;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Extensions;

namespace ReactWithDotNet.WebSite;

static class QueryStringStore
{
    const string QueryName = "q";

    public static string CalculateUrl<T>(string path, T queryData)
    {
        if (queryData is null)
        {
            return path;
        }
        var queryBuilder = new QueryBuilder { { QueryName, Json.Serialize(queryData) } };

        return path + queryBuilder.ToQueryString();
    }
    
    public static void Route<TModel>(this IReactComponent component, TModel model)
    {
        var context = component.Context;
        var client = component.Client;

        var queryCollection = context.Request.Query;

        var queryBuilder = new QueryBuilder(queryCollection.Where(x=>!QueryName.Equals(x.Key, StringComparison.OrdinalIgnoreCase)))
        {
            { QueryName, Json.Serialize(model) }
        };

        var queryString = queryBuilder.ToQueryString();

        var requestPath = context.Request.Path;
        
        client.HistoryReplaceState(null, null, requestPath + queryString);
    }

    public static TModel TryReadFromQueryString<TModel>(ReactContext context)
    {
        var json = context.Request.Query[QueryName];

        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        return Json.Deserialize<TModel>(json);
    }

    static class Json
    {
        static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented          = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            IncludeFields          = true,
        };

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, Options);
        }

        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, Options);
        }
    }
}