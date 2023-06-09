namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public partial class TabView : ElementBase
{
    [ReactProp]
    [ReactBind(targetProp = nameof(activeIndex), jsValueAccess = "e.index", eventName = "onTabChange")]
    public Expression<Func<int>> activeIndexBind { get; set; }
}

[Serializable]
public class TabViewTabChangeEvent
{
    public int index { get; set; }
}

[Serializable]
public class TabViewTabCloseEvent
{
    public int index { get; set; }
}