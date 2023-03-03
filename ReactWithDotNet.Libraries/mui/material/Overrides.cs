namespace ReactWithDotNet.Libraries.mui.material;

public sealed partial class Switch : SwitchBase
{
    [React]
    [ReactGrabEventArgumentsByUsingFunction(Core__CalculateSyntheticChangeEventArguments)]
    public Action<ChangeEvent> onChange { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<bool>> checkedBind { get; set; }
}

public sealed partial class TextField : ElementBase
{
   

  

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

public  partial class Paper : ElementBase
{
    
}

public sealed partial class Card : Paper
{

}








public sealed class IconButton : ElementBase
{
    [React]
    public string type { get; set; }
    
    [React]
    public string color { get; set; }
}

