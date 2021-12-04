using System;
using System.Collections.Generic;
using System.Linq;
using PuppeteerSharp;

namespace FP
{
    [Serializable]
    public class M
    {
       
        protected readonly List<Exception> exceptions = new List<Exception>();

       
        public IReadOnlyList<Exception> Exceptions => exceptions;

        public static readonly M Success = new M();
        public string FailMessage => string.Join(Environment.NewLine, Exceptions.Select(e => e.Message));
        
        public bool IsFail => exceptions.Count > 0;

        
        public bool IsSuccess => exceptions.Count == 0;

        public static implicit operator M(Exception exception)
        {
            var response = new M();

            response.exceptions.Add(exception);

            return response;
        }
        
    }
    [Serializable]
    public sealed class M<T>:M
    {
        public T Value { get; set; }

       
        public static implicit operator M<T>(T value)
        {
            return new M<T> { Value = value };
        }

        public static implicit operator M<T>(Exception exception)
        {
            var response = new M<T>();

            response.exceptions.Add(exception);

            return response;
        }

        public static implicit operator M<T>(Exception[] exceptions)
        {
            var response = new M<T>();

            response.exceptions.AddRange(exceptions);

            return response;
        }
    }

    public static partial class FpExtensions
    {
        public static M Try(Action action)
        {
            try
            {
                action();

                return M.Success;
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public static M<T> Try<T>(Func<T> func)
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

        public static M<B> Bind<A,B>(this M<A> a, Func<A,B> toB)
        {
            try
            {
                if (a.IsSuccess)
                {
                    return toB(a.Value);
                }

                return a.Exceptions.ToArray();
            }
            catch (Exception exception)
            {
                return exception;
            }
        }

        public static void Match<A>(this M<A> response, Action<A> success, Action<IReadOnlyList<Exception>> fail)
        {
            if (response.IsSuccess)
            {
                success(response.Value);
                return;
            }

            fail(response.Exceptions);
        }
    }



}