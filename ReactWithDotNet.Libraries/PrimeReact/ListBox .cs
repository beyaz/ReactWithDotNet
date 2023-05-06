namespace ReactWithDotNet.Libraries.PrimeReact;

public abstract class ListBox : ElementBase
{
}

[ReactRealType(typeof(ListBox))]
public class ListBoxSingleSelection<TOption> : ListBox
{
    /// <summary>
    ///     When specified, displays a filter input at header.
    /// </summary>
    [ReactProp]
    public bool filter { get; set; }

    /// <summary>
    /// Placeholder text to show when filter input is empty.
    /// </summary>
    [ReactProp]
    public string filterPlaceholder { get; set; }
    

    [ReactProp]
    [ReactTemplate(nameof(GetItemSourceForCalculatingItemTemplates))]
    public Func<TOption, Element> itemTemplate { get; set; }

    /// <summary>
    ///     Inline style of inner list element.
    /// </summary>
    [ReactProp]
    public Style listStyle { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<ListBoxChangeParams<TOption>> onChange { get; set; }

    /// <summary>
    ///     Name of the label field of an option when an arbitrary objects instead of SelectItems are used as options.
    /// </summary>
    [ReactProp]
    public string optionLabel { get; set; }

    /// <summary>
    ///     An array of objects to display as the available options.
    /// </summary>
    [ReactProp]
    public IEnumerable<TOption> options { get; set; }

    /// <summary>
    ///     Name of the value field of an option when arbitrary objects are used as options instead of SelectItems.
    /// </summary>
    [ReactProp]
    public string optionValue { get; set; }

    /// <summary>
    ///     Selected value to display.
    /// </summary>
    [ReactProp]
    public TOption value { get; set; }

    IEnumerable<TOption> GetItemSourceForCalculatingItemTemplates()
    {
        return options;
    }
}

[Serializable]
public class ListBoxChangeParams<TOption>
{
    public TOption value { get; set; }
}