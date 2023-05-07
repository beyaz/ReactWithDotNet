namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

[Serializable]
public class InputMask : ElementBase
{
    /// <summary>
    ///     Mask pattern.
    /// </summary>
    [ReactProp]
    public string mask { get; set; }

    [ReactProp]
    public string value { get; set; }


    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }




    /// <summary>
    ///     Advisory information to display on input.
    /// </summary>
    [ReactProp]
    public string placeholder { get; set; }

    /// <summary>
    ///     Maximum number of character allows in the input field.
    /// </summary>
    [ReactProp]
    public int? maxlength { get; set; }

    [ReactProp]
    public bool? autoFocus { get; set; }
}