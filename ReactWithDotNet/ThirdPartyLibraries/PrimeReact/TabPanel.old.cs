namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public partial class TabView : ElementBase
{
        


    [ReactProp]
    [ReactBind(targetProp = nameof(activeIndex), jsValueAccess = "e.index", eventName = "onTabChange")]
    public Expression<Func<int>> activeIndexBind { get; set; }



    /// <summary>
    ///     Callback to invoke when an active tab is changed.
    /// </summary>
    [ReactProp]
    public Action<TabViewTabChangeParams> onTabChange { get; set; }


    /// <summary>
    ///     Callback to invoke when an active tab is closed.
    /// </summary>
    [ReactProp]
    public Action<TabViewTabChangeParams> onTabClose { get; set; }


}

[Serializable]
public class TabViewTabChangeParams
{
    public int index { get; set; }
}
