using System;
using System.Collections.Generic;
using System.Linq;

namespace FP
{
    /// <summary>
    ///     The response
    /// </summary>
    [Serializable]
    public class Response
    {
        #region Static Fields
        /// <summary>
        ///     The success
        /// </summary>
        public static readonly Response Success = new Response();
        #endregion

        #region Fields
        /// <summary>
        ///     The exceptions
        /// </summary>
        protected readonly List<Exception> exceptions = new List<Exception>();
        #endregion

        #region Public Properties
        /// <summary>
        ///     Gets the exceptions.
        /// </summary>
        public IReadOnlyList<Exception> Exceptions => exceptions;

        /// <summary>
        ///     Gets the fail message.
        /// </summary>
        public string FailMessage => string.Join(Environment.NewLine, exceptions.Select(e => e.Message));

        /// <summary>
        ///     Gets a value indicating whether this instance is fail.
        /// </summary>
        public bool IsFail => exceptions.Count > 0;

        /// <summary>
        ///     Gets a value indicating whether this instance is success.
        /// </summary>
        public bool IsSuccess => exceptions.Count == 0;
        #endregion

        #region Public Methods
        /// <summary>
        ///     Fails the specified error message.
        /// </summary>
        public static Response Fail(string errorMessage)
        {
            return new Response
            {
                exceptions = { new Exception(errorMessage) }
            };
        }

        /// <summary>
        ///     Implements the operator +.
        /// </summary>
        public static Response operator +(Response responseX, Response responseY)
        {
            var response = new Response();

            response.exceptions.AddRange(responseX.Exceptions);
            response.exceptions.AddRange(responseY.Exceptions);

            return response;
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Exception" /> to <see cref="Response" />.
        /// </summary>
        public static implicit operator Response(Exception exception)
        {
            var response = new Response();

            response.exceptions.Add(exception);

            return response;
        }
        #endregion
    }

    /// <summary>
    ///     The response
    /// </summary>
    [Serializable]
    public sealed class Response<TValue> : Response
    {
        #region Public Properties
        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        public TValue Value { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        ///     Performs an implicit conversion from <see cref="Exception" /> to <see cref="Response{TValue}" />.
        /// </summary>
        public static implicit operator Response<TValue>(Exception exception)
        {
            var response = new Response<TValue>();

            response.exceptions.Add(exception);

            return response;
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Exception[]" /> to <see cref="Response{TValue}" />.
        /// </summary>
        public static implicit operator Response<TValue>(Exception[] errors)
        {
            var response = new Response<TValue>();

            response.exceptions.AddRange(errors);

            return response;
        }

        

        /// <summary>
        ///     Performs an implicit conversion from <see cref="TValue" /> to <see cref="Response{TValue}" />.
        /// </summary>
        public static implicit operator Response<TValue>(TValue value)
        {
            return new Response<TValue> { Value = value };
        }
        #endregion
    }
}