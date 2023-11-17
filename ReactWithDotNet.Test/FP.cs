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
}