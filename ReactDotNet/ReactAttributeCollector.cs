using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ReactDotNet
{
    static class ReactAttributeCollector
    {

      

        public static Dictionary<string,object> CollectReactAttributedProperties(this IElement element)
        {
            var attributes = new Dictionary<string, object>();

            foreach (var propertyInfo in element.GetType().GetReactAttributedProperties())
            {
                var value = propertyInfo.GetValue(element);

                if (value == null)
                {
                    continue;
                }
                
                
                attributes[propertyInfo.Name] = value;
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
}