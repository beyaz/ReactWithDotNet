using System.Collections;
using System.Collections.Immutable;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

static class ReflectionHelper
{
    public static object CreateDefaultValue(Type type, int index = 1)
    {
        if (type == typeof(string))
        {
            return "abc"+index;
        }

        if (type == typeof(int)||
            type == typeof(long)||
            type == typeof(decimal)||
            type == typeof(byte)||
            type == typeof(short)||
            type == typeof(decimal)||
            type == typeof(double)||
            type == typeof(float))
        {
            return Convert.ChangeType(index, type);
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
                genericTypeDefinition == typeof(IReadOnlyList<>)||
                genericTypeDefinition == typeof(ICollection<>)||
                genericTypeDefinition == typeof(IList<>))
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

        {
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
                    propertyInfo.SetValue(instance, CreateDefaultValue(propertyInfo.PropertyType,index));
                }
            }

            return instance;
        }

        static object createImmutableListWithSampleData(Type genericArgumenType)
        {
            var type = typeof(ImmutableList<>).MakeGenericType(genericArgumenType);
            
            var immutableList = type.GetField("Empty", BindingFlags.Static | BindingFlags.Public)!.GetValue(null);
                
            var addMethod = type.GetMethod("Add")!;

            // ReSharper disable once RedundantArgumentDefaultValue
            immutableList = addMethod.Invoke(immutableList, [CreateDefaultValue(genericArgumenType,1)]);
            immutableList = addMethod.Invoke(immutableList, [CreateDefaultValue(genericArgumenType,2)]);
                
            return immutableList;
        }
    }

    public static bool IsStaticClass(this Type type)
    {
        return type.IsClass && type.IsAbstract && type.IsSealed;
    }
}