using System;

namespace ReactWithDotNet.PrimeReact;

[Serializable]
public class Panel : ElementBase
{
    /// <summary>
    ///     	Defines the initial state of panel content, supports one or two-way binding as well.
    ///     <para>default: false</para>
    /// </summary>
    [React]
    public bool collapsed { get; set; }
    
    /// <summary>
    ///     Defines if content of panel can be expanded and collapsed.
    ///     <para>default: false</para>
    /// </summary>
    [React]
    public bool toggleable { get; set; }

    /// <summary>
    /// Custom header template of the panel.
    /// </summary>
    [React]
    public string header { get; set; }

    /// <summary>
    ///    Header template of the panel to customize more.
    /// </summary>
    [React]
    public Element headerTemplate { get; set; }

    /// <summary>
    ///     Callback to invoke when a tab gets expanded.
    /// </summary>
    [React]
    public Action<PanelToggleParams> onToggle { get; set; }
}

public sealed class PanelToggleParams
{
    public bool value { get; set; }
}