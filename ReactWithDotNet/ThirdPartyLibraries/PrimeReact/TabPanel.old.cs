namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class TabView : ElementBase
{
        

    /// <summary>
    ///     Active index of the TabView.
    ///     <para>default: 0</para>
    /// </summary>
    [ReactProp]
    public int activeIndex { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(activeIndex), jsValueAccess = "e.index", eventName = "onTabChange")]
    public Expression<Func<int>> activeIndexBind { get; set; }


    /// <summary>
    ///     Whether to render the contents of the selected tab or all tabs.
    ///     <para>default: true</para>
    /// </summary>
    [ReactProp]
    public bool renderActiveOnly { get; set; }

    /// <summary>
    ///     When enabled displays buttons at each side of the tab headers to scroll the tab list.
    ///     <para>default: false</para>
    /// </summary>
    [ReactProp]
    public bool scrollable { get; set; }

    /// <summary>
    ///     Callback to invoke when an active tab is changed.
    /// </summary>
    [ReactProp]
    public Action<TabViewTabChangeParams> onTabChange { get; set; }

    
   


}

[Serializable]
public class TabViewTabChangeParams
{
    public int index { get; set; }
}

public class TabPanel : ElementBase
{
    /// <summary>
    ///     Orientation of tab headers.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string header { get; set; }

    /// <summary>
    ///     Icons can be placed at left of a header.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string leftIcon { get; set; }

    /// <summary>
    ///     Icons can be placed at right of a header.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string rightIcon { get; set; }

    /// <summary>
    ///     Whether the tab is disabled.
    ///     <para>default: false</para>
    /// </summary>
    [ReactProp]
    public bool disabled { get; set; }

    /// <summary>
    ///     Defines if tab can be removed.
    ///     <para>default: false</para>
    /// </summary>
    [ReactProp]
    public bool closable { get; set; }

    /// <summary>
    ///     Style class of the tab header.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string headerClassName { get; set; }

    /// <summary>
    /// Inline style of the tab header.
    /// </summary>
    [ReactProp]
    public Style headerStyle { get; set; }
        

    /// <summary>
    ///     Style class of the tab content.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public string contentClassName { get; set; }

    /// <summary>
    ///     Inline style of the tab content.
    ///     <para>default: null</para>
    /// </summary>
    [ReactProp]
    public object contentStyle { get; set; }


}