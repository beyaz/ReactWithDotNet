// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public sealed class TabPanel : ElementBase
{
    /// <summary>
    ///     Orientation of tab headers.
    /// </summary>
    [ReactProp]
    public Element header { get; set; }
    
    /// <summary>
    ///     Header template of the tab to customize more.
    /// </summary>
    [ReactProp]
    public Element headerTemplate { get; set; }
    
    /// <summary>
    ///     Icons can be placed at left of a header.
    /// </summary>
    [ReactProp]
    public string leftIcon { get; set; }
    
    /// <summary>
    ///     Icons can be placed at right of a header.
    /// </summary>
    [ReactProp]
    public string rightIcon { get; set; }
    
    /// <summary>
    ///     Previous button of the tab header.
    /// </summary>
    [ReactProp]
    public string prevButton { get; set; }
    
    /// <summary>
    ///     Next button of the tab header.
    /// </summary>
    [ReactProp]
    public string nextButton { get; set; }
    
    /// <summary>
    ///     Close button of the tab header.
    /// </summary>
    [ReactProp]
    public string closeIcon { get; set; }
    
    /// <summary>
    ///     Whether the tab is disabled.
    ///     <br/>
    ///     @defaultValue false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     Defines if tab can be removed.
    ///     <br/>
    ///     @defaultValue false
    /// </summary>
    [ReactProp]
    public bool? closable { get; set; }
    
    /// <summary>
    ///     Style class of the tab header and content.
    /// </summary>
    [ReactProp]
    public string className { get; set; }
    
    /// <summary>
    ///     Inline style of the tab header.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenStyleEmpty))]
    public Style headerStyle { get; } = new ();
    
    /// <summary>
    ///     Style class of the tab header.
    /// </summary>
    [ReactProp]
    public string headerClassName { get; set; }
    
    /// <summary>
    ///     Inline style of the tab content.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenStyleEmpty))]
    public Style contentStyle { get; } = new ();
    
    /// <summary>
    ///     Style class of the tab content.
    /// </summary>
    [ReactProp]
    public string contentClassName { get; set; }
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public TabPanel(){ }
    
    public TabPanel(params Action<TabPanel>[] modifiers) => modifiers.ApplyAll(Add);
    
    public TabPanel(StyleModifier styleModifier, params Action<TabPanel>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<TabPanel> modify) => modify?.Invoke(this);
}
