using System;
using System.Collections.Generic;

namespace ReactDotNet.PrimeReact;

public class InputTextarea : ElementBase
{

    [React]
    [ReactDefaultValue(DefaultValue = "")]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public BindibleProperty<string> value { get; set; }



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