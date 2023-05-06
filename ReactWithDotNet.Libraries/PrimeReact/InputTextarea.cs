namespace ReactWithDotNet.Libraries.PrimeReact;

public class InputTextarea : ElementBase
{

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [ReactProp]
    public int rows { get; set; }

    /// <summary>
    ///When present, height of textarea changes as being typed.
    /// <para>Default: false</para>
    /// </summary>
    [ReactProp]
    public bool? autoResize { get; set; }

    [ReactProp]
    public Action<SyntheticEvent> onChange { get; set; }


    [ReactProp]
    public Action<SyntheticEvent> onFocus { get; set; }
}