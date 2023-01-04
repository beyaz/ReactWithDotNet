
namespace QuranAnalyzer;

public static class ListExtensions
{
    public static Response<TAccumulate> Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TSource, Response<TAccumulate>> func, Func<TAccumulate, TAccumulate, TAccumulate> acumulate)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (func == null)
        {
            throw new ArgumentNullException(nameof(func));
        }

        var result = seed;
        foreach (var element in source)
        {
            var response = func(element);
            if (response.IsFail)
            {
                return response.Errors.ToArray();
            }

            result = acumulate(result, response.Value);
        }

        return result;
    }



    public static TValue Unwrap<TValue>(this (TValue value, string exception) tuple)
    {
        if (tuple.exception is not null)
        {
            throw new Exception(tuple.exception);
        }

        return tuple.value;
    }

    

    public static Response<int> Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, Response<int>> selector)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        return source.Aggregate(0, selector, (total, value) => total + value);
    }

    public static IReadOnlyList<TTarget> AsListOf<TSource,TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> convertFunc)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (convertFunc == null)
        {
            throw new ArgumentNullException(nameof(convertFunc));
        }

        return source.Select(convertFunc).ToList();
    }

    public static IReadOnlyList<TTarget> AsListOf<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource,int, TTarget> convertFunc)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (convertFunc == null)
        {
            throw new ArgumentNullException(nameof(convertFunc));
        }

        return source.Select(convertFunc).ToList();
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