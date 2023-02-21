using ReactWithDotNet.Libraries.ReactWithDotNetSkeleton;

namespace ReactWithDotNet.Libraries.react_syntax_highlighter;

public sealed class SyntaxHighlighter : ThirdPartyReactComponent
{
    [React]
    public string language { get; set; }

    [React]
    public new SyntaxHighlighterStyle style { get; set; }

    protected override Element GetSuspenseFallbackElement()
    {
        return new Skeleton { WidthMaximized, Height(200) };
    }
}


public enum SyntaxHighlighterStyle
{
    a11yDark,
    a11yLight,
    agate,
    anOldHope,
    androidstudio,
    arduinoLight,
    arta,
    ascetic,
    atelierCaveDark,
    atelierCaveLight,
    atelierDuneDark,
    atelierDuneLight,
    atelierEstuaryDark,
    atelierEstuaryLight,
    atelierForestDark,
    atelierForestLight,
    atelierHeathDark,
    atelierHeathLight,
    atelierLakesideDark,
    atelierLakesideLight,
    atelierPlateauDark,
    atelierPlateauLight,
    atelierSavannaDark,
    atelierSavannaLight,
    atelierSeasideDark,
    atelierSeasideLight,
    atelierSulphurpoolDark,
    atelierSulphurpoolLight,
    atomOneDarkReasonable,
    atomOneDark,
    atomOneLight,
    brownPaper,
    codepenEmbed,
    colorBrewer,
    darcula,
    dark,
    defaultStyle,
    docco,
    dracula,
    far,
    foundation,
    githubGist,
    github,
    gml,
    googlecode,
    gradientDark,
    grayscale,
    gruvboxDark,
    gruvboxLight,
    hopscotch,
    hybrid,
    idea,
    irBlack,
    isblEditorDark,
    isblEditorLight,
    kimbieDark,
    kimbieLight,
    lightfair,
    lioshi,
    magula,
    monoBlue,
    monokaiSublime,
    monokai,
    nightOwl,
    nnfxDark,
    nnfx,
    nord,
    obsidian,
    ocean,
    paraisoDark,
    paraisoLight,
    pojoaque,
    purebasic,
    qtcreatorDark,
    qtcreatorLight,
    railscasts,
    rainbow,
    routeros,
    schoolBook,
    shadesOfPurple,
    solarizedDark,
    solarizedLight,
    srcery,
    sunburst,
    tomorrowNightBlue,
    tomorrowNightBright,
    tomorrowNightEighties,
    tomorrowNight,
    tomorrow,
    vs,
    vs2015,
    xcode,
    xt256,
    zenburn

}