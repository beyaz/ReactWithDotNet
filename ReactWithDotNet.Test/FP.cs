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

    public static FailMessage Fail(string failMessage)
    {
        return new FailMessage { Message = failMessage };
    }
}

public sealed class FailMessage
{
    public string Message { get; init; }
}
public class Response
{
    public bool Success { get; init; }
    public bool Fail { get; init; }
    public string FailMessage { get; init; }
    
}

public class Response<TValue> : Response
{
    public TValue Value { get; init; }
    
    public static implicit operator Response<TValue>(TValue value)
    {
        return new Response<TValue> { Value = value, Success = true };
    }
    
    public static implicit operator Response<TValue>(FailMessage failMessage)
    {
        return new Response<TValue> { Fail = true, FailMessage = failMessage.Message};
    }
}