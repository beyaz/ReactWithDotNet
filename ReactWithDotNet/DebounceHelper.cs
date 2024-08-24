using System.Collections.Concurrent;
using System.Reflection;

namespace ReactWithDotNet;

sealed class DebounceMethods
{
    public Func<object, object> DebounceHandlerGetFunc;
    public Func<object, object> DebounceTimeoutGetFunc;
}

static class DebounceHelper
{
    static readonly ConcurrentDictionary<Type, ConcurrentDictionary<string, DebounceMethods>> DebounceMethodCache = new();

    public static DebounceMethods CalculateDebounceMethods(PropertyInfo propertyInfo)
    {
        Func<object, object> debounceTimeoutGetFunc = null;
        Func<object, object> debounceHandlerGetFunc = null;

        var debounceTimeoutPropertyInfo = propertyInfo.DeclaringType?.GetProperty(propertyInfo.Name + "DebounceTimeout");
        if (debounceTimeoutPropertyInfo is not null)
        {
            debounceTimeoutGetFunc = ReflectionHelper.CreateGetFunction(debounceTimeoutPropertyInfo);

            var debounceHandlerPropertyInfo = propertyInfo.DeclaringType?.GetProperty(propertyInfo.Name + "DebounceHandler");
            if (debounceHandlerPropertyInfo is not null)
            {
                debounceHandlerGetFunc = ReflectionHelper.CreateGetFunction(debounceHandlerPropertyInfo);
            }
        }

        return new()
        {
            DebounceTimeoutGetFunc = debounceTimeoutGetFunc,
            DebounceHandlerGetFunc = debounceHandlerGetFunc
        };
    }

    public static DebounceMethods GetDebounceMethods(Type instanceType, string nameOfProperpertyDefinition)
    {
        if (!DebounceMethodCache.TryGetValue(instanceType, out var map))
        {
            var propertyInfo = instanceType.GetProperty(nameOfProperpertyDefinition);

            map = new();

            map.TryAdd(nameOfProperpertyDefinition, CalculateDebounceMethods(propertyInfo));

            DebounceMethodCache.TryAdd(instanceType, map);
        }

        if (!map.TryGetValue(nameOfProperpertyDefinition, out var debounceMethods))
        {
            var propertyInfo = instanceType.GetProperty(nameOfProperpertyDefinition);

            debounceMethods = CalculateDebounceMethods(propertyInfo);

            map.TryAdd(nameOfProperpertyDefinition, debounceMethods);
        }

        return debounceMethods;
    }
}