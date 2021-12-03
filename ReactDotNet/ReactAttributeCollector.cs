using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bridge;

namespace ReactDotNet
{
    static class ReactAttributeCollector
    {

        [Template("Bridge.isPlainObject({0})")]
        static extern bool IsPlainObject(object value);

        [Template("Bridge.toPlain({0})")]
        static extern object ToPlain(object value);

        static bool IsBoxed(object value)
        {
            return value != null && value["$boxed"].As<bool>();
        }

        [Template("Bridge.unbox({0})")]
        static extern object GetBoxedValue(object value);

        public static ObjectLiteral CollectReactAttributedProperties(this IElement element)
        {
            var attributes = ObjectLiteral.Create<ObjectLiteral>();

            foreach (var propertyInfo in element.GetType().GetReactAttributedProperties())
            {
                var value = propertyInfo.GetValue(element);

                if (value == null)// if (value == null && propertyInfo.PropertyType.IsClass)
                {
                    continue;
                }
                
                if (IsBoxed(value))
                {
                    value = GetBoxedValue(value);
                }
                else
                {
                    value = ToPlain(value);
                }

                if (IsPlainObject(value) && Script.Write<bool>("Object.keys(value).length === 0"))
                {
                    continue;
                }
                
                attributes[propertyInfo.Name] = value;
            }

            return attributes;
        }

        public static IEnumerable<PropertyInfo> GetReactAttributedProperties(this Type type)
        {
            foreach (var propertyInfo in type.GetProperties().Where(p => p.CanRead && p.GetCustomAttributes(typeof(ReactAttribute)).Length > 0))
            {
                yield return propertyInfo;
            }
        }
    }
}