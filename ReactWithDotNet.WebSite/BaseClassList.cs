using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite;

public abstract class ReactComponent : ReactWithDotNet.ReactComponent
{
    protected LightTheme Theme => ThemeKey[Context];

    public  StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1,Theme.grey_200));
}
public abstract class ReactPureComponent : ReactWithDotNet.ReactPureComponent
{
    protected LightTheme Theme => ThemeKey[Context];

    public StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1, Theme.grey_200));
}

public abstract class ReactComponent<TState> : ReactWithDotNet.ReactComponent<TState> where TState : new()
{
    protected LightTheme Theme => ThemeKey[Context];

    public StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1, Theme.grey_200));
}

static class QueryKey
{
    public static string Page = "p";
}

public class LightTheme
{
    static readonly IColorPalette P = new ColorPaletteLight();

    public string pink50 => "#FFF0FB";
    public string pink100=> "#FFE5F8";
    public string pink200=> "#FFD6F3";
    public string pink300=> "#FFC2EE";
    public string pink400=> "#FFA3E5";
    public string pink500=> "#FF7AD9";
    public string pink600=> "#FF4ECD";
    public string pink700=> "#D6009A";
    public string pink800=> "#B80084";
    public string pink900=> "#4D0037";
        

    public string text_primary => P.text_primary;
    public string primary_main => P.primary_main;
    public string primary_100 => P.primary_100;
    public string common_background => P.common_background;
    public string background_default => P.background_default;
    public string grey_50 => P.grey_50;
    public string grey_100 => P.grey_100;
    public string grey_200 => P.grey_200;
    public string grey_300 => P.grey_300;
    public string grey_700 => P.grey_700;
    public string primary_700 => P.primary_700;
    public string text_secondary => P.text_secondary;
    public string EditorBackground => linear_gradientTo("right","bottom", rgb(255, 255, 255), rgb(235, 235, 235));


}
