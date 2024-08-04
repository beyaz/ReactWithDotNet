using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite;

public abstract class Component : ReactWithDotNet.Component
{
    protected LightTheme Theme => ThemeKey[Context];

    public  StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1,Theme.grey_200));
    
    protected string Asset(string fileName) => $"{Context.wwwroot}/assets/{fileName}";
}
public abstract class PureComponent : ReactWithDotNet.PureComponent
{
    protected LightTheme Theme => ThemeKey[Context];

    public StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1, Theme.grey_200));
    
    protected string Asset(string fileName) => $"{Context.wwwroot}/assets/{fileName}";
}

public abstract class Component<TState> : ReactWithDotNet.Component<TState> where TState :class, new()
{
    protected LightTheme Theme => ThemeKey[Context];

    public StyleModifier BackgroundForPaper => Background(Theme.background_default);

    public StyleModifier BorderForPaper => Border(Solid(1, Theme.grey_200));
    
    protected string Asset(string fileName) => $"{Context.wwwroot}/assets/{fileName}";
}

static class QueryKey
{
    public static string Page = "p";
    public static string Id = "id";
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

    
    // B L U E  https://tailwindcss.com/docs/customizing-colors
    public string Blue50 = "#eff6ff";
    public string Blue100 = "#dbeafe";
    public string Blue200 = "#bfdbfe";
    public string Blue300 = "#93c5fd";
    public string Blue400 = "#60a5fa";
    public string Blue500 = "#3b82f6";
    public string Blue600 = "#2563eb";
    public string Blue700 = "#1d4ed8";
    public string Blue800 = "#1e40af";
    public string Blue900 = "#1e3a8a";
    public string Blue950 = "#172554";
    
    // G R A Y  https://tailwindcss.com/docs/customizing-colors
    public string Gray50  = "#f9fafb";
    public string Gray100 = "#f3f4f6";
    public string Gray200 = "#e5e7eb";
    public string Gray300 = "#d1d5db";
    public string Gray400 = "#9ca3af";
    public string Gray500 = "#6b7280";
    public string Gray600 = "#4b5563";
    public string Gray700 = "#374151";
    public string Gray800 = "#1f2937";
    public string Gray900 = "#111827";
    public string Gray950 = "#030712";

}
