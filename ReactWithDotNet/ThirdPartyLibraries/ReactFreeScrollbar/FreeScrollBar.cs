using ReactWithDotNet.ThirdPartyLibraries.ReactWithDotNetSkeleton;

namespace ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;

/// <summary>
/// https://www.npmjs.com/package/react-free-scrollbar
/// </summary>
public sealed class FreeScrollBar : ThirdPartyReactComponent
{
    public FreeScrollBar()
    {
        
    }

    public FreeScrollBar(params StyleModifier[] modifiers)
    {
        style.Apply(modifiers);
    }
    
  
    
    public FreeScrollBar(params Action<FreeScrollBar>[] modifiers) => modifiers.ApplyAll(Add);
    
    public FreeScrollBar(StyleModifier styleModifier, params Action<FreeScrollBar>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<FreeScrollBar> modify) => modify?.Invoke(this);
    
    
    /// <summary>
    /// You can pass fixed to decide if handler's position: fixed or static. If fixed equals true, then the handler will overlap the content inside the scroller.
    /// </summary>
    [ReactProp]
    public bool @fixed { get; set; }

    /// <summary>
    /// Set true if you want a macOS style auto-hide scroller.
    /// </summary>
    [ReactProp]
    public bool? autohide { get; set; }

    /// <summary>
    /// The time length of the handler disappears. Default: 2000
    /// </summary>
    [ReactProp]
    public int? timeout { get; set; }

    /// <summary>
    /// The width of the vertical handler or the height of the horizontal handler. Default: 10px
    /// </summary>
    [ReactProp]
    public string tracksize { get; set; }
    
    //[ReactProp]
    //public string className { get; set; }
    

    /// <summary>
    /// The starting position of the scroll area, can be descriptive string or an object.
    /// <br/>
    /// Options: "bottom", "bottom right", "top right", "right", {top: 20, left: 30}
    /// </summary>
    [ReactProp]
    public string start { get; set; }

    /// <summary>
    /// The browser scroll bar width. Default: "17px". It should fit for most browsers.
    /// </summary>
    [ReactProp]
    public string browserOffset { get; set; }

    /// <summary>
    /// This timeout adds a throttle for onScrollbarScroll. Default is 300. Set to 0 to remove throttle.
    /// </summary>
    [ReactProp]
    public int? onScrollbarScrollTimeout { get; set; }

    protected override Element GetSuspenseFallbackElement()
    {
        if (SuspenseFallback is not null)
        {
            return SuspenseFallback;
        }
        
        return new Skeleton() + style;
    }

    public static IModifier Modify(Action<FreeScrollBar> modifyAction) => CreateThirdPartyReactComponentModifier(modifyAction);
}