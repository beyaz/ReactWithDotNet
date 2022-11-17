using System;
using System.Linq.Expressions;

namespace ReactWithDotNet.PrimeReact;

[Serializable]
public class InputSwitch : ElementBase
{
    /// <summary>
    ///     Specifies whether a inputswitch should be checked or not.
    ///     <para>default: false</para>
    /// </summary>
    [React]
    public bool @checked { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.value", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }

    /// <summary>
    ///     When present, it specifies that the component should be disabled.
    /// </summary>
    [React]
    public bool disabled { get; set; }

    /// <summary>
    ///     Callback to invoke on value change
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<InputSwitchChangeTargetOptions> onChange { get; set; }
}

[Serializable]
public sealed class InputSwitchChangeTargetOptions
{
    public bool value { get; set; }
}