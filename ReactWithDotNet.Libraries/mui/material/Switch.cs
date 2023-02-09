﻿namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Switch : ElementBase
{
    [React]
    public bool? defaultChecked { get; set; }

    [React]
    public bool? disabled { get; set; }

    /// <summary>
    /// 	If true, the component is checked.
    /// </summary>
    [React]
    public bool? @checked { get; set; }


    [React]
    [ReactGrabEventArgumentsByUsingFunction(Core__CalculateSyntheticChangeEventArguments)]
    public Action<ChangeEvent> onChange { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<bool>> checkedBind { get; set; }

    /// <summary>
    /// The value of the component.The DOM API casts this to a string.
    /// <br/>
    /// The browser uses "on" as the default value.
    /// </summary>
    [React]
    public string value { get; set; }

}