namespace ReactWithDotNet;

static class Extensions
{

    public static (IReadOnlyList<string> path, bool isConnectedToState) AsBindingPath<T>(this Expression<Func<T>> propertyAccessor)
    {
        var memberExpression = propertyAccessor.Body as MemberExpression;
        if (memberExpression == null)
        {
            throw new ArgumentException(propertyAccessor.ToString());
        }


        
            
        var path = new List<string>();

        while (memberExpression != null)
        {
            path.Add(memberExpression.Member.Name);

            if (memberExpression.Expression is MethodCallExpression methodCallExpression)
            {
                if (methodCallExpression.Method.Name == "get_Item")
                {
                    if (methodCallExpression.Arguments[0] is MemberExpression memberExpression2)
                    {
                        if (memberExpression2.Expression is ConstantExpression constantExpression)
                        {
                            var index = constantExpression.Value.GetType().GetFields()[0].GetValue(constantExpression.Value);


                            path.Add("[");
                            path.Add(index?.ToString());
                            path.Add("]");
                            continue;
                        }
                    }

                    
                }
               
            }
            memberExpression = memberExpression.Expression as MemberExpression;
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