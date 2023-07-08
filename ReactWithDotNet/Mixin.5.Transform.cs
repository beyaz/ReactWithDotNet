using System.Globalization;

namespace ReactWithDotNet;

partial class Mixin
{
    /// <summary>
    ///     style.opacity = 0
    /// </summary>
    public static StyleModifier Opacity0 => Opacity(0);

    /// <summary>
    ///     style.opacity = 1
    /// </summary>
    public static StyleModifier Opacity1 => Opacity(1);

    /// <summary>
    ///     style.textTransform = 'capitalize'
    /// </summary>
    public static StyleModifier TextTransformCapitalize => TextTransform("capitalize");

    /// <summary>
    ///     style.textTransform = 'lowercase'
    /// </summary>
    public static StyleModifier TextTransformLowerCase => TextTransform("lowercase");

    /// <summary>
    ///     style.textTransform = 'uppercase'
    /// </summary>
    public static StyleModifier TextTransformUpperCase => TextTransform("uppercase");

    /// <summary>
    ///     style.visibility = 'collapse'
    /// </summary>
    public static StyleModifier VisibilityCollapse => Visibility("collapse");

    /// <summary>
    ///     style.visibility = 'hidden'
    /// </summary>
    public static StyleModifier VisibilityHidden => Visibility("hidden");

    /// <summary>
    ///     style.visibility = 'visible'
    /// </summary>
    public static StyleModifier VisibilityVisible => Visibility("visible");

    /// <summary>
    ///     style.opacity = <paramref name="opacity" />
    /// </summary>
    public static StyleModifier Opacity(string opacity) => new(style => style.opacity = opacity);

    /// <summary>
    ///     style.opacity = <paramref name="opacity" />
    /// </summary>
    public static StyleModifier Opacity(double opacity) => new(style => style.opacity = opacity.ToString(CultureInfo.InvariantCulture));

    /// <summary>
    ///     style.textTransform = <paramref name="textTransform" />
    /// </summary>
    public static StyleModifier TextTransform(string textTransform) => new(style => style.textTransform = textTransform);

    /// <summary>
    ///     style.transform = <paramref name="transform" />
    /// </summary>
    public static StyleModifier Transform(string transform) => new(style => style.transform = transform);

    /// <summary>
    ///     style.transition = <paramref name="transition" />
    /// </summary>
    public static StyleModifier Transition(string transition) => new(style => style.transition = transition);

    /// <summary>
    ///     style.transition = {<paramref name="propertyName" />} {<paramref name="durationAsMilliseconds" />}ms
    /// </summary>
    public static StyleModifier Transition(string propertyName, double durationAsMilliseconds)
        => Transition($"{propertyName} {durationAsMilliseconds}ms");

    /// <summary>
    ///     style.transition = {<paramref name="propertyName" />} {<paramref name="durationAsMilliseconds" />}ms {
    ///     <paramref name="delayAsMilliseconds" />}ms
    /// </summary>
    public static StyleModifier Transition(string propertyName, double durationAsMilliseconds, double delayAsMilliseconds)
        => Transition($"{propertyName} {durationAsMilliseconds}ms {delayAsMilliseconds}ms");

    /// <summary>
    ///     style.transition = {<paramref name="propertyName" />} {<paramref name="durationAsMilliseconds" />}ms {
    ///     <paramref name="easingFunction" />}
    /// </summary>
    public static StyleModifier Transition(string propertyName, double durationAsMilliseconds, string easingFunction)
        => Transition($"{propertyName} {durationAsMilliseconds}ms {easingFunction}");

    /// <summary>
    ///     style.transition = {<paramref name="propertyName" />} {<paramref name="durationAsMilliseconds" />}ms {
    ///     <paramref name="easingFunction" />} {<paramref name="delayAsMilliseconds" />}ms
    /// </summary>
    public static StyleModifier Transition(string propertyName, double durationAsMilliseconds, string easingFunction, double delayAsMilliseconds)
        => Transition($"{propertyName} {durationAsMilliseconds}ms {easingFunction} {delayAsMilliseconds}ms");

    /// <summary>
    ///     style.visibility = <paramref name="visibility" />
    /// </summary>
    public static StyleModifier Visibility(string visibility) => new(style => style.visibility = visibility);
    
    
    /// <summary>
    ///     style.transformOrigin = <paramref name="value" />
    /// </summary>
    public static StyleModifier TransformOrigin(string value) => new(style => style.transformOrigin = value);
    
}