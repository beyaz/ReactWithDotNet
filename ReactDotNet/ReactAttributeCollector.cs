using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactDotNet.Html5;

static class ReactAttributeCollector
{

      

    public static Dictionary<string,object> CollectReactAttributedProperties(this Element element)
    {
        var attributes = new Dictionary<string, object>();

        foreach (var propertyInfo in element.GetType().GetReactAttributedProperties())
        {
            var value = propertyInfo.GetValue(element);

            if (value == null)
            {
                continue;
            }

            var propName = propertyInfo.Name;

            var jsonPropertyName = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>();
            if (jsonPropertyName != null)
            {
                propName = jsonPropertyName.Name;
            }
                
            attributes[propName] = value;
        }

        return attributes;
    }

    public static IEnumerable<PropertyInfo> GetReactAttributedProperties(this Type type)
    {
        foreach (var propertyInfo in type.GetProperties().Where(p => p.CanRead && p.GetCustomAttributes(typeof(ReactAttribute)).Any()))
        {
            yield return propertyInfo;
        }
    }
}