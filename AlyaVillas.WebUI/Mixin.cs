using static AlyaVillas.WebUI.MediaSize;

namespace AlyaVillas.WebUI;

static class Mixin
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

    internal static bool IsMobile(this ReactContext reactContext)
    {
        return reactContext.ClientWidth <= 768;
    }

    public static HtmlElementModifier TitleStyle => element =>
    {
        element.style.fontWeight = "400";
        element.style.lineHeight = "1.25";
        element.style.fontFamily = "Marcellus,-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Oxygen,Ubuntu,Cantarell,Open Sans,Helvetica Neue,sans-serif";
    };
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