using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;

namespace ReactDotNet.PrimeReact;

public class AutoComplete : ElementBase
{
    [React]
    public object value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }
    
    /// <summary>
    ///     An array of suggestions to display.
    /// </summary>
    [React]
    public IEnumerable suggestions { get; set; }

    /// <summary>
    ///     Callback to invoke when autocomplete value changes.
    /// </summary>
    [React]
    public Action<AutoCompleteChangeParams> onChange { get; set; }

    /// <summary>
    ///     Callback to invoke to search for suggestions.
    /// </summary>
    [React]
    public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

    /// <summary>
    ///     Field of a suggested object to resolve and display.
    /// </summary>
    [React]
    public string field { get; set; }

    /// <summary>
    ///     When present, autocomplete clears the manual input if it does not match of the suggestions to force only accepting
    ///     values from the suggestions.
    /// </summary>
    [React]
    public bool forceSelection { get; set; }

    /// <summary>
    /// Displays a button next to the input field when enabled.
    /// </summary>
    [React]
    public bool dropdown { get; set; }

    [React]
    public ItemTemplateInfo itemTemplate { get; set; }
    
}

public class AutoCompleteChangeParams
{
    //public JObject value { get; set; }
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

public class AutoCompleteCompleteMethodParams
{
    public string query { get; set; }
}