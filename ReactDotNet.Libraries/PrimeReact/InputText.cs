
using System;

namespace ReactDotNet.PrimeReact;

public class InputText : ElementBase
{

    [React]
    [ReactDefaultValue(DefaultValue = "" )]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public BindibleProperty<string> value { get; set; }

    [React]
    public Func<string> valueBind { get; set; }


    [React]
    public string placeholder { get; set; }

    /// <summary>
    /// Format definition of the keys to block.
    /// <para>Default: null</para>
    /// <para>Type: string/regex</para>
    /// </summary>
    [React]
    public ReactTransformValueInClient_Regex keyfilter { get; set; }

    [React]
    public bool? autoFocus { get; set; }
    
}