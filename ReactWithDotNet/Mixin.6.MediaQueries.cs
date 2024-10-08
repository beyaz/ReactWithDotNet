namespace ReactWithDotNet;

partial class Mixin
{
    /// <summary>
    ///     min-width: 1024px
    /// </summary>
    public static StyleModifier LG(params StyleModifier[] styleModifiers)
    {
        return MediaQuery("(min-width: 1024px)", new Style(styleModifiers));
    }

    /// <summary>
    ///     min-width: 768px
    /// </summary>
    public static StyleModifier MD(params StyleModifier[] styleModifiers)
    {
        return MediaQuery("(min-width: 768px)", new Style(styleModifiers));
    }

    public static StyleModifier MediaQuery(string query, Style styleForOverride)
    {
        return new(style => style.MediaQueries.Add(new(query, styleForOverride)));
    }

    public static StyleModifier MediaQuery(string query, params StyleModifier[] styleModifiers)
    {
        return MediaQuery(query, new Style(styleModifiers));
    }

    /// <summary>
    ///     min-width: 640px
    /// </summary>
    public static StyleModifier SM(params StyleModifier[] styleModifiers)
    {
        return MediaQuery("(min-width: 640px)", new Style(styleModifiers));
    }

    /// <summary>
    ///     (min-width: <paramref name="minWidthAsPixel" /> + 'px') and (max-width: <paramref name="maxWidthAsPixel" /> + 'px')
    /// </summary>
    public static StyleModifier WhenMediaWidthBetween(int minWidthAsPixel, int maxWidthAsPixel, params StyleModifier[] styleModifiers)
    {
        return MediaQuery($"(min-width: {minWidthAsPixel}px) and (max-width: {maxWidthAsPixel}px)", styleModifiers);
    }

    /// <summary>
    ///     min-width: <paramref name="widthAsPixel" /> + 'px'
    /// </summary>
    public static StyleModifier WhenMediaSizeGreaterThan(int widthAsPixel, params StyleModifier[] styleModifiers)
    {
        return MediaQuery($"(min-width: {widthAsPixel}px)", styleModifiers);
    }

    /// <summary>
    ///     Sample Usage:
    ///     <code>
    ///      WhenMediaSizeGreaterThan(SM, BorderRadius(8))
    ///     </code>
    /// </summary>
    public static StyleModifier WhenMediaSizeGreaterThan(Func<StyleModifier[], StyleModifier> breakpoint, params StyleModifier[] styleModifiers)
    {
        return WhenMediaSizeGreaterThan(ConvertToNumber(breakpoint), styleModifiers);
    }

    /// <summary>
    ///     max-width: <paramref name="widthAsPixel" /> + 'px'
    /// </summary>
    public static StyleModifier WhenMediaSizeLessThan(int widthAsPixel, params StyleModifier[] styleModifiers)
    {
        return MediaQuery($"(max-width: {widthAsPixel}px)", styleModifiers);
    }

    /// <summary>
    ///     Sample Usage:
    ///     <code>
    ///      WhenMediaSizeLessThan(SM, BorderRadius(8))
    ///     </code>
    /// </summary>
    public static StyleModifier WhenMediaSizeLessThan(Func<StyleModifier[], StyleModifier> breakpoint, params StyleModifier[] styleModifiers)
    {
        return WhenMediaSizeLessThan(ConvertToNumber(breakpoint), styleModifiers);
    }

    /// <summary>
    ///     min-width: 1280px
    /// </summary>
    public static StyleModifier XL(params StyleModifier[] styleModifiers)
    {
        return MediaQuery("(min-width: 1280px)", new Style(styleModifiers));
    }

    /// <summary>
    ///     min-width: 1536px
    /// </summary>
    public static StyleModifier XXL(params StyleModifier[] styleModifiers)
    {
        return MediaQuery("(min-width: 1536px)", new Style(styleModifiers));
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
}