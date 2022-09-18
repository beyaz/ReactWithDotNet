using static AlyaVillas.WebUI.MediaSize;

namespace AlyaVillas.WebUI;

static class App
{
    public static string SITE_SHORT_NAME { get; set; } = "Alya Villas";

    public static string ImageUrl(string path)
    {
        return "wwwroot"+path;
    }

    internal static bool Is(this ReactContext reactContext, MediaSize mediaSize)
    {
        var actualMediaSize = reactContext.ClientWidth switch
        {
            >= (int)wide => wide,
            >= (int)giant => giant,
            >= (int)sideMenuBreakpoint => sideMenuBreakpoint,
            >= (int)desktop => desktop,
            >= (int)tablet => tablet,
            >= (int)phone => phone,
            >= (int)mini => mini,
            _ => supermini
        };

        return mediaSize == actualMediaSize;
    }

    public static  int containerWidth = 1360;
    public static int headerHeight= 90;
    public static int headerHeightMobile =56;

    public static string w50 = "#F6F1E4";


    internal static bool IsMobile(this ReactContext reactContext)
    {
        return reactContext.ClientWidth <= 768;
    }
}

enum MediaSize
{
    wide = 1800,
    giant = 1460,
    sideMenuBreakpoint = 1280,
    desktop = 1200,
    tablet = 1024,
    phone = 768,
    mini = 414,
    supermini = 375
}

class container :   ReactWithDotNet.ReactComponent
{
    readonly Action<HtmlElement>[] modifiers;

    public container(params Action<HtmlElement>[] modifiers)
    {
        this.modifiers = modifiers;
    }
        
    protected override Element render()
    {
        var style = new Style
        {
            maxWidth = $"{App.containerWidth}px",
            width    = "100%",
            margin = "0 auto",
            padding = "0 40px"
        };

        if (Context.IsMobile())
        {
            style.padding  = "0 24px";
            style.maxWidth = "100%";
        }

        if (Context.Is(mini))
        {
            style.padding  = "0 16px";
            style.maxWidth = "100%";
        }

        var returnDiv = new div(style)
        {
            Children = children
        };

        returnDiv.Apply(modifiers);
        
        return returnDiv;
    }
}