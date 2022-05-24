using System;
using System.Linq.Expressions;

namespace ReactDotNet.PrimeReact;

public class InputText : ElementBase
{

    [React]
    [ReactDefaultValue(DefaultValue = "" )]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value",  eventName = "onChange")]
    [ReactDefaultValue(DefaultValue = "")]
    public Expression<Func<string>> valueBind { get; set; }


    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    [ReactDefaultValue(DefaultValue = "")]
    public BindingSourcePath<string> valueBindNew { get; set; }

    [React]
    public string placeholder { get; set; }

    /// <summary>
    /// Format definition of the keys to block.
    /// <para>Default: null</para>
    /// <para>Type: string/regex</para>
    /// </summary>
    [React]
    public ReactTransformValueInClient_Regex keyfilter { get; set; }

    [React]
    public bool? autoFocus { get; set; }
    
}

public sealed class BindingSourcePath<T>
{
    public Expression<Func<T>> Expression { get; set; }
    public string PathInState { get; set; }

    public static implicit operator BindingSourcePath<T>(Expression<Func<T>> expression)
    {
        return new BindingSourcePath<T> { Expression = expression};
    }

    public static implicit operator BindingSourcePath<T>(string pathInState)
    {
        return new BindingSourcePath<T> { PathInState = pathInState };
    }
}