// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public partial class TabView
{
    /// <summary>
    ///     Active index of the TabView.
    ///     <br/>
    ///     @defaultValue 0
    /// </summary>
    [ReactProp]
    public double? activeIndex { get; set; }
    
    /// <summary>
    ///     Style class of the panels container of the tabview.
    /// </summary>
    [ReactProp]
    public string panelContainerClassName { get; set; }
    
    /// <summary>
    ///     Inline style of the panels container of the tabview.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenStyleEmpty))]
    public Style panelContainerStyle { get; } = new ();
    
    /// <summary>
    ///     Whether to render the contents of the selected tab or all tabs.
    ///     <br/>
    ///     @defaultValue true
    /// </summary>
    [ReactProp]
    public bool? renderActiveOnly { get; set; }
    
    /// <summary>
    ///     When enabled displays buttons at each side of the tab headers to scroll the tab list.
    ///     <br/>
    ///     @defaultValue false
    /// </summary>
    [ReactProp]
    public bool? scrollable { get; set; }
    
    /// <summary>
    ///     Callback to invoke when an active tab is changed.
    ///     <br/>
    ///     @param {TabViewTabChangeEvent} event -  Custom tab change event.
    /// </summary>
    [ReactProp]
    public Action<TabViewTabChangeEvent> onTabChange { get; set; }
    
    /// <summary>
    ///     Callback to invoke when an active tab is closed.
    ///     <br/>
    ///     @param {TabViewTabCloseEvent} event - Custom tab close event.
    /// </summary>
    [ReactProp]
    public Action<TabViewTabCloseEvent> onTabClose { get; set; }
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public TabView(){ }
    
    public TabView(params Action<TabView>[] modifiers) => modifiers.ApplyAll(Add);
    
    public TabView(StyleModifier styleModifier, params Action<TabView>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<TabView> modify) => modify?.Invoke(this);
}
