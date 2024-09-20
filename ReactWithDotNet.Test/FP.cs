namespace ReactWithDotNet;

static class FP
{
    public static (bool success, T value) Or<T>(this (bool success, T value) a, Func<(bool success, T value)> b)
    {
        if (a.success)
        {
            return (true, a.value);
        }

        return b();
    }
    
    public static (bool success, T value) Or<T>(this Result<T> a, Func<(bool success, T value)> b)
    {
        if (a.Success)
        {
            return (true, a.Value);
        }

        return b();
    }
    
    public static Result<TTarget> To<TSource,TTarget>(this Result<TSource> sourceResult, Func<TSource, TTarget> nextFunc)
    {
        if (sourceResult.Success)
        {
            return nextFunc(sourceResult.Value);
        }

        return sourceResult.FailInfo;
    }

    public static FailInfo Fail(string failMessage)
    {
        return new FailInfo { Message = failMessage };
    }

    public static FailInfo None => Fail("None");

    public static Result<IReadOnlyList<TTarget>> Select<TSource, TTarget>(this Result<IReadOnlyList<IReadOnlyList<TSource>>> result, Func<IReadOnlyList<TSource>, Result<TTarget>> convertFunc)
    {
        if (result.Fail)
        {
            return result.FailInfo;
        }

        if (result.Value == null)
        {
            return null;
        }

        var returnList = new List<TTarget>();
        
        foreach (var item in result.Value)
        {
            var convertResponse = convertFunc(item);
            if (convertResponse.Fail)
            {
                return convertResponse.FailInfo;
            }
            
            returnList.Add(convertResponse.Value);
        }

        return returnList;
    }
    
    public static Result<IReadOnlyList<TTarget>> Select<TSource, TTarget>(this Result<IReadOnlyList<TSource>> result, Func<TSource, Result<TTarget>> convertFunc)
    {
        if (result.Fail)
        {
            return result.FailInfo;
        }

        if (result.Value == null)
        {
            return null;
        }

        var returnList = new List<TTarget>();
        
        foreach (var item in result.Value)
        {
            var convertResponse = convertFunc(item);
            if (convertResponse.Fail)
            {
                return convertResponse.FailInfo;
            }
            
            returnList.Add(convertResponse.Value);
        }

        return returnList;
    }
}

public sealed class FailInfo
{
    public string Message { get; init; }
}

public class Result
{
    public bool Success { get; init; }
    public bool Fail { get; init; }
    public FailInfo FailInfo { get; init; }
    
    public string FailMessage => FailInfo.Message;
}

public class Result<TValue> : Result
{
    public TValue Value { get; init; }
    
    public static implicit operator Result<TValue>(TValue value)
    {
        return new() { Value = value, Success = true };
    }
    
    public static implicit operator Result<TValue>(FailInfo failInfo)
    {
        return new() { Fail = true, FailInfo= failInfo};
    }
}