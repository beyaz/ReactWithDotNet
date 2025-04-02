﻿// ReSharper disable all InconsistentNaming

using Microsoft.AspNetCore.SignalR;

namespace ReactWithDotNet;

partial class Mixin
{


    /// <summary>
    ///     50*percent returns like '50%'
    /// </summary>
    public static readonly CssUnit percent = new("%", "");

    /// <summary>
    ///     2*rem returns like '2rem'
    /// </summary>
    public static readonly CssUnit rem = new("rem", "");

    /// <summary>
    ///     7*vh returns like '7vh'
    /// </summary>
    public static readonly CssUnit vh = new("vh", "");

    /// <summary>
    ///     7*vw returns like '7vw'
    /// </summary>
    public static readonly CssUnit vw = new("vw", "");
    
    /// <summary>
    ///     2*px returns like '2px'
    /// </summary>
    public static readonly CssUnit px = new("px", "");

    /// <summary>
    ///     'linear-gradient(<paramref name="degree" /><b>+deg</b>, <paramref name="fromColor" />, <paramref name="toColor" />
    ///     )'
    /// </summary>
    public static string linear_gradient(int degree, string fromColor, string toColor)
        => $"linear-gradient({degree}deg, {fromColor}, {toColor})";

    /// <summary>
    ///     'linear-gradient(<b>to </b><paramref name="directionFrom" /> <paramref name="directionTo" />,
    ///     <paramref name="fromColor" />, <paramref name="toColor" />)'
    /// </summary>
    public static string linear_gradientTo(string directionFrom, string directionTo, string fromColor, string toColor)
        => $"linear-gradient(to {directionFrom} {directionTo}, {fromColor}, {toColor})";

    /// <summary>
    ///     'linear-gradient(<b>to </b> <paramref name="targetDirection" />,
    ///     <paramref name="fromColor" />, <paramref name="toColor" />)'
    ///     <code>
    ///      Example:
    ///         new div
    ///         {
    ///             BackgroundImage(linear_gradientTo("right","#8490ff","#a3eeff"))
    ///         }
    ///     </code>
    /// </summary>
    public static string linear_gradientTo(string targetDirection, string fromColor, string toColor)
        => $"linear-gradient(to {targetDirection}, {fromColor}, {toColor})";

    /// <summary>
    ///     Return new string as 'rgb(<paramref name="red" />, <paramref name="green" />, <paramref name="blue" />)'
    /// </summary>
    public static string rgb(double red, double green, double blue)
        => $"rgb({red.AsString()}, {green.AsString()}, {blue.AsString()})";

    /// <summary>
    ///     Return new string as 'rgb(<paramref name="red" />, <paramref name="green" />, <paramref name="blue" />,
    ///     <paramref name="alpha" />)'
    /// </summary>
    public static string rgba(double red, double green, double blue, double alpha)
        => $"rgba({red.AsString()},{green.AsString()},{blue.AsString()},{alpha.AsString()})";


    
    /// <summary>
    ///     Return new string as 'hsl(<paramref name="hue" />, <paramref name="saturation" />, <paramref name="lightness" />,
    ///     <paramref name="alpha" />)'
    /// </summary>
    /// <param name="hue">Defines a degree on the color circle (from 0 to 360) - 0 (or 360) is red, 120 is green, 240 is blue</param>
    /// <param name="saturation">Defines the saturation; 0% is a shade of gray and 100% is the full color (full saturation)</param>
    /// <param name="lightness">Defines the lightness; 0% is black, 50% is normal, and 100% is white</param>
    /// <param name="alpha">Defines the opacity as a number between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
    public static string hsl(int hue, int saturation, int lightness, double alpha)
        => $"hsl({hue},{saturation},{lightness},{alpha.AsString()})";
    
    /// <summary>
    ///     Return new string as 'hsl(<paramref name="hue" />, <paramref name="saturation" />, <paramref name="lightness" />,
    ///     <paramref name="alpha" />)'
    /// </summary>
    /// <param name="hue">Defines a degree on the color circle (from 0 to 360) - 0 (or 360) is red, 120 is green, 240 is blue</param>
    /// <param name="saturation">Defines the saturation; 0% is a shade of gray and 100% is the full color (full saturation)</param>
    /// <param name="lightness">Defines the lightness; 0% is black, 50% is normal, and 100% is white</param>
    /// <param name="alpha">Defines the opacity as a number between 0.0 (fully transparent) and 1.0 (fully opaque)</param>
    public static string hsla(int hue, int saturation, int lightness, double alpha)
        => $"hsla({hue},{saturation},{lightness},{alpha.AsString()})";

    /// <summary>
    ///     cubic-bezier(<paramref name="x1"/>, <paramref name="y1"/>, <paramref name="x2"/>, <paramref name="y2"/>)
    /// </summary>
    public static string cubic_bezier(double x1, double y1, double x2, double y2)
        => $"cubic-bezier({x1.AsString()}, {y1.AsString()}, {x2.AsString()}, {y2.AsString()})";

    public static StyleModifier Width(CssUnit width) => Width(width.ToString());
    
    public static StyleModifier Width(double width) => Width(width.AsPixel());
    
    public static StyleModifier Height(CssUnit value) => Height(value.ToString());


    public static readonly string inherit = "inherit";
    public static readonly string none = "none";
    public static readonly string inset = "inset";
    public static readonly string auto = "auto";
    public static readonly string solid = "solid";
    public static readonly string dotted = "dotted";
    public static readonly string dashed = "dashed";
    public static readonly string transparent = "transparent";
}

public sealed class CssUnit
{
    /// <summary>
    ///     2|em returns like '2em'
    /// </summary>
    public static readonly CssUnit em = new("em", "");
    
    readonly string _finalValue;
    readonly string _type;

    internal CssUnit(string type, string finalValue)
    {
        _type       = type;
        _finalValue = finalValue;
    }

    public static CssUnit operator *(double value, CssUnit cssUnit)
    {
        return new CssUnit(cssUnit._type, value.AsString() + cssUnit._type);
    }

    public static implicit operator CssUnit(double valueInPx)
    {
        return new CssUnit("px", valueInPx.AsPixel());
    }

    public static implicit operator CssUnit(int valueInPx)
    {
        return new CssUnit("px", valueInPx + "px");
    }

    public static implicit operator string(CssUnit cssUnit)
    {
        return cssUnit.ToString();
    }
    
    public override string ToString()
    {
        return _finalValue;
    }
}