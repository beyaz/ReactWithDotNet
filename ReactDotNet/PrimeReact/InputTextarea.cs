using System;

namespace ReactDotNet.PrimeReact;

public class InputTextarea : ElementBase
{

    [React]
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
    public Action<string> onChange { get; set; }
}