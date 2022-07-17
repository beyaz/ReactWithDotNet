using System;
using System.Collections;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace ReactDotNet.PrimeReact;

public class Dropdown : ElementBase
{
    [React]
    public Action<DropdownChangeParams> onChange { get; set; }

    [React]
    public string optionLabel { get; set; }
    
    [React]
    public string optionValue { get; set; }

    [React]
    public string placeholder { get; set; }

    [React]
    public IEnumerable options { get; set; }

    [React]
    public object value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public bool? autoFocus { get; set; }

    [React]
    public ItemTemplateInfo itemTemplate { get; set; }

    [React]
    public ItemTemplateInfo valueTemplate { get; set; }

    /// <summary>
    /// When filtering is enabled, filterBy decides which field or fields (comma separated) to search against.
    /// <para>Default: label</para>
    /// </summary>
    [React]
    public string filterBy { get; set; }

    /// <summary>
    /// When enabled, a clear icon is displayed to clear the value.
    /// </summary>
    [React]
    public bool showClear { get; set; }
    
    /// <summary>
    /// When specified, displays an input field to filter the items on keyup.
    /// </summary>
    [React]
    public bool filter { get; set; }
}

public class DropdownChangeParams
{
    public object value { get; set; }

    public T GetValue<T>()
    {
        if (value is JValue jValue)
        {
            return jValue.ToObject<T>();
        }

        if (value is JObject jObject)
        {
            return jObject.ToObject<T>();
        }

        if (value is string && typeof(T) != typeof(string))
        {
            return default;
        }

        return (T)value;
    }
}

