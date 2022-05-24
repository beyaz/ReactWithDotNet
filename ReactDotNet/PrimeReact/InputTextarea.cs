using System;
using System.Linq.Expressions;

namespace ReactDotNet.PrimeReact;

public class InputTextarea : ElementBase
{

    [React]
    public string value { get; set; }

    [React]
    public int rows { get; set; }

    /// <summary>
    ///When present, height of textarea changes as being typed.
    /// <para>Default: false</para>
    /// </summary>
    [React]
    public bool? autoResize { get; set; }

    //[React]
    //[ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    ////[ReactDefaultValue(DefaultValue = "")]
    //public Expression<Func<string>> valueBind { get; set; }

    [React]
    public Action<string> onChange { get; set; }
}