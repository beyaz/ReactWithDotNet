using System.Collections.Immutable;

namespace ReactWithDotNet.UIDesigner;

static class ReflectionHelper
{
    public static void ArrangeMap(Dictionary<string, object> map, string key, Type type)
    {
        if (key == null)
        {
            return;
        }

        if (map.ContainsKey(key))
        {
            return;
        }

        map.Add(key, CreateDefaultValue(type));
    }


    static object CreateDefaultValue(Type type)
    {
        if (type == typeof(string))
        {
            return "";
        }

        if (type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }

        if (type.IsArray)
        {
            var elementType = type.GetElementType();
            if (elementType is not null)
            {
                return Array.CreateInstance(elementType, 0);
            }
        }

        if (type.IsGenericType)
        {
            var genericTypeDefinition = type.GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(IReadOnlyList<>) ||
                genericTypeDefinition == typeof(IReadOnlyCollection<>) ||
                genericTypeDefinition == typeof(IList<>) ||
                genericTypeDefinition == typeof(ICollection<>) ||
                genericTypeDefinition == typeof(List<>) ||
                genericTypeDefinition == typeof(ImmutableList<>))
            {
                var genericArgument = type.GetGenericArguments().FirstOrDefault();
                if (genericArgument is not null)
                {
                    return Array.CreateInstance(genericArgument, 0);
                }
            }
        }

        var instance = Activator.CreateInstance(type);

        foreach (var propertyInfo in type.GetProperties())
        {
            var existingValue = propertyInfo.GetValue(instance);
            if (existingValue == null)
            {
                propertyInfo.SetValue(instance, CreateDefaultValue(propertyInfo.PropertyType));
            }
            
        }

        return instance;
    }


  
}