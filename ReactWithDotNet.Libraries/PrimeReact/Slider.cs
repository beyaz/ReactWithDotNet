using System;
using System.Linq.Expressions;

namespace ReactWithDotNet.PrimeReact;

/// <summary>
///     Slider is a component to provide input using dragging of a handle.
/// </summary>
public class Slider : ElementBase
{
    /// <summary>
    ///     Value of the component.
    /// </summary>
    [React]
    public int value { get; set; }

    /// <summary>
    ///     Value of the component.
    /// </summary>
    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<int>> valueBind { get; set; }

    /// <summary>
    ///     Mininum boundary value.
    ///     <para>Default: 0</para>
    /// </summary>
    [React]
    public int? min { get; set; }

    /// <summary>
    ///     Maximum boundary value.
    ///     <para>Default: 100</para>
    /// </summary>
    [React]
    public int? max { get; set; }

    /// <summary>
    ///     Step factor to increment/decrement the value.
    ///     <para>Default: 1</para>
    /// </summary>
    [React]
    public int? step { get; set; }

    /// <summary>
    ///     Orientation of the slider, valid values are horizontal and vertical.
    ///     <para>Default: horizontal</para>
    /// </summary>
    [React]
    public SliderOrientationType? orientation { get; set; }

    /// <summary>
    /// Callback to invoke on value change via slide.
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<SliderChangeParams> onChange { get; set; }

    /// <summary>
    /// Callback to invoke when slide ends.
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<SliderChangeParams> onSlideEnd { get; set; }
}

public enum SliderOrientationType
{
    horizontal,
    vertical
}

public sealed class SliderChangeParams
{
    public int value { get; set; }
}