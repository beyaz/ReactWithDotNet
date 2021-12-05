using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace FP
{
    /// <summary>
    ///     The fp extensions
    /// </summary>
    public static class FpExtensions
    {
        #region Public Methods
        /// <summary>
        ///     Binds the specified function.
        /// </summary>
        public static Response<B> Bind<A, B>(this Response<A> response, Func<A, Response<B>> func)
        {
            if (response.IsFail)
            {
                return response.Exceptions.ToArray();
            }

            return func(response.Value);
        }

        /// <summary>
        ///     Binds the specified to b.
        /// </summary>
        public static Response<B> Bind<A, B>(this Response<A> response, Func<A, B> toB)
        {
            try
            {
                if (response.IsSuccess)
                {
                    return toB(response.Value);
                }

                return response.Exceptions.ToArray();
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        /// <summary>
        ///     Funs the specified f.
        /// </summary>
        [Pure]
        public static Func<R> fun<R>(Func<R> f) => f;

        /// <summary>
        ///     Funs the specified f.
        /// </summary>
        [Pure]
        public static Func<T1, R> fun<T1, R>(Func<T1, R> f) => f;

        /// <summary>
        ///     Matches the specified on success.
        /// </summary>
        public static void Match<TValue>(this Response<TValue> response, Action<TValue> onSuccess, Action<IReadOnlyList<Exception>> onFail)
        {
            if (response.IsSuccess)
            {
                onSuccess(response.Value);
                return;
            }

            onFail(response.Exceptions);
        }

        

        /// <summary>
        ///     Tries the specified function.
        /// </summary>
        public static Response<A> Try<A>(Func<A> func)
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
        #endregion
    }
}