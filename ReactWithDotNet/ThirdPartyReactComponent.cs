using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;


namespace ReactWithDotNet;

public abstract class ThirdPartyReactComponent : Element
{
    internal Style _style;
    protected internal  ReactContext Context { get; set; }

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
    [System.Text.Json.Serialization.JsonIgnore]
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

    /// <summary>
    /// This is designed for Suspense part of react. When page first rendered as pure html.
    /// <br/>
    /// When react component fully loaded then this element will be replace by original component.
    /// <br/>
    /// Default value is sipmle empty div element
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Element SuspenseFallback { get; set; }
    
    protected virtual Element GetSuspenseFallbackElement()
    {
        if (SuspenseFallback == null)
        {
            return new div { Style = style };
        }

        return SuspenseFallback + style;
    }

    internal Element InvokeSuspenseFallback() => GetSuspenseFallbackElement();
}