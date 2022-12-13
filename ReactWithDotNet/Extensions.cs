using System.Reflection;

namespace ReactWithDotNet;

static class Extensions
{
    public static string GetNameWithToken(this MethodInfo methodInfo)
    {
        return $"{methodInfo.MetadataToken}|{methodInfo.Name}";
    }

    public static int? TryGetMethodInfoMetadataTokenFromName(string nameWithToken)
    {
        var index = nameWithToken.IndexOf('|');
        if (index > 0)
        {
            return int.Parse(nameWithToken[..index]);
        }

        return null;
    }

    public static (IReadOnlyList<string> path, bool isConnectedToState) AsBindingPath<T>(this Expression<Func<T>> propertyAccessor)
    {
        var expression = propertyAccessor.Body;

        var path = new List<string>();

        while (expression is not null)
        {
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
                    path.Add(constantExpression.Value.ToString());
                    path.Add("[");

                    expression = binaryExpression.Left;
                    continue;
                }
            }

            if (expression is MethodCallExpression methodCallExpression)
            {
                if (methodCallExpression.Method.Name == "get_Item")
                {
                    if (methodCallExpression.Arguments[0] is ConstantExpression constantExpression1)
                    {
                        path.Add("]");
                        path.Add(constantExpression1.Value.ToString());
                        path.Add("[");

                        expression = methodCallExpression.Object;
                        continue;
                    }

                    if (methodCallExpression.Arguments[0] is MemberExpression memberExpression2)
                    {
                        if (memberExpression2.Expression is ConstantExpression constantExpression)
                        {
                            var index = constantExpression.Value.GetType().GetFields()[0].GetValue(constantExpression.Value);

                            path.Add("]");
                            path.Add(index?.ToString());
                            path.Add("[");

                            expression = methodCallExpression.Object;
                            continue;
                        }
                    }
                }
            }

            throw new DeveloperException(propertyAccessor.ToString());
        }

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
    }

    public static Exception DeveloperException(string message)
    {
        return new DeveloperException(message);
    }

    public static object GetDefaultValue(this Type t)
    {
        if (t.IsValueType)
        {
            return Activator.CreateInstance(t);
        }

        return null;
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