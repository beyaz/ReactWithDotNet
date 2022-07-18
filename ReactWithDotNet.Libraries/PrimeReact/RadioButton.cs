using System;
using System.Linq.Expressions;

namespace ReactWithDotNet.PrimeReact;

[Serializable]
public class RadioButtonChangeParams
{
    public bool @checked { get; set; }

    public string value { get; set; }
}

[Serializable]
public class RadioButton : ElementBase
{

    /// <summary>
    /// Value of the radiobutton.
    /// </summary>
    [React]
    public string value { get; set; }

    /// <summary>
    ///     Specifies whether a radiobutton should be checked or not.
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
    /// Callback to invoke on radio button click.
    /// </summary>
    [React]
    public Action<RadioButtonChangeParams> onChange { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}


[Serializable]
public class Divider : ElementBase
{
    /// <summary>
    ///     Specifies the orientation, valid values are "horizontal" and "vertical".
    /// </summary>
    [React]
    public string layout { get; set; }


    /// <summary>
    ///     Alignment of the content, options are "left", "center", "right" for horizontal layout and "top", "center", "bottom" for vertical.
    /// </summary>
    [React]
    public string align { get; set; }


    /// <summary>
    /// Border style type, default is "solid" and other options are "dashed" and "dotted".
    /// </summary>
    [React]
    public string type { get; set; }
}