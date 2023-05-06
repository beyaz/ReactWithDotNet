namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier MediaQuery(string query, Style styleForOverride)
        => new(style => style.MediaQueries.Add(new MediaQuery(query, styleForOverride)));

    public static StyleModifier MediaQuery(string query, params StyleModifier[] styleModifiers)
        => MediaQuery(query, new Style(styleModifiers));

    /// <summary>
    ///     min-width: 1024px
    /// </summary>
    public static StyleModifier MediaQueryOnDesktop(Style styleForOverride)
        => MediaQuery("(min-width: 1024px)", styleForOverride);

    /// <summary>
    ///     min-width: 1024px
    /// </summary>
    public static StyleModifier MediaQueryOnDesktop(params StyleModifier[] styleModifiers)
        => MediaQueryOnDesktop(new Style(styleModifiers));

    /// <summary>
    ///     max-width: 767px
    /// </summary>
    public static StyleModifier MediaQueryOnMobile(Style styleForOverride)
        => MediaQuery("(max-width: 767px)", styleForOverride);

    /// <summary>
    ///     max-width: 767px
    /// </summary>
    public static StyleModifier MediaQueryOnMobile(params StyleModifier[] styleModifiers)
        => MediaQueryOnMobile(new Style(styleModifiers));

    /// <summary>
    ///     max-width: 1023px
    /// </summary>
    public static StyleModifier MediaQueryOnMobileOrTablet(Style styleForOverride)
        => MediaQuery("(max-width: 1023px)", styleForOverride);

    /// <summary>
    ///     max-width: 1023px
    /// </summary>
    public static StyleModifier MediaQueryOnMobileOrTablet(params StyleModifier[] styleModifiers)
        => MediaQueryOnMobileOrTablet(new Style(styleModifiers));

    /// <summary>
    ///     min-width: 768px and max-width: 1023px
    /// </summary>
    public static StyleModifier MediaQueryOnTablet(Style styleForOverride)
        => MediaQuery("(min-width: 768px) and (max-width: 1023px)", styleForOverride);

    /// <summary>
    ///     min-width: 768px and max-width: 1023px
    /// </summary>
    public static StyleModifier MediaQueryOnTablet(params StyleModifier[] styleModifiers)
        => MediaQueryOnTablet(new Style(styleModifiers));

    /// <summary>
    ///     min-width: 768px
    /// </summary>
    public static StyleModifier MediaQueryOnTabletOrDesktop(Style styleForOverride)
        => MediaQuery("(min-width: 768px)", styleForOverride);

    /// <summary>
    ///     min-width: 768px
    /// </summary>
    public static StyleModifier MediaQueryOnTabletOrDesktop(params StyleModifier[] styleModifiers)
        => MediaQueryOnTabletOrDesktop(new Style(styleModifiers));
}