using System;
using System.Linq.Expressions;

namespace ReactDotNet.PrimeReact;

public class InputText : ElementBase
{

    [React]
    [ReactDefaultValue(DefaultValue = "" )]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value",  eventName = "onChange")]
    [ReactDefaultValue(DefaultValue = "")]
    public Expression<Func<string>> valueBind { get; set; }
        
    [React]
    public string placeholder { get; set; }

    /// <summary>
    /// Format definition of the keys to block.
    /// <para>Default: null</para>
    /// <para>Type: string/regex</para>
    /// </summary>
    [React]
    public string keyfilter { get; set; }

    [React]
    public bool? autoFocus { get; set; }
    
}