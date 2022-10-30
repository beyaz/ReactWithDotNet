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
        var sb = new StringBuilder();

        if (alignContent != null)
        {
            sb.Append("align-content");
            sb.Append(": ");
            sb.Append(alignContent);
            sb.Append(";");
        }

        if (alignItems != null)
        {
            sb.Append("align-items");
            sb.Append(": ");
            sb.Append(alignItems);
            sb.Append(";");
        }

        if (alignSelf != null)
        {
            sb.Append("align-self");
            sb.Append(": ");
            sb.Append(alignSelf);
            sb.Append(";");
        }

        if (all != null)
        {
            sb.Append("all");
            sb.Append(": ");
            sb.Append(all);
            sb.Append(";");
        }

        if (animation != null)
        {
            sb.Append("animation");
            sb.Append(": ");
            sb.Append(animation);
            sb.Append(";");
        }

        if (animationDelay != null)
        {
            sb.Append("animation-delay");
            sb.Append(": ");
            sb.Append(animationDelay);
            sb.Append(";");
        }

        if (animationDirection != null)
        {
            sb.Append("animation-direction");
            sb.Append(": ");
            sb.Append(animationDirection);
            sb.Append(";");
        }

        if (animationDuration != null)
        {
            sb.Append("animation-duration");
            sb.Append(": ");
            sb.Append(animationDuration);
            sb.Append(";");
        }

        if (animationFillMode != null)
        {
            sb.Append("animation-fill-mode");
            sb.Append(": ");
            sb.Append(animationFillMode);
            sb.Append(";");
        }

        if (animationIterationCount != null)
        {
            sb.Append("animation-iteration-count");
            sb.Append(": ");
            sb.Append(animationIterationCount);
            sb.Append(";");
        }

        if (animationName != null)
        {
            sb.Append("animation-name");
            sb.Append(": ");
            sb.Append(animationName);
            sb.Append(";");
        }

        if (animationPlayState != null)
        {
            sb.Append("animation-play-state");
            sb.Append(": ");
            sb.Append(animationPlayState);
            sb.Append(";");
        }

        if (animationTimingFunction != null)
        {
            sb.Append("animation-timing-function");
            sb.Append(": ");
            sb.Append(animationTimingFunction);
            sb.Append(";");
        }

        if (backfaceVisibility != null)
        {
            sb.Append("backface-visibility");
            sb.Append(": ");
            sb.Append(backfaceVisibility);
            sb.Append(";");
        }

        if (background != null)
        {
            sb.Append("background");
            sb.Append(": ");
            sb.Append(background);
            sb.Append(";");
        }

        if (backgroundAttachment != null)
        {
            sb.Append("background-attachment");
            sb.Append(": ");
            sb.Append(backgroundAttachment);
            sb.Append(";");
        }

        if (backgroundBlendMode != null)
        {
            sb.Append("background-blend-mode");
            sb.Append(": ");
            sb.Append(backgroundBlendMode);
            sb.Append(";");
        }

        if (backgroundClip != null)
        {
            sb.Append("background-clip");
            sb.Append(": ");
            sb.Append(backgroundClip);
            sb.Append(";");
        }

        if (backgroundColor != null)
        {
            sb.Append("background-color");
            sb.Append(": ");
            sb.Append(backgroundColor);
            sb.Append(";");
        }

        if (backgroundImage != null)
        {
            sb.Append("background-image");
            sb.Append(": ");
            sb.Append(backgroundImage);
            sb.Append(";");
        }

        if (backgroundOrigin != null)
        {
            sb.Append("background-origin");
            sb.Append(": ");
            sb.Append(backgroundOrigin);
            sb.Append(";");
        }

        if (backgroundPosition != null)
        {
            sb.Append("background-position");
            sb.Append(": ");
            sb.Append(backgroundPosition);
            sb.Append(";");
        }

        if (backgroundRepeat != null)
        {
            sb.Append("background-repeat");
            sb.Append(": ");
            sb.Append(backgroundRepeat);
            sb.Append(";");
        }

        if (backgroundSize != null)
        {
            sb.Append("background-size");
            sb.Append(": ");
            sb.Append(backgroundSize);
            sb.Append(";");
        }

        if (border != null)
        {
            sb.Append("border");
            sb.Append(": ");
            sb.Append(border);
            sb.Append(";");
        }

        if (borderBottom != null)
        {
            sb.Append("border-bottom");
            sb.Append(": ");
            sb.Append(borderBottom);
            sb.Append(";");
        }

        if (borderBottomColor != null)
        {
            sb.Append("border-bottom-color");
            sb.Append(": ");
            sb.Append(borderBottomColor);
            sb.Append(";");
        }

        if (borderBottomLeftRadius != null)
        {
            sb.Append("border-bottom-left-radius");
            sb.Append(": ");
            sb.Append(borderBottomLeftRadius);
            sb.Append(";");
        }

        if (borderBottomRightRadius != null)
        {
            sb.Append("border-bottom-right-radius");
            sb.Append(": ");
            sb.Append(borderBottomRightRadius);
            sb.Append(";");
        }

        if (borderBottomStyle != null)
        {
            sb.Append("border-bottom-style");
            sb.Append(": ");
            sb.Append(borderBottomStyle);
            sb.Append(";");
        }

        if (borderBottomWidth != null)
        {
            sb.Append("border-bottom-width");
            sb.Append(": ");
            sb.Append(borderBottomWidth);
            sb.Append(";");
        }

        if (borderCollapse != null)
        {
            sb.Append("border-collapse");
            sb.Append(": ");
            sb.Append(borderCollapse);
            sb.Append(";");
        }

        if (borderColor != null)
        {
            sb.Append("border-color");
            sb.Append(": ");
            sb.Append(borderColor);
            sb.Append(";");
        }

        if (borderImage != null)
        {
            sb.Append("border-image");
            sb.Append(": ");
            sb.Append(borderImage);
            sb.Append(";");
        }

        if (borderImageOutset != null)
        {
            sb.Append("border-image-outset");
            sb.Append(": ");
            sb.Append(borderImageOutset);
            sb.Append(";");
        }

        if (borderImageRepeat != null)
        {
            sb.Append("border-image-repeat");
            sb.Append(": ");
            sb.Append(borderImageRepeat);
            sb.Append(";");
        }

        if (borderImageSlice != null)
        {
            sb.Append("border-image-slice");
            sb.Append(": ");
            sb.Append(borderImageSlice);
            sb.Append(";");
        }

        if (borderImageSource != null)
        {
            sb.Append("border-image-source");
            sb.Append(": ");
            sb.Append(borderImageSource);
            sb.Append(";");
        }

        if (borderImageWidth != null)
        {
            sb.Append("border-image-width");
            sb.Append(": ");
            sb.Append(borderImageWidth);
            sb.Append(";");
        }

        if (borderLeft != null)
        {
            sb.Append("border-left");
            sb.Append(": ");
            sb.Append(borderLeft);
            sb.Append(";");
        }

        if (borderLeftColor != null)
        {
            sb.Append("border-left-color");
            sb.Append(": ");
            sb.Append(borderLeftColor);
            sb.Append(";");
        }

        if (borderLeftStyle != null)
        {
            sb.Append("border-left-style");
            sb.Append(": ");
            sb.Append(borderLeftStyle);
            sb.Append(";");
        }

        if (borderLeftWidth != null)
        {
            sb.Append("border-left-width");
            sb.Append(": ");
            sb.Append(borderLeftWidth);
            sb.Append(";");
        }

        if (borderRadius != null)
        {
            sb.Append("border-radius");
            sb.Append(": ");
            sb.Append(borderRadius);
            sb.Append(";");
        }

        if (borderRight != null)
        {
            sb.Append("border-right");
            sb.Append(": ");
            sb.Append(borderRight);
            sb.Append(";");
        }

        if (borderRightColor != null)
        {
            sb.Append("border-right-color");
            sb.Append(": ");
            sb.Append(borderRightColor);
            sb.Append(";");
        }

        if (borderRightStyle != null)
        {
            sb.Append("border-right-style");
            sb.Append(": ");
            sb.Append(borderRightStyle);
            sb.Append(";");
        }

        if (borderRightWidth != null)
        {
            sb.Append("border-right-width");
            sb.Append(": ");
            sb.Append(borderRightWidth);
            sb.Append(";");
        }

        if (borderSpacing != null)
        {
            sb.Append("border-spacing");
            sb.Append(": ");
            sb.Append(borderSpacing);
            sb.Append(";");
        }

        if (borderStyle != null)
        {
            sb.Append("border-style");
            sb.Append(": ");
            sb.Append(borderStyle);
            sb.Append(";");
        }

        if (borderTop != null)
        {
            sb.Append("border-top");
            sb.Append(": ");
            sb.Append(borderTop);
            sb.Append(";");
        }

        if (borderTopColor != null)
        {
            sb.Append("border-top-color");
            sb.Append(": ");
            sb.Append(borderTopColor);
            sb.Append(";");
        }

        if (borderTopLeftRadius != null)
        {
            sb.Append("border-top-left-radius");
            sb.Append(": ");
            sb.Append(borderTopLeftRadius);
            sb.Append(";");
        }

        if (borderTopRightRadius != null)
        {
            sb.Append("border-top-right-radius");
            sb.Append(": ");
            sb.Append(borderTopRightRadius);
            sb.Append(";");
        }

        if (borderTopStyle != null)
        {
            sb.Append("border-top-style");
            sb.Append(": ");
            sb.Append(borderTopStyle);
            sb.Append(";");
        }

        if (borderTopWidth != null)
        {
            sb.Append("border-top-width");
            sb.Append(": ");
            sb.Append(borderTopWidth);
            sb.Append(";");
        }

        if (borderWidth != null)
        {
            sb.Append("border-width");
            sb.Append(": ");
            sb.Append(borderWidth);
            sb.Append(";");
        }

        if (bottom != null)
        {
            sb.Append("bottom");
            sb.Append(": ");
            sb.Append(bottom);
            sb.Append(";");
        }

        if (boxDecorationBreak != null)
        {
            sb.Append("box-decoration-break");
            sb.Append(": ");
            sb.Append(boxDecorationBreak);
            sb.Append(";");
        }

        if (boxShadow != null)
        {
            sb.Append("box-shadow");
            sb.Append(": ");
            sb.Append(boxShadow);
            sb.Append(";");
        }

        if (boxSizing != null)
        {
            sb.Append("box-sizing");
            sb.Append(": ");
            sb.Append(boxSizing);
            sb.Append(";");
        }

        if (captionSide != null)
        {
            sb.Append("caption-side");
            sb.Append(": ");
            sb.Append(captionSide);
            sb.Append(";");
        }

        if (clear != null)
        {
            sb.Append("clear");
            sb.Append(": ");
            sb.Append(clear);
            sb.Append(";");
        }

        if (clip != null)
        {
            sb.Append("clip");
            sb.Append(": ");
            sb.Append(clip);
            sb.Append(";");
        }

        if (clipPath != null)
        {
            sb.Append("clip-path");
            sb.Append(": ");
            sb.Append(clipPath);
            sb.Append(";");
        }

        if (color != null)
        {
            sb.Append("color");
            sb.Append(": ");
            sb.Append(color);
            sb.Append(";");
        }

        if (columns != null)
        {
            sb.Append("columns");
            sb.Append(": ");
            sb.Append(columns);
            sb.Append(";");
        }

        if (columnCount != null)
        {
            sb.Append("column-count");
            sb.Append(": ");
            sb.Append(columnCount);
            sb.Append(";");
        }

        if (columnFill != null)
        {
            sb.Append("column-fill");
            sb.Append(": ");
            sb.Append(columnFill);
            sb.Append(";");
        }

        if (columnGap != null)
        {
            sb.Append("column-gap");
            sb.Append(": ");
            sb.Append(columnGap);
            sb.Append(";");
        }

        if (rowGap != null)
        {
            sb.Append("row-gap");
            sb.Append(": ");
            sb.Append(rowGap);
            sb.Append(";");
        }

        if (gap != null)
        {
            sb.Append("gap");
            sb.Append(": ");
            sb.Append(gap);
            sb.Append(";");
        }

        if (columnRule != null)
        {
            sb.Append("column-rule");
            sb.Append(": ");
            sb.Append(columnRule);
            sb.Append(";");
        }

        if (columnRuleColor != null)
        {
            sb.Append("column-rule-color");
            sb.Append(": ");
            sb.Append(columnRuleColor);
            sb.Append(";");
        }

        if (columnRuleStyle != null)
        {
            sb.Append("column-rule-style");
            sb.Append(": ");
            sb.Append(columnRuleStyle);
            sb.Append(";");
        }

        if (columnRuleWidth != null)
        {
            sb.Append("column-rule-width");
            sb.Append(": ");
            sb.Append(columnRuleWidth);
            sb.Append(";");
        }

        if (columnSpan != null)
        {
            sb.Append("column-span");
            sb.Append(": ");
            sb.Append(columnSpan);
            sb.Append(";");
        }

        if (columnWidth != null)
        {
            sb.Append("column-width");
            sb.Append(": ");
            sb.Append(columnWidth);
            sb.Append(";");
        }

        if (content != null)
        {
            sb.Append("content");
            sb.Append(": ");
            sb.Append("'" + content + "'");
            sb.Append(";");
        }
        
        if (contentVisibility != null)
        {
            sb.Append("content-visibility");
            sb.Append(": ");
            sb.Append(contentVisibility);
            sb.Append(";");
        }
        
        if (counterIncrement != null)
        {
            sb.Append("counter-increment");
            sb.Append(": ");
            sb.Append(counterIncrement);
            sb.Append(";");
        }

        if (counterReset != null)
        {
            sb.Append("counter-reset");
            sb.Append(": ");
            sb.Append(counterReset);
            sb.Append(";");
        }

        if (cssFloat != null)
        {
            sb.Append("css-float");
            sb.Append(": ");
            sb.Append(cssFloat);
            sb.Append(";");
        }

        if (cssText != null)
        {
            sb.Append("css-text");
            sb.Append(": ");
            sb.Append(cssText);
            sb.Append(";");
        }

        if (cursor != null)
        {
            sb.Append("cursor");
            sb.Append(": ");
            sb.Append(cursor);
            sb.Append(";");
        }

        if (direction != null)
        {
            sb.Append("direction");
            sb.Append(": ");
            sb.Append(direction);
            sb.Append(";");
        }

        if (display != null)
        {
            sb.Append("display");
            sb.Append(": ");
            sb.Append(display);
            sb.Append(";");
        }

        if (dominantBaseline != null)
        {
            sb.Append("dominant-baseline");
            sb.Append(": ");
            sb.Append(dominantBaseline);
            sb.Append(";");
        }

        if (emptyCells != null)
        {
            sb.Append("empty-cells");
            sb.Append(": ");
            sb.Append(emptyCells);
            sb.Append(";");
        }

        if (fill != null)
        {
            sb.Append("fill");
            sb.Append(": ");
            sb.Append(fill);
            sb.Append(";");
        }

        if (fillOpacity != null)
        {
            sb.Append("fill-opacity");
            sb.Append(": ");
            sb.Append(fillOpacity);
            sb.Append(";");
        }

        if (fillRule != null)
        {
            sb.Append("fill-rule");
            sb.Append(": ");
            sb.Append(fillRule);
            sb.Append(";");
        }

        if (filter != null)
        {
            sb.Append("filter");
            sb.Append(": ");
            sb.Append(filter);
            sb.Append(";");
        }

        if (flex != null)
        {
            sb.Append("flex");
            sb.Append(": ");
            sb.Append(flex);
            sb.Append(";");
        }

        if (flexBasis != null)
        {
            sb.Append("flex-basis");
            sb.Append(": ");
            sb.Append(flexBasis);
            sb.Append(";");
        }

        if (flexDirection != null)
        {
            sb.Append("flex-direction");
            sb.Append(": ");
            sb.Append(flexDirection);
            sb.Append(";");
        }

        if (flexFlow != null)
        {
            sb.Append("flex-flow");
            sb.Append(": ");
            sb.Append(flexFlow);
            sb.Append(";");
        }

        if (flexGrow != null)
        {
            sb.Append("flex-grow");
            sb.Append(": ");
            sb.Append(flexGrow);
            sb.Append(";");
        }

        if (flexShrink != null)
        {
            sb.Append("flex-shrink");
            sb.Append(": ");
            sb.Append(flexShrink);
            sb.Append(";");
        }

        if (flexWrap != null)
        {
            sb.Append("flex-wrap");
            sb.Append(": ");
            sb.Append(flexWrap);
            sb.Append(";");
        }

        if (floodColor != null)
        {
            sb.Append("flood-color");
            sb.Append(": ");
            sb.Append(floodColor);
            sb.Append(";");
        }

        if (floodOpacity != null)
        {
            sb.Append("flood-opacity");
            sb.Append(": ");
            sb.Append(floodOpacity);
            sb.Append(";");
        }

        if (font != null)
        {
            sb.Append("font");
            sb.Append(": ");
            sb.Append(font);
            sb.Append(";");
        }

        if (fontFamily != null)
        {
            sb.Append("font-family");
            sb.Append(": ");
            sb.Append(fontFamily);
            sb.Append(";");
        }

        if (fontFeatureSettings != null)
        {
            sb.Append("font-feature-settings");
            sb.Append(": ");
            sb.Append(fontFeatureSettings);
            sb.Append(";");
        }

        if (fontKerning != null)
        {
            sb.Append("font-kerning");
            sb.Append(": ");
            sb.Append(fontKerning);
            sb.Append(";");
        }

        if (fontLanguageOverride != null)
        {
            sb.Append("font-language-override");
            sb.Append(": ");
            sb.Append(fontLanguageOverride);
            sb.Append(";");
        }

        if (fontSize != null)
        {
            sb.Append("font-size");
            sb.Append(": ");
            sb.Append(fontSize);
            sb.Append(";");
        }

        if (fontSizeAdjust != null)
        {
            sb.Append("font-size-adjust");
            sb.Append(": ");
            sb.Append(fontSizeAdjust);
            sb.Append(";");
        }

        if (fontStretch != null)
        {
            sb.Append("font-stretch");
            sb.Append(": ");
            sb.Append(fontStretch);
            sb.Append(";");
        }

        if (fontStyle != null)
        {
            sb.Append("font-style");
            sb.Append(": ");
            sb.Append(fontStyle);
            sb.Append(";");
        }

        if (fontSynthesis != null)
        {
            sb.Append("font-synthesis");
            sb.Append(": ");
            sb.Append(fontSynthesis);
            sb.Append(";");
        }

        if (fontVariant != null)
        {
            sb.Append("font-variant");
            sb.Append(": ");
            sb.Append(fontVariant);
            sb.Append(";");
        }

        if (fontVariantAlternates != null)
        {
            sb.Append("font-variant-alternates");
            sb.Append(": ");
            sb.Append(fontVariantAlternates);
            sb.Append(";");
        }

        if (fontVariantCaps != null)
        {
            sb.Append("font-variant-caps");
            sb.Append(": ");
            sb.Append(fontVariantCaps);
            sb.Append(";");
        }

        if (fontVariantEastAsian != null)
        {
            sb.Append("font-variant-east-asian");
            sb.Append(": ");
            sb.Append(fontVariantEastAsian);
            sb.Append(";");
        }

        if (fontVariantLigatures != null)
        {
            sb.Append("font-variant-ligatures");
            sb.Append(": ");
            sb.Append(fontVariantLigatures);
            sb.Append(";");
        }

        if (fontVariantNumeric != null)
        {
            sb.Append("font-variant-numeric");
            sb.Append(": ");
            sb.Append(fontVariantNumeric);
            sb.Append(";");
        }

        if (fontVariantPosition != null)
        {
            sb.Append("font-variant-position");
            sb.Append(": ");
            sb.Append(fontVariantPosition);
            sb.Append(";");
        }

        if (fontWeight != null)
        {
            sb.Append("font-weight");
            sb.Append(": ");
            sb.Append(fontWeight);
            sb.Append(";");
        }

        if (grid != null)
        {
            sb.Append("grid");
            sb.Append(": ");
            sb.Append(grid);
            sb.Append(";");
        }

        if (gridArea != null)
        {
            sb.Append("grid-area");
            sb.Append(": ");
            sb.Append(gridArea);
            sb.Append(";");
        }

        if (gridAutoColumns != null)
        {
            sb.Append("grid-auto-columns");
            sb.Append(": ");
            sb.Append(gridAutoColumns);
            sb.Append(";");
        }

        if (gridAutoFlow != null)
        {
            sb.Append("grid-auto-flow");
            sb.Append(": ");
            sb.Append(gridAutoFlow);
            sb.Append(";");
        }

        if (gridAutoPosition != null)
        {
            sb.Append("grid-auto-position");
            sb.Append(": ");
            sb.Append(gridAutoPosition);
            sb.Append(";");
        }

        if (gridAutoRows != null)
        {
            sb.Append("grid-auto-rows");
            sb.Append(": ");
            sb.Append(gridAutoRows);
            sb.Append(";");
        }

        if (gridColumn != null)
        {
            sb.Append("grid-column");
            sb.Append(": ");
            sb.Append(gridColumn);
            sb.Append(";");
        }

        if (gridColumnStart != null)
        {
            sb.Append("grid-column-start");
            sb.Append(": ");
            sb.Append(gridColumnStart);
            sb.Append(";");
        }

        if (gridColumnEnd != null)
        {
            sb.Append("grid-column-end");
            sb.Append(": ");
            sb.Append(gridColumnEnd);
            sb.Append(";");
        }

        if (gridRow != null)
        {
            sb.Append("grid-row");
            sb.Append(": ");
            sb.Append(gridRow);
            sb.Append(";");
        }

        if (gridRowStart != null)
        {
            sb.Append("grid-row-start");
            sb.Append(": ");
            sb.Append(gridRowStart);
            sb.Append(";");
        }

        if (gridRowEnd != null)
        {
            sb.Append("grid-row-end");
            sb.Append(": ");
            sb.Append(gridRowEnd);
            sb.Append(";");
        }

        if (gridTemplate != null)
        {
            sb.Append("grid-template");
            sb.Append(": ");
            sb.Append(gridTemplate);
            sb.Append(";");
        }

        if (gridTemplateAreas != null)
        {
            sb.Append("grid-template-areas");
            sb.Append(": ");
            sb.Append(gridTemplateAreas);
            sb.Append(";");
        }

        if (gridTemplateRows != null)
        {
            sb.Append("grid-template-rows");
            sb.Append(": ");
            sb.Append(gridTemplateRows);
            sb.Append(";");
        }

        if (gridTemplateColumns != null)
        {
            sb.Append("grid-template-columns");
            sb.Append(": ");
            sb.Append(gridTemplateColumns);
            sb.Append(";");
        }

        if (height != null)
        {
            sb.Append("height");
            sb.Append(": ");
            sb.Append(height);
            sb.Append(";");
        }

        if (hyphens != null)
        {
            sb.Append("hyphens");
            sb.Append(": ");
            sb.Append(hyphens);
            sb.Append(";");
        }

        if (icon != null)
        {
            sb.Append("icon");
            sb.Append(": ");
            sb.Append(icon);
            sb.Append(";");
        }

        if (imageRendering != null)
        {
            sb.Append("image-rendering");
            sb.Append(": ");
            sb.Append(imageRendering);
            sb.Append(";");
        }

        if (imageResolution != null)
        {
            sb.Append("image-resolution");
            sb.Append(": ");
            sb.Append(imageResolution);
            sb.Append(";");
        }

        if (imageOrientation != null)
        {
            sb.Append("image-orientation");
            sb.Append(": ");
            sb.Append(imageOrientation);
            sb.Append(";");
        }

        if (imeMode != null)
        {
            sb.Append("ime-mode");
            sb.Append(": ");
            sb.Append(imeMode);
            sb.Append(";");
        }

        if (justifyContent != null)
        {
            sb.Append("justify-content");
            sb.Append(": ");
            sb.Append(justifyContent);
            sb.Append(";");
        }

        if (left != null)
        {
            sb.Append("left");
            sb.Append(": ");
            sb.Append(left);
            sb.Append(";");
        }

        if (letterSpacing != null)
        {
            sb.Append("letter-spacing");
            sb.Append(": ");
            sb.Append(letterSpacing);
            sb.Append(";");
        }

        if (lightingColor != null)
        {
            sb.Append("lighting-color");
            sb.Append(": ");
            sb.Append(lightingColor);
            sb.Append(";");
        }

        if (lineHeight != null)
        {
            sb.Append("line-height");
            sb.Append(": ");
            sb.Append(lineHeight);
            sb.Append(";");
        }

        if (listStyle != null)
        {
            sb.Append("list-style");
            sb.Append(": ");
            sb.Append(listStyle);
            sb.Append(";");
        }

        if (listStyleImage != null)
        {
            sb.Append("list-style-image");
            sb.Append(": ");
            sb.Append(listStyleImage);
            sb.Append(";");
        }

        if (listStylePosition != null)
        {
            sb.Append("list-style-position");
            sb.Append(": ");
            sb.Append(listStylePosition);
            sb.Append(";");
        }

        if (listStyleType != null)
        {
            sb.Append("list-style-type");
            sb.Append(": ");
            sb.Append(listStyleType);
            sb.Append(";");
        }

        if (margin != null)
        {
            sb.Append("margin");
            sb.Append(": ");
            sb.Append(margin);
            sb.Append(";");
        }

        if (marginBottom != null)
        {
            sb.Append("margin-bottom");
            sb.Append(": ");
            sb.Append(marginBottom);
            sb.Append(";");
        }

        if (marginLeft != null)
        {
            sb.Append("margin-left");
            sb.Append(": ");
            sb.Append(marginLeft);
            sb.Append(";");
        }

        if (marginRight != null)
        {
            sb.Append("margin-right");
            sb.Append(": ");
            sb.Append(marginRight);
            sb.Append(";");
        }

        if (marginTop != null)
        {
            sb.Append("margin-top");
            sb.Append(": ");
            sb.Append(marginTop);
            sb.Append(";");
        }

        if (marks != null)
        {
            sb.Append("marks");
            sb.Append(": ");
            sb.Append(marks);
            sb.Append(";");
        }

        if (mask != null)
        {
            sb.Append("mask");
            sb.Append(": ");
            sb.Append(mask);
            sb.Append(";");
        }

        if (maskType != null)
        {
            sb.Append("mask-type");
            sb.Append(": ");
            sb.Append(maskType);
            sb.Append(";");
        }

        if (maxHeight != null)
        {
            sb.Append("max-height");
            sb.Append(": ");
            sb.Append(maxHeight);
            sb.Append(";");
        }

        if (maxWidth != null)
        {
            sb.Append("max-width");
            sb.Append(": ");
            sb.Append(maxWidth);
            sb.Append(";");
        }

        if (minHeight != null)
        {
            sb.Append("min-height");
            sb.Append(": ");
            sb.Append(minHeight);
            sb.Append(";");
        }

        if (minWidth != null)
        {
            sb.Append("min-width");
            sb.Append(": ");
            sb.Append(minWidth);
            sb.Append(";");
        }

        if (mixBlendMode != null)
        {
            sb.Append("mix-blend-mode");
            sb.Append(": ");
            sb.Append(mixBlendMode);
            sb.Append(";");
        }

        if (navDown != null)
        {
            sb.Append("nav-down");
            sb.Append(": ");
            sb.Append(navDown);
            sb.Append(";");
        }

        if (navIndex != null)
        {
            sb.Append("nav-index");
            sb.Append(": ");
            sb.Append(navIndex);
            sb.Append(";");
        }

        if (navLeft != null)
        {
            sb.Append("nav-left");
            sb.Append(": ");
            sb.Append(navLeft);
            sb.Append(";");
        }

        if (navRight != null)
        {
            sb.Append("nav-right");
            sb.Append(": ");
            sb.Append(navRight);
            sb.Append(";");
        }

        if (navUp != null)
        {
            sb.Append("nav-up");
            sb.Append(": ");
            sb.Append(navUp);
            sb.Append(";");
        }

        if (objectFit != null)
        {
            sb.Append("object-fit");
            sb.Append(": ");
            sb.Append(objectFit);
            sb.Append(";");
        }

        if (objectPosition != null)
        {
            sb.Append("object-position");
            sb.Append(": ");
            sb.Append(objectPosition);
            sb.Append(";");
        }

        if (opacity != null)
        {
            sb.Append("opacity");
            sb.Append(": ");
            sb.Append(opacity);
            sb.Append(";");
        }

        if (order != null)
        {
            sb.Append("order");
            sb.Append(": ");
            sb.Append(order);
            sb.Append(";");
        }

        if (orphans != null)
        {
            sb.Append("orphans");
            sb.Append(": ");
            sb.Append(orphans);
            sb.Append(";");
        }

        if (outline != null)
        {
            sb.Append("outline");
            sb.Append(": ");
            sb.Append(outline);
            sb.Append(";");
        }

        if (outlineColor != null)
        {
            sb.Append("outline-color");
            sb.Append(": ");
            sb.Append(outlineColor);
            sb.Append(";");
        }

        if (outlineOffset != null)
        {
            sb.Append("outline-offset");
            sb.Append(": ");
            sb.Append(outlineOffset);
            sb.Append(";");
        }

        if (outlineStyle != null)
        {
            sb.Append("outline-style");
            sb.Append(": ");
            sb.Append(outlineStyle);
            sb.Append(";");
        }

        if (outlineWidth != null)
        {
            sb.Append("outline-width");
            sb.Append(": ");
            sb.Append(outlineWidth);
            sb.Append(";");
        }

        if (overflow != null)
        {
            sb.Append("overflow");
            sb.Append(": ");
            sb.Append(overflow);
            sb.Append(";");
        }

        if (overflowWrap != null)
        {
            sb.Append("overflow-wrap");
            sb.Append(": ");
            sb.Append(overflowWrap);
            sb.Append(";");
        }

        if (overflowX != null)
        {
            sb.Append("overflow-x");
            sb.Append(": ");
            sb.Append(overflowX);
            sb.Append(";");
        }

        if (overflowY != null)
        {
            sb.Append("overflow-y");
            sb.Append(": ");
            sb.Append(overflowY);
            sb.Append(";");
        }

        if (overflowClipBox != null)
        {
            sb.Append("overflow-clip-box");
            sb.Append(": ");
            sb.Append(overflowClipBox);
            sb.Append(";");
        }

        if (padding != null)
        {
            sb.Append("padding");
            sb.Append(": ");
            sb.Append(padding);
            sb.Append(";");
        }

        if (paddingBottom != null)
        {
            sb.Append("padding-bottom");
            sb.Append(": ");
            sb.Append(paddingBottom);
            sb.Append(";");
        }

        if (paddingLeft != null)
        {
            sb.Append("padding-left");
            sb.Append(": ");
            sb.Append(paddingLeft);
            sb.Append(";");
        }

        if (paddingRight != null)
        {
            sb.Append("padding-right");
            sb.Append(": ");
            sb.Append(paddingRight);
            sb.Append(";");
        }

        if (paddingTop != null)
        {
            sb.Append("padding-top");
            sb.Append(": ");
            sb.Append(paddingTop);
            sb.Append(";");
        }

        if (pageBreakAfter != null)
        {
            sb.Append("page-break-after");
            sb.Append(": ");
            sb.Append(pageBreakAfter);
            sb.Append(";");
        }

        if (pageBreakBefore != null)
        {
            sb.Append("page-break-before");
            sb.Append(": ");
            sb.Append(pageBreakBefore);
            sb.Append(";");
        }

        if (pageBreakInside != null)
        {
            sb.Append("page-break-inside");
            sb.Append(": ");
            sb.Append(pageBreakInside);
            sb.Append(";");
        }

        if (perspective != null)
        {
            sb.Append("perspective");
            sb.Append(": ");
            sb.Append(perspective);
            sb.Append(";");
        }

        if (perspectiveOrigin != null)
        {
            sb.Append("perspective-origin");
            sb.Append(": ");
            sb.Append(perspectiveOrigin);
            sb.Append(";");
        }

        if (pointerEvents != null)
        {
            sb.Append("pointer-events");
            sb.Append(": ");
            sb.Append(pointerEvents);
            sb.Append(";");
        }

        if (position != null)
        {
            sb.Append("position");
            sb.Append(": ");
            sb.Append(position);
            sb.Append(";");
        }

        if (quotes != null)
        {
            sb.Append("quotes");
            sb.Append(": ");
            sb.Append(quotes);
            sb.Append(";");
        }

        if (resize != null)
        {
            sb.Append("resize");
            sb.Append(": ");
            sb.Append(resize);
            sb.Append(";");
        }

        if (right != null)
        {
            sb.Append("right");
            sb.Append(": ");
            sb.Append(right);
            sb.Append(";");
        }

        if (tableLayout != null)
        {
            sb.Append("table-layout");
            sb.Append(": ");
            sb.Append(tableLayout);
            sb.Append(";");
        }

        if (tabSize != null)
        {
            sb.Append("tab-size");
            sb.Append(": ");
            sb.Append(tabSize);
            sb.Append(";");
        }

        if (textAlign != null)
        {
            sb.Append("text-align");
            sb.Append(": ");
            sb.Append(textAlign);
            sb.Append(";");
        }

        if (textAlignLast != null)
        {
            sb.Append("text-align-last");
            sb.Append(": ");
            sb.Append(textAlignLast);
            sb.Append(";");
        }

        if (textCombineHorizontal != null)
        {
            sb.Append("text-combine-horizontal");
            sb.Append(": ");
            sb.Append(textCombineHorizontal);
            sb.Append(";");
        }

        if (textDecoration != null)
        {
            sb.Append("text-decoration");
            sb.Append(": ");
            sb.Append(textDecoration);
            sb.Append(";");
        }

        if (textDecorationColor != null)
        {
            sb.Append("text-decoration-color");
            sb.Append(": ");
            sb.Append(textDecorationColor);
            sb.Append(";");
        }

        if (textDecorationLine != null)
        {
            sb.Append("text-decoration-line");
            sb.Append(": ");
            sb.Append(textDecorationLine);
            sb.Append(";");
        }

        if (textDecorationStyle != null)
        {
            sb.Append("text-decoration-style");
            sb.Append(": ");
            sb.Append(textDecorationStyle);
            sb.Append(";");
        }

        if (textIndent != null)
        {
            sb.Append("text-indent");
            sb.Append(": ");
            sb.Append(textIndent);
            sb.Append(";");
        }

        if (textOrientation != null)
        {
            sb.Append("text-orientation");
            sb.Append(": ");
            sb.Append(textOrientation);
            sb.Append(";");
        }

        if (textOverflow != null)
        {
            sb.Append("text-overflow");
            sb.Append(": ");
            sb.Append(textOverflow);
            sb.Append(";");
        }

        if (textRendering != null)
        {
            sb.Append("text-rendering");
            sb.Append(": ");
            sb.Append(textRendering);
            sb.Append(";");
        }

        if (textShadow != null)
        {
            sb.Append("text-shadow");
            sb.Append(": ");
            sb.Append(textShadow);
            sb.Append(";");
        }

        if (textTransform != null)
        {
            sb.Append("text-transform");
            sb.Append(": ");
            sb.Append(textTransform);
            sb.Append(";");
        }

        if (textUnderlinePosition != null)
        {
            sb.Append("text-underline-position");
            sb.Append(": ");
            sb.Append(textUnderlinePosition);
            sb.Append(";");
        }

        if (top != null)
        {
            sb.Append("top");
            sb.Append(": ");
            sb.Append(top);
            sb.Append(";");
        }

        if (touchAction != null)
        {
            sb.Append("touch-action");
            sb.Append(": ");
            sb.Append(touchAction);
            sb.Append(";");
        }

        if (transform != null)
        {
            sb.Append("transform");
            sb.Append(": ");
            sb.Append(transform);
            sb.Append(";");
        }

        if (transformOrigin != null)
        {
            sb.Append("transform-origin");
            sb.Append(": ");
            sb.Append(transformOrigin);
            sb.Append(";");
        }

        if (transformStyle != null)
        {
            sb.Append("transform-style");
            sb.Append(": ");
            sb.Append(transformStyle);
            sb.Append(";");
        }

        if (transition != null)
        {
            sb.Append("transition");
            sb.Append(": ");
            sb.Append(transition);
            sb.Append(";");
        }

        if (transitionDelay != null)
        {
            sb.Append("transition-delay");
            sb.Append(": ");
            sb.Append(transitionDelay);
            sb.Append(";");
        }

        if (transitionDuration != null)
        {
            sb.Append("transition-duration");
            sb.Append(": ");
            sb.Append(transitionDuration);
            sb.Append(";");
        }

        if (transitionProperty != null)
        {
            sb.Append("transition-property");
            sb.Append(": ");
            sb.Append(transitionProperty);
            sb.Append(";");
        }

        if (transitionTimingFunction != null)
        {
            sb.Append("transition-timing-function");
            sb.Append(": ");
            sb.Append(transitionTimingFunction);
            sb.Append(";");
        }

        if (unicodeBidi != null)
        {
            sb.Append("unicode-bidi");
            sb.Append(": ");
            sb.Append(unicodeBidi);
            sb.Append(";");
        }

        if (unicodeRange != null)
        {
            sb.Append("unicode-range");
            sb.Append(": ");
            sb.Append(unicodeRange);
            sb.Append(";");
        }

        if (verticalAlign != null)
        {
            sb.Append("vertical-align");
            sb.Append(": ");
            sb.Append(verticalAlign);
            sb.Append(";");
        }

        if (visibility != null)
        {
            sb.Append("visibility");
            sb.Append(": ");
            sb.Append(visibility);
            sb.Append(";");
        }

        if (whiteSpace != null)
        {
            sb.Append("white-space");
            sb.Append(": ");
            sb.Append(whiteSpace);
            sb.Append(";");
        }

        if (widows != null)
        {
            sb.Append("widows");
            sb.Append(": ");
            sb.Append(widows);
            sb.Append(";");
        }

        if (width != null)
        {
            sb.Append("width");
            sb.Append(": ");
            sb.Append(width);
            sb.Append(";");
        }

        if (willChange != null)
        {
            sb.Append("will-change");
            sb.Append(": ");
            sb.Append(willChange);
            sb.Append(";");
        }

        if (wordBreak != null)
        {
            sb.Append("word-break");
            sb.Append(": ");
            sb.Append(wordBreak);
            sb.Append(";");
        }

        if (wordSpacing != null)
        {
            sb.Append("word-spacing");
            sb.Append(": ");
            sb.Append(wordSpacing);
            sb.Append(";");
        }

        if (wordWrap != null)
        {
            sb.Append("word-wrap");
            sb.Append(": ");
            sb.Append(wordWrap);
            sb.Append(";");
        }

        if (writingMode != null)
        {
            sb.Append("writing-mode");
            sb.Append(": ");
            sb.Append(writingMode);
            sb.Append(";");
        }

        if (zIndex != null)
        {
            sb.Append("z-index");
            sb.Append(": ");
            sb.Append(zIndex);
            sb.Append(";");
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