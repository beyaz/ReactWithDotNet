using System.Collections;

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public abstract class Dropdown : ElementBase
{
    [ReactProp]
    public string optionLabel { get; set; }
    
    [ReactProp]
    public string optionValue { get; set; }

    [ReactProp]
    public string placeholder { get; set; }
    
    [ReactProp]
    public bool? autoFocus { get; set; }
    
    /// <summary>
    /// When filtering is enabled, filterBy decides which field or fields (comma separated) to search against.
    /// <para>Default: label</para>
    /// </summary>
    [ReactProp]
    public string filterBy { get; set; }

    /// <summary>
    /// When enabled, a clear icon is displayed to clear the value.
    /// </summary>
    [ReactProp]
    public bool showClear { get; set; }
    
    /// <summary>
    /// When specified, displays an input field to filter the items on keyup.
    /// </summary>
    [ReactProp]
    public bool filter { get; set; }
}

[ReactRealType(typeof(Dropdown))]
public class Dropdown<TOption> : Dropdown
{
    [ReactProp]
    public  Action<DropdownChangeParams<TOption>> onChange { get; set; }

    [ReactProp]
    public  IEnumerable<TOption> options { get; set; }
    
    [ReactProp]
    public  TOption value { get; set; }

    [ReactProp]
    [ReactTemplate(nameof(GetItemSourceForCalculatingItemTemplates))]
    public  Func<TOption,Element> itemTemplate { get; set; }

    //[React]
    //[ReactTemplate(nameof(GetItemSourceForCalculatingValueTemplate))]
    //[ReactTemplateForNull]
    //public  Func<TOption, Element> valueTemplate { get; set; }

    IEnumerable GetItemSourceForCalculatingItemTemplates()
    {
        return options;
    }
}

[Serializable]
public sealed class DropdownChangeParams<TOption>
{
    public TOption value { get; set; }
}

