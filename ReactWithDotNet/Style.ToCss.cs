using System.Text;

namespace ReactWithDotNet;

partial class Style
{
    public static Style ParseCss(string css)
    {
        var style = new Style();

        style.Import(css);

        return style;
    }

    public void Import(string css)
    {
        if (css == null)
        {
            return;
        }

        foreach (var line in css.Trim().Split(";").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            var array = line.Trim().Split(":").Select(x => x.Trim()).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            if (array.Length != 2)
            {
                throw CssParseException(line);
            }

            var cssAttributeName = array[0].Trim();
            if (cssAttributeName.StartsWith("/*", StringComparison.OrdinalIgnoreCase))
            {
                var endCommentIndex = cssAttributeName.LastIndexOf("*/", StringComparison.OrdinalIgnoreCase);
                if (endCommentIndex < 0)
                {
                    throw CssParseException(line);
                }

                cssAttributeName = cssAttributeName.Substring(endCommentIndex + 2).Trim();
            }

            this[cssAttributeName] = array[1];
        }
    }

    public string ToCss()
    {
        return ToCss(isImportant: false);
    }

    public string ToCssWithImportant()
    {
        return ToCss(isImportant: true);
    }

    public override string ToString()
    {
        return ToCss();
    }

    static Exception CssParseException(string message)
    {
        return new Exception("Css parse error." + message);
    }

    string ToCss(bool isImportant)
    {
        var sb = new StringBuilder();

        var separator = isImportant ? " !important;" : ";";

        if (alignContent != null)
        {
            sb.Append("align-content");
            sb.Append(":");
            sb.Append(alignContent);
            sb.Append(separator);
        }

        if (alignItems != null)
        {
            sb.Append("align-items");
            sb.Append(":");
            sb.Append(alignItems);
            sb.Append(separator);
        }

        if (alignSelf != null)
        {
            sb.Append("align-self");
            sb.Append(":");
            sb.Append(alignSelf);
            sb.Append(separator);
        }

        if (all != null)
        {
            sb.Append("all");
            sb.Append(":");
            sb.Append(all);
            sb.Append(separator);
        }

        if (animation != null)
        {
            sb.Append("animation");
            sb.Append(":");
            sb.Append(animation);
            sb.Append(separator);
        }

        if (animationDelay != null)
        {
            sb.Append("animation-delay");
            sb.Append(":");
            sb.Append(animationDelay);
            sb.Append(separator);
        }

        if (animationDirection != null)
        {
            sb.Append("animation-direction");
            sb.Append(":");
            sb.Append(animationDirection);
            sb.Append(separator);
        }

        if (animationDuration != null)
        {
            sb.Append("animation-duration");
            sb.Append(":");
            sb.Append(animationDuration);
            sb.Append(separator);
        }

        if (animationFillMode != null)
        {
            sb.Append("animation-fill-mode");
            sb.Append(":");
            sb.Append(animationFillMode);
            sb.Append(separator);
        }

        if (animationIterationCount != null)
        {
            sb.Append("animation-iteration-count");
            sb.Append(":");
            sb.Append(animationIterationCount);
            sb.Append(separator);
        }

        if (animationName != null)
        {
            sb.Append("animation-name");
            sb.Append(":");
            sb.Append(animationName);
            sb.Append(separator);
        }

        if (animationPlayState != null)
        {
            sb.Append("animation-play-state");
            sb.Append(":");
            sb.Append(animationPlayState);
            sb.Append(separator);
        }

        if (animationTimingFunction != null)
        {
            sb.Append("animation-timing-function");
            sb.Append(":");
            sb.Append(animationTimingFunction);
            sb.Append(separator);
        }

        if (backdropFilter != null)
        {
            sb.Append("backdrop-filter");
            sb.Append(":");
            sb.Append(backdropFilter);
            sb.Append(separator);
        }

        if (backfaceVisibility != null)
        {
            sb.Append("backface-visibility");
            sb.Append(":");
            sb.Append(backfaceVisibility);
            sb.Append(separator);
        }

        if (background != null)
        {
            sb.Append("background");
            sb.Append(":");
            sb.Append(background);
            sb.Append(separator);
        }

        if (backgroundAttachment != null)
        {
            sb.Append("background-attachment");
            sb.Append(":");
            sb.Append(backgroundAttachment);
            sb.Append(separator);
        }

        if (backgroundBlendMode != null)
        {
            sb.Append("background-blend-mode");
            sb.Append(":");
            sb.Append(backgroundBlendMode);
            sb.Append(separator);
        }

        if (backgroundClip != null)
        {
            sb.Append("background-clip");
            sb.Append(":");
            sb.Append(backgroundClip);
            sb.Append(separator);
        }

        if (backgroundColor != null)
        {
            sb.Append("background-color");
            sb.Append(":");
            sb.Append(backgroundColor);
            sb.Append(separator);
        }

        if (backgroundImage != null)
        {
            sb.Append("background-image");
            sb.Append(":");
            sb.Append(backgroundImage);
            sb.Append(separator);
        }

        if (backgroundOrigin != null)
        {
            sb.Append("background-origin");
            sb.Append(":");
            sb.Append(backgroundOrigin);
            sb.Append(separator);
        }

        if (backgroundPosition != null)
        {
            sb.Append("background-position");
            sb.Append(":");
            sb.Append(backgroundPosition);
            sb.Append(separator);
        }

        if (backgroundRepeat != null)
        {
            sb.Append("background-repeat");
            sb.Append(":");
            sb.Append(backgroundRepeat);
            sb.Append(separator);
        }

        if (backgroundSize != null)
        {
            sb.Append("background-size");
            sb.Append(":");
            sb.Append(backgroundSize);
            sb.Append(separator);
        }

        if (border != null)
        {
            sb.Append("border");
            sb.Append(":");
            sb.Append(border);
            sb.Append(separator);
        }

        if (borderBottom != null)
        {
            sb.Append("border-bottom");
            sb.Append(":");
            sb.Append(borderBottom);
            sb.Append(separator);
        }

        if (borderBottomColor != null)
        {
            sb.Append("border-bottom-color");
            sb.Append(":");
            sb.Append(borderBottomColor);
            sb.Append(separator);
        }

        if (borderBottomLeftRadius != null)
        {
            sb.Append("border-bottom-left-radius");
            sb.Append(":");
            sb.Append(borderBottomLeftRadius);
            sb.Append(separator);
        }

        if (borderBottomRightRadius != null)
        {
            sb.Append("border-bottom-right-radius");
            sb.Append(":");
            sb.Append(borderBottomRightRadius);
            sb.Append(separator);
        }

        if (borderBottomStyle != null)
        {
            sb.Append("border-bottom-style");
            sb.Append(":");
            sb.Append(borderBottomStyle);
            sb.Append(separator);
        }

        if (borderBottomWidth != null)
        {
            sb.Append("border-bottom-width");
            sb.Append(":");
            sb.Append(borderBottomWidth);
            sb.Append(separator);
        }

        if (borderCollapse != null)
        {
            sb.Append("border-collapse");
            sb.Append(":");
            sb.Append(borderCollapse);
            sb.Append(separator);
        }

        if (borderColor != null)
        {
            sb.Append("border-color");
            sb.Append(":");
            sb.Append(borderColor);
            sb.Append(separator);
        }

        if (borderImage != null)
        {
            sb.Append("border-image");
            sb.Append(":");
            sb.Append(borderImage);
            sb.Append(separator);
        }

        if (borderImageOutset != null)
        {
            sb.Append("border-image-outset");
            sb.Append(":");
            sb.Append(borderImageOutset);
            sb.Append(separator);
        }

        if (borderImageRepeat != null)
        {
            sb.Append("border-image-repeat");
            sb.Append(":");
            sb.Append(borderImageRepeat);
            sb.Append(separator);
        }

        if (borderImageSlice != null)
        {
            sb.Append("border-image-slice");
            sb.Append(":");
            sb.Append(borderImageSlice);
            sb.Append(separator);
        }

        if (borderImageSource != null)
        {
            sb.Append("border-image-source");
            sb.Append(":");
            sb.Append(borderImageSource);
            sb.Append(separator);
        }

        if (borderImageWidth != null)
        {
            sb.Append("border-image-width");
            sb.Append(":");
            sb.Append(borderImageWidth);
            sb.Append(separator);
        }

        if (borderLeft != null)
        {
            sb.Append("border-left");
            sb.Append(":");
            sb.Append(borderLeft);
            sb.Append(separator);
        }

        if (borderLeftColor != null)
        {
            sb.Append("border-left-color");
            sb.Append(":");
            sb.Append(borderLeftColor);
            sb.Append(separator);
        }

        if (borderLeftStyle != null)
        {
            sb.Append("border-left-style");
            sb.Append(":");
            sb.Append(borderLeftStyle);
            sb.Append(separator);
        }

        if (borderLeftWidth != null)
        {
            sb.Append("border-left-width");
            sb.Append(":");
            sb.Append(borderLeftWidth);
            sb.Append(separator);
        }

        if (borderRadius != null)
        {
            sb.Append("border-radius");
            sb.Append(":");
            sb.Append(borderRadius);
            sb.Append(separator);
        }

        if (borderRight != null)
        {
            sb.Append("border-right");
            sb.Append(":");
            sb.Append(borderRight);
            sb.Append(separator);
        }

        if (borderRightColor != null)
        {
            sb.Append("border-right-color");
            sb.Append(":");
            sb.Append(borderRightColor);
            sb.Append(separator);
        }

        if (borderRightStyle != null)
        {
            sb.Append("border-right-style");
            sb.Append(":");
            sb.Append(borderRightStyle);
            sb.Append(separator);
        }

        if (borderRightWidth != null)
        {
            sb.Append("border-right-width");
            sb.Append(":");
            sb.Append(borderRightWidth);
            sb.Append(separator);
        }

        if (borderSpacing != null)
        {
            sb.Append("border-spacing");
            sb.Append(":");
            sb.Append(borderSpacing);
            sb.Append(separator);
        }

        if (borderStyle != null)
        {
            sb.Append("border-style");
            sb.Append(":");
            sb.Append(borderStyle);
            sb.Append(separator);
        }

        if (borderTop != null)
        {
            sb.Append("border-top");
            sb.Append(":");
            sb.Append(borderTop);
            sb.Append(separator);
        }

        if (borderTopColor != null)
        {
            sb.Append("border-top-color");
            sb.Append(":");
            sb.Append(borderTopColor);
            sb.Append(separator);
        }

        if (borderTopLeftRadius != null)
        {
            sb.Append("border-top-left-radius");
            sb.Append(":");
            sb.Append(borderTopLeftRadius);
            sb.Append(separator);
        }

        if (borderTopRightRadius != null)
        {
            sb.Append("border-top-right-radius");
            sb.Append(":");
            sb.Append(borderTopRightRadius);
            sb.Append(separator);
        }

        if (borderTopStyle != null)
        {
            sb.Append("border-top-style");
            sb.Append(":");
            sb.Append(borderTopStyle);
            sb.Append(separator);
        }

        if (borderTopWidth != null)
        {
            sb.Append("border-top-width");
            sb.Append(":");
            sb.Append(borderTopWidth);
            sb.Append(separator);
        }

        if (borderWidth != null)
        {
            sb.Append("border-width");
            sb.Append(":");
            sb.Append(borderWidth);
            sb.Append(separator);
        }

        if (bottom != null)
        {
            sb.Append("bottom");
            sb.Append(":");
            sb.Append(bottom);
            sb.Append(separator);
        }

        if (boxDecorationBreak != null)
        {
            sb.Append("box-decoration-break");
            sb.Append(":");
            sb.Append(boxDecorationBreak);
            sb.Append(separator);
        }

        if (boxShadow != null)
        {
            sb.Append("box-shadow");
            sb.Append(":");
            sb.Append(boxShadow);
            sb.Append(separator);
        }

        if (boxSizing != null)
        {
            sb.Append("box-sizing");
            sb.Append(":");
            sb.Append(boxSizing);
            sb.Append(separator);
        }

        if (captionSide != null)
        {
            sb.Append("caption-side");
            sb.Append(":");
            sb.Append(captionSide);
            sb.Append(separator);
        }

        if (clear != null)
        {
            sb.Append("clear");
            sb.Append(":");
            sb.Append(clear);
            sb.Append(separator);
        }

        if (clip != null)
        {
            sb.Append("clip");
            sb.Append(":");
            sb.Append(clip);
            sb.Append(separator);
        }

        if (clipPath != null)
        {
            sb.Append("clip-path");
            sb.Append(":");
            sb.Append(clipPath);
            sb.Append(separator);
        }

        if (color != null)
        {
            sb.Append("color");
            sb.Append(":");
            sb.Append(color);
            sb.Append(separator);
        }

        if (columns != null)
        {
            sb.Append("columns");
            sb.Append(":");
            sb.Append(columns);
            sb.Append(separator);
        }

        if (columnCount != null)
        {
            sb.Append("column-count");
            sb.Append(":");
            sb.Append(columnCount);
            sb.Append(separator);
        }

        if (columnFill != null)
        {
            sb.Append("column-fill");
            sb.Append(":");
            sb.Append(columnFill);
            sb.Append(separator);
        }

        if (columnGap != null)
        {
            sb.Append("column-gap");
            sb.Append(":");
            sb.Append(columnGap);
            sb.Append(separator);
        }

        if (rowGap != null)
        {
            sb.Append("row-gap");
            sb.Append(":");
            sb.Append(rowGap);
            sb.Append(separator);
        }

        if (gap != null)
        {
            sb.Append("gap");
            sb.Append(":");
            sb.Append(gap);
            sb.Append(separator);
        }

        if (columnRule != null)
        {
            sb.Append("column-rule");
            sb.Append(":");
            sb.Append(columnRule);
            sb.Append(separator);
        }

        if (columnRuleColor != null)
        {
            sb.Append("column-rule-color");
            sb.Append(":");
            sb.Append(columnRuleColor);
            sb.Append(separator);
        }

        if (columnRuleStyle != null)
        {
            sb.Append("column-rule-style");
            sb.Append(":");
            sb.Append(columnRuleStyle);
            sb.Append(separator);
        }

        if (columnRuleWidth != null)
        {
            sb.Append("column-rule-width");
            sb.Append(":");
            sb.Append(columnRuleWidth);
            sb.Append(separator);
        }

        if (columnSpan != null)
        {
            sb.Append("column-span");
            sb.Append(":");
            sb.Append(columnSpan);
            sb.Append(separator);
        }

        if (columnWidth != null)
        {
            sb.Append("column-width");
            sb.Append(":");
            sb.Append(columnWidth);
            sb.Append(separator);
        }

        if (content != null)
        {
            sb.Append("content");
            sb.Append(":");
            sb.Append("'" + content + "'");
            sb.Append(separator);
        }

        if (contentVisibility != null)
        {
            sb.Append("content-visibility");
            sb.Append(":");
            sb.Append(contentVisibility);
            sb.Append(separator);
        }

        if (counterIncrement != null)
        {
            sb.Append("counter-increment");
            sb.Append(":");
            sb.Append(counterIncrement);
            sb.Append(separator);
        }

        if (counterReset != null)
        {
            sb.Append("counter-reset");
            sb.Append(":");
            sb.Append(counterReset);
            sb.Append(separator);
        }

        if (cssFloat != null)
        {
            sb.Append("float");
            sb.Append(":");
            sb.Append(cssFloat);
            sb.Append(separator);
        }

        if (cssText != null)
        {
            sb.Append("text");
            sb.Append(":");
            sb.Append(cssText);
            sb.Append(separator);
        }

        if (cursor != null)
        {
            sb.Append("cursor");
            sb.Append(":");
            sb.Append(cursor);
            sb.Append(separator);
        }

        if (direction != null)
        {
            sb.Append("direction");
            sb.Append(":");
            sb.Append(direction);
            sb.Append(separator);
        }

        if (display != null)
        {
            sb.Append("display");
            sb.Append(":");
            sb.Append(display);
            sb.Append(separator);
        }

        if (dominantBaseline != null)
        {
            sb.Append("dominant-baseline");
            sb.Append(":");
            sb.Append(dominantBaseline);
            sb.Append(separator);
        }

        if (emptyCells != null)
        {
            sb.Append("empty-cells");
            sb.Append(":");
            sb.Append(emptyCells);
            sb.Append(separator);
        }

        if (fill != null)
        {
            sb.Append("fill");
            sb.Append(":");
            sb.Append(fill);
            sb.Append(separator);
        }

        if (fillOpacity != null)
        {
            sb.Append("fill-opacity");
            sb.Append(":");
            sb.Append(fillOpacity);
            sb.Append(separator);
        }

        if (fillRule != null)
        {
            sb.Append("fill-rule");
            sb.Append(":");
            sb.Append(fillRule);
            sb.Append(separator);
        }

        if (filter != null)
        {
            sb.Append("filter");
            sb.Append(":");
            sb.Append(filter);
            sb.Append(separator);
        }

        if (flex != null)
        {
            sb.Append("flex");
            sb.Append(":");
            sb.Append(flex);
            sb.Append(separator);
        }

        if (flexBasis != null)
        {
            sb.Append("flex-basis");
            sb.Append(":");
            sb.Append(flexBasis);
            sb.Append(separator);
        }

        if (flexDirection != null)
        {
            sb.Append("flex-direction");
            sb.Append(":");
            sb.Append(flexDirection);
            sb.Append(separator);
        }

        if (flexFlow != null)
        {
            sb.Append("flex-flow");
            sb.Append(":");
            sb.Append(flexFlow);
            sb.Append(separator);
        }

        if (flexGrow != null)
        {
            sb.Append("flex-grow");
            sb.Append(":");
            sb.Append(flexGrow);
            sb.Append(separator);
        }

        if (flexShrink != null)
        {
            sb.Append("flex-shrink");
            sb.Append(":");
            sb.Append(flexShrink);
            sb.Append(separator);
        }

        if (flexWrap != null)
        {
            sb.Append("flex-wrap");
            sb.Append(":");
            sb.Append(flexWrap);
            sb.Append(separator);
        }

        if (floodColor != null)
        {
            sb.Append("flood-color");
            sb.Append(":");
            sb.Append(floodColor);
            sb.Append(separator);
        }

        if (floodOpacity != null)
        {
            sb.Append("flood-opacity");
            sb.Append(":");
            sb.Append(floodOpacity);
            sb.Append(separator);
        }

        if (font != null)
        {
            sb.Append("font");
            sb.Append(":");
            sb.Append(font);
            sb.Append(separator);
        }

        if (fontFamily != null)
        {
            sb.Append("font-family");
            sb.Append(":");
            sb.Append(fontFamily);
            sb.Append(separator);
        }

        if (fontFeatureSettings != null)
        {
            sb.Append("font-feature-settings");
            sb.Append(":");
            sb.Append(fontFeatureSettings);
            sb.Append(separator);
        }

        if (fontKerning != null)
        {
            sb.Append("font-kerning");
            sb.Append(":");
            sb.Append(fontKerning);
            sb.Append(separator);
        }

        if (fontLanguageOverride != null)
        {
            sb.Append("font-language-override");
            sb.Append(":");
            sb.Append(fontLanguageOverride);
            sb.Append(separator);
        }

        if (fontSize != null)
        {
            sb.Append("font-size");
            sb.Append(":");
            sb.Append(fontSize);
            sb.Append(separator);
        }

        if (fontSizeAdjust != null)
        {
            sb.Append("font-size-adjust");
            sb.Append(":");
            sb.Append(fontSizeAdjust);
            sb.Append(separator);
        }

        if (fontStretch != null)
        {
            sb.Append("font-stretch");
            sb.Append(":");
            sb.Append(fontStretch);
            sb.Append(separator);
        }

        if (fontStyle != null)
        {
            sb.Append("font-style");
            sb.Append(":");
            sb.Append(fontStyle);
            sb.Append(separator);
        }

        if (fontSynthesis != null)
        {
            sb.Append("font-synthesis");
            sb.Append(":");
            sb.Append(fontSynthesis);
            sb.Append(separator);
        }

        if (fontVariant != null)
        {
            sb.Append("font-variant");
            sb.Append(":");
            sb.Append(fontVariant);
            sb.Append(separator);
        }

        if (fontVariantAlternates != null)
        {
            sb.Append("font-variant-alternates");
            sb.Append(":");
            sb.Append(fontVariantAlternates);
            sb.Append(separator);
        }

        if (fontVariantCaps != null)
        {
            sb.Append("font-variant-caps");
            sb.Append(":");
            sb.Append(fontVariantCaps);
            sb.Append(separator);
        }

        if (fontVariantEastAsian != null)
        {
            sb.Append("font-variant-east-asian");
            sb.Append(":");
            sb.Append(fontVariantEastAsian);
            sb.Append(separator);
        }

        if (fontVariantLigatures != null)
        {
            sb.Append("font-variant-ligatures");
            sb.Append(":");
            sb.Append(fontVariantLigatures);
            sb.Append(separator);
        }

        if (fontVariantNumeric != null)
        {
            sb.Append("font-variant-numeric");
            sb.Append(":");
            sb.Append(fontVariantNumeric);
            sb.Append(separator);
        }

        if (fontVariantPosition != null)
        {
            sb.Append("font-variant-position");
            sb.Append(":");
            sb.Append(fontVariantPosition);
            sb.Append(separator);
        }

        if (fontWeight != null)
        {
            sb.Append("font-weight");
            sb.Append(":");
            sb.Append(fontWeight);
            sb.Append(separator);
        }

        if (grid != null)
        {
            sb.Append("grid");
            sb.Append(":");
            sb.Append(grid);
            sb.Append(separator);
        }

        if (gridArea != null)
        {
            sb.Append("grid-area");
            sb.Append(":");
            sb.Append(gridArea);
            sb.Append(separator);
        }

        if (gridAutoColumns != null)
        {
            sb.Append("grid-auto-columns");
            sb.Append(":");
            sb.Append(gridAutoColumns);
            sb.Append(separator);
        }

        if (gridAutoFlow != null)
        {
            sb.Append("grid-auto-flow");
            sb.Append(":");
            sb.Append(gridAutoFlow);
            sb.Append(separator);
        }

        if (gridAutoPosition != null)
        {
            sb.Append("grid-auto-position");
            sb.Append(":");
            sb.Append(gridAutoPosition);
            sb.Append(separator);
        }

        if (gridAutoRows != null)
        {
            sb.Append("grid-auto-rows");
            sb.Append(":");
            sb.Append(gridAutoRows);
            sb.Append(separator);
        }

        if (gridColumn != null)
        {
            sb.Append("grid-column");
            sb.Append(":");
            sb.Append(gridColumn);
            sb.Append(separator);
        }

        if (gridColumnStart != null)
        {
            sb.Append("grid-column-start");
            sb.Append(":");
            sb.Append(gridColumnStart);
            sb.Append(separator);
        }

        if (gridColumnEnd != null)
        {
            sb.Append("grid-column-end");
            sb.Append(":");
            sb.Append(gridColumnEnd);
            sb.Append(separator);
        }

        if (gridRow != null)
        {
            sb.Append("grid-row");
            sb.Append(":");
            sb.Append(gridRow);
            sb.Append(separator);
        }

        if (gridRowStart != null)
        {
            sb.Append("grid-row-start");
            sb.Append(":");
            sb.Append(gridRowStart);
            sb.Append(separator);
        }

        if (gridRowEnd != null)
        {
            sb.Append("grid-row-end");
            sb.Append(":");
            sb.Append(gridRowEnd);
            sb.Append(separator);
        }

        if (gridTemplate != null)
        {
            sb.Append("grid-template");
            sb.Append(":");
            sb.Append(gridTemplate);
            sb.Append(separator);
        }

        if (gridTemplateAreas != null)
        {
            sb.Append("grid-template-areas");
            sb.Append(":");
            sb.Append(gridTemplateAreas);
            sb.Append(separator);
        }

        if (gridTemplateRows != null)
        {
            sb.Append("grid-template-rows");
            sb.Append(":");
            sb.Append(gridTemplateRows);
            sb.Append(separator);
        }

        if (gridTemplateColumns != null)
        {
            sb.Append("grid-template-columns");
            sb.Append(":");
            sb.Append(gridTemplateColumns);
            sb.Append(separator);
        }

        if (height != null)
        {
            sb.Append("height");
            sb.Append(":");
            sb.Append(height);
            sb.Append(separator);
        }

        if (hyphens != null)
        {
            sb.Append("hyphens");
            sb.Append(":");
            sb.Append(hyphens);
            sb.Append(separator);
        }

        if (icon != null)
        {
            sb.Append("icon");
            sb.Append(":");
            sb.Append(icon);
            sb.Append(separator);
        }

        if (imageRendering != null)
        {
            sb.Append("image-rendering");
            sb.Append(":");
            sb.Append(imageRendering);
            sb.Append(separator);
        }

        if (imageResolution != null)
        {
            sb.Append("image-resolution");
            sb.Append(":");
            sb.Append(imageResolution);
            sb.Append(separator);
        }

        if (imageOrientation != null)
        {
            sb.Append("image-orientation");
            sb.Append(":");
            sb.Append(imageOrientation);
            sb.Append(separator);
        }

        if (imeMode != null)
        {
            sb.Append("ime-mode");
            sb.Append(":");
            sb.Append(imeMode);
            sb.Append(separator);
        }

        if (justifyContent != null)
        {
            sb.Append("justify-content");
            sb.Append(":");
            sb.Append(justifyContent);
            sb.Append(separator);
        }

        if (left != null)
        {
            sb.Append("left");
            sb.Append(":");
            sb.Append(left);
            sb.Append(separator);
        }

        if (letterSpacing != null)
        {
            sb.Append("letter-spacing");
            sb.Append(":");
            sb.Append(letterSpacing);
            sb.Append(separator);
        }

        if (lightingColor != null)
        {
            sb.Append("lighting-color");
            sb.Append(":");
            sb.Append(lightingColor);
            sb.Append(separator);
        }

        if (lineHeight != null)
        {
            sb.Append("line-height");
            sb.Append(":");
            sb.Append(lineHeight);
            sb.Append(separator);
        }

        if (listStyle != null)
        {
            sb.Append("list-style");
            sb.Append(":");
            sb.Append(listStyle);
            sb.Append(separator);
        }

        if (listStyleImage != null)
        {
            sb.Append("list-style-image");
            sb.Append(":");
            sb.Append(listStyleImage);
            sb.Append(separator);
        }

        if (listStylePosition != null)
        {
            sb.Append("list-style-position");
            sb.Append(":");
            sb.Append(listStylePosition);
            sb.Append(separator);
        }

        if (listStyleType != null)
        {
            sb.Append("list-style-type");
            sb.Append(":");
            sb.Append(listStyleType);
            sb.Append(separator);
        }

        if (margin != null)
        {
            sb.Append("margin");
            sb.Append(":");
            sb.Append(margin);
            sb.Append(separator);
        }

        if (marginBottom != null)
        {
            sb.Append("margin-bottom");
            sb.Append(":");
            sb.Append(marginBottom);
            sb.Append(separator);
        }

        if (marginLeft != null)
        {
            sb.Append("margin-left");
            sb.Append(":");
            sb.Append(marginLeft);
            sb.Append(separator);
        }

        if (marginRight != null)
        {
            sb.Append("margin-right");
            sb.Append(":");
            sb.Append(marginRight);
            sb.Append(separator);
        }

        if (marginTop != null)
        {
            sb.Append("margin-top");
            sb.Append(":");
            sb.Append(marginTop);
            sb.Append(separator);
        }

        if (marks != null)
        {
            sb.Append("marks");
            sb.Append(":");
            sb.Append(marks);
            sb.Append(separator);
        }

        if (mask != null)
        {
            sb.Append("mask");
            sb.Append(":");
            sb.Append(mask);
            sb.Append(separator);
        }

        if (maskType != null)
        {
            sb.Append("mask-type");
            sb.Append(":");
            sb.Append(maskType);
            sb.Append(separator);
        }

        if (maxHeight != null)
        {
            sb.Append("max-height");
            sb.Append(":");
            sb.Append(maxHeight);
            sb.Append(separator);
        }

        if (maxWidth != null)
        {
            sb.Append("max-width");
            sb.Append(":");
            sb.Append(maxWidth);
            sb.Append(separator);
        }

        if (minHeight != null)
        {
            sb.Append("min-height");
            sb.Append(":");
            sb.Append(minHeight);
            sb.Append(separator);
        }

        if (minWidth != null)
        {
            sb.Append("min-width");
            sb.Append(":");
            sb.Append(minWidth);
            sb.Append(separator);
        }

        if (mixBlendMode != null)
        {
            sb.Append("mix-blend-mode");
            sb.Append(":");
            sb.Append(mixBlendMode);
            sb.Append(separator);
        }

        if (navDown != null)
        {
            sb.Append("nav-down");
            sb.Append(":");
            sb.Append(navDown);
            sb.Append(separator);
        }

        if (navIndex != null)
        {
            sb.Append("nav-index");
            sb.Append(":");
            sb.Append(navIndex);
            sb.Append(separator);
        }

        if (navLeft != null)
        {
            sb.Append("nav-left");
            sb.Append(":");
            sb.Append(navLeft);
            sb.Append(separator);
        }

        if (navRight != null)
        {
            sb.Append("nav-right");
            sb.Append(":");
            sb.Append(navRight);
            sb.Append(separator);
        }

        if (navUp != null)
        {
            sb.Append("nav-up");
            sb.Append(":");
            sb.Append(navUp);
            sb.Append(separator);
        }

        if (objectFit != null)
        {
            sb.Append("object-fit");
            sb.Append(":");
            sb.Append(objectFit);
            sb.Append(separator);
        }

        if (objectPosition != null)
        {
            sb.Append("object-position");
            sb.Append(":");
            sb.Append(objectPosition);
            sb.Append(separator);
        }

        if (opacity != null)
        {
            sb.Append("opacity");
            sb.Append(":");
            sb.Append(opacity);
            sb.Append(separator);
        }

        if (order != null)
        {
            sb.Append("order");
            sb.Append(":");
            sb.Append(order);
            sb.Append(separator);
        }

        if (orphans != null)
        {
            sb.Append("orphans");
            sb.Append(":");
            sb.Append(orphans);
            sb.Append(separator);
        }

        if (outline != null)
        {
            sb.Append("outline");
            sb.Append(":");
            sb.Append(outline);
            sb.Append(separator);
        }

        if (outlineColor != null)
        {
            sb.Append("outline-color");
            sb.Append(":");
            sb.Append(outlineColor);
            sb.Append(separator);
        }

        if (outlineOffset != null)
        {
            sb.Append("outline-offset");
            sb.Append(":");
            sb.Append(outlineOffset);
            sb.Append(separator);
        }

        if (outlineStyle != null)
        {
            sb.Append("outline-style");
            sb.Append(":");
            sb.Append(outlineStyle);
            sb.Append(separator);
        }

        if (outlineWidth != null)
        {
            sb.Append("outline-width");
            sb.Append(":");
            sb.Append(outlineWidth);
            sb.Append(separator);
        }

        if (overflow != null)
        {
            sb.Append("overflow");
            sb.Append(":");
            sb.Append(overflow);
            sb.Append(separator);
        }

        if (overflowWrap != null)
        {
            sb.Append("overflow-wrap");
            sb.Append(":");
            sb.Append(overflowWrap);
            sb.Append(separator);
        }

        if (overflowX != null)
        {
            sb.Append("overflow-x");
            sb.Append(":");
            sb.Append(overflowX);
            sb.Append(separator);
        }

        if (overflowY != null)
        {
            sb.Append("overflow-y");
            sb.Append(":");
            sb.Append(overflowY);
            sb.Append(separator);
        }

        if (overflowClipBox != null)
        {
            sb.Append("overflow-clip-box");
            sb.Append(":");
            sb.Append(overflowClipBox);
            sb.Append(separator);
        }

        if (padding != null)
        {
            sb.Append("padding");
            sb.Append(":");
            sb.Append(padding);
            sb.Append(separator);
        }

        if (paddingBottom != null)
        {
            sb.Append("padding-bottom");
            sb.Append(":");
            sb.Append(paddingBottom);
            sb.Append(separator);
        }

        if (paddingLeft != null)
        {
            sb.Append("padding-left");
            sb.Append(":");
            sb.Append(paddingLeft);
            sb.Append(separator);
        }

        if (paddingRight != null)
        {
            sb.Append("padding-right");
            sb.Append(":");
            sb.Append(paddingRight);
            sb.Append(separator);
        }

        if (paddingTop != null)
        {
            sb.Append("padding-top");
            sb.Append(":");
            sb.Append(paddingTop);
            sb.Append(separator);
        }

        if (pageBreakAfter != null)
        {
            sb.Append("page-break-after");
            sb.Append(":");
            sb.Append(pageBreakAfter);
            sb.Append(separator);
        }

        if (pageBreakBefore != null)
        {
            sb.Append("page-break-before");
            sb.Append(":");
            sb.Append(pageBreakBefore);
            sb.Append(separator);
        }

        if (pageBreakInside != null)
        {
            sb.Append("page-break-inside");
            sb.Append(":");
            sb.Append(pageBreakInside);
            sb.Append(separator);
        }

        if (perspective != null)
        {
            sb.Append("perspective");
            sb.Append(":");
            sb.Append(perspective);
            sb.Append(separator);
        }

        if (perspectiveOrigin != null)
        {
            sb.Append("perspective-origin");
            sb.Append(":");
            sb.Append(perspectiveOrigin);
            sb.Append(separator);
        }

        if (pointerEvents != null)
        {
            sb.Append("pointer-events");
            sb.Append(":");
            sb.Append(pointerEvents);
            sb.Append(separator);
        }

        if (position != null)
        {
            sb.Append("position");
            sb.Append(":");
            sb.Append(position);
            sb.Append(separator);
        }

        if (quotes != null)
        {
            sb.Append("quotes");
            sb.Append(":");
            sb.Append(quotes);
            sb.Append(separator);
        }

        if (resize != null)
        {
            sb.Append("resize");
            sb.Append(":");
            sb.Append(resize);
            sb.Append(separator);
        }

        if (right != null)
        {
            sb.Append("right");
            sb.Append(":");
            sb.Append(right);
            sb.Append(separator);
        }

        if (tableLayout != null)
        {
            sb.Append("table-layout");
            sb.Append(":");
            sb.Append(tableLayout);
            sb.Append(separator);
        }

        if (tabSize != null)
        {
            sb.Append("tab-size");
            sb.Append(":");
            sb.Append(tabSize);
            sb.Append(separator);
        }

        if (textAlign != null)
        {
            sb.Append("text-align");
            sb.Append(":");
            sb.Append(textAlign);
            sb.Append(separator);
        }

        if (textAlignLast != null)
        {
            sb.Append("text-align-last");
            sb.Append(":");
            sb.Append(textAlignLast);
            sb.Append(separator);
        }

        if (textCombineHorizontal != null)
        {
            sb.Append("text-combine-horizontal");
            sb.Append(":");
            sb.Append(textCombineHorizontal);
            sb.Append(separator);
        }

        if (textDecoration != null)
        {
            sb.Append("text-decoration");
            sb.Append(":");
            sb.Append(textDecoration);
            sb.Append(separator);
        }

        if (textDecorationColor != null)
        {
            sb.Append("text-decoration-color");
            sb.Append(":");
            sb.Append(textDecorationColor);
            sb.Append(separator);
        }

        if (textDecorationLine != null)
        {
            sb.Append("text-decoration-line");
            sb.Append(":");
            sb.Append(textDecorationLine);
            sb.Append(separator);
        }

        if (textDecorationStyle != null)
        {
            sb.Append("text-decoration-style");
            sb.Append(":");
            sb.Append(textDecorationStyle);
            sb.Append(separator);
        }

        if (textIndent != null)
        {
            sb.Append("text-indent");
            sb.Append(":");
            sb.Append(textIndent);
            sb.Append(separator);
        }

        if (textOrientation != null)
        {
            sb.Append("text-orientation");
            sb.Append(":");
            sb.Append(textOrientation);
            sb.Append(separator);
        }

        if (textOverflow != null)
        {
            sb.Append("text-overflow");
            sb.Append(":");
            sb.Append(textOverflow);
            sb.Append(separator);
        }

        if (textRendering != null)
        {
            sb.Append("text-rendering");
            sb.Append(":");
            sb.Append(textRendering);
            sb.Append(separator);
        }

        if (textShadow != null)
        {
            sb.Append("text-shadow");
            sb.Append(":");
            sb.Append(textShadow);
            sb.Append(separator);
        }

        if (textTransform != null)
        {
            sb.Append("text-transform");
            sb.Append(":");
            sb.Append(textTransform);
            sb.Append(separator);
        }

        if (textUnderlinePosition != null)
        {
            sb.Append("text-underline-position");
            sb.Append(":");
            sb.Append(textUnderlinePosition);
            sb.Append(separator);
        }

        if (top != null)
        {
            sb.Append("top");
            sb.Append(":");
            sb.Append(top);
            sb.Append(separator);
        }

        if (touchAction != null)
        {
            sb.Append("touch-action");
            sb.Append(":");
            sb.Append(touchAction);
            sb.Append(separator);
        }

        if (transform != null)
        {
            sb.Append("transform");
            sb.Append(":");
            sb.Append(transform);
            sb.Append(separator);
        }

        if (transformOrigin != null)
        {
            sb.Append("transform-origin");
            sb.Append(":");
            sb.Append(transformOrigin);
            sb.Append(separator);
        }

        if (transformStyle != null)
        {
            sb.Append("transform-style");
            sb.Append(":");
            sb.Append(transformStyle);
            sb.Append(separator);
        }

        if (transition != null)
        {
            sb.Append("transition");
            sb.Append(":");
            sb.Append(transition);
            sb.Append(separator);
        }

        if (transitionDelay != null)
        {
            sb.Append("transition-delay");
            sb.Append(":");
            sb.Append(transitionDelay);
            sb.Append(separator);
        }

        if (transitionDuration != null)
        {
            sb.Append("transition-duration");
            sb.Append(":");
            sb.Append(transitionDuration);
            sb.Append(separator);
        }

        if (transitionProperty != null)
        {
            sb.Append("transition-property");
            sb.Append(":");
            sb.Append(transitionProperty);
            sb.Append(separator);
        }

        if (transitionTimingFunction != null)
        {
            sb.Append("transition-timing-function");
            sb.Append(":");
            sb.Append(transitionTimingFunction);
            sb.Append(separator);
        }

        if (unicodeBidi != null)
        {
            sb.Append("unicode-bidi");
            sb.Append(":");
            sb.Append(unicodeBidi);
            sb.Append(separator);
        }

        if (unicodeRange != null)
        {
            sb.Append("unicode-range");
            sb.Append(":");
            sb.Append(unicodeRange);
            sb.Append(separator);
        }

        if (verticalAlign != null)
        {
            sb.Append("vertical-align");
            sb.Append(":");
            sb.Append(verticalAlign);
            sb.Append(separator);
        }

        if (visibility != null)
        {
            sb.Append("visibility");
            sb.Append(":");
            sb.Append(visibility);
            sb.Append(separator);
        }

        if (whiteSpace != null)
        {
            sb.Append("white-space");
            sb.Append(":");
            sb.Append(whiteSpace);
            sb.Append(separator);
        }

        if (widows != null)
        {
            sb.Append("widows");
            sb.Append(":");
            sb.Append(widows);
            sb.Append(separator);
        }

        if (width != null)
        {
            sb.Append("width");
            sb.Append(":");
            sb.Append(width);
            sb.Append(separator);
        }

        if (willChange != null)
        {
            sb.Append("will-change");
            sb.Append(":");
            sb.Append(willChange);
            sb.Append(separator);
        }

        if (wordBreak != null)
        {
            sb.Append("word-break");
            sb.Append(":");
            sb.Append(wordBreak);
            sb.Append(separator);
        }

        if (wordSpacing != null)
        {
            sb.Append("word-spacing");
            sb.Append(":");
            sb.Append(wordSpacing);
            sb.Append(separator);
        }

        if (wordWrap != null)
        {
            sb.Append("word-wrap");
            sb.Append(":");
            sb.Append(wordWrap);
            sb.Append(separator);
        }

        if (writingMode != null)
        {
            sb.Append("writing-mode");
            sb.Append(":");
            sb.Append(writingMode);
            sb.Append(separator);
        }

        if (zIndex != null)
        {
            sb.Append("z-index");
            sb.Append(":");
            sb.Append(zIndex);
            sb.Append(separator);
        }

        if (webkitBackgroundClip != null)
        {
            sb.Append("-webkit-background-clip");
            sb.Append(":");
            sb.Append(webkitBackgroundClip);
            sb.Append(separator);
        }

        if (webkitTextFillColor != null)
        {
            sb.Append("-webkit-text-fill-color");
            sb.Append(":");
            sb.Append(webkitTextFillColor);
            sb.Append(separator);
        }
        if (webkitFontSmoothing != null)
        {
            sb.Append("-webkit-font-smoothing");
            sb.Append(":");
            sb.Append(webkitFontSmoothing);
            sb.Append(separator);
        }

        if (mozOsxFontSmoothing != null)
        {
            sb.Append("-moz-osx-font-smoothing");
            sb.Append(":");
            sb.Append(mozOsxFontSmoothing);
            sb.Append(separator);
        }

        if (sb.Length == 0)
        {
            return null;
        }

        return sb.ToString();
    }
}