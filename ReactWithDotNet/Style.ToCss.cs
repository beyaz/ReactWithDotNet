using System.Text;

namespace ReactWithDotNet;

partial class Style
{
    public override string ToString()
    {
        return ToCss();
    }

    public string ToCss()
    {
        return ToCss(isImportant: false);
    }

   
    public string ToCssWithImportant()
    {
        return ToCss(isImportant: true);
    }


    string ToCss(bool isImportant)
    {
        var sb = new StringBuilder();

        var seperator = isImportant ? " !important;" : ";";

        if (alignContent != null)
        {
            sb.Append("align-content");
            sb.Append(": ");
            sb.Append(alignContent);
            sb.Append(seperator);
        }

        if (alignItems != null)
        {
            sb.Append("align-items");
            sb.Append(": ");
            sb.Append(alignItems);
            sb.Append(seperator);
        }

        if (alignSelf != null)
        {
            sb.Append("align-self");
            sb.Append(": ");
            sb.Append(alignSelf);
            sb.Append(seperator);
        }

        if (all != null)
        {
            sb.Append("all");
            sb.Append(": ");
            sb.Append(all);
            sb.Append(seperator);
        }

        if (animation != null)
        {
            sb.Append("animation");
            sb.Append(": ");
            sb.Append(animation);
            sb.Append(seperator);
        }

        if (animationDelay != null)
        {
            sb.Append("animation-delay");
            sb.Append(": ");
            sb.Append(animationDelay);
            sb.Append(seperator);
        }

        if (animationDirection != null)
        {
            sb.Append("animation-direction");
            sb.Append(": ");
            sb.Append(animationDirection);
            sb.Append(seperator);
        }

        if (animationDuration != null)
        {
            sb.Append("animation-duration");
            sb.Append(": ");
            sb.Append(animationDuration);
            sb.Append(seperator);
        }

        if (animationFillMode != null)
        {
            sb.Append("animation-fill-mode");
            sb.Append(": ");
            sb.Append(animationFillMode);
            sb.Append(seperator);
        }

        if (animationIterationCount != null)
        {
            sb.Append("animation-iteration-count");
            sb.Append(": ");
            sb.Append(animationIterationCount);
            sb.Append(seperator);
        }

        if (animationName != null)
        {
            sb.Append("animation-name");
            sb.Append(": ");
            sb.Append(animationName);
            sb.Append(seperator);
        }

        if (animationPlayState != null)
        {
            sb.Append("animation-play-state");
            sb.Append(": ");
            sb.Append(animationPlayState);
            sb.Append(seperator);
        }

        if (animationTimingFunction != null)
        {
            sb.Append("animation-timing-function");
            sb.Append(": ");
            sb.Append(animationTimingFunction);
            sb.Append(seperator);
        }
		
        if (backdropFilter != null)
        {
            sb.Append("backdrop-filter");
            sb.Append(": ");
            sb.Append(backdropFilter);
            sb.Append(seperator);
        }

        if (backfaceVisibility != null)
        {
            sb.Append("backface-visibility");
            sb.Append(": ");
            sb.Append(backfaceVisibility);
            sb.Append(seperator);
        }

        if (background != null)
        {
            sb.Append("background");
            sb.Append(": ");
            sb.Append(background);
            sb.Append(seperator);
        }

        if (backgroundAttachment != null)
        {
            sb.Append("background-attachment");
            sb.Append(": ");
            sb.Append(backgroundAttachment);
            sb.Append(seperator);
        }

        if (backgroundBlendMode != null)
        {
            sb.Append("background-blend-mode");
            sb.Append(": ");
            sb.Append(backgroundBlendMode);
            sb.Append(seperator);
        }

        if (backgroundClip != null)
        {
            sb.Append("background-clip");
            sb.Append(": ");
            sb.Append(backgroundClip);
            sb.Append(seperator);
        }

        if (backgroundColor != null)
        {
            sb.Append("background-color");
            sb.Append(": ");
            sb.Append(backgroundColor);
            sb.Append(seperator);
        }

        if (backgroundImage != null)
        {
            sb.Append("background-image");
            sb.Append(": ");
            sb.Append(backgroundImage);
            sb.Append(seperator);
        }

        if (backgroundOrigin != null)
        {
            sb.Append("background-origin");
            sb.Append(": ");
            sb.Append(backgroundOrigin);
            sb.Append(seperator);
        }

        if (backgroundPosition != null)
        {
            sb.Append("background-position");
            sb.Append(": ");
            sb.Append(backgroundPosition);
            sb.Append(seperator);
        }

        if (backgroundRepeat != null)
        {
            sb.Append("background-repeat");
            sb.Append(": ");
            sb.Append(backgroundRepeat);
            sb.Append(seperator);
        }

        if (backgroundSize != null)
        {
            sb.Append("background-size");
            sb.Append(": ");
            sb.Append(backgroundSize);
            sb.Append(seperator);
        }

        if (border != null)
        {
            sb.Append("border");
            sb.Append(": ");
            sb.Append(border);
            sb.Append(seperator);
        }

        if (borderBottom != null)
        {
            sb.Append("border-bottom");
            sb.Append(": ");
            sb.Append(borderBottom);
            sb.Append(seperator);
        }

        if (borderBottomColor != null)
        {
            sb.Append("border-bottom-color");
            sb.Append(": ");
            sb.Append(borderBottomColor);
            sb.Append(seperator);
        }

        if (borderBottomLeftRadius != null)
        {
            sb.Append("border-bottom-left-radius");
            sb.Append(": ");
            sb.Append(borderBottomLeftRadius);
            sb.Append(seperator);
        }

        if (borderBottomRightRadius != null)
        {
            sb.Append("border-bottom-right-radius");
            sb.Append(": ");
            sb.Append(borderBottomRightRadius);
            sb.Append(seperator);
        }

        if (borderBottomStyle != null)
        {
            sb.Append("border-bottom-style");
            sb.Append(": ");
            sb.Append(borderBottomStyle);
            sb.Append(seperator);
        }

        if (borderBottomWidth != null)
        {
            sb.Append("border-bottom-width");
            sb.Append(": ");
            sb.Append(borderBottomWidth);
            sb.Append(seperator);
        }

        if (borderCollapse != null)
        {
            sb.Append("border-collapse");
            sb.Append(": ");
            sb.Append(borderCollapse);
            sb.Append(seperator);
        }

        if (borderColor != null)
        {
            sb.Append("border-color");
            sb.Append(": ");
            sb.Append(borderColor);
            sb.Append(seperator);
        }

        if (borderImage != null)
        {
            sb.Append("border-image");
            sb.Append(": ");
            sb.Append(borderImage);
            sb.Append(seperator);
        }

        if (borderImageOutset != null)
        {
            sb.Append("border-image-outset");
            sb.Append(": ");
            sb.Append(borderImageOutset);
            sb.Append(seperator);
        }

        if (borderImageRepeat != null)
        {
            sb.Append("border-image-repeat");
            sb.Append(": ");
            sb.Append(borderImageRepeat);
            sb.Append(seperator);
        }

        if (borderImageSlice != null)
        {
            sb.Append("border-image-slice");
            sb.Append(": ");
            sb.Append(borderImageSlice);
            sb.Append(seperator);
        }

        if (borderImageSource != null)
        {
            sb.Append("border-image-source");
            sb.Append(": ");
            sb.Append(borderImageSource);
            sb.Append(seperator);
        }

        if (borderImageWidth != null)
        {
            sb.Append("border-image-width");
            sb.Append(": ");
            sb.Append(borderImageWidth);
            sb.Append(seperator);
        }

        if (borderLeft != null)
        {
            sb.Append("border-left");
            sb.Append(": ");
            sb.Append(borderLeft);
            sb.Append(seperator);
        }

        if (borderLeftColor != null)
        {
            sb.Append("border-left-color");
            sb.Append(": ");
            sb.Append(borderLeftColor);
            sb.Append(seperator);
        }

        if (borderLeftStyle != null)
        {
            sb.Append("border-left-style");
            sb.Append(": ");
            sb.Append(borderLeftStyle);
            sb.Append(seperator);
        }

        if (borderLeftWidth != null)
        {
            sb.Append("border-left-width");
            sb.Append(": ");
            sb.Append(borderLeftWidth);
            sb.Append(seperator);
        }

        if (borderRadius != null)
        {
            sb.Append("border-radius");
            sb.Append(": ");
            sb.Append(borderRadius);
            sb.Append(seperator);
        }

        if (borderRight != null)
        {
            sb.Append("border-right");
            sb.Append(": ");
            sb.Append(borderRight);
            sb.Append(seperator);
        }

        if (borderRightColor != null)
        {
            sb.Append("border-right-color");
            sb.Append(": ");
            sb.Append(borderRightColor);
            sb.Append(seperator);
        }

        if (borderRightStyle != null)
        {
            sb.Append("border-right-style");
            sb.Append(": ");
            sb.Append(borderRightStyle);
            sb.Append(seperator);
        }

        if (borderRightWidth != null)
        {
            sb.Append("border-right-width");
            sb.Append(": ");
            sb.Append(borderRightWidth);
            sb.Append(seperator);
        }

        if (borderSpacing != null)
        {
            sb.Append("border-spacing");
            sb.Append(": ");
            sb.Append(borderSpacing);
            sb.Append(seperator);
        }

        if (borderStyle != null)
        {
            sb.Append("border-style");
            sb.Append(": ");
            sb.Append(borderStyle);
            sb.Append(seperator);
        }

        if (borderTop != null)
        {
            sb.Append("border-top");
            sb.Append(": ");
            sb.Append(borderTop);
            sb.Append(seperator);
        }

        if (borderTopColor != null)
        {
            sb.Append("border-top-color");
            sb.Append(": ");
            sb.Append(borderTopColor);
            sb.Append(seperator);
        }

        if (borderTopLeftRadius != null)
        {
            sb.Append("border-top-left-radius");
            sb.Append(": ");
            sb.Append(borderTopLeftRadius);
            sb.Append(seperator);
        }

        if (borderTopRightRadius != null)
        {
            sb.Append("border-top-right-radius");
            sb.Append(": ");
            sb.Append(borderTopRightRadius);
            sb.Append(seperator);
        }

        if (borderTopStyle != null)
        {
            sb.Append("border-top-style");
            sb.Append(": ");
            sb.Append(borderTopStyle);
            sb.Append(seperator);
        }

        if (borderTopWidth != null)
        {
            sb.Append("border-top-width");
            sb.Append(": ");
            sb.Append(borderTopWidth);
            sb.Append(seperator);
        }

        if (borderWidth != null)
        {
            sb.Append("border-width");
            sb.Append(": ");
            sb.Append(borderWidth);
            sb.Append(seperator);
        }

        if (bottom != null)
        {
            sb.Append("bottom");
            sb.Append(": ");
            sb.Append(bottom);
            sb.Append(seperator);
        }

        if (boxDecorationBreak != null)
        {
            sb.Append("box-decoration-break");
            sb.Append(": ");
            sb.Append(boxDecorationBreak);
            sb.Append(seperator);
        }

        if (boxShadow != null)
        {
            sb.Append("box-shadow");
            sb.Append(": ");
            sb.Append(boxShadow);
            sb.Append(seperator);
        }

        if (boxSizing != null)
        {
            sb.Append("box-sizing");
            sb.Append(": ");
            sb.Append(boxSizing);
            sb.Append(seperator);
        }

        if (captionSide != null)
        {
            sb.Append("caption-side");
            sb.Append(": ");
            sb.Append(captionSide);
            sb.Append(seperator);
        }

        if (clear != null)
        {
            sb.Append("clear");
            sb.Append(": ");
            sb.Append(clear);
            sb.Append(seperator);
        }

        if (clip != null)
        {
            sb.Append("clip");
            sb.Append(": ");
            sb.Append(clip);
            sb.Append(seperator);
        }

        if (clipPath != null)
        {
            sb.Append("clip-path");
            sb.Append(": ");
            sb.Append(clipPath);
            sb.Append(seperator);
        }

        if (color != null)
        {
            sb.Append("color");
            sb.Append(": ");
            sb.Append(color);
            sb.Append(seperator);
        }

        if (columns != null)
        {
            sb.Append("columns");
            sb.Append(": ");
            sb.Append(columns);
            sb.Append(seperator);
        }

        if (columnCount != null)
        {
            sb.Append("column-count");
            sb.Append(": ");
            sb.Append(columnCount);
            sb.Append(seperator);
        }

        if (columnFill != null)
        {
            sb.Append("column-fill");
            sb.Append(": ");
            sb.Append(columnFill);
            sb.Append(seperator);
        }

        if (columnGap != null)
        {
            sb.Append("column-gap");
            sb.Append(": ");
            sb.Append(columnGap);
            sb.Append(seperator);
        }

        if (rowGap != null)
        {
            sb.Append("row-gap");
            sb.Append(": ");
            sb.Append(rowGap);
            sb.Append(seperator);
        }

        if (gap != null)
        {
            sb.Append("gap");
            sb.Append(": ");
            sb.Append(gap);
            sb.Append(seperator);
        }

        if (columnRule != null)
        {
            sb.Append("column-rule");
            sb.Append(": ");
            sb.Append(columnRule);
            sb.Append(seperator);
        }

        if (columnRuleColor != null)
        {
            sb.Append("column-rule-color");
            sb.Append(": ");
            sb.Append(columnRuleColor);
            sb.Append(seperator);
        }

        if (columnRuleStyle != null)
        {
            sb.Append("column-rule-style");
            sb.Append(": ");
            sb.Append(columnRuleStyle);
            sb.Append(seperator);
        }

        if (columnRuleWidth != null)
        {
            sb.Append("column-rule-width");
            sb.Append(": ");
            sb.Append(columnRuleWidth);
            sb.Append(seperator);
        }

        if (columnSpan != null)
        {
            sb.Append("column-span");
            sb.Append(": ");
            sb.Append(columnSpan);
            sb.Append(seperator);
        }

        if (columnWidth != null)
        {
            sb.Append("column-width");
            sb.Append(": ");
            sb.Append(columnWidth);
            sb.Append(seperator);
        }

        if (content != null)
        {
            sb.Append("content");
            sb.Append(": ");
            sb.Append("'" + content + "'");
            sb.Append(seperator);
        }

        if (contentVisibility != null)
        {
            sb.Append("content-visibility");
            sb.Append(": ");
            sb.Append(contentVisibility);
            sb.Append(seperator);
        }

        if (counterIncrement != null)
        {
            sb.Append("counter-increment");
            sb.Append(": ");
            sb.Append(counterIncrement);
            sb.Append(seperator);
        }

        if (counterReset != null)
        {
            sb.Append("counter-reset");
            sb.Append(": ");
            sb.Append(counterReset);
            sb.Append(seperator);
        }

        if (cssFloat != null)
        {
            sb.Append("float");
            sb.Append(": ");
            sb.Append(cssFloat);
            sb.Append(seperator);
        }

        if (cssText != null)
        {
            sb.Append("text");
            sb.Append(": ");
            sb.Append(cssText);
            sb.Append(seperator);
        }

        if (cursor != null)
        {
            sb.Append("cursor");
            sb.Append(": ");
            sb.Append(cursor);
            sb.Append(seperator);
        }

        if (direction != null)
        {
            sb.Append("direction");
            sb.Append(": ");
            sb.Append(direction);
            sb.Append(seperator);
        }

        if (display != null)
        {
            sb.Append("display");
            sb.Append(": ");
            sb.Append(display);
            sb.Append(seperator);
        }

        if (dominantBaseline != null)
        {
            sb.Append("dominant-baseline");
            sb.Append(": ");
            sb.Append(dominantBaseline);
            sb.Append(seperator);
        }

        if (emptyCells != null)
        {
            sb.Append("empty-cells");
            sb.Append(": ");
            sb.Append(emptyCells);
            sb.Append(seperator);
        }

        if (fill != null)
        {
            sb.Append("fill");
            sb.Append(": ");
            sb.Append(fill);
            sb.Append(seperator);
        }

        if (fillOpacity != null)
        {
            sb.Append("fill-opacity");
            sb.Append(": ");
            sb.Append(fillOpacity);
            sb.Append(seperator);
        }

        if (fillRule != null)
        {
            sb.Append("fill-rule");
            sb.Append(": ");
            sb.Append(fillRule);
            sb.Append(seperator);
        }

        if (filter != null)
        {
            sb.Append("filter");
            sb.Append(": ");
            sb.Append(filter);
            sb.Append(seperator);
        }

        if (flex != null)
        {
            sb.Append("flex");
            sb.Append(": ");
            sb.Append(flex);
            sb.Append(seperator);
        }

        if (flexBasis != null)
        {
            sb.Append("flex-basis");
            sb.Append(": ");
            sb.Append(flexBasis);
            sb.Append(seperator);
        }

        if (flexDirection != null)
        {
            sb.Append("flex-direction");
            sb.Append(": ");
            sb.Append(flexDirection);
            sb.Append(seperator);
        }

        if (flexFlow != null)
        {
            sb.Append("flex-flow");
            sb.Append(": ");
            sb.Append(flexFlow);
            sb.Append(seperator);
        }

        if (flexGrow != null)
        {
            sb.Append("flex-grow");
            sb.Append(": ");
            sb.Append(flexGrow);
            sb.Append(seperator);
        }

        if (flexShrink != null)
        {
            sb.Append("flex-shrink");
            sb.Append(": ");
            sb.Append(flexShrink);
            sb.Append(seperator);
        }

        if (flexWrap != null)
        {
            sb.Append("flex-wrap");
            sb.Append(": ");
            sb.Append(flexWrap);
            sb.Append(seperator);
        }

        if (floodColor != null)
        {
            sb.Append("flood-color");
            sb.Append(": ");
            sb.Append(floodColor);
            sb.Append(seperator);
        }

        if (floodOpacity != null)
        {
            sb.Append("flood-opacity");
            sb.Append(": ");
            sb.Append(floodOpacity);
            sb.Append(seperator);
        }

        if (font != null)
        {
            sb.Append("font");
            sb.Append(": ");
            sb.Append(font);
            sb.Append(seperator);
        }

        if (fontFamily != null)
        {
            sb.Append("font-family");
            sb.Append(": ");
            sb.Append(fontFamily);
            sb.Append(seperator);
        }

        if (fontFeatureSettings != null)
        {
            sb.Append("font-feature-settings");
            sb.Append(": ");
            sb.Append(fontFeatureSettings);
            sb.Append(seperator);
        }

        if (fontKerning != null)
        {
            sb.Append("font-kerning");
            sb.Append(": ");
            sb.Append(fontKerning);
            sb.Append(seperator);
        }

        if (fontLanguageOverride != null)
        {
            sb.Append("font-language-override");
            sb.Append(": ");
            sb.Append(fontLanguageOverride);
            sb.Append(seperator);
        }

        if (fontSize != null)
        {
            sb.Append("font-size");
            sb.Append(": ");
            sb.Append(fontSize);
            sb.Append(seperator);
        }

        if (fontSizeAdjust != null)
        {
            sb.Append("font-size-adjust");
            sb.Append(": ");
            sb.Append(fontSizeAdjust);
            sb.Append(seperator);
        }

        if (fontStretch != null)
        {
            sb.Append("font-stretch");
            sb.Append(": ");
            sb.Append(fontStretch);
            sb.Append(seperator);
        }

        if (fontStyle != null)
        {
            sb.Append("font-style");
            sb.Append(": ");
            sb.Append(fontStyle);
            sb.Append(seperator);
        }

        if (fontSynthesis != null)
        {
            sb.Append("font-synthesis");
            sb.Append(": ");
            sb.Append(fontSynthesis);
            sb.Append(seperator);
        }

        if (fontVariant != null)
        {
            sb.Append("font-variant");
            sb.Append(": ");
            sb.Append(fontVariant);
            sb.Append(seperator);
        }

        if (fontVariantAlternates != null)
        {
            sb.Append("font-variant-alternates");
            sb.Append(": ");
            sb.Append(fontVariantAlternates);
            sb.Append(seperator);
        }

        if (fontVariantCaps != null)
        {
            sb.Append("font-variant-caps");
            sb.Append(": ");
            sb.Append(fontVariantCaps);
            sb.Append(seperator);
        }

        if (fontVariantEastAsian != null)
        {
            sb.Append("font-variant-east-asian");
            sb.Append(": ");
            sb.Append(fontVariantEastAsian);
            sb.Append(seperator);
        }

        if (fontVariantLigatures != null)
        {
            sb.Append("font-variant-ligatures");
            sb.Append(": ");
            sb.Append(fontVariantLigatures);
            sb.Append(seperator);
        }

        if (fontVariantNumeric != null)
        {
            sb.Append("font-variant-numeric");
            sb.Append(": ");
            sb.Append(fontVariantNumeric);
            sb.Append(seperator);
        }

        if (fontVariantPosition != null)
        {
            sb.Append("font-variant-position");
            sb.Append(": ");
            sb.Append(fontVariantPosition);
            sb.Append(seperator);
        }

        if (fontWeight != null)
        {
            sb.Append("font-weight");
            sb.Append(": ");
            sb.Append(fontWeight);
            sb.Append(seperator);
        }

        if (grid != null)
        {
            sb.Append("grid");
            sb.Append(": ");
            sb.Append(grid);
            sb.Append(seperator);
        }

        if (gridArea != null)
        {
            sb.Append("grid-area");
            sb.Append(": ");
            sb.Append(gridArea);
            sb.Append(seperator);
        }

        if (gridAutoColumns != null)
        {
            sb.Append("grid-auto-columns");
            sb.Append(": ");
            sb.Append(gridAutoColumns);
            sb.Append(seperator);
        }

        if (gridAutoFlow != null)
        {
            sb.Append("grid-auto-flow");
            sb.Append(": ");
            sb.Append(gridAutoFlow);
            sb.Append(seperator);
        }

        if (gridAutoPosition != null)
        {
            sb.Append("grid-auto-position");
            sb.Append(": ");
            sb.Append(gridAutoPosition);
            sb.Append(seperator);
        }

        if (gridAutoRows != null)
        {
            sb.Append("grid-auto-rows");
            sb.Append(": ");
            sb.Append(gridAutoRows);
            sb.Append(seperator);
        }

        if (gridColumn != null)
        {
            sb.Append("grid-column");
            sb.Append(": ");
            sb.Append(gridColumn);
            sb.Append(seperator);
        }

        if (gridColumnStart != null)
        {
            sb.Append("grid-column-start");
            sb.Append(": ");
            sb.Append(gridColumnStart);
            sb.Append(seperator);
        }

        if (gridColumnEnd != null)
        {
            sb.Append("grid-column-end");
            sb.Append(": ");
            sb.Append(gridColumnEnd);
            sb.Append(seperator);
        }

        if (gridRow != null)
        {
            sb.Append("grid-row");
            sb.Append(": ");
            sb.Append(gridRow);
            sb.Append(seperator);
        }

        if (gridRowStart != null)
        {
            sb.Append("grid-row-start");
            sb.Append(": ");
            sb.Append(gridRowStart);
            sb.Append(seperator);
        }

        if (gridRowEnd != null)
        {
            sb.Append("grid-row-end");
            sb.Append(": ");
            sb.Append(gridRowEnd);
            sb.Append(seperator);
        }

        if (gridTemplate != null)
        {
            sb.Append("grid-template");
            sb.Append(": ");
            sb.Append(gridTemplate);
            sb.Append(seperator);
        }

        if (gridTemplateAreas != null)
        {
            sb.Append("grid-template-areas");
            sb.Append(": ");
            sb.Append(gridTemplateAreas);
            sb.Append(seperator);
        }

        if (gridTemplateRows != null)
        {
            sb.Append("grid-template-rows");
            sb.Append(": ");
            sb.Append(gridTemplateRows);
            sb.Append(seperator);
        }

        if (gridTemplateColumns != null)
        {
            sb.Append("grid-template-columns");
            sb.Append(": ");
            sb.Append(gridTemplateColumns);
            sb.Append(seperator);
        }

        if (height != null)
        {
            sb.Append("height");
            sb.Append(": ");
            sb.Append(height);
            sb.Append(seperator);
        }

        if (hyphens != null)
        {
            sb.Append("hyphens");
            sb.Append(": ");
            sb.Append(hyphens);
            sb.Append(seperator);
        }

        if (icon != null)
        {
            sb.Append("icon");
            sb.Append(": ");
            sb.Append(icon);
            sb.Append(seperator);
        }

        if (imageRendering != null)
        {
            sb.Append("image-rendering");
            sb.Append(": ");
            sb.Append(imageRendering);
            sb.Append(seperator);
        }

        if (imageResolution != null)
        {
            sb.Append("image-resolution");
            sb.Append(": ");
            sb.Append(imageResolution);
            sb.Append(seperator);
        }

        if (imageOrientation != null)
        {
            sb.Append("image-orientation");
            sb.Append(": ");
            sb.Append(imageOrientation);
            sb.Append(seperator);
        }

        if (imeMode != null)
        {
            sb.Append("ime-mode");
            sb.Append(": ");
            sb.Append(imeMode);
            sb.Append(seperator);
        }

        if (justifyContent != null)
        {
            sb.Append("justify-content");
            sb.Append(": ");
            sb.Append(justifyContent);
            sb.Append(seperator);
        }

        if (left != null)
        {
            sb.Append("left");
            sb.Append(": ");
            sb.Append(left);
            sb.Append(seperator);
        }

        if (letterSpacing != null)
        {
            sb.Append("letter-spacing");
            sb.Append(": ");
            sb.Append(letterSpacing);
            sb.Append(seperator);
        }

        if (lightingColor != null)
        {
            sb.Append("lighting-color");
            sb.Append(": ");
            sb.Append(lightingColor);
            sb.Append(seperator);
        }

        if (lineHeight != null)
        {
            sb.Append("line-height");
            sb.Append(": ");
            sb.Append(lineHeight);
            sb.Append(seperator);
        }

        if (listStyle != null)
        {
            sb.Append("list-style");
            sb.Append(": ");
            sb.Append(listStyle);
            sb.Append(seperator);
        }

        if (listStyleImage != null)
        {
            sb.Append("list-style-image");
            sb.Append(": ");
            sb.Append(listStyleImage);
            sb.Append(seperator);
        }

        if (listStylePosition != null)
        {
            sb.Append("list-style-position");
            sb.Append(": ");
            sb.Append(listStylePosition);
            sb.Append(seperator);
        }

        if (listStyleType != null)
        {
            sb.Append("list-style-type");
            sb.Append(": ");
            sb.Append(listStyleType);
            sb.Append(seperator);
        }

        if (margin != null)
        {
            sb.Append("margin");
            sb.Append(": ");
            sb.Append(margin);
            sb.Append(seperator);
        }

        if (marginBottom != null)
        {
            sb.Append("margin-bottom");
            sb.Append(": ");
            sb.Append(marginBottom);
            sb.Append(seperator);
        }

        if (marginLeft != null)
        {
            sb.Append("margin-left");
            sb.Append(": ");
            sb.Append(marginLeft);
            sb.Append(seperator);
        }

        if (marginRight != null)
        {
            sb.Append("margin-right");
            sb.Append(": ");
            sb.Append(marginRight);
            sb.Append(seperator);
        }

        if (marginTop != null)
        {
            sb.Append("margin-top");
            sb.Append(": ");
            sb.Append(marginTop);
            sb.Append(seperator);
        }

        if (marks != null)
        {
            sb.Append("marks");
            sb.Append(": ");
            sb.Append(marks);
            sb.Append(seperator);
        }

        if (mask != null)
        {
            sb.Append("mask");
            sb.Append(": ");
            sb.Append(mask);
            sb.Append(seperator);
        }

        if (maskType != null)
        {
            sb.Append("mask-type");
            sb.Append(": ");
            sb.Append(maskType);
            sb.Append(seperator);
        }

        if (maxHeight != null)
        {
            sb.Append("max-height");
            sb.Append(": ");
            sb.Append(maxHeight);
            sb.Append(seperator);
        }

        if (maxWidth != null)
        {
            sb.Append("max-width");
            sb.Append(": ");
            sb.Append(maxWidth);
            sb.Append(seperator);
        }

        if (minHeight != null)
        {
            sb.Append("min-height");
            sb.Append(": ");
            sb.Append(minHeight);
            sb.Append(seperator);
        }

        if (minWidth != null)
        {
            sb.Append("min-width");
            sb.Append(": ");
            sb.Append(minWidth);
            sb.Append(seperator);
        }

        if (mixBlendMode != null)
        {
            sb.Append("mix-blend-mode");
            sb.Append(": ");
            sb.Append(mixBlendMode);
            sb.Append(seperator);
        }

        if (navDown != null)
        {
            sb.Append("nav-down");
            sb.Append(": ");
            sb.Append(navDown);
            sb.Append(seperator);
        }

        if (navIndex != null)
        {
            sb.Append("nav-index");
            sb.Append(": ");
            sb.Append(navIndex);
            sb.Append(seperator);
        }

        if (navLeft != null)
        {
            sb.Append("nav-left");
            sb.Append(": ");
            sb.Append(navLeft);
            sb.Append(seperator);
        }

        if (navRight != null)
        {
            sb.Append("nav-right");
            sb.Append(": ");
            sb.Append(navRight);
            sb.Append(seperator);
        }

        if (navUp != null)
        {
            sb.Append("nav-up");
            sb.Append(": ");
            sb.Append(navUp);
            sb.Append(seperator);
        }

        if (objectFit != null)
        {
            sb.Append("object-fit");
            sb.Append(": ");
            sb.Append(objectFit);
            sb.Append(seperator);
        }

        if (objectPosition != null)
        {
            sb.Append("object-position");
            sb.Append(": ");
            sb.Append(objectPosition);
            sb.Append(seperator);
        }

        if (opacity != null)
        {
            sb.Append("opacity");
            sb.Append(": ");
            sb.Append(opacity);
            sb.Append(seperator);
        }

        if (order != null)
        {
            sb.Append("order");
            sb.Append(": ");
            sb.Append(order);
            sb.Append(seperator);
        }

        if (orphans != null)
        {
            sb.Append("orphans");
            sb.Append(": ");
            sb.Append(orphans);
            sb.Append(seperator);
        }

        if (outline != null)
        {
            sb.Append("outline");
            sb.Append(": ");
            sb.Append(outline);
            sb.Append(seperator);
        }

        if (outlineColor != null)
        {
            sb.Append("outline-color");
            sb.Append(": ");
            sb.Append(outlineColor);
            sb.Append(seperator);
        }

        if (outlineOffset != null)
        {
            sb.Append("outline-offset");
            sb.Append(": ");
            sb.Append(outlineOffset);
            sb.Append(seperator);
        }

        if (outlineStyle != null)
        {
            sb.Append("outline-style");
            sb.Append(": ");
            sb.Append(outlineStyle);
            sb.Append(seperator);
        }

        if (outlineWidth != null)
        {
            sb.Append("outline-width");
            sb.Append(": ");
            sb.Append(outlineWidth);
            sb.Append(seperator);
        }

        if (overflow != null)
        {
            sb.Append("overflow");
            sb.Append(": ");
            sb.Append(overflow);
            sb.Append(seperator);
        }

        if (overflowWrap != null)
        {
            sb.Append("overflow-wrap");
            sb.Append(": ");
            sb.Append(overflowWrap);
            sb.Append(seperator);
        }

        if (overflowX != null)
        {
            sb.Append("overflow-x");
            sb.Append(": ");
            sb.Append(overflowX);
            sb.Append(seperator);
        }

        if (overflowY != null)
        {
            sb.Append("overflow-y");
            sb.Append(": ");
            sb.Append(overflowY);
            sb.Append(seperator);
        }

        if (overflowClipBox != null)
        {
            sb.Append("overflow-clip-box");
            sb.Append(": ");
            sb.Append(overflowClipBox);
            sb.Append(seperator);
        }

        if (padding != null)
        {
            sb.Append("padding");
            sb.Append(": ");
            sb.Append(padding);
            sb.Append(seperator);
        }

        if (paddingBottom != null)
        {
            sb.Append("padding-bottom");
            sb.Append(": ");
            sb.Append(paddingBottom);
            sb.Append(seperator);
        }

        if (paddingLeft != null)
        {
            sb.Append("padding-left");
            sb.Append(": ");
            sb.Append(paddingLeft);
            sb.Append(seperator);
        }

        if (paddingRight != null)
        {
            sb.Append("padding-right");
            sb.Append(": ");
            sb.Append(paddingRight);
            sb.Append(seperator);
        }

        if (paddingTop != null)
        {
            sb.Append("padding-top");
            sb.Append(": ");
            sb.Append(paddingTop);
            sb.Append(seperator);
        }

        if (pageBreakAfter != null)
        {
            sb.Append("page-break-after");
            sb.Append(": ");
            sb.Append(pageBreakAfter);
            sb.Append(seperator);
        }

        if (pageBreakBefore != null)
        {
            sb.Append("page-break-before");
            sb.Append(": ");
            sb.Append(pageBreakBefore);
            sb.Append(seperator);
        }

        if (pageBreakInside != null)
        {
            sb.Append("page-break-inside");
            sb.Append(": ");
            sb.Append(pageBreakInside);
            sb.Append(seperator);
        }

        if (perspective != null)
        {
            sb.Append("perspective");
            sb.Append(": ");
            sb.Append(perspective);
            sb.Append(seperator);
        }

        if (perspectiveOrigin != null)
        {
            sb.Append("perspective-origin");
            sb.Append(": ");
            sb.Append(perspectiveOrigin);
            sb.Append(seperator);
        }

        if (pointerEvents != null)
        {
            sb.Append("pointer-events");
            sb.Append(": ");
            sb.Append(pointerEvents);
            sb.Append(seperator);
        }

        if (position != null)
        {
            sb.Append("position");
            sb.Append(": ");
            sb.Append(position);
            sb.Append(seperator);
        }

        if (quotes != null)
        {
            sb.Append("quotes");
            sb.Append(": ");
            sb.Append(quotes);
            sb.Append(seperator);
        }

        if (resize != null)
        {
            sb.Append("resize");
            sb.Append(": ");
            sb.Append(resize);
            sb.Append(seperator);
        }

        if (right != null)
        {
            sb.Append("right");
            sb.Append(": ");
            sb.Append(right);
            sb.Append(seperator);
        }

        if (tableLayout != null)
        {
            sb.Append("table-layout");
            sb.Append(": ");
            sb.Append(tableLayout);
            sb.Append(seperator);
        }

        if (tabSize != null)
        {
            sb.Append("tab-size");
            sb.Append(": ");
            sb.Append(tabSize);
            sb.Append(seperator);
        }

        if (textAlign != null)
        {
            sb.Append("text-align");
            sb.Append(": ");
            sb.Append(textAlign);
            sb.Append(seperator);
        }

        if (textAlignLast != null)
        {
            sb.Append("text-align-last");
            sb.Append(": ");
            sb.Append(textAlignLast);
            sb.Append(seperator);
        }

        if (textCombineHorizontal != null)
        {
            sb.Append("text-combine-horizontal");
            sb.Append(": ");
            sb.Append(textCombineHorizontal);
            sb.Append(seperator);
        }

        if (textDecoration != null)
        {
            sb.Append("text-decoration");
            sb.Append(": ");
            sb.Append(textDecoration);
            sb.Append(seperator);
        }

        if (textDecorationColor != null)
        {
            sb.Append("text-decoration-color");
            sb.Append(": ");
            sb.Append(textDecorationColor);
            sb.Append(seperator);
        }

        if (textDecorationLine != null)
        {
            sb.Append("text-decoration-line");
            sb.Append(": ");
            sb.Append(textDecorationLine);
            sb.Append(seperator);
        }

        if (textDecorationStyle != null)
        {
            sb.Append("text-decoration-style");
            sb.Append(": ");
            sb.Append(textDecorationStyle);
            sb.Append(seperator);
        }

        if (textIndent != null)
        {
            sb.Append("text-indent");
            sb.Append(": ");
            sb.Append(textIndent);
            sb.Append(seperator);
        }

        if (textOrientation != null)
        {
            sb.Append("text-orientation");
            sb.Append(": ");
            sb.Append(textOrientation);
            sb.Append(seperator);
        }

        if (textOverflow != null)
        {
            sb.Append("text-overflow");
            sb.Append(": ");
            sb.Append(textOverflow);
            sb.Append(seperator);
        }

        if (textRendering != null)
        {
            sb.Append("text-rendering");
            sb.Append(": ");
            sb.Append(textRendering);
            sb.Append(seperator);
        }

        if (textShadow != null)
        {
            sb.Append("text-shadow");
            sb.Append(": ");
            sb.Append(textShadow);
            sb.Append(seperator);
        }

        if (textTransform != null)
        {
            sb.Append("text-transform");
            sb.Append(": ");
            sb.Append(textTransform);
            sb.Append(seperator);
        }

        if (textUnderlinePosition != null)
        {
            sb.Append("text-underline-position");
            sb.Append(": ");
            sb.Append(textUnderlinePosition);
            sb.Append(seperator);
        }

        if (top != null)
        {
            sb.Append("top");
            sb.Append(": ");
            sb.Append(top);
            sb.Append(seperator);
        }

        if (touchAction != null)
        {
            sb.Append("touch-action");
            sb.Append(": ");
            sb.Append(touchAction);
            sb.Append(seperator);
        }

        if (transform != null)
        {
            sb.Append("transform");
            sb.Append(": ");
            sb.Append(transform);
            sb.Append(seperator);
        }

        if (transformOrigin != null)
        {
            sb.Append("transform-origin");
            sb.Append(": ");
            sb.Append(transformOrigin);
            sb.Append(seperator);
        }

        if (transformStyle != null)
        {
            sb.Append("transform-style");
            sb.Append(": ");
            sb.Append(transformStyle);
            sb.Append(seperator);
        }

        if (transition != null)
        {
            sb.Append("transition");
            sb.Append(": ");
            sb.Append(transition);
            sb.Append(seperator);
        }

        if (transitionDelay != null)
        {
            sb.Append("transition-delay");
            sb.Append(": ");
            sb.Append(transitionDelay);
            sb.Append(seperator);
        }

        if (transitionDuration != null)
        {
            sb.Append("transition-duration");
            sb.Append(": ");
            sb.Append(transitionDuration);
            sb.Append(seperator);
        }

        if (transitionProperty != null)
        {
            sb.Append("transition-property");
            sb.Append(": ");
            sb.Append(transitionProperty);
            sb.Append(seperator);
        }

        if (transitionTimingFunction != null)
        {
            sb.Append("transition-timing-function");
            sb.Append(": ");
            sb.Append(transitionTimingFunction);
            sb.Append(seperator);
        }

        if (unicodeBidi != null)
        {
            sb.Append("unicode-bidi");
            sb.Append(": ");
            sb.Append(unicodeBidi);
            sb.Append(seperator);
        }

        if (unicodeRange != null)
        {
            sb.Append("unicode-range");
            sb.Append(": ");
            sb.Append(unicodeRange);
            sb.Append(seperator);
        }

        if (verticalAlign != null)
        {
            sb.Append("vertical-align");
            sb.Append(": ");
            sb.Append(verticalAlign);
            sb.Append(seperator);
        }

        if (visibility != null)
        {
            sb.Append("visibility");
            sb.Append(": ");
            sb.Append(visibility);
            sb.Append(seperator);
        }

        if (whiteSpace != null)
        {
            sb.Append("white-space");
            sb.Append(": ");
            sb.Append(whiteSpace);
            sb.Append(seperator);
        }

        if (widows != null)
        {
            sb.Append("widows");
            sb.Append(": ");
            sb.Append(widows);
            sb.Append(seperator);
        }

        if (width != null)
        {
            sb.Append("width");
            sb.Append(": ");
            sb.Append(width);
            sb.Append(seperator);
        }

        if (willChange != null)
        {
            sb.Append("will-change");
            sb.Append(": ");
            sb.Append(willChange);
            sb.Append(seperator);
        }

        if (wordBreak != null)
        {
            sb.Append("word-break");
            sb.Append(": ");
            sb.Append(wordBreak);
            sb.Append(seperator);
        }

        if (wordSpacing != null)
        {
            sb.Append("word-spacing");
            sb.Append(": ");
            sb.Append(wordSpacing);
            sb.Append(seperator);
        }

        if (wordWrap != null)
        {
            sb.Append("word-wrap");
            sb.Append(": ");
            sb.Append(wordWrap);
            sb.Append(seperator);
        }

        if (writingMode != null)
        {
            sb.Append("writing-mode");
            sb.Append(": ");
            sb.Append(writingMode);
            sb.Append(seperator);
        }

        if (zIndex != null)
        {
            sb.Append("z-index");
            sb.Append(": ");
            sb.Append(zIndex);
            sb.Append(seperator);
        }

        if (webkitBackgroundClip != null)
        {
            sb.Append("-webkit-background-clip");
            sb.Append(": ");
            sb.Append(webkitBackgroundClip);
            sb.Append(seperator);
        }
        
        if (webkitTextFillColor != null)
        {
            sb.Append("-webkit-text-fill-color");
            sb.Append(": ");
            sb.Append(webkitTextFillColor);
            sb.Append(seperator);
        }

        if (sb.Length == 0)
        {
            return null;
        }

        return sb.ToString();
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

    static Exception CssParseException(string message)
    {
        return new Exception("Css parse error." + message);
    }

    public static Style ParseCss(string css)
    {
        var style = new Style();
        
        style.Import(css);

        return style;
    }
}