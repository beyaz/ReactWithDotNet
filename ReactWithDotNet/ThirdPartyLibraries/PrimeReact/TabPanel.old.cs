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
