using System;
using System.Linq.Expressions;

namespace ReactWithDotNet.PrimeReact;

[Serializable]
public class InputMask : ElementBase
{
    /// <summary>
    ///     Mask pattern.
    /// </summary>
    [React]
    public string mask { get; set; }

    [React]
    public string value { get; set; }


    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }




    /// <summary>
    ///     Advisory information to display on input.
    /// </summary>
    [React]
    public string placeholder { get; set; }

    /// <summary>
    ///     Maximum number of character allows in the input field.
    /// </summary>
    [React]
    public int? maxlength { get; set; }

    [React]
    public bool? autoFocus { get; set; }
}