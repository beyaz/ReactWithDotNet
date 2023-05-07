﻿namespace ReactWithDotNet;

partial class Mixin
{
    public static StyleModifier FontSize10 => FontSize(10);
    public static StyleModifier FontSize11 => FontSize(11);
    public static StyleModifier FontSize12 => FontSize(12);
    public static StyleModifier FontSize13 => FontSize(13);
    public static StyleModifier FontSize14 => FontSize(14);
    public static StyleModifier FontSize15 => FontSize(15);
    public static StyleModifier FontSize16 => FontSize(16);
    public static StyleModifier FontSize17 => FontSize(17);
    public static StyleModifier FontSize18 => FontSize(18);
    public static StyleModifier FontSize19 => FontSize(19);
    public static StyleModifier FontSize20 => FontSize(20);
    public static StyleModifier FontSize21 => FontSize(21);
    public static StyleModifier FontSize22 => FontSize(22);
    public static StyleModifier FontSize23 => FontSize(23);
    public static StyleModifier FontSize24 => FontSize(24);
    public static StyleModifier FontSize25 => FontSize(25);
    public static StyleModifier FontSize26 => FontSize(25);
    public static StyleModifier FontSize27 => FontSize(25);
    public static StyleModifier FontSize28 => FontSize(25);
    public static StyleModifier FontSize29 => FontSize(25);
    public static StyleModifier FontSize30 => FontSize(25);
    public static StyleModifier FontSize9 => FontSize(9);

    /// <summary>
    ///     style.fontSize = 'large'
    /// </summary>
    public static StyleModifier FontSizeLarge => FontSize("large");

    /// <summary>
    ///     style.fontSize = 'larger'
    ///     <br />
    ///     Sets the font-size to a larger size than the parent element
    /// </summary>
    public static StyleModifier FontSizeLarger => FontSize("larger");

    /// <summary>
    ///     style.fontSize = 'small'
    /// </summary>
    public static StyleModifier FontSizeSmall => FontSize("small");

    public static StyleModifier FontStyleItalic => FontStyle("italic");

    public static StyleModifier FontStyleNormal => FontStyle("normal");

    public static StyleModifier FontWeight400 => FontWeight("400");
    public static StyleModifier FontWeight500 => FontWeight("500");
    public static StyleModifier FontWeight600 => FontWeight("600");
    public static StyleModifier FontWeight700 => FontWeight("700");
    public static StyleModifier FontWeight800 => FontWeight("800");
    public static StyleModifier FontWeight900 => FontWeight("900");

    public static StyleModifier FontWeightBold => FontWeight700;

    /// <summary>
    ///     style.fontWeight = '800'
    /// </summary>
    public static StyleModifier FontWeightExtraBold => FontWeight800;

    /// <summary>
    ///     style.fontWeight = '500'
    /// </summary>
    public static StyleModifier FontWeightMedium => FontWeight500;

    /// <summary>
    ///     style.fontWeight = '600'
    /// </summary>
    public static StyleModifier FontWeightSemiBold => FontWeight600;

    /// <summary>
    ///     style.fontFamily = fontFamily
    /// </summary>
    public static StyleModifier FontFamily(string fontFamily) => new(style => style.fontFamily = fontFamily);

    public static StyleModifier FontSize(string fontSize) => new(style => style.fontSize = fontSize);

    public static StyleModifier FontSize(double fontSizePx) => FontSize(fontSizePx.AsPixel());

    public static StyleModifier FontSize(CssUnit cssUnit) => FontSize(cssUnit.ToString());

    public static StyleModifier FontStyle(string fontStyle) => new(style => style.fontStyle = fontStyle);

    public static StyleModifier FontWeight(string fontWeight) => new(style => style.fontWeight = fontWeight);

    /// <summary>
    /// <b>-webkit-font-smoothing</b> = <paramref name="value"/>
    /// </summary>
    public static StyleModifier WebkitFontSmoothing(string value) 
        => new(style => style.webkitFontSmoothing = value);


    /// <summary>
    /// <b>-webkit-font-smoothing</b> = grayscale
    /// </summary>
    public static StyleModifier WebkitFontSmoothingAntialiased => WebkitFontSmoothing("antialiased");

    /// <summary>
    /// <b>-moz-osx-font-smoothing</b> = <paramref name="value"/>
    /// </summary>
    public static StyleModifier MozOsxFontSmoothing(string value)
        => new(style => style.mozOsxFontSmoothing = value);


    /// <summary>
    /// <b>-moz-osx-font-smoothing</b> = grayscale
    /// </summary>
    public static StyleModifier MozOsxFontSmoothingGrayScale => MozOsxFontSmoothing("grayscale");


    public static StyleModifier LineHeight10 => LineHeight(10);
    public static StyleModifier LineHeight11 => LineHeight(11);
    public static StyleModifier LineHeight12 => LineHeight(12);
    public static StyleModifier LineHeight13 => LineHeight(13);
    public static StyleModifier LineHeight14 => LineHeight(14);
    public static StyleModifier LineHeight15 => LineHeight(15);
    public static StyleModifier LineHeight16 => LineHeight(16);
    public static StyleModifier LineHeight17 => LineHeight(17);
    public static StyleModifier LineHeight18 => LineHeight(18);
    public static StyleModifier LineHeight19 => LineHeight(19);
    public static StyleModifier LineHeight20 => LineHeight(20);
    public static StyleModifier LineHeight21 => LineHeight(21);
    public static StyleModifier LineHeight22 => LineHeight(22);
    public static StyleModifier LineHeight23 => LineHeight(23);
    public static StyleModifier LineHeight24 => LineHeight(24);
    public static StyleModifier LineHeight25 => LineHeight(25);

    public static StyleModifier LineHeight26 => LineHeight(25);
    public static StyleModifier LineHeight27 => LineHeight(25);
    public static StyleModifier LineHeight28 => LineHeight(25);
    public static StyleModifier LineHeight29 => LineHeight(25);
    public static StyleModifier LineHeight30 => LineHeight(25);
    public static StyleModifier LineHeight31 => LineHeight(25);
    public static StyleModifier LineHeight32 => LineHeight(25);
    public static StyleModifier LineHeight33 => LineHeight(25);
    public static StyleModifier LineHeight34 => LineHeight(25);
    public static StyleModifier LineHeight35 => LineHeight(25);
    public static StyleModifier LineHeight36 => LineHeight(25);
    public static StyleModifier LineHeight37 => LineHeight(25);
    public static StyleModifier LineHeight38 => LineHeight(25);
    public static StyleModifier LineHeight39 => LineHeight(25);
    public static StyleModifier LineHeight40 => LineHeight(25);

    public static StyleModifier LineHeight9 => LineHeight(9);

    /// <summary>
    ///     style.letterSpacing = <paramref name="letterSpacingAsPixel" /> + 'px'
    /// </summary>
    public static StyleModifier LetterSpacing(double letterSpacingAsPixel) 
        => new(style => style.letterSpacing = letterSpacingAsPixel.AsPixel());


    /// <summary>
    ///     style.letterSpacing = normal
    /// </summary>
    public static StyleModifier LetterSpacingNormal
        => new(style => style.letterSpacing = "normal");

    public static StyleModifier LineHeight(string lineHeight) => new(style => style.lineHeight = lineHeight);
    public static StyleModifier LineHeight(double lineHeightPx) => LineHeight(lineHeightPx.AsPixel());


    public static StyleModifier LineHeight(CssUnit cssUnit) => LineHeight(cssUnit.ToString());
}