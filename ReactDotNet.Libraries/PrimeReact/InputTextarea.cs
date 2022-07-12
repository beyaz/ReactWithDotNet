using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReactDotNet.PrimeReact;

public class InputTextarea : ElementBase
{

    [React]
    [ReactDefaultValue(DefaultValue = "")]
    public string value { get; set; }

    [React]
    [ReactDefaultValue(DefaultValue = "")]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public int rows { get; set; }

    /// <summary>
    ///When present, height of textarea changes as being typed.
    /// <para>Default: false</para>
    /// </summary>
    [React]
    public bool? autoResize { get; set; }

    [React]
    public Action<SyntheticEvent> onChange { get; set; }


    [React]
    public Action<SyntheticEvent> onFocus { get; set; }
}