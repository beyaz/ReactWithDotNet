using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Style
{
    // G e n e r a t e r   C o d e

    //var sb = new StringBuilder();

    //foreach (var propertyInfo in typeof(Style).GetProperties().Where(p=>p.CanRead && p.CanWrite))
    //{
    //    sb.AppendLine($"if(newStyle.{propertyInfo.Name} != null)");
    //    sb.AppendLine("{");
    //    sb.AppendLine($"style.{propertyInfo.Name} = newStyle.{propertyInfo.Name};");
    //    sb.AppendLine("}");
    //    sb.AppendLine();
    //}

    //var code = sb.ToString();

    public string this[string key]
    {
        get
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var cssAttributeName = key.Replace("-", string.Empty);

            if (nameof(alignContent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return alignContent;
            }

            if (nameof(alignItems).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return alignItems;
            }

            if (nameof(alignSelf).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return alignSelf;
            }

            if (nameof(all).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return all;
            }

            if (nameof(animation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animation;
            }

            if (nameof(animationDelay).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationDelay;
            }

            if (nameof(animationDirection).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationDirection;
            }

            if (nameof(animationDuration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationDuration;
            }

            if (nameof(animationFillMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationFillMode;
            }

            if (nameof(animationIterationCount).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationIterationCount;
            }

            if (nameof(animationName).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationName;
            }

            if (nameof(animationPlayState).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationPlayState;
            }

            if (nameof(animationTimingFunction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return animationTimingFunction;
            }

            if (nameof(backfaceVisibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backfaceVisibility;
            }

            if (nameof(background).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return background;
            }

            if (nameof(backgroundAttachment).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundAttachment;
            }

            if (nameof(backgroundBlendMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundBlendMode;
            }

            if (nameof(backgroundClip).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundClip;
            }

            if (nameof(backgroundColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundColor;
            }

            if (nameof(backgroundImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundImage;
            }

            if (nameof(backgroundOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundOrigin;
            }

            if (nameof(backgroundPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundPosition;
            }

            if (nameof(backgroundRepeat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundRepeat;
            }

            if (nameof(backgroundSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return backgroundSize;
            }

            if (nameof(border).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return border;
            }

            if (nameof(borderBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottom;
            }

            if (nameof(borderBottomColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottomColor;
            }

            if (nameof(borderBottomLeftRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottomLeftRadius;
            }

            if (nameof(borderBottomRightRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottomRightRadius;
            }

            if (nameof(borderBottomStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottomStyle;
            }

            if (nameof(borderBottomWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderBottomWidth;
            }

            if (nameof(borderCollapse).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderCollapse;
            }

            if (nameof(borderColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderColor;
            }

            if (nameof(borderImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImage;
            }

            if (nameof(borderImageOutset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImageOutset;
            }

            if (nameof(borderImageRepeat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImageRepeat;
            }

            if (nameof(borderImageSlice).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImageSlice;
            }

            if (nameof(borderImageSource).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImageSource;
            }

            if (nameof(borderImageWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderImageWidth;
            }

            if (nameof(borderLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderLeft;
            }

            if (nameof(borderLeftColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderLeftColor;
            }

            if (nameof(borderLeftStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderLeftStyle;
            }

            if (nameof(borderLeftWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderLeftWidth;
            }

            if (nameof(borderRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderRadius;
            }

            if (nameof(borderRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderRight;
            }

            if (nameof(borderRightColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderRightColor;
            }

            if (nameof(borderRightStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderRightStyle;
            }

            if (nameof(borderRightWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderRightWidth;
            }

            if (nameof(borderSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderSpacing;
            }

            if (nameof(borderStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderStyle;
            }

            if (nameof(borderTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTop;
            }

            if (nameof(borderTopColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTopColor;
            }

            if (nameof(borderTopLeftRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTopLeftRadius;
            }

            if (nameof(borderTopRightRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTopRightRadius;
            }

            if (nameof(borderTopStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTopStyle;
            }

            if (nameof(borderTopWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderTopWidth;
            }

            if (nameof(borderWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderWidth;
            }

            if (nameof(bottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return bottom;
            }

            if (nameof(boxDecorationBreak).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return boxDecorationBreak;
            }

            if (nameof(boxShadow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return boxShadow;
            }

            if (nameof(boxSizing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return boxSizing;
            }

            if (nameof(captionSide).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return captionSide;
            }

            if (nameof(clear).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return clear;
            }

            if (nameof(clip).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return clip;
            }

            if (nameof(clipPath).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return clipPath;
            }

            if (nameof(color).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return color;
            }

            if (nameof(columns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columns;
            }

            if (nameof(columnCount).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnCount;
            }

            if (nameof(columnFill).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnFill;
            }

            if (nameof(columnGap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnGap;
            }

            if (nameof(rowGap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return rowGap;
            }

            if (nameof(gap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gap;
            }

            if (nameof(columnRule).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnRule;
            }

            if (nameof(columnRuleColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnRuleColor;
            }

            if (nameof(columnRuleStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnRuleStyle;
            }

            if (nameof(columnRuleWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnRuleWidth;
            }

            if (nameof(columnSpan).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnSpan;
            }

            if (nameof(columnWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return columnWidth;
            }

            if (nameof(content).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return content;
            }
            
            if (nameof(contentVisibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return contentVisibility;
            }

            if (nameof(counterIncrement).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return counterIncrement;
            }

            if (nameof(counterReset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return counterReset;
            }

            if (nameof(cssFloat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return cssFloat;
            }

            if (nameof(cssText).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return cssText;
            }

            if (nameof(cursor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return cursor;
            }

            if (nameof(direction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return direction;
            }

            if (nameof(display).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return display;
            }

            if (nameof(dominantBaseline).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return dominantBaseline;
            }

            if (nameof(emptyCells).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return emptyCells;
            }

            if (nameof(fill).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fill;
            }

            if (nameof(fillOpacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fillOpacity;
            }

            if (nameof(fillRule).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fillRule;
            }

            if (nameof(filter).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return filter;
            }

            if (nameof(flex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flex;
            }

            if (nameof(flexBasis).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexBasis;
            }

            if (nameof(flexDirection).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexDirection;
            }

            if (nameof(flexFlow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexFlow;
            }

            if (nameof(flexGrow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexGrow;
            }

            if (nameof(flexShrink).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexShrink;
            }

            if (nameof(flexWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return flexWrap;
            }

            if (nameof(floodColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return floodColor;
            }

            if (nameof(floodOpacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return floodOpacity;
            }

            if (nameof(font).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return font;
            }

            if (nameof(fontFamily).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontFamily;
            }

            if (nameof(fontFeatureSettings).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontFeatureSettings;
            }

            if (nameof(fontKerning).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontKerning;
            }

            if (nameof(fontLanguageOverride).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontLanguageOverride;
            }

            if (nameof(fontSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontSize;
            }

            if (nameof(fontSizeAdjust).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontSizeAdjust;
            }

            if (nameof(fontStretch).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontStretch;
            }

            if (nameof(fontStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontStyle;
            }

            if (nameof(fontSynthesis).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontSynthesis;
            }

            if (nameof(fontVariant).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariant;
            }

            if (nameof(fontVariantAlternates).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantAlternates;
            }

            if (nameof(fontVariantCaps).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantCaps;
            }

            if (nameof(fontVariantEastAsian).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantEastAsian;
            }

            if (nameof(fontVariantLigatures).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantLigatures;
            }

            if (nameof(fontVariantNumeric).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantNumeric;
            }

            if (nameof(fontVariantPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontVariantPosition;
            }

            if (nameof(fontWeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return fontWeight;
            }

            if (nameof(grid).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return grid;
            }

            if (nameof(gridArea).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridArea;
            }

            if (nameof(gridAutoColumns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridAutoColumns;
            }

            if (nameof(gridAutoFlow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridAutoFlow;
            }

            if (nameof(gridAutoPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridAutoPosition;
            }

            if (nameof(gridAutoRows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridAutoRows;
            }

            if (nameof(gridColumn).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridColumn;
            }

            if (nameof(gridColumnStart).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridColumnStart;
            }

            if (nameof(gridColumnEnd).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridColumnEnd;
            }

            if (nameof(gridRow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridRow;
            }

            if (nameof(gridRowStart).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridRowStart;
            }

            if (nameof(gridRowEnd).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridRowEnd;
            }

            if (nameof(gridTemplate).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridTemplate;
            }

            if (nameof(gridTemplateAreas).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridTemplateAreas;
            }

            if (nameof(gridTemplateRows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridTemplateRows;
            }

            if (nameof(gridTemplateColumns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return gridTemplateColumns;
            }

            if (nameof(height).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return height;
            }

            if (nameof(hyphens).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return hyphens;
            }

            if (nameof(icon).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return icon;
            }

            if (nameof(imageRendering).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return imageRendering;
            }

            if (nameof(imageResolution).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return imageResolution;
            }

            if (nameof(imageOrientation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return imageOrientation;
            }

            if (nameof(imeMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return imeMode;
            }

            if (nameof(justifyContent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return justifyContent;
            }

            if (nameof(left).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return left;
            }

            if (nameof(letterSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return letterSpacing;
            }

            if (nameof(lightingColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return lightingColor;
            }

            if (nameof(lineHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return lineHeight;
            }

            if (nameof(listStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return listStyle;
            }

            if (nameof(listStyleImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return listStyleImage;
            }

            if (nameof(listStylePosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return listStylePosition;
            }

            if (nameof(listStyleType).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return listStyleType;
            }

            if (nameof(margin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return margin;
            }

            if (nameof(marginBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return marginBottom;
            }

            if (nameof(marginLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return marginLeft;
            }

            if (nameof(marginRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return marginRight;
            }

            if (nameof(marginTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return marginTop;
            }

            if (nameof(marks).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return marks;
            }

            if (nameof(mask).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return mask;
            }

            if (nameof(maskType).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return maskType;
            }

            if (nameof(maxHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return maxHeight;
            }

            if (nameof(maxWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return maxWidth;
            }

            if (nameof(minHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return minHeight;
            }

            if (nameof(minWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return minWidth;
            }

            if (nameof(mixBlendMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return mixBlendMode;
            }

            if (nameof(navDown).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return navDown;
            }

            if (nameof(navIndex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return navIndex;
            }

            if (nameof(navLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return navLeft;
            }

            if (nameof(navRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return navRight;
            }

            if (nameof(navUp).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return navUp;
            }

            if (nameof(objectFit).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return objectFit;
            }

            if (nameof(objectPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return objectPosition;
            }

            if (nameof(opacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return opacity;
            }

            if (nameof(order).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return order;
            }

            if (nameof(orphans).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return orphans;
            }

            if (nameof(outline).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return outline;
            }

            if (nameof(outlineColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return outlineColor;
            }

            if (nameof(outlineOffset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return outlineOffset;
            }

            if (nameof(outlineStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return outlineStyle;
            }

            if (nameof(outlineWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return outlineWidth;
            }

            if (nameof(overflow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return overflow;
            }

            if (nameof(overflowWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return overflowWrap;
            }

            if (nameof(overflowX).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return overflowX;
            }

            if (nameof(overflowY).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return overflowY;
            }

            if (nameof(overflowClipBox).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return overflowClipBox;
            }

            if (nameof(padding).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return padding;
            }

            if (nameof(paddingBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return paddingBottom;
            }

            if (nameof(paddingLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return paddingLeft;
            }

            if (nameof(paddingRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return paddingRight;
            }

            if (nameof(paddingTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return paddingTop;
            }

            if (nameof(pageBreakAfter).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return pageBreakAfter;
            }

            if (nameof(pageBreakBefore).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return pageBreakBefore;
            }

            if (nameof(pageBreakInside).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return pageBreakInside;
            }

            if (nameof(perspective).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return perspective;
            }

            if (nameof(perspectiveOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return perspectiveOrigin;
            }

            if (nameof(pointerEvents).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return pointerEvents;
            }

            if (nameof(position).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return position;
            }

            if (nameof(quotes).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return quotes;
            }

            if (nameof(resize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return resize;
            }

            if (nameof(right).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return right;
            }

            if (nameof(tableLayout).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return tableLayout;
            }

            if (nameof(tabSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return tabSize;
            }

            if (nameof(textAlign).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textAlign;
            }

            if (nameof(textAlignLast).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textAlignLast;
            }

            if (nameof(textCombineHorizontal).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textCombineHorizontal;
            }

            if (nameof(textDecoration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textDecoration;
            }

            if (nameof(textDecorationColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textDecorationColor;
            }

            if (nameof(textDecorationLine).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textDecorationLine;
            }

            if (nameof(textDecorationStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textDecorationStyle;
            }

            if (nameof(textIndent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textIndent;
            }

            if (nameof(textOrientation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textOrientation;
            }

            if (nameof(textOverflow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textOverflow;
            }

            if (nameof(textRendering).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textRendering;
            }

            if (nameof(textShadow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textShadow;
            }

            if (nameof(textTransform).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textTransform;
            }

            if (nameof(textUnderlinePosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return textUnderlinePosition;
            }

            if (nameof(top).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return top;
            }

            if (nameof(touchAction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return touchAction;
            }

            if (nameof(transform).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transform;
            }

            if (nameof(transformOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transformOrigin;
            }

            if (nameof(transformStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transformStyle;
            }

            if (nameof(transition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transition;
            }

            if (nameof(transitionDelay).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transitionDelay;
            }

            if (nameof(transitionDuration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transitionDuration;
            }

            if (nameof(transitionProperty).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transitionProperty;
            }

            if (nameof(transitionTimingFunction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return transitionTimingFunction;
            }

            if (nameof(unicodeBidi).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return unicodeBidi;
            }

            if (nameof(unicodeRange).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return unicodeRange;
            }

            if (nameof(verticalAlign).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return verticalAlign;
            }

            if (nameof(visibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return visibility;
            }

            if (nameof(whiteSpace).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return whiteSpace;
            }

            if (nameof(widows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return widows;
            }

            if (nameof(width).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return width;
            }

            if (nameof(willChange).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return willChange;
            }

            if (nameof(wordBreak).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return wordBreak;
            }

            if (nameof(wordSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return wordSpacing;
            }

            if (nameof(wordWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return wordWrap;
            }

            if (nameof(writingMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return writingMode;
            }

            if (nameof(zIndex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return zIndex;
            }

            if (nameof(borderInlineStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                return borderInlineStyle;
            }

            throw CssParseException(key);
        }
        set
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var cssAttributeName = key.Replace("-", string.Empty);

            if (nameof(alignContent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                alignContent = value;
                return;
            }

            if (nameof(alignItems).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                alignItems = value;
                return;
            }

            if (nameof(alignSelf).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                alignSelf = value;
                return;
            }

            if (nameof(all).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                all = value;
                return;
            }

            if (nameof(animation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animation = value;
                return;
            }

            if (nameof(animationDelay).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationDelay = value;
                return;
            }

            if (nameof(animationDirection).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationDirection = value;
                return;
            }

            if (nameof(animationDuration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationDuration = value;
                return;
            }

            if (nameof(animationFillMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationFillMode = value;
                return;
            }

            if (nameof(animationIterationCount).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationIterationCount = value;
                return;
            }

            if (nameof(animationName).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationName = value;
                return;
            }

            if (nameof(animationPlayState).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationPlayState = value;
                return;
            }

            if (nameof(animationTimingFunction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                animationTimingFunction = value;
                return;
            }

            if (nameof(backfaceVisibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backfaceVisibility = value;
                return;
            }

            if (nameof(background).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                background = value;
                return;
            }

            if (nameof(backgroundAttachment).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundAttachment = value;
                return;
            }

            if (nameof(backgroundBlendMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundBlendMode = value;
                return;
            }

            if (nameof(backgroundClip).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundClip = value;
                return;
            }

            if (nameof(backgroundColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundColor = value;
                return;
            }

            if (nameof(backgroundImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundImage = value;
                return;
            }

            if (nameof(backgroundOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundOrigin = value;
                return;
            }

            if (nameof(backgroundPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundPosition = value;
                return;
            }

            if (nameof(backgroundRepeat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundRepeat = value;
                return;
            }

            if (nameof(backgroundSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                backgroundSize = value;
                return;
            }

            if (nameof(border).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                border = value;
                return;
            }

            if (nameof(borderBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottom = value;
                return;
            }

            if (nameof(borderBottomColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottomColor = value;
                return;
            }

            if (nameof(borderBottomLeftRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottomLeftRadius = value;
                return;
            }

            if (nameof(borderBottomRightRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottomRightRadius = value;
                return;
            }

            if (nameof(borderBottomStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottomStyle = value;
                return;
            }

            if (nameof(borderBottomWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderBottomWidth = value;
                return;
            }

            if (nameof(borderCollapse).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderCollapse = value;
                return;
            }

            if (nameof(borderColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderColor = value;
                return;
            }

            if (nameof(borderImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImage = value;
                return;
            }

            if (nameof(borderImageOutset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImageOutset = value;
                return;
            }

            if (nameof(borderImageRepeat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImageRepeat = value;
                return;
            }

            if (nameof(borderImageSlice).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImageSlice = value;
                return;
            }

            if (nameof(borderImageSource).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImageSource = value;
                return;
            }

            if (nameof(borderImageWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderImageWidth = value;
                return;
            }

            if (nameof(borderLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderLeft = value;
                return;
            }

            if (nameof(borderLeftColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderLeftColor = value;
                return;
            }

            if (nameof(borderLeftStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderLeftStyle = value;
                return;
            }

            if (nameof(borderLeftWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderLeftWidth = value;
                return;
            }

            if (nameof(borderRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderRadius = value;
                return;
            }

            if (nameof(borderRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderRight = value;
                return;
            }

            if (nameof(borderRightColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderRightColor = value;
                return;
            }

            if (nameof(borderRightStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderRightStyle = value;
                return;
            }

            if (nameof(borderRightWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderRightWidth = value;
                return;
            }

            if (nameof(borderSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderSpacing = value;
                return;
            }

            if (nameof(borderStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderStyle = value;
                return;
            }

            if (nameof(borderTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTop = value;
                return;
            }

            if (nameof(borderTopColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTopColor = value;
                return;
            }

            if (nameof(borderTopLeftRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTopLeftRadius = value;
                return;
            }

            if (nameof(borderTopRightRadius).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTopRightRadius = value;
                return;
            }

            if (nameof(borderTopStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTopStyle = value;
                return;
            }

            if (nameof(borderTopWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderTopWidth = value;
                return;
            }

            if (nameof(borderWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderWidth = value;
                return;
            }

            if (nameof(bottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                bottom = value;
                return;
            }

            if (nameof(boxDecorationBreak).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                boxDecorationBreak = value;
                return;
            }

            if (nameof(boxShadow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                boxShadow = value;
                return;
            }

            if (nameof(boxSizing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                boxSizing = value;
                return;
            }

            if (nameof(captionSide).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                captionSide = value;
                return;
            }

            if (nameof(clear).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                clear = value;
                return;
            }

            if (nameof(clip).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                clip = value;
                return;
            }

            if (nameof(clipPath).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                clipPath = value;
                return;
            }

            if (nameof(color).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                color = value;
                return;
            }

            if (nameof(columns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columns = value;
                return;
            }

            if (nameof(columnCount).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnCount = value;
                return;
            }

            if (nameof(columnFill).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnFill = value;
                return;
            }

            if (nameof(columnGap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnGap = value;
                return;
            }

            if (nameof(rowGap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                rowGap = value;
                return;
            }

            if (nameof(gap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gap = value;
                return;
            }

            if (nameof(columnRule).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnRule = value;
                return;
            }

            if (nameof(columnRuleColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnRuleColor = value;
                return;
            }

            if (nameof(columnRuleStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnRuleStyle = value;
                return;
            }

            if (nameof(columnRuleWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnRuleWidth = value;
                return;
            }

            if (nameof(columnSpan).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnSpan = value;
                return;
            }

            if (nameof(columnWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                columnWidth = value;
                return;
            }

            if (nameof(content).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                content = value;
                return;
            }
            
            if (nameof(contentVisibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                contentVisibility = value;
                return;
            }

            if (nameof(counterIncrement).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                counterIncrement = value;
                return;
            }

            if (nameof(counterReset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                counterReset = value;
                return;
            }

            if (nameof(cssFloat).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                cssFloat = value;
                return;
            }

            if (nameof(cssText).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                cssText = value;
                return;
            }

            if (nameof(cursor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                cursor = value;
                return;
            }

            if (nameof(direction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                direction = value;
                return;
            }

            if (nameof(display).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                display = value;
                return;
            }

            if (nameof(dominantBaseline).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                dominantBaseline = value;
                return;
            }

            if (nameof(emptyCells).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                emptyCells = value;
                return;
            }

            if (nameof(fill).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fill = value;
                return;
            }

            if (nameof(fillOpacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fillOpacity = value;
                return;
            }

            if (nameof(fillRule).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fillRule = value;
                return;
            }

            if (nameof(filter).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                filter = value;
                return;
            }

            if (nameof(flex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flex = value;
                return;
            }

            if (nameof(flexBasis).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexBasis = value;
                return;
            }

            if (nameof(flexDirection).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexDirection = value;
                return;
            }

            if (nameof(flexFlow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexFlow = value;
                return;
            }

            if (nameof(flexGrow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexGrow = value;
                return;
            }

            if (nameof(flexShrink).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexShrink = value;
                return;
            }

            if (nameof(flexWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                flexWrap = value;
                return;
            }

            if (nameof(floodColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                floodColor = value;
                return;
            }

            if (nameof(floodOpacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                floodOpacity = value;
                return;
            }

            if (nameof(font).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                font = value;
                return;
            }

            if (nameof(fontFamily).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontFamily = value;
                return;
            }

            if (nameof(fontFeatureSettings).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontFeatureSettings = value;
                return;
            }

            if (nameof(fontKerning).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontKerning = value;
                return;
            }

            if (nameof(fontLanguageOverride).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontLanguageOverride = value;
                return;
            }

            if (nameof(fontSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontSize = value;
                return;
            }

            if (nameof(fontSizeAdjust).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontSizeAdjust = value;
                return;
            }

            if (nameof(fontStretch).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontStretch = value;
                return;
            }

            if (nameof(fontStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontStyle = value;
                return;
            }

            if (nameof(fontSynthesis).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontSynthesis = value;
                return;
            }

            if (nameof(fontVariant).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariant = value;
                return;
            }

            if (nameof(fontVariantAlternates).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantAlternates = value;
                return;
            }

            if (nameof(fontVariantCaps).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantCaps = value;
                return;
            }

            if (nameof(fontVariantEastAsian).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantEastAsian = value;
                return;
            }

            if (nameof(fontVariantLigatures).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantLigatures = value;
                return;
            }

            if (nameof(fontVariantNumeric).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantNumeric = value;
                return;
            }

            if (nameof(fontVariantPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontVariantPosition = value;
                return;
            }

            if (nameof(fontWeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                fontWeight = value;
                return;
            }

            if (nameof(grid).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                grid = value;
                return;
            }

            if (nameof(gridArea).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridArea = value;
                return;
            }

            if (nameof(gridAutoColumns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridAutoColumns = value;
                return;
            }

            if (nameof(gridAutoFlow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridAutoFlow = value;
                return;
            }

            if (nameof(gridAutoPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridAutoPosition = value;
                return;
            }

            if (nameof(gridAutoRows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridAutoRows = value;
                return;
            }

            if (nameof(gridColumn).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridColumn = value;
                return;
            }

            if (nameof(gridColumnStart).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridColumnStart = value;
                return;
            }

            if (nameof(gridColumnEnd).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridColumnEnd = value;
                return;
            }

            if (nameof(gridRow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridRow = value;
                return;
            }

            if (nameof(gridRowStart).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridRowStart = value;
                return;
            }

            if (nameof(gridRowEnd).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridRowEnd = value;
                return;
            }

            if (nameof(gridTemplate).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridTemplate = value;
                return;
            }

            if (nameof(gridTemplateAreas).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridTemplateAreas = value;
                return;
            }

            if (nameof(gridTemplateRows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridTemplateRows = value;
                return;
            }

            if (nameof(gridTemplateColumns).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                gridTemplateColumns = value;
                return;
            }

            if (nameof(height).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                height = value;
                return;
            }

            if (nameof(hyphens).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                hyphens = value;
                return;
            }

            if (nameof(icon).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                icon = value;
                return;
            }

            if (nameof(imageRendering).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                imageRendering = value;
                return;
            }

            if (nameof(imageResolution).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                imageResolution = value;
                return;
            }

            if (nameof(imageOrientation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                imageOrientation = value;
                return;
            }

            if (nameof(imeMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                imeMode = value;
                return;
            }

            if (nameof(justifyContent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                justifyContent = value;
                return;
            }

            if (nameof(left).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                left = value;
                return;
            }

            if (nameof(letterSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                letterSpacing = value;
                return;
            }

            if (nameof(lightingColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                lightingColor = value;
                return;
            }

            if (nameof(lineHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                lineHeight = value;
                return;
            }

            if (nameof(listStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                listStyle = value;
                return;
            }

            if (nameof(listStyleImage).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                listStyleImage = value;
                return;
            }

            if (nameof(listStylePosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                listStylePosition = value;
                return;
            }

            if (nameof(listStyleType).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                listStyleType = value;
                return;
            }

            if (nameof(margin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                margin = value;
                return;
            }

            if (nameof(marginBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                marginBottom = value;
                return;
            }

            if (nameof(marginLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                marginLeft = value;
                return;
            }

            if (nameof(marginRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                marginRight = value;
                return;
            }

            if (nameof(marginTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                marginTop = value;
                return;
            }

            if (nameof(marks).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                marks = value;
                return;
            }

            if (nameof(mask).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                mask = value;
                return;
            }

            if (nameof(maskType).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                maskType = value;
                return;
            }

            if (nameof(maxHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                maxHeight = value;
                return;
            }

            if (nameof(maxWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                maxWidth = value;
                return;
            }

            if (nameof(minHeight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                minHeight = value;
                return;
            }

            if (nameof(minWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                minWidth = value;
                return;
            }

            if (nameof(mixBlendMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                mixBlendMode = value;
                return;
            }

            if (nameof(navDown).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                navDown = value;
                return;
            }

            if (nameof(navIndex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                navIndex = value;
                return;
            }

            if (nameof(navLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                navLeft = value;
                return;
            }

            if (nameof(navRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                navRight = value;
                return;
            }

            if (nameof(navUp).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                navUp = value;
                return;
            }

            if (nameof(objectFit).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                objectFit = value;
                return;
            }

            if (nameof(objectPosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                objectPosition = value;
                return;
            }

            if (nameof(opacity).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                opacity = value;
                return;
            }

            if (nameof(order).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                order = value;
                return;
            }

            if (nameof(orphans).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                orphans = value;
                return;
            }

            if (nameof(outline).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                outline = value;
                return;
            }

            if (nameof(outlineColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                outlineColor = value;
                return;
            }

            if (nameof(outlineOffset).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                outlineOffset = value;
                return;
            }

            if (nameof(outlineStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                outlineStyle = value;
                return;
            }

            if (nameof(outlineWidth).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                outlineWidth = value;
                return;
            }

            if (nameof(overflow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                overflow = value;
                return;
            }

            if (nameof(overflowWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                overflowWrap = value;
                return;
            }

            if (nameof(overflowX).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                overflowX = value;
                return;
            }

            if (nameof(overflowY).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                overflowY = value;
                return;
            }

            if (nameof(overflowClipBox).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                overflowClipBox = value;
                return;
            }

            if (nameof(padding).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                padding = value;
                return;
            }

            if (nameof(paddingBottom).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                paddingBottom = value;
                return;
            }

            if (nameof(paddingLeft).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                paddingLeft = value;
                return;
            }

            if (nameof(paddingRight).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                paddingRight = value;
                return;
            }

            if (nameof(paddingTop).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                paddingTop = value;
                return;
            }

            if (nameof(pageBreakAfter).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                pageBreakAfter = value;
                return;
            }

            if (nameof(pageBreakBefore).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                pageBreakBefore = value;
                return;
            }

            if (nameof(pageBreakInside).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                pageBreakInside = value;
                return;
            }

            if (nameof(perspective).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                perspective = value;
                return;
            }

            if (nameof(perspectiveOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                perspectiveOrigin = value;
                return;
            }

            if (nameof(pointerEvents).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                pointerEvents = value;
                return;
            }

            if (nameof(position).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                position = value;
                return;
            }

            if (nameof(quotes).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                quotes = value;
                return;
            }

            if (nameof(resize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                resize = value;
                return;
            }

            if (nameof(right).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                right = value;
                return;
            }

            if (nameof(tableLayout).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                tableLayout = value;
                return;
            }

            if (nameof(tabSize).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                tabSize = value;
                return;
            }

            if (nameof(textAlign).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textAlign = value;
                return;
            }

            if (nameof(textAlignLast).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textAlignLast = value;
                return;
            }

            if (nameof(textCombineHorizontal).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textCombineHorizontal = value;
                return;
            }

            if (nameof(textDecoration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textDecoration = value;
                return;
            }

            if (nameof(textDecorationColor).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textDecorationColor = value;
                return;
            }

            if (nameof(textDecorationLine).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textDecorationLine = value;
                return;
            }

            if (nameof(textDecorationStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textDecorationStyle = value;
                return;
            }

            if (nameof(textIndent).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textIndent = value;
                return;
            }

            if (nameof(textOrientation).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textOrientation = value;
                return;
            }

            if (nameof(textOverflow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textOverflow = value;
                return;
            }

            if (nameof(textRendering).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textRendering = value;
                return;
            }

            if (nameof(textShadow).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textShadow = value;
                return;
            }

            if (nameof(textTransform).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textTransform = value;
                return;
            }

            if (nameof(textUnderlinePosition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                textUnderlinePosition = value;
                return;
            }

            if (nameof(top).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                top = value;
                return;
            }

            if (nameof(touchAction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                touchAction = value;
                return;
            }

            if (nameof(transform).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transform = value;
                return;
            }

            if (nameof(transformOrigin).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transformOrigin = value;
                return;
            }

            if (nameof(transformStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transformStyle = value;
                return;
            }

            if (nameof(transition).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transition = value;
                return;
            }

            if (nameof(transitionDelay).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transitionDelay = value;
                return;
            }

            if (nameof(transitionDuration).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transitionDuration = value;
                return;
            }

            if (nameof(transitionProperty).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transitionProperty = value;
                return;
            }

            if (nameof(transitionTimingFunction).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                transitionTimingFunction = value;
                return;
            }

            if (nameof(unicodeBidi).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                unicodeBidi = value;
                return;
            }

            if (nameof(unicodeRange).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                unicodeRange = value;
                return;
            }

            if (nameof(verticalAlign).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                verticalAlign = value;
                return;
            }

            if (nameof(visibility).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                visibility = value;
                return;
            }

            if (nameof(whiteSpace).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                whiteSpace = value;
                return;
            }

            if (nameof(widows).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                widows = value;
                return;
            }

            if (nameof(width).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                width = value;
                return;
            }

            if (nameof(willChange).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                willChange = value;
                return;
            }

            if (nameof(wordBreak).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                wordBreak = value;
                return;
            }

            if (nameof(wordSpacing).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                wordSpacing = value;
                return;
            }

            if (nameof(wordWrap).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                wordWrap = value;
                return;
            }

            if (nameof(writingMode).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                writingMode = value;
                return;
            }

            if (nameof(zIndex).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                zIndex = value;
                return;
            }

            if (nameof(borderInlineStyle).Equals(cssAttributeName, StringComparison.OrdinalIgnoreCase))
            {
                borderInlineStyle = value;
                return;
            }

            throw CssParseException(key);
        }
    }

    public void Import(Style newStyle)
    {
        if (newStyle == null)
        {
            return;
        }

        if (newStyle.alignContent != null)
        {
            alignContent = newStyle.alignContent;
        }

        if (newStyle.alignItems != null)
        {
            alignItems = newStyle.alignItems;
        }

        if (newStyle.alignSelf != null)
        {
            alignSelf = newStyle.alignSelf;
        }

        if (newStyle.all != null)
        {
            all = newStyle.all;
        }

        if (newStyle.animation != null)
        {
            animation = newStyle.animation;
        }

        if (newStyle.animationDelay != null)
        {
            animationDelay = newStyle.animationDelay;
        }

        if (newStyle.animationDirection != null)
        {
            animationDirection = newStyle.animationDirection;
        }

        if (newStyle.animationDuration != null)
        {
            animationDuration = newStyle.animationDuration;
        }

        if (newStyle.animationFillMode != null)
        {
            animationFillMode = newStyle.animationFillMode;
        }

        if (newStyle.animationIterationCount != null)
        {
            animationIterationCount = newStyle.animationIterationCount;
        }

        if (newStyle.animationName != null)
        {
            animationName = newStyle.animationName;
        }

        if (newStyle.animationPlayState != null)
        {
            animationPlayState = newStyle.animationPlayState;
        }

        if (newStyle.animationTimingFunction != null)
        {
            animationTimingFunction = newStyle.animationTimingFunction;
        }

        if (newStyle.backfaceVisibility != null)
        {
            backfaceVisibility = newStyle.backfaceVisibility;
        }

        if (newStyle.background != null)
        {
            background = newStyle.background;
        }

        if (newStyle.backgroundAttachment != null)
        {
            backgroundAttachment = newStyle.backgroundAttachment;
        }

        if (newStyle.backgroundBlendMode != null)
        {
            backgroundBlendMode = newStyle.backgroundBlendMode;
        }

        if (newStyle.backgroundClip != null)
        {
            backgroundClip = newStyle.backgroundClip;
        }

        if (newStyle.backgroundColor != null)
        {
            backgroundColor = newStyle.backgroundColor;
        }

        if (newStyle.backgroundImage != null)
        {
            backgroundImage = newStyle.backgroundImage;
        }

        if (newStyle.backgroundOrigin != null)
        {
            backgroundOrigin = newStyle.backgroundOrigin;
        }

        if (newStyle.backgroundPosition != null)
        {
            backgroundPosition = newStyle.backgroundPosition;
        }

        if (newStyle.backgroundRepeat != null)
        {
            backgroundRepeat = newStyle.backgroundRepeat;
        }

        if (newStyle.backgroundSize != null)
        {
            backgroundSize = newStyle.backgroundSize;
        }

        if (newStyle.border != null)
        {
            border = newStyle.border;
        }

        if (newStyle.borderBottom != null)
        {
            borderBottom = newStyle.borderBottom;
        }

        if (newStyle.borderBottomColor != null)
        {
            borderBottomColor = newStyle.borderBottomColor;
        }

        if (newStyle.borderBottomLeftRadius != null)
        {
            borderBottomLeftRadius = newStyle.borderBottomLeftRadius;
        }

        if (newStyle.borderBottomRightRadius != null)
        {
            borderBottomRightRadius = newStyle.borderBottomRightRadius;
        }

        if (newStyle.borderBottomStyle != null)
        {
            borderBottomStyle = newStyle.borderBottomStyle;
        }

        if (newStyle.borderBottomWidth != null)
        {
            borderBottomWidth = newStyle.borderBottomWidth;
        }

        if (newStyle.borderCollapse != null)
        {
            borderCollapse = newStyle.borderCollapse;
        }

        if (newStyle.borderColor != null)
        {
            borderColor = newStyle.borderColor;
        }

        if (newStyle.borderImage != null)
        {
            borderImage = newStyle.borderImage;
        }

        if (newStyle.borderImageOutset != null)
        {
            borderImageOutset = newStyle.borderImageOutset;
        }

        if (newStyle.borderImageRepeat != null)
        {
            borderImageRepeat = newStyle.borderImageRepeat;
        }

        if (newStyle.borderImageSlice != null)
        {
            borderImageSlice = newStyle.borderImageSlice;
        }

        if (newStyle.borderImageSource != null)
        {
            borderImageSource = newStyle.borderImageSource;
        }

        if (newStyle.borderImageWidth != null)
        {
            borderImageWidth = newStyle.borderImageWidth;
        }

        if (newStyle.borderLeft != null)
        {
            borderLeft = newStyle.borderLeft;
        }

        if (newStyle.borderLeftColor != null)
        {
            borderLeftColor = newStyle.borderLeftColor;
        }

        if (newStyle.borderLeftStyle != null)
        {
            borderLeftStyle = newStyle.borderLeftStyle;
        }

        if (newStyle.borderLeftWidth != null)
        {
            borderLeftWidth = newStyle.borderLeftWidth;
        }

        if (newStyle.borderRadius != null)
        {
            borderRadius = newStyle.borderRadius;
        }

        if (newStyle.borderRight != null)
        {
            borderRight = newStyle.borderRight;
        }

        if (newStyle.borderRightColor != null)
        {
            borderRightColor = newStyle.borderRightColor;
        }

        if (newStyle.borderRightStyle != null)
        {
            borderRightStyle = newStyle.borderRightStyle;
        }

        if (newStyle.borderRightWidth != null)
        {
            borderRightWidth = newStyle.borderRightWidth;
        }

        if (newStyle.borderSpacing != null)
        {
            borderSpacing = newStyle.borderSpacing;
        }

        if (newStyle.borderStyle != null)
        {
            borderStyle = newStyle.borderStyle;
        }

        if (newStyle.borderTop != null)
        {
            borderTop = newStyle.borderTop;
        }

        if (newStyle.borderTopColor != null)
        {
            borderTopColor = newStyle.borderTopColor;
        }

        if (newStyle.borderTopLeftRadius != null)
        {
            borderTopLeftRadius = newStyle.borderTopLeftRadius;
        }

        if (newStyle.borderTopRightRadius != null)
        {
            borderTopRightRadius = newStyle.borderTopRightRadius;
        }

        if (newStyle.borderTopStyle != null)
        {
            borderTopStyle = newStyle.borderTopStyle;
        }

        if (newStyle.borderTopWidth != null)
        {
            borderTopWidth = newStyle.borderTopWidth;
        }

        if (newStyle.borderWidth != null)
        {
            borderWidth = newStyle.borderWidth;
        }

        if (newStyle.bottom != null)
        {
            bottom = newStyle.bottom;
        }

        if (newStyle.boxDecorationBreak != null)
        {
            boxDecorationBreak = newStyle.boxDecorationBreak;
        }

        if (newStyle.boxShadow != null)
        {
            boxShadow = newStyle.boxShadow;
        }

        if (newStyle.boxSizing != null)
        {
            boxSizing = newStyle.boxSizing;
        }

        if (newStyle.captionSide != null)
        {
            captionSide = newStyle.captionSide;
        }

        if (newStyle.clear != null)
        {
            clear = newStyle.clear;
        }

        if (newStyle.clip != null)
        {
            clip = newStyle.clip;
        }

        if (newStyle.clipPath != null)
        {
            clipPath = newStyle.clipPath;
        }

        if (newStyle.color != null)
        {
            color = newStyle.color;
        }

        if (newStyle.columns != null)
        {
            columns = newStyle.columns;
        }

        if (newStyle.columnCount != null)
        {
            columnCount = newStyle.columnCount;
        }

        if (newStyle.columnFill != null)
        {
            columnFill = newStyle.columnFill;
        }

        if (newStyle.columnGap != null)
        {
            columnGap = newStyle.columnGap;
        }

        if (newStyle.rowGap != null)
        {
            rowGap = newStyle.rowGap;
        }

        if (newStyle.gap != null)
        {
            gap = newStyle.gap;
        }

        if (newStyle.columnRule != null)
        {
            columnRule = newStyle.columnRule;
        }

        if (newStyle.columnRuleColor != null)
        {
            columnRuleColor = newStyle.columnRuleColor;
        }

        if (newStyle.columnRuleStyle != null)
        {
            columnRuleStyle = newStyle.columnRuleStyle;
        }

        if (newStyle.columnRuleWidth != null)
        {
            columnRuleWidth = newStyle.columnRuleWidth;
        }

        if (newStyle.columnSpan != null)
        {
            columnSpan = newStyle.columnSpan;
        }

        if (newStyle.columnWidth != null)
        {
            columnWidth = newStyle.columnWidth;
        }

        if (newStyle.content != null)
        {
            content = newStyle.content;
        }
        
        if (newStyle.contentVisibility != null)
        {
            contentVisibility = newStyle.contentVisibility;
        }

        if (newStyle.counterIncrement != null)
        {
            counterIncrement = newStyle.counterIncrement;
        }

        if (newStyle.counterReset != null)
        {
            counterReset = newStyle.counterReset;
        }

        if (newStyle.cssFloat != null)
        {
            cssFloat = newStyle.cssFloat;
        }

        if (newStyle.cssText != null)
        {
            cssText = newStyle.cssText;
        }

        if (newStyle.cursor != null)
        {
            cursor = newStyle.cursor;
        }

        if (newStyle.direction != null)
        {
            direction = newStyle.direction;
        }

        if (newStyle.display != null)
        {
            display = newStyle.display;
        }

        if (newStyle.dominantBaseline != null)
        {
            dominantBaseline = newStyle.dominantBaseline;
        }

        if (newStyle.emptyCells != null)
        {
            emptyCells = newStyle.emptyCells;
        }

        if (newStyle.fill != null)
        {
            fill = newStyle.fill;
        }

        if (newStyle.fillOpacity != null)
        {
            fillOpacity = newStyle.fillOpacity;
        }

        if (newStyle.fillRule != null)
        {
            fillRule = newStyle.fillRule;
        }

        if (newStyle.filter != null)
        {
            filter = newStyle.filter;
        }

        if (newStyle.flex != null)
        {
            flex = newStyle.flex;
        }

        if (newStyle.flexBasis != null)
        {
            flexBasis = newStyle.flexBasis;
        }

        if (newStyle.flexDirection != null)
        {
            flexDirection = newStyle.flexDirection;
        }

        if (newStyle.flexFlow != null)
        {
            flexFlow = newStyle.flexFlow;
        }

        if (newStyle.flexGrow != null)
        {
            flexGrow = newStyle.flexGrow;
        }

        if (newStyle.flexShrink != null)
        {
            flexShrink = newStyle.flexShrink;
        }

        if (newStyle.flexWrap != null)
        {
            flexWrap = newStyle.flexWrap;
        }

        if (newStyle.floodColor != null)
        {
            floodColor = newStyle.floodColor;
        }

        if (newStyle.floodOpacity != null)
        {
            floodOpacity = newStyle.floodOpacity;
        }

        if (newStyle.font != null)
        {
            font = newStyle.font;
        }

        if (newStyle.fontFamily != null)
        {
            fontFamily = newStyle.fontFamily;
        }

        if (newStyle.fontFeatureSettings != null)
        {
            fontFeatureSettings = newStyle.fontFeatureSettings;
        }

        if (newStyle.fontKerning != null)
        {
            fontKerning = newStyle.fontKerning;
        }

        if (newStyle.fontLanguageOverride != null)
        {
            fontLanguageOverride = newStyle.fontLanguageOverride;
        }

        if (newStyle.fontSize != null)
        {
            fontSize = newStyle.fontSize;
        }

        if (newStyle.fontSizeAdjust != null)
        {
            fontSizeAdjust = newStyle.fontSizeAdjust;
        }

        if (newStyle.fontStretch != null)
        {
            fontStretch = newStyle.fontStretch;
        }

        if (newStyle.fontStyle != null)
        {
            fontStyle = newStyle.fontStyle;
        }

        if (newStyle.fontSynthesis != null)
        {
            fontSynthesis = newStyle.fontSynthesis;
        }

        if (newStyle.fontVariant != null)
        {
            fontVariant = newStyle.fontVariant;
        }

        if (newStyle.fontVariantAlternates != null)
        {
            fontVariantAlternates = newStyle.fontVariantAlternates;
        }

        if (newStyle.fontVariantCaps != null)
        {
            fontVariantCaps = newStyle.fontVariantCaps;
        }

        if (newStyle.fontVariantEastAsian != null)
        {
            fontVariantEastAsian = newStyle.fontVariantEastAsian;
        }

        if (newStyle.fontVariantLigatures != null)
        {
            fontVariantLigatures = newStyle.fontVariantLigatures;
        }

        if (newStyle.fontVariantNumeric != null)
        {
            fontVariantNumeric = newStyle.fontVariantNumeric;
        }

        if (newStyle.fontVariantPosition != null)
        {
            fontVariantPosition = newStyle.fontVariantPosition;
        }

        if (newStyle.fontWeight != null)
        {
            fontWeight = newStyle.fontWeight;
        }

        if (newStyle.grid != null)
        {
            grid = newStyle.grid;
        }

        if (newStyle.gridArea != null)
        {
            gridArea = newStyle.gridArea;
        }

        if (newStyle.gridAutoColumns != null)
        {
            gridAutoColumns = newStyle.gridAutoColumns;
        }

        if (newStyle.gridAutoFlow != null)
        {
            gridAutoFlow = newStyle.gridAutoFlow;
        }

        if (newStyle.gridAutoPosition != null)
        {
            gridAutoPosition = newStyle.gridAutoPosition;
        }

        if (newStyle.gridAutoRows != null)
        {
            gridAutoRows = newStyle.gridAutoRows;
        }

        if (newStyle.gridColumn != null)
        {
            gridColumn = newStyle.gridColumn;
        }

        if (newStyle.gridColumnStart != null)
        {
            gridColumnStart = newStyle.gridColumnStart;
        }

        if (newStyle.gridColumnEnd != null)
        {
            gridColumnEnd = newStyle.gridColumnEnd;
        }

        if (newStyle.gridRow != null)
        {
            gridRow = newStyle.gridRow;
        }

        if (newStyle.gridRowStart != null)
        {
            gridRowStart = newStyle.gridRowStart;
        }

        if (newStyle.gridRowEnd != null)
        {
            gridRowEnd = newStyle.gridRowEnd;
        }

        if (newStyle.gridTemplate != null)
        {
            gridTemplate = newStyle.gridTemplate;
        }

        if (newStyle.gridTemplateAreas != null)
        {
            gridTemplateAreas = newStyle.gridTemplateAreas;
        }

        if (newStyle.gridTemplateRows != null)
        {
            gridTemplateRows = newStyle.gridTemplateRows;
        }

        if (newStyle.gridTemplateColumns != null)
        {
            gridTemplateColumns = newStyle.gridTemplateColumns;
        }

        if (newStyle.height != null)
        {
            height = newStyle.height;
        }

        if (newStyle.hyphens != null)
        {
            hyphens = newStyle.hyphens;
        }

        if (newStyle.icon != null)
        {
            icon = newStyle.icon;
        }

        if (newStyle.imageRendering != null)
        {
            imageRendering = newStyle.imageRendering;
        }

        if (newStyle.imageResolution != null)
        {
            imageResolution = newStyle.imageResolution;
        }

        if (newStyle.imageOrientation != null)
        {
            imageOrientation = newStyle.imageOrientation;
        }

        if (newStyle.imeMode != null)
        {
            imeMode = newStyle.imeMode;
        }

        if (newStyle.justifyContent != null)
        {
            justifyContent = newStyle.justifyContent;
        }

        if (newStyle.left != null)
        {
            left = newStyle.left;
        }

        if (newStyle.letterSpacing != null)
        {
            letterSpacing = newStyle.letterSpacing;
        }

        if (newStyle.lightingColor != null)
        {
            lightingColor = newStyle.lightingColor;
        }

        if (newStyle.lineHeight != null)
        {
            lineHeight = newStyle.lineHeight;
        }

        if (newStyle.listStyle != null)
        {
            listStyle = newStyle.listStyle;
        }

        if (newStyle.listStyleImage != null)
        {
            listStyleImage = newStyle.listStyleImage;
        }

        if (newStyle.listStylePosition != null)
        {
            listStylePosition = newStyle.listStylePosition;
        }

        if (newStyle.listStyleType != null)
        {
            listStyleType = newStyle.listStyleType;
        }

        if (newStyle.margin != null)
        {
            margin = newStyle.margin;
        }

        if (newStyle.marginBottom != null)
        {
            marginBottom = newStyle.marginBottom;
        }

        if (newStyle.marginLeft != null)
        {
            marginLeft = newStyle.marginLeft;
        }

        if (newStyle.marginRight != null)
        {
            marginRight = newStyle.marginRight;
        }

        if (newStyle.marginTop != null)
        {
            marginTop = newStyle.marginTop;
        }

        if (newStyle.marks != null)
        {
            marks = newStyle.marks;
        }

        if (newStyle.mask != null)
        {
            mask = newStyle.mask;
        }

        if (newStyle.maskType != null)
        {
            maskType = newStyle.maskType;
        }

        if (newStyle.maxHeight != null)
        {
            maxHeight = newStyle.maxHeight;
        }

        if (newStyle.maxWidth != null)
        {
            maxWidth = newStyle.maxWidth;
        }

        if (newStyle.minHeight != null)
        {
            minHeight = newStyle.minHeight;
        }

        if (newStyle.minWidth != null)
        {
            minWidth = newStyle.minWidth;
        }

        if (newStyle.mixBlendMode != null)
        {
            mixBlendMode = newStyle.mixBlendMode;
        }

        if (newStyle.navDown != null)
        {
            navDown = newStyle.navDown;
        }

        if (newStyle.navIndex != null)
        {
            navIndex = newStyle.navIndex;
        }

        if (newStyle.navLeft != null)
        {
            navLeft = newStyle.navLeft;
        }

        if (newStyle.navRight != null)
        {
            navRight = newStyle.navRight;
        }

        if (newStyle.navUp != null)
        {
            navUp = newStyle.navUp;
        }

        if (newStyle.objectFit != null)
        {
            objectFit = newStyle.objectFit;
        }

        if (newStyle.objectPosition != null)
        {
            objectPosition = newStyle.objectPosition;
        }

        if (newStyle.opacity != null)
        {
            opacity = newStyle.opacity;
        }

        if (newStyle.order != null)
        {
            order = newStyle.order;
        }

        if (newStyle.orphans != null)
        {
            orphans = newStyle.orphans;
        }

        if (newStyle.outline != null)
        {
            outline = newStyle.outline;
        }

        if (newStyle.outlineColor != null)
        {
            outlineColor = newStyle.outlineColor;
        }

        if (newStyle.outlineOffset != null)
        {
            outlineOffset = newStyle.outlineOffset;
        }

        if (newStyle.outlineStyle != null)
        {
            outlineStyle = newStyle.outlineStyle;
        }

        if (newStyle.outlineWidth != null)
        {
            outlineWidth = newStyle.outlineWidth;
        }

        if (newStyle.overflow != null)
        {
            overflow = newStyle.overflow;
        }

        if (newStyle.overflowWrap != null)
        {
            overflowWrap = newStyle.overflowWrap;
        }

        if (newStyle.overflowX != null)
        {
            overflowX = newStyle.overflowX;
        }

        if (newStyle.overflowY != null)
        {
            overflowY = newStyle.overflowY;
        }

        if (newStyle.overflowClipBox != null)
        {
            overflowClipBox = newStyle.overflowClipBox;
        }

        if (newStyle.padding != null)
        {
            padding = newStyle.padding;
        }

        if (newStyle.paddingBottom != null)
        {
            paddingBottom = newStyle.paddingBottom;
        }

        if (newStyle.paddingLeft != null)
        {
            paddingLeft = newStyle.paddingLeft;
        }

        if (newStyle.paddingRight != null)
        {
            paddingRight = newStyle.paddingRight;
        }

        if (newStyle.paddingTop != null)
        {
            paddingTop = newStyle.paddingTop;
        }

        if (newStyle.pageBreakAfter != null)
        {
            pageBreakAfter = newStyle.pageBreakAfter;
        }

        if (newStyle.pageBreakBefore != null)
        {
            pageBreakBefore = newStyle.pageBreakBefore;
        }

        if (newStyle.pageBreakInside != null)
        {
            pageBreakInside = newStyle.pageBreakInside;
        }

        if (newStyle.perspective != null)
        {
            perspective = newStyle.perspective;
        }

        if (newStyle.perspectiveOrigin != null)
        {
            perspectiveOrigin = newStyle.perspectiveOrigin;
        }

        if (newStyle.pointerEvents != null)
        {
            pointerEvents = newStyle.pointerEvents;
        }

        if (newStyle.position != null)
        {
            position = newStyle.position;
        }

        if (newStyle.quotes != null)
        {
            quotes = newStyle.quotes;
        }

        if (newStyle.resize != null)
        {
            resize = newStyle.resize;
        }

        if (newStyle.right != null)
        {
            right = newStyle.right;
        }

        if (newStyle.tableLayout != null)
        {
            tableLayout = newStyle.tableLayout;
        }

        if (newStyle.tabSize != null)
        {
            tabSize = newStyle.tabSize;
        }

        if (newStyle.textAlign != null)
        {
            textAlign = newStyle.textAlign;
        }

        if (newStyle.textAlignLast != null)
        {
            textAlignLast = newStyle.textAlignLast;
        }

        if (newStyle.textCombineHorizontal != null)
        {
            textCombineHorizontal = newStyle.textCombineHorizontal;
        }

        if (newStyle.textDecoration != null)
        {
            textDecoration = newStyle.textDecoration;
        }

        if (newStyle.textDecorationColor != null)
        {
            textDecorationColor = newStyle.textDecorationColor;
        }

        if (newStyle.textDecorationLine != null)
        {
            textDecorationLine = newStyle.textDecorationLine;
        }

        if (newStyle.textDecorationStyle != null)
        {
            textDecorationStyle = newStyle.textDecorationStyle;
        }

        if (newStyle.textIndent != null)
        {
            textIndent = newStyle.textIndent;
        }

        if (newStyle.textOrientation != null)
        {
            textOrientation = newStyle.textOrientation;
        }

        if (newStyle.textOverflow != null)
        {
            textOverflow = newStyle.textOverflow;
        }

        if (newStyle.textRendering != null)
        {
            textRendering = newStyle.textRendering;
        }

        if (newStyle.textShadow != null)
        {
            textShadow = newStyle.textShadow;
        }

        if (newStyle.textTransform != null)
        {
            textTransform = newStyle.textTransform;
        }

        if (newStyle.textUnderlinePosition != null)
        {
            textUnderlinePosition = newStyle.textUnderlinePosition;
        }

        if (newStyle.top != null)
        {
            top = newStyle.top;
        }

        if (newStyle.touchAction != null)
        {
            touchAction = newStyle.touchAction;
        }

        if (newStyle.transform != null)
        {
            transform = newStyle.transform;
        }

        if (newStyle.transformOrigin != null)
        {
            transformOrigin = newStyle.transformOrigin;
        }

        if (newStyle.transformStyle != null)
        {
            transformStyle = newStyle.transformStyle;
        }

        if (newStyle.transition != null)
        {
            transition = newStyle.transition;
        }

        if (newStyle.transitionDelay != null)
        {
            transitionDelay = newStyle.transitionDelay;
        }

        if (newStyle.transitionDuration != null)
        {
            transitionDuration = newStyle.transitionDuration;
        }

        if (newStyle.transitionProperty != null)
        {
            transitionProperty = newStyle.transitionProperty;
        }

        if (newStyle.transitionTimingFunction != null)
        {
            transitionTimingFunction = newStyle.transitionTimingFunction;
        }

        if (newStyle.unicodeBidi != null)
        {
            unicodeBidi = newStyle.unicodeBidi;
        }

        if (newStyle.unicodeRange != null)
        {
            unicodeRange = newStyle.unicodeRange;
        }

        if (newStyle.verticalAlign != null)
        {
            verticalAlign = newStyle.verticalAlign;
        }

        if (newStyle.visibility != null)
        {
            visibility = newStyle.visibility;
        }

        if (newStyle.whiteSpace != null)
        {
            whiteSpace = newStyle.whiteSpace;
        }

        if (newStyle.widows != null)
        {
            widows = newStyle.widows;
        }

        if (newStyle.width != null)
        {
            width = newStyle.width;
        }

        if (newStyle.willChange != null)
        {
            willChange = newStyle.willChange;
        }

        if (newStyle.wordBreak != null)
        {
            wordBreak = newStyle.wordBreak;
        }

        if (newStyle.wordSpacing != null)
        {
            wordSpacing = newStyle.wordSpacing;
        }

        if (newStyle.wordWrap != null)
        {
            wordWrap = newStyle.wordWrap;
        }

        if (newStyle.writingMode != null)
        {
            writingMode = newStyle.writingMode;
        }

        if (newStyle.zIndex != null)
        {
            zIndex = newStyle.zIndex;
        }

        if (newStyle.borderInlineStyle != null)
        {
            borderInlineStyle = newStyle.borderInlineStyle;
        }
    }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool IsEmpty
    {
        get
        {
            if (alignContent != null)
            {
                return false;
            }

            if (alignItems != null)
            {
                return false;
            }

            if (alignSelf != null)
            {
                return false;
            }

            if (all != null)
            {
                return false;
            }

            if (animation != null)
            {
                return false;
            }

            if (animationDelay != null)
            {
                return false;
            }

            if (animationDirection != null)
            {
                return false;
            }

            if (animationDuration != null)
            {
                return false;
            }

            if (animationFillMode != null)
            {
                return false;
            }

            if (animationIterationCount != null)
            {
                return false;
            }

            if (animationName != null)
            {
                return false;
            }

            if (animationPlayState != null)
            {
                return false;
            }

            if (animationTimingFunction != null)
            {
                return false;
            }

            if (backfaceVisibility != null)
            {
                return false;
            }

            if (background != null)
            {
                return false;
            }

            if (backgroundAttachment != null)
            {
                return false;
            }

            if (backgroundBlendMode != null)
            {
                return false;
            }

            if (backgroundClip != null)
            {
                return false;
            }

            if (backgroundColor != null)
            {
                return false;
            }

            if (backgroundImage != null)
            {
                return false;
            }

            if (backgroundOrigin != null)
            {
                return false;
            }

            if (backgroundPosition != null)
            {
                return false;
            }

            if (backgroundRepeat != null)
            {
                return false;
            }

            if (backgroundSize != null)
            {
                return false;
            }

            if (border != null)
            {
                return false;
            }

            if (borderBottom != null)
            {
                return false;
            }

            if (borderBottomColor != null)
            {
                return false;
            }

            if (borderBottomLeftRadius != null)
            {
                return false;
            }

            if (borderBottomRightRadius != null)
            {
                return false;
            }

            if (borderBottomStyle != null)
            {
                return false;
            }

            if (borderBottomWidth != null)
            {
                return false;
            }

            if (borderCollapse != null)
            {
                return false;
            }

            if (borderColor != null)
            {
                return false;
            }

            if (borderImage != null)
            {
                return false;
            }

            if (borderImageOutset != null)
            {
                return false;
            }

            if (borderImageRepeat != null)
            {
                return false;
            }

            if (borderImageSlice != null)
            {
                return false;
            }

            if (borderImageSource != null)
            {
                return false;
            }

            if (borderImageWidth != null)
            {
                return false;
            }

            if (borderLeft != null)
            {
                return false;
            }

            if (borderLeftColor != null)
            {
                return false;
            }

            if (borderLeftStyle != null)
            {
                return false;
            }

            if (borderLeftWidth != null)
            {
                return false;
            }

            if (borderRadius != null)
            {
                return false;
            }

            if (borderRight != null)
            {
                return false;
            }

            if (borderRightColor != null)
            {
                return false;
            }

            if (borderRightStyle != null)
            {
                return false;
            }

            if (borderRightWidth != null)
            {
                return false;
            }

            if (borderSpacing != null)
            {
                return false;
            }

            if (borderStyle != null)
            {
                return false;
            }

            if (borderTop != null)
            {
                return false;
            }

            if (borderTopColor != null)
            {
                return false;
            }

            if (borderTopLeftRadius != null)
            {
                return false;
            }

            if (borderTopRightRadius != null)
            {
                return false;
            }

            if (borderTopStyle != null)
            {
                return false;
            }

            if (borderTopWidth != null)
            {
                return false;
            }

            if (borderWidth != null)
            {
                return false;
            }

            if (bottom != null)
            {
                return false;
            }

            if (boxDecorationBreak != null)
            {
                return false;
            }

            if (boxShadow != null)
            {
                return false;
            }

            if (boxSizing != null)
            {
                return false;
            }

            if (captionSide != null)
            {
                return false;
            }

            if (clear != null)
            {
                return false;
            }

            if (clip != null)
            {
                return false;
            }

            if (clipPath != null)
            {
                return false;
            }

            if (color != null)
            {
                return false;
            }

            if (columns != null)
            {
                return false;
            }

            if (columnCount != null)
            {
                return false;
            }

            if (columnFill != null)
            {
                return false;
            }

            if (columnGap != null)
            {
                return false;
            }

            if (rowGap != null)
            {
                return false;
            }

            if (gap != null)
            {
                return false;
            }

            if (columnRule != null)
            {
                return false;
            }

            if (columnRuleColor != null)
            {
                return false;
            }

            if (columnRuleStyle != null)
            {
                return false;
            }

            if (columnRuleWidth != null)
            {
                return false;
            }

            if (columnSpan != null)
            {
                return false;
            }

            if (columnWidth != null)
            {
                return false;
            }

            if (content != null)
            {
                return false;
            }
            
            if (contentVisibility != null)
            {
                return false;
            }

            if (counterIncrement != null)
            {
                return false;
            }

            if (counterReset != null)
            {
                return false;
            }

            if (cssFloat != null)
            {
                return false;
            }

            if (cssText != null)
            {
                return false;
            }

            if (cursor != null)
            {
                return false;
            }

            if (direction != null)
            {
                return false;
            }

            if (display != null)
            {
                return false;
            }

            if (dominantBaseline != null)
            {
                return false;
            }

            if (emptyCells != null)
            {
                return false;
            }

            if (fill != null)
            {
                return false;
            }

            if (fillOpacity != null)
            {
                return false;
            }

            if (fillRule != null)
            {
                return false;
            }

            if (filter != null)
            {
                return false;
            }

            if (flex != null)
            {
                return false;
            }

            if (flexBasis != null)
            {
                return false;
            }

            if (flexDirection != null)
            {
                return false;
            }

            if (flexFlow != null)
            {
                return false;
            }

            if (flexGrow != null)
            {
                return false;
            }

            if (flexShrink != null)
            {
                return false;
            }

            if (flexWrap != null)
            {
                return false;
            }

            if (floodColor != null)
            {
                return false;
            }

            if (floodOpacity != null)
            {
                return false;
            }

            if (font != null)
            {
                return false;
            }

            if (fontFamily != null)
            {
                return false;
            }

            if (fontFeatureSettings != null)
            {
                return false;
            }

            if (fontKerning != null)
            {
                return false;
            }

            if (fontLanguageOverride != null)
            {
                return false;
            }

            if (fontSize != null)
            {
                return false;
            }

            if (fontSizeAdjust != null)
            {
                return false;
            }

            if (fontStretch != null)
            {
                return false;
            }

            if (fontStyle != null)
            {
                return false;
            }

            if (fontSynthesis != null)
            {
                return false;
            }

            if (fontVariant != null)
            {
                return false;
            }

            if (fontVariantAlternates != null)
            {
                return false;
            }

            if (fontVariantCaps != null)
            {
                return false;
            }

            if (fontVariantEastAsian != null)
            {
                return false;
            }

            if (fontVariantLigatures != null)
            {
                return false;
            }

            if (fontVariantNumeric != null)
            {
                return false;
            }

            if (fontVariantPosition != null)
            {
                return false;
            }

            if (fontWeight != null)
            {
                return false;
            }

            if (grid != null)
            {
                return false;
            }

            if (gridArea != null)
            {
                return false;
            }

            if (gridAutoColumns != null)
            {
                return false;
            }

            if (gridAutoFlow != null)
            {
                return false;
            }

            if (gridAutoPosition != null)
            {
                return false;
            }

            if (gridAutoRows != null)
            {
                return false;
            }

            if (gridColumn != null)
            {
                return false;
            }

            if (gridColumnStart != null)
            {
                return false;
            }

            if (gridColumnEnd != null)
            {
                return false;
            }

            if (gridRow != null)
            {
                return false;
            }

            if (gridRowStart != null)
            {
                return false;
            }

            if (gridRowEnd != null)
            {
                return false;
            }

            if (gridTemplate != null)
            {
                return false;
            }

            if (gridTemplateAreas != null)
            {
                return false;
            }

            if (gridTemplateRows != null)
            {
                return false;
            }

            if (gridTemplateColumns != null)
            {
                return false;
            }

            if (height != null)
            {
                return false;
            }

            if (hyphens != null)
            {
                return false;
            }

            if (icon != null)
            {
                return false;
            }

            if (imageRendering != null)
            {
                return false;
            }

            if (imageResolution != null)
            {
                return false;
            }

            if (imageOrientation != null)
            {
                return false;
            }

            if (imeMode != null)
            {
                return false;
            }

            if (justifyContent != null)
            {
                return false;
            }

            if (left != null)
            {
                return false;
            }

            if (letterSpacing != null)
            {
                return false;
            }

            if (lightingColor != null)
            {
                return false;
            }

            if (lineHeight != null)
            {
                return false;
            }

            if (listStyle != null)
            {
                return false;
            }

            if (listStyleImage != null)
            {
                return false;
            }

            if (listStylePosition != null)
            {
                return false;
            }

            if (listStyleType != null)
            {
                return false;
            }

            if (margin != null)
            {
                return false;
            }

            if (marginBottom != null)
            {
                return false;
            }

            if (marginLeft != null)
            {
                return false;
            }

            if (marginRight != null)
            {
                return false;
            }

            if (marginTop != null)
            {
                return false;
            }

            if (marks != null)
            {
                return false;
            }

            if (mask != null)
            {
                return false;
            }

            if (maskType != null)
            {
                return false;
            }

            if (maxHeight != null)
            {
                return false;
            }

            if (maxWidth != null)
            {
                return false;
            }

            if (minHeight != null)
            {
                return false;
            }

            if (minWidth != null)
            {
                return false;
            }

            if (mixBlendMode != null)
            {
                return false;
            }

            if (navDown != null)
            {
                return false;
            }

            if (navIndex != null)
            {
                return false;
            }

            if (navLeft != null)
            {
                return false;
            }

            if (navRight != null)
            {
                return false;
            }

            if (navUp != null)
            {
                return false;
            }

            if (objectFit != null)
            {
                return false;
            }

            if (objectPosition != null)
            {
                return false;
            }

            if (opacity != null)
            {
                return false;
            }

            if (order != null)
            {
                return false;
            }

            if (orphans != null)
            {
                return false;
            }

            if (outline != null)
            {
                return false;
            }

            if (outlineColor != null)
            {
                return false;
            }

            if (outlineOffset != null)
            {
                return false;
            }

            if (outlineStyle != null)
            {
                return false;
            }

            if (outlineWidth != null)
            {
                return false;
            }

            if (overflow != null)
            {
                return false;
            }

            if (overflowWrap != null)
            {
                return false;
            }

            if (overflowX != null)
            {
                return false;
            }

            if (overflowY != null)
            {
                return false;
            }

            if (overflowClipBox != null)
            {
                return false;
            }

            if (padding != null)
            {
                return false;
            }

            if (paddingBottom != null)
            {
                return false;
            }

            if (paddingLeft != null)
            {
                return false;
            }

            if (paddingRight != null)
            {
                return false;
            }

            if (paddingTop != null)
            {
                return false;
            }

            if (pageBreakAfter != null)
            {
                return false;
            }

            if (pageBreakBefore != null)
            {
                return false;
            }

            if (pageBreakInside != null)
            {
                return false;
            }

            if (perspective != null)
            {
                return false;
            }

            if (perspectiveOrigin != null)
            {
                return false;
            }

            if (pointerEvents != null)
            {
                return false;
            }

            if (position != null)
            {
                return false;
            }

            if (quotes != null)
            {
                return false;
            }

            if (resize != null)
            {
                return false;
            }

            if (right != null)
            {
                return false;
            }

            if (tableLayout != null)
            {
                return false;
            }

            if (tabSize != null)
            {
                return false;
            }

            if (textAlign != null)
            {
                return false;
            }

            if (textAlignLast != null)
            {
                return false;
            }

            if (textCombineHorizontal != null)
            {
                return false;
            }

            if (textDecoration != null)
            {
                return false;
            }

            if (textDecorationColor != null)
            {
                return false;
            }

            if (textDecorationLine != null)
            {
                return false;
            }

            if (textDecorationStyle != null)
            {
                return false;
            }

            if (textIndent != null)
            {
                return false;
            }

            if (textOrientation != null)
            {
                return false;
            }

            if (textOverflow != null)
            {
                return false;
            }

            if (textRendering != null)
            {
                return false;
            }

            if (textShadow != null)
            {
                return false;
            }

            if (textTransform != null)
            {
                return false;
            }

            if (textUnderlinePosition != null)
            {
                return false;
            }

            if (top != null)
            {
                return false;
            }

            if (touchAction != null)
            {
                return false;
            }

            if (transform != null)
            {
                return false;
            }

            if (transformOrigin != null)
            {
                return false;
            }

            if (transformStyle != null)
            {
                return false;
            }

            if (transition != null)
            {
                return false;
            }

            if (transitionDelay != null)
            {
                return false;
            }

            if (transitionDuration != null)
            {
                return false;
            }

            if (transitionProperty != null)
            {
                return false;
            }

            if (transitionTimingFunction != null)
            {
                return false;
            }

            if (unicodeBidi != null)
            {
                return false;
            }

            if (unicodeRange != null)
            {
                return false;
            }

            if (verticalAlign != null)
            {
                return false;
            }

            if (visibility != null)
            {
                return false;
            }

            if (whiteSpace != null)
            {
                return false;
            }

            if (widows != null)
            {
                return false;
            }

            if (width != null)
            {
                return false;
            }

            if (willChange != null)
            {
                return false;
            }

            if (wordBreak != null)
            {
                return false;
            }

            if (wordSpacing != null)
            {
                return false;
            }

            if (wordWrap != null)
            {
                return false;
            }

            if (writingMode != null)
            {
                return false;
            }

            if (zIndex != null)
            {
                return false;
            }

            if (borderInlineStyle != null)
            {
                return false;
            }

            return true;
        }
    }
}