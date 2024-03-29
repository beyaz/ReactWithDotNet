﻿namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

/// <summary>
///     Slider is a component to provide input using dragging of a handle.
/// </summary>
public class Slider : ElementBase
{
    /// <summary>
    ///     Value of the component.
    /// </summary>
    [ReactProp]
    public int value { get; set; }

    /// <summary>
    ///     Value of the component.
    /// </summary>
    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<int>> valueBind { get; set; }

    /// <summary>
    ///     Mininum boundary value.
    ///     <para>Default: 0</para>
    /// </summary>
    [ReactProp]
    public int? min { get; set; }

    /// <summary>
    ///     Maximum boundary value.
    ///     <para>Default: 100</para>
    /// </summary>
    [ReactProp]
    public int? max { get; set; }

    /// <summary>
    ///     Step factor to increment/decrement the value.
    ///     <para>Default: 1</para>
    /// </summary>
    [ReactProp]
    public int? step { get; set; }

    /// <summary>
    ///     Orientation of the slider, valid values are horizontal and vertical.
    ///     <para>Default: horizontal</para>
    /// </summary>
    [ReactProp]
    public SliderOrientationType? orientation { get; set; }

    /// <summary>
    /// Callback to invoke on value change via slide.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<SliderChangeParams> onChange { get; set; }

    /// <summary>
    /// Callback to invoke when slide ends.
    /// </summary>
    [ReactProp]
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