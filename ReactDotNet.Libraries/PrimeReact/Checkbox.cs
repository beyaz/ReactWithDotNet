using System;
using System.Linq.Expressions;
using ReactDotNet.Html5;

namespace ReactDotNet.PrimeReact;

[Serializable]
public class Checkbox : ElementBase
{

    /// <summary>
    /// Value of the checkbox.
    /// </summary>
    [React]
    public string value { get; set; }

    /// <summary>
    ///    Specifies whether a checkbox should be checked or not.
    ///     <para>default: false</para>
    /// </summary>
    [React]
    public bool @checked { get; set; }


    

    /// <summary>
    /// When present, it specifies that the element value cannot be altered.
    /// </summary>
    [React]
    public bool disabled { get; set; }

    /// <summary>
    /// Callback to invoke on value change
    /// </summary>
    [React]
    public Action<CheckboxChangeParams> onChange { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}


[Serializable]
public class CheckboxChangeParams
{
    public bool @checked { get; set; }

    public string value { get; set; }
}