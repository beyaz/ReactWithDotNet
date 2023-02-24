using System;
using System.Runtime.Intrinsics.X86;

namespace ReactWithDotNet.Libraries.mui.material;

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

public sealed class TextField : ElementBase
{
    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public Action valueBindDebounceHandler { get; set; }


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }
}

public sealed class Input : ElementBase
{
}
public sealed class InputBase : ElementBase
{
   
    [React]
    public string placeholder { get; set; }
    
}
public sealed class Paper : ElementBase
{
    /// <summary>
    /// The component used for the root node. Either a string to use a HTML element or a component.
    /// </summary>
    [React]
    public string component { get; set; }

    /// <summary>
    /// 	The variant to use.
    /// <br/>
    /// 'elevation'
    /// <br/>
    /// 'outlined'
    /// </summary>
    [React]
    public string variant { get; set; }
}

public sealed class Card : ElementBase
{
    /// <summary>
    /// The component used for the root node. Either a string to use a HTML element or a component.
    /// </summary>
    [React]
    public string component { get; set; }
    
    /// <summary>
    /// 	If true, the card will use raised styling.
    /// </summary>
    [React]
    public bool? raised { get; set; }

    /// <summary>
    /// 	The variant to use.
    /// <br/>
    /// 'elevation'
    /// <br/>
    /// 'outlined'
    /// </summary>
    [React]
    public string variant { get; set; }
}
public sealed class CardMedia : ElementBase
{
    [React]
    public string image { get; set; }

    /// <summary>
    /// 	If true, the card will use raised styling.
    /// </summary>
    [React]
    public string title { get; set; }
}


public sealed class Divider : ElementBase
{
    [React]
    public string orientation { get; set; }
}
public sealed class IconButton : ElementBase
{
    [React]
    public string type { get; set; }
    
    [React]
    public string color { get; set; }
}

