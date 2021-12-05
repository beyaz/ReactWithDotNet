using System;
using System.Collections.Generic;
using System.Linq;

namespace FP
{
    /// <summary>
    ///     The error
    /// </summary>
    [Serializable]
    public sealed class Error
    {
        #region Public Properties
        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        public string Message { get; set; }
        #endregion

        #region Public Methods
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
        #endregion
    }

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
       
        protected readonly List<Error> exceptions = new List<Error>();
        #endregion

        #region Public Properties
       
        public IReadOnlyList<Error> Exceptions => exceptions;

        /// <summary>
        ///     Gets the fail message.
        /// </summary>
        public string FailMessage => string.Join(Environment.NewLine, exceptions.Select(e => e.Message));

        /// <summary>
        ///     Gets a value indicating whether this instance is fail.
        /// </summary>
        public bool IsFail => exceptions.Count > 0;

        
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
                exceptions = { errorMessage }
            };
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

      

        public static Response  operator +(Response responseX, Response responseY)
        {
            var response = new Response();

            response.exceptions.AddRange(responseX.Exceptions);
            response.exceptions.AddRange(responseY.Exceptions);

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
        ///     Performs an implicit conversion from <see cref="Error" /> to <see cref="Response{TValue}" />.
        /// </summary>
        public static implicit operator Response<TValue>(Error error)
        {
            var response = new Response<TValue>();

            response.exceptions.Add(error);

            return response;
        }

        /// <summary>
        ///     Performs an implicit conversion from <see cref="Error" /> to <see cref="Response{TValue}" />.
        /// </summary>
        public static implicit operator Response<TValue>(Error[] errors)
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