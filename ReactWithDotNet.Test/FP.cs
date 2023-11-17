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
public class OperationResponse
{
    public bool Success { get; init; }
    public bool Fail { get; init; }
    public string FailMessage { get; init; }
    
}

public class OperationResponse<TValue> : OperationResponse
{
    public TValue Value { get; init; }
    
    public static implicit operator OperationResponse<TValue>(TValue value)
    {
        return new OperationResponse<TValue> { Value = value };
    }
    
    public static implicit operator OperationResponse<TValue>(FailMessage failMessage)
    {
        return new OperationResponse<TValue> { Fail = true, FailMessage = failMessage.Message};
    }
}