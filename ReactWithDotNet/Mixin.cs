using System.Reflection;
using System.Text.Json;
using System.Web;

namespace ReactWithDotNet;

public static partial class Mixin
{
    internal static IReadOnlyDictionary<string, string> ParseQueryString(string query)
    {
        var items = new Dictionary<string, string>();

        if (query == null)
        {
            return items;
        }

        var nameValueCollection = HttpUtility.ParseQueryString(query);
        foreach (var key in nameValueCollection.AllKeys)
        {
            items.Add(key, nameValueCollection.Get(key));
        }

        return items;
    }

    public static  JsonNamingPolicy JsonNamingPolicy;

    public static string ToJson(object value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();
            
        return JsonSerializer.Serialize(value, options);
    }


    internal static IReadOnlyList<(PropertyInfo propertyInfo, object newValue)> GetValues(this Style style)
    {
        var returnItems = new List<(PropertyInfo propertyInfo, object newValue)>();

        foreach (var propertyInfo in typeof(Style).GetProperties().Where(p=>p.CanRead))
        {
            var value = propertyInfo.GetValue(style);

            var defaultValue = propertyInfo.PropertyType.GetDefaultValue();

            if (value == null)
            {
                if (defaultValue == null)
                {
                    continue;
                }

                returnItems.Add((propertyInfo, null));
                continue;
            }

            if (!value.Equals(defaultValue))
            {
                returnItems.Add((propertyInfo, value));
            }
        }

        return returnItems;
    }

        

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }


    


    

    

   

    public static TParent appendChild<TParent,TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }
}