﻿using System.Collections;

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class AutoComplete : ElementBase
{
    //    [React]
    //    public object value { get; set; }

    //    [React]
    //    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    //    public Expression<Func<string>> valueBind { get; set; }

    //    /// <summary>
    //    ///     An array of suggestions to display.
    //    /// </summary>
    //    [React]
    //    public IEnumerable suggestions { get; set; }

    //    /// <summary>
    //    ///     Callback to invoke when autocomplete value changes.
    //    /// </summary>
    //    [React]
    //    public Action<AutoCompleteChangeParams> onChange { get; set; }

    //    /// <summary>
    //    ///     Callback to invoke to search for suggestions.
    //    /// </summary>
    //    [React]
    //    public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

    //    /// <summary>
    //    ///     Field of a suggested object to resolve and display.
    //    /// </summary>
    //    [React]
    //    public string field { get; set; }

    //    /// <summary>
    //    ///     When present, autocomplete clears the manual input if it does not match of the suggestions to force only accepting
    //    ///     values from the suggestions.
    //    /// </summary>
    //    [React]
    //    public bool forceSelection { get; set; }

    //    /// <summary>
    //    /// Displays a button next to the input field when enabled.
    //    /// </summary>
    //    [React]
    //    public bool dropdown { get; set; }

    //    [React]
    //    [ReactTemplate]
    //    public Func<string, Element> itemTemplate { get; set; }

    //    [React]
    //    [ReactTemplate]
    //    public Func<string, Element> selectedItemTemplate { get; set; }


    //    internal List<KeyValuePair<object, object>> GetItemTemplates(Func<object, IReadOnlyDictionary<string, object>> toMap)
    //    {
    //        var map = new List<KeyValuePair<object, object>>();

    //        foreach (var suggestion in suggestions)
    //        {
    //            map.Add(new KeyValuePair<object, object>(suggestion, toMap(suggestion)));
    //        }

    //        return map;
    //    }


    protected  override Element GetSuspenseFallbackElement()
    {
        return base.GetSuspenseFallbackElement() + MinHeight(30) + MinWidth(150);
    }

}



[ReactRealType(typeof(AutoComplete))]
public class AutoComplete<TSuggestion> : ElementBase
{
    [ReactProp]
    public bool? autoFocus { get; set; }


    /// <summary>
    /// Delay between keystrokes to wait before sending a query.
    /// </summary>
    [ReactProp]
    public double? delay { get; set; }

    [ReactProp]
    public TSuggestion value { get; set; }
    
    /// <summary>
    ///     An array of suggestions to display.
    /// </summary>
    [ReactProp]
    public IEnumerable<TSuggestion> suggestions { get; set; }

    /// <summary>
    ///     Callback to invoke when autocomplete value changes.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<AutoCompleteChangeParams<TSuggestion>> onChange { get; set; }

    /// <summary>
    ///     Callback to invoke to search for suggestions.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(GrabWithoutOriginalEvent))]
    public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

    /// <summary>
    ///     Field of a suggested object to resolve and display.
    /// </summary>
    [ReactProp]
    public string field { get; set; }

    /// <summary>
    ///     When present, autocomplete clears the manual input if it does not match of the suggestions to force only accepting
    ///     values from the suggestions.
    /// </summary>
    [ReactProp]
    public bool forceSelection { get; set; }

    /// <summary>
    /// Displays a button next to the input field when enabled.
    /// </summary>
    [ReactProp]
    public bool dropdown { get; set; }

    [ReactProp]
    [ReactTemplate(nameof(GetItemTemplates))]
    public Func<TSuggestion, Element> itemTemplate { get; set; }

    [ReactProp]
    [ReactTemplate(nameof(GetItemTemplates))]
    public Func<TSuggestion, Element> selectedItemTemplate { get; set; }

    internal IEnumerable GetItemTemplates()
    {
        return suggestions;
    }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public Style inputStyle { get; } = new();

    protected  override Element GetSuspenseFallbackElement()
    {
        return base.GetSuspenseFallbackElement() + MinHeight(30) + MinWidth(150);
    }
}


//public class AutoCompleteChangeParams
//{
//    //public JObject value { get; set; }
//    public object value { get; set; }

//    public T GetValue<T>()
//    {
//        if (value is JValue jValue)
//        {
//            return jValue.ToObject<T>();
//        }

//        if (value is JObject jObject)
//        {
//            return jObject.ToObject<T>();
//        }

//        if (value is string && typeof(T) != typeof(string))
//        {
//            return default;
//        }

//        return (T)value;
//    }
//}


public sealed class AutoCompleteChangeParams<TSuggestion>
{
    public TSuggestion value { get; set; }
}

public class AutoCompleteCompleteMethodParams
{
    public string query { get; set; }
}



//[ReactRealType(typeof(AutoComplete))]
//public class AutoComplete<TSuggestion,TSelectedValueType> : ElementBase
//{
//    [React]
//    public bool? autoFocus { get; set; }


//    /// <summary>
//    /// Delay between keystrokes to wait before sending a query.
//    /// </summary>
//    [React]
//    public double? delay { get; set; }

//    [React]
//    public TSelectedValueType value { get; set; }

//    /// <summary>
//    ///     An array of suggestions to display.
//    /// </summary>
//    [React]
//    public IEnumerable<TSuggestion> suggestions { get; set; }

//    /// <summary>
//    ///     Callback to invoke when autocomplete value changes.
//    /// </summary>
//    [React]
//    public Action<AutoCompleteChangeParams<TSelectedValueType>> onChange { get; set; }

//    /// <summary>
//    ///     Callback to invoke to search for suggestions.
//    /// </summary>
//    [React]
//    public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

//    /// <summary>
//    ///     Field of a suggested object to resolve and display.
//    /// </summary>
//    [React]
//    public string field { get; set; }

//    /// <summary>
//    ///     When present, autocomplete clears the manual input if it does not match of the suggestions to force only accepting
//    ///     values from the suggestions.
//    /// </summary>
//    [React]
//    public bool forceSelection { get; set; }

//    /// <summary>
//    /// Displays a button next to the input field when enabled.
//    /// </summary>
//    [React]
//    public bool dropdown { get; set; }

//    [React]
//    [ReactTemplate]
//    public Func<TSuggestion, Element> itemTemplate { get; set; }

//    [React]
//    [ReactTemplate]
//    public Func<TSuggestion, Element> selectedItemTemplate { get; set; }


//    internal List<KeyValuePair<object, object>> GetItemTemplates(Func<object, IReadOnlyDictionary<string, object>> toMap)
//    {
//        var map = new List<KeyValuePair<object, object>>();

//        foreach (var suggestion in suggestions)
//        {
//            map.Add(new KeyValuePair<object, object>(suggestion, toMap(suggestion)));
//        }

//        return map;
//    }

//}

