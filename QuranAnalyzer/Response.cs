namespace QuranAnalyzer;

/// <summary>
///     The error
/// </summary>
[Serializable]
public sealed class Error
{
    /// <summary>
    ///     Gets or sets the message.
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Exception" /> to <see cref="Error" />.
    /// </summary>
    public static implicit operator Error(Exception exception)
    {
        return new Error
        {
            Message = exception.ToString()
        };
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="System.String" /> to <see cref="Error" />.
    /// </summary>
    public static implicit operator Error(string errorMessage)
    {
        return new Error
        {
            Message = errorMessage
        };
    }

    public override string ToString()
    {
        return Message;
    }
}

/// <summary>
///     The response
/// </summary>
[Serializable]
public class Response
{
    /// <summary>
    ///     The success
    /// </summary>
    public static readonly Response Success = new Response();

    /// <summary>
    ///     The errors
    /// </summary>
    protected readonly List<Error> errors = new List<Error>();

    /// <summary>
    ///     Gets the results.
    /// </summary>
    public IReadOnlyList<Error> Errors => errors;

    /// <summary>
    ///     Returns as array of errors
    /// </summary>
    public Error[] ErrorsAsArray => errors.ToArray();

    /// <summary>
    ///     Gets the fail message.
    /// </summary>
    public string FailMessage => string.Join(Environment.NewLine, errors.Select(e => e.Message));

    /// <summary>
    ///     Gets a value indicating whether this instance is fail.
    /// </summary>
    public bool IsFail => errors.Count > 0;

    /// <summary>
    ///     Gets a value indicating whether this instance is success.
    /// </summary>
    public bool IsSuccess => errors.Count == 0;

    /// <summary>
    ///     Fails the specified error message.
    /// </summary>
    public static Response Fail(string errorMessage)
    {
        return new Response
        {
            errors = { errorMessage }
        };
    }

    public static Response operator +(Response responseX, Response responseY)
    {
        var response = new Response();

        response.errors.AddRange(responseX.Errors);
        response.errors.AddRange(responseY.Errors);

        return response;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Exception" /> to <see cref="Response" />.
    /// </summary>
    public static implicit operator Response(Exception exception)
    {
        var response = new Response();

        response.errors.Add(exception);

        return response;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Error" /> to <see cref="Response" />.
    /// </summary>
    public static implicit operator Response(Error error)
    {
        var response = new Response();

        response.errors.Add(error);

        return response;
    }
}

/// <summary>
///     The response
/// </summary>
[Serializable]
public sealed class Response<TValue> : Response
{
    /// <summary>
    ///     Gets or sets the value.
    /// </summary>
    public TValue Value { get; set; }

    public static Response<TValue> Fail(Response response)
    {
        var newResponse = new Response<TValue>();

        newResponse.errors.AddRange(response.Errors);

        return newResponse;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Exception" /> to <see cref="Response{TValue}" />.
    /// </summary>
    public static implicit operator Response<TValue>(Exception exception)
    {
        var response = new Response<TValue>();

        response.errors.Add(exception);

        return response;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Error" /> to <see cref="Response{TValue}" />.
    /// </summary>
    public static implicit operator Response<TValue>(Error error)
    {
        var response = new Response<TValue>();

        response.errors.Add(error);

        return response;
    }

    public static implicit operator Response<TValue>(string error)
    {
        var response = new Response<TValue>();

        response.errors.Add(error);

        return response;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="Error" /> to <see cref="Response{TValue}" />.
    /// </summary>
    public static implicit operator Response<TValue>(Error[] errors)
    {
        var response = new Response<TValue>();

        response.errors.AddRange(errors);

        return response;
    }

    /// <summary>
    ///     Performs an implicit conversion from <see cref="TValue" /> to <see cref="Response{TValue}" />.
    /// </summary>
    public static implicit operator Response<TValue>(TValue value)
    {
        return new Response<TValue> { Value = value };
    }

    public TValue Unwrap()
    {
        if (IsFail)
        {
            throw new Exception(FailMessage);
        }

        return Value;
    }
}

public static class FpExtensions
{
    public static Response<B> Apply<A, B>(Func<A, Response<B>> functionAToB, A a)
    {
        return functionAToB(a);
    }

    public static Response<C> Apply<A, B, C>(Func<A, B, Response<C>> fn, Response<A> responseA, Response<B> responseB)
    {
        if (responseA.IsFail)
        {
            return responseA.Errors.ToArray();
        }

        if (responseB.IsFail)
        {
            return responseB.Errors.ToArray();
        }

        return fn(responseA.Value, responseB.Value);
    }

    public static Response<IReadOnlyList<TTarget>> AsListOf<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, Response<TTarget>> convertFunc)
    {
        if (source == null)
        {
            throw new ArgumentNullException(nameof(source));
        }

        if (convertFunc == null)
        {
            throw new ArgumentNullException(nameof(convertFunc));
        }

        var result = new List<TTarget>();

        foreach (var item in source)
        {
            var response = convertFunc(item);
            if (response.IsFail)
            {
                return response.Errors.ToArray();
            }

            result.Add(response.Value);
        }

        return result;
    }

    public static Response<int> GetIndex<T>(this T[] array, T value)
    {
        var index = Array.IndexOf(array, value);
        if (index < 0)
        {
            return $"{value} değeri listede bulunamadı";
        }

        return index;
    }

    public static Response<T> GetValueAt<T>(this T[] array, int index)
    {
        if (array is null)
        {
            return new ArgumentNullException(nameof(array));
        }

        if (index >= 0 && index < array.Length)
        {
            return new Response<T> { Value = array[index] };
        }

        return new IndexOutOfRangeException("index:" + index);
    }

    public static Response<int> ParseInt(string value)
    {
        return Try(() => int.Parse(value));
    }

    public static Response<B> Pipe<A, B>(Response<A> responseA, Func<A, Response<B>> func1)
    {
        if (responseA.IsFail)
        {
            return responseA.Errors.ToArray();
        }

        return func1(responseA.Value);
    }

    public static Response<B> Then<A, B>(this Response<A> response, Func<A, Response<B>> nextFunc)
    {
        if (response.IsFail)
        {
            return response.Errors.ToArray();
        }

        return nextFunc(response.Value);
    }

    public static Response<B> Then<A, B>(this Response<A> response, Func<A, B> nextFunc)
    {
        if (response.IsFail)
        {
            return response.Errors.ToArray();
        }

        return nextFunc(response.Value);
    }

    public static B Then<A, B>(this Response<A> response, Func<A, B> successFunc, Func<string, B> failFunc)
    {
        if (response.IsFail)
        {
            return failFunc(response.FailMessage);
        }

        return successFunc(response.Value);
    }

    public static C Then<A, B, C>(this Response<(A, B)> response, Func<A, B, C> successFunc, Func<string, C> failFunc)
    {
        if (response.IsFail)
        {
            return failFunc(response.FailMessage);
        }

        return successFunc(response.Value.Item1, response.Value.Item2);
    }

    public static D Then<A, B, C, D>(this Response<(A, B, C)> response, Func<A, B, C, D> successFunc, Func<string, D> failFunc)
    {
        if (response.IsFail)
        {
            return failFunc(response.FailMessage);
        }

        return successFunc(response.Value.Item1, response.Value.Item2, response.Value.Item3);
    }

    static Response<T> Try<T>(Func<T> func)
    {
        try
        {
            return func();
        }
        catch (Exception exception)
        {
            return exception;
        }
    }
}