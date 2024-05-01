using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;

namespace ReactWithDotNet;

static class Extensions
{
    public static (IReadOnlyList<string> path, bool isConnectedToState) AsBindingPath(this LambdaExpression propertyAccessor)
    {
        var path = AsPath(propertyAccessor.Body);

        if (path.Count == 0)
        {
            return default;
        }

        if (path[^1] == "state")
        {
            path.RemoveAt(path.Count - 1);

            path.Reverse();

            return (path, true);
        }

        path.Reverse();

        return (path, false);

        static List<string> AsPath(Expression expression)
        {
            var path = new List<string>();

            while (expression is not null)
            {
                if (expression is UnaryExpression unaryExpression)
                {
                    expression = unaryExpression.Operand;

                    continue;
                }

                if (expression is MemberExpression memberExpression)
                {
                    path.Add(memberExpression.Member.Name);

                    expression = memberExpression.Expression;

                    if (expression is ConstantExpression)
                    {
                        break;
                    }

                    continue;
                }

                if (expression is BinaryExpression binaryExpression)
                {
                    if (binaryExpression.Right is ConstantExpression constantExpression)
                    {
                        path.Add("]");
                        path.Add(constantExpression.Value!.ToString());
                        path.Add("[");

                        expression = binaryExpression.Left;
                        continue;
                    }
                }

                if (expression is MethodCallExpression { Method.Name: "get_Item" } methodCallExpression)
                {
                    if (methodCallExpression.Arguments[0] is ConstantExpression constantExpression1)
                    {
                        path.Add("]");
                        path.Add(constantExpression1.Value!.ToString());
                        path.Add("[");

                        expression = methodCallExpression.Object;
                        continue;
                    }

                    if (methodCallExpression.Arguments[0] is MemberExpression { Expression: ConstantExpression constantExpression })
                    {
                        object index = null;

                        foreach (var fieldInfo in constantExpression.Value!.GetType().GetFields())
                        {
                            if (isNumberType(fieldInfo.FieldType))
                            {
                                index = fieldInfo.GetValue(constantExpression.Value);
                                break;
                            }
                        }

                        if (index is null)
                        {
                            throw new DeveloperException(constantExpression.ToString());
                        }

                        path.Add("]");
                        path.Add(index.ToString());
                        path.Add("[");

                        expression = methodCallExpression.Object;
                        continue;
                    }

                    if (methodCallExpression.Arguments[0] is MemberExpression { Expression: MemberExpression e1 })
                    {
                        path.Add("]");
                        path.AddRange(AsPath(e1));
                        path.Add("[");

                        expression = methodCallExpression.Object;
                        continue;
                    }
                }

                throw new DeveloperException(expression.ToString());
            }

            return path;

            static bool isNumberType(Type type)
            {
                return type == typeof(int) ||
                       type == typeof(long) ||
                       type == typeof(decimal) ||
                       type == typeof(byte) ||
                       type == typeof(short) ||
                       type == typeof(decimal) ||
                       type == typeof(double) ||
                       type == typeof(float);
            }
        }
    }

    public static (object value, Exception exception) ChangeType(object value, Type targetType)
    {
        if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            if (value == null)
            {
                return (null, default);
            }

            targetType = Nullable.GetUnderlyingType(targetType);
            if (targetType is null)
            {
                return (default, new InvalidCastException($"{value}"));
            }
        }

        try
        {
            return (Convert.ChangeType(value, targetType), default);
        }
        catch (Exception exception)
        {
            return (default, exception);
        }
    }

    public static Exception DeveloperException(string message)
    {
        return new DeveloperException(message);
    }

    public static IReadOnlyList<T> NewListWith<T>(this IReadOnlyList<T> readOnlyList, T newValue)
    {
        if (readOnlyList == null)
        {
            throw new ArgumentNullException(nameof(readOnlyList));
        }

        return new List<T>(readOnlyList) { newValue };
    }

    /// <summary>
    ///     Removes value from end of str
    /// </summary>
    public static string RemoveFromEnd(this string data, string value)
    {
        return RemoveFromEnd(data, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    public static string RemoveFromEnd(this string data, string value, StringComparison comparison)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    public static string RemoveFromStart(this string data, string value)
    {
        return RemoveFromStart(data, value, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    public static string RemoveFromStart(this string data, string value, StringComparison comparison)
    {
        if (data == null)
        {
            return null;
        }

        if (data.StartsWith(value, comparison))
        {
            return data.Substring(value.Length, data.Length - value.Length);
        }

        return data;
    }

    public static IReadOnlyList<B> ToReadOnlyListOf<A, B>(this IEnumerable enumerable, Func<A, B> convertFunc)
    {
        if (enumerable == null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        var list = new List<B>();

        foreach (A item in enumerable)
        {
            list.Add(convertFunc(item));
        }

        return list;
    }
}

static class MethodAccess
{
    static readonly ConcurrentDictionary<string, MethodInfo> Cache = new();

    public static string GetNameWithToken(this MethodInfo methodInfo)
    {
        var cacheKey = methodInfo.GetCacheKey();

        Cache.TryAdd(cacheKey, methodInfo);

        return cacheKey;
    }

    public static bool TryResolveMethodInfo(string cacheKey, ref MethodInfo methodInfo)
    {
        if (Cache.TryGetValue(cacheKey, out methodInfo))
        {
            return true;
        }

        methodInfo = FindMethod(cacheKey);
        if (methodInfo is null)
        {
            return false;
        }

        Cache.TryAdd(cacheKey, methodInfo);

        return true;
    }

    static MethodInfo FindMethod(string cacheKey)
    {
        var index = cacheKey.IndexOf('#');
        if (index <= 0)
        {
            return null;
        }

        var type = Type.GetType(cacheKey[(index + 1)..]);
        if (type is null)
        {
            return null;
        }

        foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (methodInfo.GetCacheKey() == cacheKey)
            {
                return methodInfo;
            }
        }

        return null;
    }

    static string GetCacheKey(this MethodInfo methodInfo)
    {
        if (methodInfo == null)
        {
            throw new ArgumentNullException(nameof(methodInfo));
        }

        var str = methodInfo.ToString();
        if (str is null)
        {
            throw new ArgumentException(nameof(str));
        }

        var methodNameWithoutReturnType = str.Substring(str.IndexOf(' ') + 1);

        var type = methodInfo.ReflectedType;
        if (type is null)
        {
            throw new ArgumentException(nameof(type));
        }

        var assemblyName = type.Assembly.GetName().Name;

        return $"{methodNameWithoutReturnType}#{type},{assemblyName}";
    }
}

[Serializable]
public sealed class DeveloperException : Exception
{
    public DeveloperException(string message) : base(message)
    {
    }

    public DeveloperException(string message, Exception innerException) : base(message, innerException)
    {
    }
}