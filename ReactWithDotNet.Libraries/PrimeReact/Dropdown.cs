using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactWithDotNet.PrimeReact;

public abstract class Dropdown : ElementBase
{
    [React]
    public string optionLabel { get; set; }
    
    [React]
    public string optionValue { get; set; }

    [React]
    public string placeholder { get; set; }
    
    [React]
    public bool? autoFocus { get; set; }
    
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

[ReactRealType(typeof(Dropdown))]
public class Dropdown<TOption> : Dropdown
{
    [React]
    public  Action<DropdownChangeParams<TOption>> onChange { get; set; }

    [React]
    public  IEnumerable<TOption> options { get; set; }
    
    [React]
    public  TOption value { get; set; }

    [React]
    [ReactTemplate]
    public  Func<TOption,Element> itemTemplate { get; set; }

    [React]
    [ReactTemplate]
    [ReactTemplateForNull]
    public  Func<TOption, Element> valueTemplate { get; set; }

    internal List<KeyValuePair<object, object>> GetItemTemplates(Func<object, IReadOnlyDictionary<string, object>> toMap)
    {
        var map = new List<KeyValuePair<object, object>>();

        foreach (var option in options)
        {
            map.Add(new KeyValuePair<object, object>(option, toMap(option)));
        }

        return map;
    }
}

[Serializable]
public sealed class DropdownChangeParams<TOption>
{
    public TOption value { get; set; }
}

