namespace ReactWithDotNet;

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
    public static StyleModifier FontSize26 => FontSize(26);
    public static StyleModifier FontSize27 => FontSize(27);
    public static StyleModifier FontSize28 => FontSize(28);
    public static StyleModifier FontSize29 => FontSize(29);
    public static StyleModifier FontSize30 => FontSize(30);
    public static StyleModifier FontSize31 => FontSize(30);
    public static StyleModifier FontSize32 => FontSize(30);
    public static StyleModifier FontSize33 => FontSize(30);
    public static StyleModifier FontSize34 => FontSize(30);
    public static StyleModifier FontSize35 => FontSize(30);
    public static StyleModifier FontSize36 => FontSize(30);
    public static StyleModifier FontSize37 => FontSize(30);
    public static StyleModifier FontSize38 => FontSize(30);
    public static StyleModifier FontSize39 => FontSize(30);
    public static StyleModifier FontSize40 => FontSize(30);

	public static StyleModifier FontSize5 => FontSize(5);
	public static StyleModifier FontSize6 => FontSize(6);
	public static StyleModifier FontSize7 => FontSize(7);
	public static StyleModifier FontSize8 => FontSize(8);
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

    public static StyleModifier FontWeight300 => FontWeight("300");
    public static StyleModifier FontWeight400 => FontWeight("400");
    public static StyleModifier FontWeight500 => FontWeight("500");
    public static StyleModifier FontWeight600 => FontWeight("600");
    public static StyleModifier FontWeight700 => FontWeight("700");
    public static StyleModifier FontWeight800 => FontWeight("800");
    public static StyleModifier FontWeight900 => FontWeight("900");
    
    public static StyleModifier FontWeightNormal => FontWeight("normal");

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
    ///     style.letterSpacing = normal
    /// </summary>
    public static StyleModifier LetterSpacingNormal
        => new(style => style.letterSpacing = "normal");

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
    public static StyleModifier LineHeight26 => LineHeight(26);
    public static StyleModifier LineHeight27 => LineHeight(27);
    public static StyleModifier LineHeight28 => LineHeight(28);
    public static StyleModifier LineHeight29 => LineHeight(29);
    public static StyleModifier LineHeight30 => LineHeight(30);
    public static StyleModifier LineHeight31 => LineHeight(31);
    public static StyleModifier LineHeight32 => LineHeight(32);
    public static StyleModifier LineHeight33 => LineHeight(33);
    public static StyleModifier LineHeight34 => LineHeight(34);
    public static StyleModifier LineHeight35 => LineHeight(35);
    public static StyleModifier LineHeight36 => LineHeight(36);
    public static StyleModifier LineHeight37 => LineHeight(37);
    public static StyleModifier LineHeight38 => LineHeight(38);
    public static StyleModifier LineHeight39 => LineHeight(39);
    public static StyleModifier LineHeight40 => LineHeight(40);

    
    public static StyleModifier LineHeight5 => LineHeight(5);
    public static StyleModifier LineHeight6 => LineHeight(6);
    public static StyleModifier LineHeight7 => LineHeight(7);
    public static StyleModifier LineHeight8 => LineHeight(8);
    public static StyleModifier LineHeight9 => LineHeight(9);
    
    public static StyleModifier LineHeightNormal => LineHeight("normal");

    /// <summary>
    ///     <b>-moz-osx-font-smoothing</b> = grayscale
    /// </summary>
    public static StyleModifier MozOsxFontSmoothingGrayScale => MozOsxFontSmoothing("grayscale");

    /// <summary>
    ///     <b>-webkit-font-smoothing</b> = grayscale
    /// </summary>
    public static StyleModifier WebkitFontSmoothingAntialiased => WebkitFontSmoothing("antialiased");

   

   

    public static StyleModifier FontSize(double fontSizePx)
    {
        return FontSize(fontSizePx.AsPixel());
    }

    public static StyleModifier FontSize(CssUnit cssUnit)
    {
        return FontSize(cssUnit.ToString());
    }

   

    

    /// <summary>
    ///     style.letterSpacing = <paramref name="letterSpacingAsPixel" /> + 'px'
    /// </summary>
    public static StyleModifier LetterSpacing(double letterSpacingAsPixel)
    {
        return new StyleModifier(style => style.letterSpacing = letterSpacingAsPixel.AsPixel());
    }

    

    public static StyleModifier LineHeight(double lineHeightPx)
    {
        return LineHeight(lineHeightPx.AsPixel());
    }

    public static StyleModifier LineHeight(CssUnit cssUnit)
    {
        return LineHeight(cssUnit.ToString());
    }

    

   
    
    
    
    
}