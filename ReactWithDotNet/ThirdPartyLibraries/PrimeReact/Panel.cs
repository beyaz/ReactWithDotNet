namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

[Serializable]
public class Panel : ElementBase
{
    /// <summary>
    ///     	Defines the initial state of panel content, supports one or two-way binding as well.
    ///     <para>default: false</para>
    /// </summary>
    [ReactProp]
    public bool collapsed { get; set; }
    
    /// <summary>
    ///     Defines if content of panel can be expanded and collapsed.
    ///     <para>default: false</para>
    /// </summary>
    [ReactProp]
    public bool toggleable { get; set; }

    /// <summary>
    /// Custom header template of the panel.
    /// </summary>
    [ReactProp]
    public string header { get; set; }

    /// <summary>
    ///    Header template of the panel to customize more.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient("ReactWithDotNet.PrimeReact.Panel::GetHeaderTemplate")]
    public string headerTemplate { get; set; }

    /// <summary>
    ///     Callback to invoke when a tab gets expanded.
    /// </summary>
    [ReactProp]
    public Action<PanelToggleParams> onToggle { get; set; }
}

public sealed class PanelToggleParams
{
    public bool value { get; set; }
}