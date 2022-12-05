namespace ReactWithDotNet.PrimeReact;

public abstract class ListBox : ElementBase
{
}

[ReactRealType(typeof(ListBox))]
public class ListBoxSingleSelection<TOption> : ListBox
{
    /// <summary>
    ///     When specified, displays a filter input at header.
    /// </summary>
    [React]
    public bool filter { get; set; }

    [React]
    [ReactTemplate(nameof(GetItemSourceForCalculatingItemTemplates))]
    public Func<TOption, Element> itemTemplate { get; set; }

    /// <summary>
    ///     Inline style of inner list element.
    /// </summary>
    [React]
    public Style listStyle { get; } = new();

    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<ListBoxChangeParams<TOption>> onChange { get; set; }

    /// <summary>
    ///     Name of the label field of an option when an arbitrary objects instead of SelectItems are used as options.
    /// </summary>
    [React]
    public string optionLabel { get; set; }

    /// <summary>
    ///     An array of objects to display as the available options.
    /// </summary>
    [React]
    public IEnumerable<TOption> options { get; set; }

    /// <summary>
    ///     Name of the value field of an option when arbitrary objects are used as options instead of SelectItems.
    /// </summary>
    [React]
    public string optionValue { get; set; }

    /// <summary>
    ///     Selected value to display.
    /// </summary>
    [React]
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