// ReSharper disable all InconsistentNaming

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
    /// </summary>
    public static string linear_gradientTo(string targetDirection, string fromColor, string toColor)
        => $"linear-gradient(to {targetDirection}, {fromColor}, {toColor})";

    /// <summary>
    ///     Return new string as 'rgb(<paramref name="red" />, <paramref name="green" />, <paramref name="blue" />)'
    /// </summary>
    public static string rgb(double red, double green, double blue)
        => $"rgb({red}, {green}, {blue})";

    /// <summary>
    ///     Return new string as 'rgb(<paramref name="red" />, <paramref name="green" />, <paramref name="blue" />,
    ///     <paramref name="alpha" />)'
    /// </summary>
    public static string rgba(double red, double green, double blue, double alpha)
        => $"rgba({red},{green},{blue},{alpha})";

    /// <summary>
    ///     cubic-bezier(<paramref name="x1"/>, <paramref name="y1"/>, <paramref name="x2"/>, <paramref name="y2"/>)
    /// </summary>
    public static string cubic_bezier(double x1, double y1, double x2, double y2)
        => $"cubic-bezier({x1}, {y1}, {x2}, {y2})";

    public static StyleModifier Width(CssUnit width) => Width(width.ToString());
    
    public static StyleModifier Width(double width) => Width(width.AsPixel());
    
    public static StyleModifier Height(CssUnit value) => Height(value.ToString());


    public const string inherit = "inherit";
    public const string none = "none";
    public const string auto = "auto";
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
        return new CssUnit(cssUnit._type, value + cssUnit._type);
    }

    public static implicit operator CssUnit(double valueInPx)
    {
        return new CssUnit("px", valueInPx + "px");
    }

    public static implicit operator CssUnit(int valueInPx)
    {
        return new CssUnit("px", valueInPx + "px");
    }

    public override string ToString()
    {
        return _finalValue;
    }
}