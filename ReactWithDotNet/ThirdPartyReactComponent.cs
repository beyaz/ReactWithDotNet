﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ThirdPartyReactComponent : Element
{
    Style _style;

    protected ThirdPartyReactComponent()
    {
    }

    protected ThirdPartyReactComponent(params StyleModifier[] modifiers)
    {
        style.Apply(modifiers);
    }

    /// <summary>
    ///     Gets or sets the name of the class.
    /// </summary>
    [ReactProp]
    public string className { get; set; }

    //[ReactProp]
    //public string className { get; set; }

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
    ///     This is designed for Suspense part of react. When page first rendered as pure html.
    ///     <br />
    ///     When react component fully loaded then this element will be replace by original component.
    ///     <br />
    ///     Default value is simple empty div element
    /// </summary>
    [JsonIgnore]
    public Element SuspenseFallback { get; set; }

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

    internal bool HasStyle => _style != null;

    protected internal ReactContext Context { get; set; }

    public static Element operator +(ThirdPartyReactComponent element, Modifier modifier)
    {
        ModifyHelper.ProcessModifier(element, modifier);

        return element;
    }

    public static Element operator +(ThirdPartyReactComponent thirdPartyReactComponent, StyleModifier modifier)
    {
        modifier.ModifyStyle(thirdPartyReactComponent.style);

        return thirdPartyReactComponent;
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public void Add(Style style)
    {
        this.style.Import(style);
    }

    public void AddClass(string cssClassName)
    {
        if (string.IsNullOrWhiteSpace(className))
        {
            className = cssClassName;
            return;
        }

        className += " " + cssClassName;
    }

    internal Element InvokeSuspenseFallback()
    {
        return GetSuspenseFallbackElement();
    }

    protected virtual Element GetSuspenseFallbackElement()
    {
        if (SuspenseFallback == null)
        {
            return new div { Style = style };
        }

        return SuspenseFallback + style;
    }
}