using System.Collections;
using System.Collections.Immutable;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

static class ReflectionHelper
{
    public static object CreateDefaultValue(Type type)
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

            if (genericTypeDefinition == typeof(ImmutableList<>) ||
                genericTypeDefinition == typeof(IReadOnlyList<>))
            {
                var genericArgument = type.GetGenericArguments().First();
                
                return createImmutableListWithSampleData(genericArgument);
            }
            
            if (genericTypeDefinition.GetInterfaces().Contains(typeof(IEnumerable)))
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
            if (propertyInfo.GetIndexParameters().Length > 0)
            {
                continue;
            }

            var existingValue = propertyInfo.GetValue(instance);
            if (existingValue == null)
            {
                propertyInfo.SetValue(instance, CreateDefaultValue(propertyInfo.PropertyType));
            }
        }

        return instance;

        static object createImmutableListWithSampleData(Type genericArgumenType)
        {
            var type = typeof(ImmutableList<>).MakeGenericType(genericArgumenType);
            
            var immutableList = type.GetField("Empty", BindingFlags.Static | BindingFlags.Public)!.GetValue(null);
                
            var addMethod = type.GetMethod("Add")!;

            immutableList = addMethod.Invoke(immutableList, [CreateDefaultValue(genericArgumenType)]);
            immutableList = addMethod.Invoke(immutableList, [CreateDefaultValue(genericArgumenType)]);
            immutableList = addMethod.Invoke(immutableList, [CreateDefaultValue(genericArgumenType)]);
                
            return immutableList;
        }
    }

    public static bool IsStaticClass(this Type type)
    {
        return type.IsClass && type.IsAbstract && type.IsSealed;
    }
}