
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace ReactDotNet.Html5;

partial class Mixin
{
    public static Style ParseCss(string css)
    {
        var style = new Style();

        foreach (var (propertyInfo, propertyValue) in GetCss(css))
        {
            propertyInfo.SetValue(style, propertyValue);
        }

        return style;
    }
    

    static readonly IReadOnlyList<PropertyInfo> StyleProperties = typeof(Style).GetProperties();

    static IReadOnlyList<(PropertyInfo propertyInfo, object propertyValue)> GetCss(string css)
    {
        var items = new List<(PropertyInfo propertyInfo, object propertyValue)>();

        if (css == null)
        {
            return items;
        }

        foreach (var line in css.Trim().Split(";").Select(x=>x.Trim()).Where(x=>!string.IsNullOrWhiteSpace(x)))
        {
            var (key, value) = parseLine(line);

            key = key.Replace("-", "");

            var propertyInfo = StyleProperties.First(x => key.Equals(x.Name,StringComparison.OrdinalIgnoreCase));
            if (propertyInfo == null)
            {
                throw new Exception(line);
            }

            if (propertyInfo.PropertyType == typeof(string))
            {
                items.Add((propertyInfo, value));
                continue;
            }

            if (propertyInfo.PropertyType.GetGenericTypeDefinition()?.Equals(typeof(Union<,>))== true)
            {
                var converter = propertyInfo.PropertyType.GetMethod("op_Implicit", new[] { typeof(string) });
                if (converter == null)
                {
                    throw new Exception(line);
                }
                var propertyValue = converter.Invoke(null, new object[] {value});

                items.Add((propertyInfo, propertyValue));
                continue;
            }

            throw new Exception(line);

        }

        return items;

        static (string key, string value) parseLine(string line)
        {
            var array = line.Trim().Split(":").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (array.Length != 2)
            {
                throw new Exception(line);
            }

            return (array[0],array[1]);
        }
    }
}
        