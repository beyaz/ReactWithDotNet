using ReactWithDotNet.Libraries.ReactWithDotNetSkeleton;

namespace ReactWithDotNet.Libraries.react_free_scrollbar;

/// <summary>
/// https://www.npmjs.com/package/react-free-scrollbar
/// </summary>
public sealed class FreeScrollBar : ThirdPartyReactComponent
{
    /// <summary>
    /// You can pass fixed to decide if handler's position: fixed or static. If fixed equals true, then the handler will overlap the content inside the scroller.
    /// </summary>
    [React]
    public bool @fixed { get; set; }

    /// <summary>
    /// Set true if you want a macOS style auto-hide scroller.
    /// </summary>
    [React]
    public bool? autohide { get; set; }

    /// <summary>
    /// The time length of the handler disappears. Default: 2000
    /// </summary>
    [React]
    public int? timeout { get; set; }

    /// <summary>
    /// The width of the vertical handler or the height of the horizontal handler. Default: 10px
    /// </summary>
    [React]
    public string tracksize { get; set; }

    /// <summary>
    /// The starting position of the scroll area, can be descriptive string or an object.
    /// <br/>
    /// Options: "bottom", "bottom right", "top right", "right", {top: 20, left: 30}
    /// </summary>
    [React]
    public string start { get; set; }

    /// <summary>
    /// The browser scroll bar width. Default: "17px". It should fit for most browsers.
    /// </summary>
    [React]
    public string browserOffset { get; set; }

    /// <summary>
    /// This timeout adds a throttle for onScrollbarScroll. Default is 300. Set to 0 to remove throttle.
    /// </summary>
    [React]
    public int? onScrollbarScrollTimeout { get; set; }

    protected override Element GetSuspenseFallbackElement()
    {
        return new Skeleton() + style;
    }
}