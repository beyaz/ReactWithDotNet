using System;

namespace ReactDotNet;

public sealed class BindibleProperty<T>
{
    public string PathInState { get; set; }

    public T RawValue { get; set; }

    public string[] AsBindingSourcePathInState()
    {
        return PathInState.Split(".[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
    }

    public static implicit operator BindibleProperty<T>(T rawValue)
    {
        return new BindibleProperty<T> { RawValue = rawValue };
    }
}