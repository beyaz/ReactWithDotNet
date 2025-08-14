namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Chip : ElementBase
{
    [ReactProp]
    public bool? clickable { get; set; }

    /// <summary>
    ///     'default'| 'primary'| 'secondary'| 'error'| 'info'| 'success'| 'warning'
    /// </summary>
    [ReactProp]
    public string color { get; set; }

    [ReactProp]
    public Element component { get; set; }

    [ReactProp]
    public Element deleteIcon { get; set; }

    [ReactProp]
    public bool? disabled { get; set; }

    [ReactProp]
    public Element icon { get; set; }

    [ReactProp]
    public string label { get; set; }

    [ReactProp]
    public Func<Task> onDelete { get; set; }

    [ReactProp]
    public string size { get; set; }

    [ReactProp]
    public bool? skipFocusWhenDisabled { get; set; }

    ///// <summary>
    /////     The system prop that allows defining system overrides as well as additional CSS styles.
    ///// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();

    /// <summary>
    ///     'filled' | 'outlined'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }
}