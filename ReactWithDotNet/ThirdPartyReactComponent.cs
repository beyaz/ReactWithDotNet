using System.Diagnostics.CodeAnalysis;
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

    public static Element operator +(ThirdPartyReactComponent thirdPartyReactComponent, StyleModifier modifier)
    {
        modifier.modifyStyle(thirdPartyReactComponent.style);

        return thirdPartyReactComponent;
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public void Add(Style style)
    {
        this.style.Import(style);
    }

    protected virtual Element SuspenseFallback()
    {
        return new div { aria = { { "component", GetType().FullName } }, Style = style};
    }
}