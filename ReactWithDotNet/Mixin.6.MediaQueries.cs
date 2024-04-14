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
    
    
    
    
    /// <summary>
    ///     max-width: 767px
    /// </summary>
    public static StyleModifier WhenMediaSizeIsMobile(Style styleForOverride)
        => MediaQuery("(max-width: 767px)", styleForOverride);

    /// <summary>
    ///     max-width: 767px
    /// </summary>
    public static StyleModifier WhenMediaSizeIsMobile(params StyleModifier[] styleModifiers)
        => WhenMediaSizeIsMobile(new Style(styleModifiers));
    
    /// <summary>
    ///     min-width: <paramref name="widthAsPixel"/> + 'px'
    /// </summary>
    public static StyleModifier WhenMediaSizeIsGreaterThan(int widthAsPixel,params StyleModifier[] styleModifiers)
        => MediaQuery($"(min-width: {widthAsPixel}px)", styleModifiers);
    
    /// <summary>
    ///     Sample Usage:
    ///     <code>
    ///      WhenMediaSizeIsGreaterThan(SM, BorderRadius(8))
    ///     </code>
    /// </summary>
    public static StyleModifier WhenMediaSizeIsGreaterThan(Func<StyleModifier[], StyleModifier> breakpoint, params StyleModifier[] styleModifiers)
    {
        return WhenMediaSizeIsGreaterThan(ConvertToNumber(breakpoint), styleModifiers);
    }
    
    /// <summary>
    ///     max-width: <paramref name="widthAsPixel"/> + 'px'
    /// </summary>
    public static StyleModifier WhenMediaSizeIsLessThan(int widthAsPixel,params StyleModifier[] styleModifiers)
        => MediaQuery($"(max-width: {widthAsPixel}px)", styleModifiers);

    /// <summary>
    ///     Sample Usage:
    ///     <code>
    ///      WhenMediaSizeIsLessThan(SM, BorderRadius(8))
    ///     </code>
    /// </summary>
    public static StyleModifier WhenMediaSizeIsLessThan(Func<StyleModifier[], StyleModifier> breakpoint, params StyleModifier[] styleModifiers)
    {
        return WhenMediaSizeIsLessThan(ConvertToNumber(breakpoint), styleModifiers);
    }

    static int ConvertToNumber(Func<StyleModifier[], StyleModifier> breakpoint)
    {
        if (breakpoint == SM)
        {
            return 640;
        }

        if (breakpoint == MD)
        {
            return 768;
        }

        if (breakpoint == LG)
        {
            return 1024;
        }

        if (breakpoint == XL)
        {
            return 1280;
        }

        if (breakpoint == XXL)
        {
            return 1536;
        }

        throw DeveloperException($"{breakpoint} parameter can be SM or MD or LG or XML or XXL");
    }
    
    /// <summary>
    ///     (min-width: <paramref name="minWidthAsPixel"/> + 'px') and (max-width: <paramref name="maxWidthAsPixel"/> + 'px')
    /// </summary>
    public static StyleModifier WhenMediaSizeBetween(int minWidthAsPixel,int maxWidthAsPixel, params StyleModifier[] styleModifiers)
        => MediaQuery($"(min-width: {minWidthAsPixel}px) and (max-width: {maxWidthAsPixel}px)", styleModifiers);
    
    
    /// <summary>
    ///     min-width: 640px
    /// </summary>
    public static StyleModifier SM(params StyleModifier[] styleModifiers)
        => MediaQuery("(min-width: 640px)", new Style(styleModifiers));
    
    /// <summary>
    ///     min-width: 768px
    /// </summary>
    public static StyleModifier MD(params StyleModifier[] styleModifiers)
        => MediaQuery("(min-width: 768px)", new Style(styleModifiers));
    
    /// <summary>
    ///     min-width: 1024px
    /// </summary>
    public static StyleModifier LG(params StyleModifier[] styleModifiers)
        => MediaQuery("(min-width: 1024px)", new Style(styleModifiers));
    
    /// <summary>
    ///     min-width: 1280px
    /// </summary>
    public static StyleModifier XL(params StyleModifier[] styleModifiers)
        => MediaQuery("(min-width: 1280px)", new Style(styleModifiers));
    
    /// <summary>
    ///     min-width: 1536px
    /// </summary>
    public static StyleModifier XXL(params StyleModifier[] styleModifiers)
        => MediaQuery("(min-width: 1536px)", new Style(styleModifiers));
    
}