using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace FP
{
    public static class FpExtensions
    {
        public static Response Try(Action action)
        {
            try
            {
                action();
                
                return Response.Success;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        

        public static Response After<T>(this T response, Action action) where T: Response
        {
            if (response.IsFail)
            {
                return response;
            }

            return Try(action);
        }

        public static void After<T>(this T response, Action onSuccess, Action<IReadOnlyList<Error>> onFail) where T: Response
        {
            if (response.IsSuccess)
            {
                onSuccess();
                return;
            }

            onFail(response.Exceptions);
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> applyAction)
        {
            foreach (var item in items)
            {
                applyAction(item);
            }
        }

        public static Response ForEach<T>(this IEnumerable<T> items, Func<T,Response> applyAction)
        {
            var response = Response.Success;

            foreach (var item in items)
            {
                response += applyAction(item);
            }

            return response;
        }

        [Pure]
        public static Func<R> fun<R>(Func<R> f) => f;
        [Pure]
        public static Func<T1, R> fun<T1, R>(Func<T1, R> f) => f;
    }
}