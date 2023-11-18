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
    
    public static (bool success, T value) Or<T>(this Response<T> a, Func<(bool success, T value)> b)
    {
        if (a.IsSuccess)
        {
            return (true, a.Value);
        }

        return b();
    }
    
    public static Response<TTarget> To<TSource,TTarget>(this Response<TSource> sourceResponse, Func<TSource, TTarget> nextFunc)
    {
        if (sourceResponse.IsSuccess)
        {
            return nextFunc(sourceResponse.Value);
        }

        return sourceResponse.FailInfo;
    }

    public static FailInfo Fail(string failMessage)
    {
        return new FailInfo { Message = failMessage };
    }

    public static FailInfo None => Fail("None");

    public static Response<IReadOnlyList<TTarget>> Select<TSource, TTarget>(this Response<IReadOnlyList<IReadOnlyList<TSource>>> response, Func<IReadOnlyList<TSource>, Response<TTarget>> convertFunc)
    {
        if (response.IsFail)
        {
            return response.FailInfo;
        }

        if (response.Value == null)
        {
            return null;
        }

        var returnList = new List<TTarget>();
        
        foreach (var item in response.Value)
        {
            var convertResponse = convertFunc(item);
            if (convertResponse.IsFail)
            {
                return convertResponse.FailInfo;
            }
            
            returnList.Add(convertResponse.Value);
        }

        return returnList;
    }
    
    public static Response<IReadOnlyList<TTarget>> Select<TSource, TTarget>(this Response<IReadOnlyList<TSource>> response, Func<TSource, Response<TTarget>> convertFunc)
    {
        if (response.IsFail)
        {
            return response.FailInfo;
        }

        if (response.Value == null)
        {
            return null;
        }

        var returnList = new List<TTarget>();
        
        foreach (var item in response.Value)
        {
            var convertResponse = convertFunc(item);
            if (convertResponse.IsFail)
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
public class Response
{
    public bool IsSuccess { get; init; }
    public bool IsFail { get; init; }
    public FailInfo FailInfo { get; init; }
    
    public string FailMessage => FailInfo.Message;
    
}

public class Response<TValue> : Response
{
    public TValue Value { get; init; }
    
    public static implicit operator Response<TValue>(TValue value)
    {
        return new Response<TValue> { Value = value, IsSuccess = true };
    }
    
    public static implicit operator Response<TValue>(FailInfo failInfo)
    {
        return new Response<TValue> { IsFail = true, FailInfo= failInfo};
    }
}