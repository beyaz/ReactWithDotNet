using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ThirdPartyReactComponent : Element
{
    internal Style _style;

    protected ThirdPartyReactComponent()
    {
    }

    protected ThirdPartyReactComponent(params StyleModifier[] modifiers)
    {
        style.Apply(modifiers);
    }

    [React]
    public string className { get; set; }

    public void AddClass(string cssClassName)
    {
        if (string.IsNullOrWhiteSpace(className))
        {
            className = cssClassName;
            return;
        }

        className += " " + cssClassName;
    }

    /// <summary>
    ///     Gets the style.
    /// </summary>
    [JsonIgnore]
    public Style style
    {
        get
        {
            _style ??= new Style();

            return _style;
        }
    }

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style
    {
        set => style.Import(value);
    }

    [JsonPropertyName("$type")]
    public virtual string Type
    {
        get
        {
            var type = GetType();

            var reactRealType = type.GetCustomAttributes<ReactRealTypeAttribute>().FirstOrDefault()?.Type;

            return (reactRealType ?? type).FullName;
        }
    }

    public static Element operator |(ThirdPartyReactComponent element, StyleModifier modifier)
    {
        element.ProcessModifier(modifier);

        return element;
    }

    public static Element operator +(ThirdPartyReactComponent element, StyleModifier modifier)
    {
        element.ProcessModifier(modifier);

        return element;
    }

    protected internal sealed override void ProcessModifier(IModifier modifier)
    {
        if (modifier == null)
        {
            return;
        }

        if (modifier is StyleModifier styleModifier)
        {
            styleModifier.Modify(style);
            return;
        }

        throw new InvalidOperationException("Expected only StyleModifier but found HtmlModifier");
    }
}