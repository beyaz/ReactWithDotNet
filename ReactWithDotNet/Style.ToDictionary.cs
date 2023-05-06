namespace ReactWithDotNet;

partial class Style
{
    public IReadOnlyDictionary<string, string> ToDictionary()
    {
        var map = new Dictionary<string, string>();

        VisitNotNullValues(map.Add);

        return map;
    }

    internal void VisitNotNullValues(Action<string, string> action)
    {
        if (alignContent != null)
        {
            action(nameof(alignContent), alignContent);
        }

        if (alignItems != null)
        {
            action(nameof(alignItems), alignItems);
        }

        if (alignSelf != null)
        {
            action(nameof(alignSelf), alignSelf);
        }

        if (all != null)
        {
            action(nameof(all), all);
        }

        if (animation != null)
        {
            action(nameof(animation), animation);
        }

        if (animationDelay != null)
        {
            action(nameof(animationDelay), animationDelay);
        }

        if (animationDirection != null)
        {
            action(nameof(animationDirection), animationDirection);
        }

        if (animationDuration != null)
        {
            action(nameof(animationDuration), animationDuration);
        }

        if (animationFillMode != null)
        {
            action(nameof(animationFillMode), animationFillMode);
        }

        if (animationIterationCount != null)
        {
            action(nameof(animationIterationCount), animationIterationCount);
        }

        if (animationName != null)
        {
            action(nameof(animationName), animationName);
        }

        if (animationPlayState != null)
        {
            action(nameof(animationPlayState), animationPlayState);
        }

        if (animationTimingFunction != null)
        {
            action(nameof(animationTimingFunction), animationTimingFunction);
        }

        if (backdropFilter != null)
        {
            action(nameof(backdropFilter), backdropFilter);
        }

        if (backfaceVisibility != null)
        {
            action(nameof(backfaceVisibility), backfaceVisibility);
        }

        if (background != null)
        {
            action(nameof(background), background);
        }

        if (backgroundAttachment != null)
        {
            action(nameof(backgroundAttachment), backgroundAttachment);
        }

        if (backgroundBlendMode != null)
        {
            action(nameof(backgroundBlendMode), backgroundBlendMode);
        }

        if (backgroundClip != null)
        {
            action(nameof(backgroundClip), backgroundClip);
        }

        if (backgroundColor != null)
        {
            action(nameof(backgroundColor), backgroundColor);
        }

        if (backgroundImage != null)
        {
            action(nameof(backgroundImage), backgroundImage);
        }

        if (backgroundOrigin != null)
        {
            action(nameof(backgroundOrigin), backgroundOrigin);
        }

        if (backgroundPosition != null)
        {
            action(nameof(backgroundPosition), backgroundPosition);
        }

        if (backgroundRepeat != null)
        {
            action(nameof(backgroundRepeat), backgroundRepeat);
        }

        if (backgroundSize != null)
        {
            action(nameof(backgroundSize), backgroundSize);
        }

        if (border != null)
        {
            action(nameof(border), border);
        }

        if (borderBottom != null)
        {
            action(nameof(borderBottom), borderBottom);
        }

        if (borderBottomColor != null)
        {
            action(nameof(borderBottomColor), borderBottomColor);
        }

        if (borderBottomLeftRadius != null)
        {
            action(nameof(borderBottomLeftRadius), borderBottomLeftRadius);
        }

        if (borderBottomRightRadius != null)
        {
            action(nameof(borderBottomRightRadius), borderBottomRightRadius);
        }

        if (borderBottomStyle != null)
        {
            action(nameof(borderBottomStyle), borderBottomStyle);
        }

        if (borderBottomWidth != null)
        {
            action(nameof(borderBottomWidth), borderBottomWidth);
        }

        if (borderCollapse != null)
        {
            action(nameof(borderCollapse), borderCollapse);
        }

        if (borderColor != null)
        {
            action(nameof(borderColor), borderColor);
        }

        if (borderImage != null)
        {
            action(nameof(borderImage), borderImage);
        }

        if (borderImageOutset != null)
        {
            action(nameof(borderImageOutset), borderImageOutset);
        }

        if (borderImageRepeat != null)
        {
            action(nameof(borderImageRepeat), borderImageRepeat);
        }

        if (borderImageSlice != null)
        {
            action(nameof(borderImageSlice), borderImageSlice);
        }

        if (borderImageSource != null)
        {
            action(nameof(borderImageSource), borderImageSource);
        }

        if (borderImageWidth != null)
        {
            action(nameof(borderImageWidth), borderImageWidth);
        }

        if (borderLeft != null)
        {
            action(nameof(borderLeft), borderLeft);
        }

        if (borderLeftColor != null)
        {
            action(nameof(borderLeftColor), borderLeftColor);
        }

        if (borderLeftStyle != null)
        {
            action(nameof(borderLeftStyle), borderLeftStyle);
        }

        if (borderLeftWidth != null)
        {
            action(nameof(borderLeftWidth), borderLeftWidth);
        }

        if (borderRadius != null)
        {
            action(nameof(borderRadius), borderRadius);
        }

        if (borderRight != null)
        {
            action(nameof(borderRight), borderRight);
        }

        if (borderRightColor != null)
        {
            action(nameof(borderRightColor), borderRightColor);
        }

        if (borderRightStyle != null)
        {
            action(nameof(borderRightStyle), borderRightStyle);
        }

        if (borderRightWidth != null)
        {
            action(nameof(borderRightWidth), borderRightWidth);
        }

        if (borderSpacing != null)
        {
            action(nameof(borderSpacing), borderSpacing);
        }

        if (borderStyle != null)
        {
            action(nameof(borderStyle), borderStyle);
        }

        if (borderTop != null)
        {
            action(nameof(borderTop), borderTop);
        }

        if (borderTopColor != null)
        {
            action(nameof(borderTopColor), borderTopColor);
        }

        if (borderTopLeftRadius != null)
        {
            action(nameof(borderTopLeftRadius), borderTopLeftRadius);
        }

        if (borderTopRightRadius != null)
        {
            action(nameof(borderTopRightRadius), borderTopRightRadius);
        }

        if (borderTopStyle != null)
        {
            action(nameof(borderTopStyle), borderTopStyle);
        }

        if (borderTopWidth != null)
        {
            action(nameof(borderTopWidth), borderTopWidth);
        }

        if (borderWidth != null)
        {
            action(nameof(borderWidth), borderWidth);
        }

        if (bottom != null)
        {
            action(nameof(bottom), bottom);
        }

        if (boxDecorationBreak != null)
        {
            action(nameof(boxDecorationBreak), boxDecorationBreak);
        }

        if (boxShadow != null)
        {
            action(nameof(boxShadow), boxShadow);
        }

        if (boxSizing != null)
        {
            action(nameof(boxSizing), boxSizing);
        }

        if (captionSide != null)
        {
            action(nameof(captionSide), captionSide);
        }

        if (clear != null)
        {
            action(nameof(clear), clear);
        }

        if (clip != null)
        {
            action(nameof(clip), clip);
        }

        if (clipPath != null)
        {
            action(nameof(clipPath), clipPath);
        }

        if (color != null)
        {
            action(nameof(color), color);
        }

        if (columns != null)
        {
            action(nameof(columns), columns);
        }

        if (columnCount != null)
        {
            action(nameof(columnCount), columnCount);
        }

        if (columnFill != null)
        {
            action(nameof(columnFill), columnFill);
        }

        if (columnGap != null)
        {
            action(nameof(columnGap), columnGap);
        }

        if (rowGap != null)
        {
            action(nameof(rowGap), rowGap);
        }

        if (gap != null)
        {
            action(nameof(gap), gap);
        }

        if (columnRule != null)
        {
            action(nameof(columnRule), columnRule);
        }

        if (columnRuleColor != null)
        {
            action(nameof(columnRuleColor), columnRuleColor);
        }

        if (columnRuleStyle != null)
        {
            action(nameof(columnRuleStyle), columnRuleStyle);
        }

        if (columnRuleWidth != null)
        {
            action(nameof(columnRuleWidth), columnRuleWidth);
        }

        if (columnSpan != null)
        {
            action(nameof(columnSpan), columnSpan);
        }

        if (columnWidth != null)
        {
            action(nameof(columnWidth), columnWidth);
        }

        if (content != null)
        {
            action(nameof(content), content);
        }

        if (contentVisibility != null)
        {
            action(nameof(contentVisibility), contentVisibility);
        }

        if (counterIncrement != null)
        {
            action(nameof(counterIncrement), counterIncrement);
        }

        if (counterReset != null)
        {
            action(nameof(counterReset), counterReset);
        }

        if (cssFloat != null)
        {
            action(nameof(cssFloat), cssFloat);
        }

        if (cssText != null)
        {
            action(nameof(cssText), cssText);
        }

        if (cursor != null)
        {
            action(nameof(cursor), cursor);
        }

        if (direction != null)
        {
            action(nameof(direction), direction);
        }

        if (display != null)
        {
            action(nameof(display), display);
        }

        if (dominantBaseline != null)
        {
            action(nameof(dominantBaseline), dominantBaseline);
        }

        if (emptyCells != null)
        {
            action(nameof(emptyCells), emptyCells);
        }

        if (fill != null)
        {
            action(nameof(fill), fill);
        }

        if (fillOpacity != null)
        {
            action(nameof(fillOpacity), fillOpacity);
        }

        if (fillRule != null)
        {
            action(nameof(fillRule), fillRule);
        }

        if (filter != null)
        {
            action(nameof(filter), filter);
        }

        if (flex != null)
        {
            action(nameof(flex), flex);
        }

        if (flexBasis != null)
        {
            action(nameof(flexBasis), flexBasis);
        }

        if (flexDirection != null)
        {
            action(nameof(flexDirection), flexDirection);
        }

        if (flexFlow != null)
        {
            action(nameof(flexFlow), flexFlow);
        }

        if (flexGrow != null)
        {
            action(nameof(flexGrow), flexGrow);
        }

        if (flexShrink != null)
        {
            action(nameof(flexShrink), flexShrink);
        }

        if (flexWrap != null)
        {
            action(nameof(flexWrap), flexWrap);
        }

        if (floodColor != null)
        {
            action(nameof(floodColor), floodColor);
        }

        if (floodOpacity != null)
        {
            action(nameof(floodOpacity), floodOpacity);
        }

        if (font != null)
        {
            action(nameof(font), font);
        }

        if (fontFamily != null)
        {
            action(nameof(fontFamily), fontFamily);
        }

        if (fontFeatureSettings != null)
        {
            action(nameof(fontFeatureSettings), fontFeatureSettings);
        }

        if (fontKerning != null)
        {
            action(nameof(fontKerning), fontKerning);
        }

        if (fontLanguageOverride != null)
        {
            action(nameof(fontLanguageOverride), fontLanguageOverride);
        }

        if (fontSize != null)
        {
            action(nameof(fontSize), fontSize);
        }

        if (fontSizeAdjust != null)
        {
            action(nameof(fontSizeAdjust), fontSizeAdjust);
        }

        if (fontStretch != null)
        {
            action(nameof(fontStretch), fontStretch);
        }

        if (fontStyle != null)
        {
            action(nameof(fontStyle), fontStyle);
        }

        if (fontSynthesis != null)
        {
            action(nameof(fontSynthesis), fontSynthesis);
        }

        if (fontVariant != null)
        {
            action(nameof(fontVariant), fontVariant);
        }

        if (fontVariantAlternates != null)
        {
            action(nameof(fontVariantAlternates), fontVariantAlternates);
        }

        if (fontVariantCaps != null)
        {
            action(nameof(fontVariantCaps), fontVariantCaps);
        }

        if (fontVariantEastAsian != null)
        {
            action(nameof(fontVariantEastAsian), fontVariantEastAsian);
        }

        if (fontVariantLigatures != null)
        {
            action(nameof(fontVariantLigatures), fontVariantLigatures);
        }

        if (fontVariantNumeric != null)
        {
            action(nameof(fontVariantNumeric), fontVariantNumeric);
        }

        if (fontVariantPosition != null)
        {
            action(nameof(fontVariantPosition), fontVariantPosition);
        }

        if (fontWeight != null)
        {
            action(nameof(fontWeight), fontWeight);
        }

        if (grid != null)
        {
            action(nameof(grid), grid);
        }

        if (gridArea != null)
        {
            action(nameof(gridArea), gridArea);
        }

        if (gridAutoColumns != null)
        {
            action(nameof(gridAutoColumns), gridAutoColumns);
        }

        if (gridAutoFlow != null)
        {
            action(nameof(gridAutoFlow), gridAutoFlow);
        }

        if (gridAutoPosition != null)
        {
            action(nameof(gridAutoPosition), gridAutoPosition);
        }

        if (gridAutoRows != null)
        {
            action(nameof(gridAutoRows), gridAutoRows);
        }

        if (gridColumn != null)
        {
            action(nameof(gridColumn), gridColumn);
        }

        if (gridColumnStart != null)
        {
            action(nameof(gridColumnStart), gridColumnStart);
        }

        if (gridColumnEnd != null)
        {
            action(nameof(gridColumnEnd), gridColumnEnd);
        }

        if (gridRow != null)
        {
            action(nameof(gridRow), gridRow);
        }

        if (gridRowStart != null)
        {
            action(nameof(gridRowStart), gridRowStart);
        }

        if (gridRowEnd != null)
        {
            action(nameof(gridRowEnd), gridRowEnd);
        }

        if (gridTemplate != null)
        {
            action(nameof(gridTemplate), gridTemplate);
        }

        if (gridTemplateAreas != null)
        {
            action(nameof(gridTemplateAreas), gridTemplateAreas);
        }

        if (gridTemplateRows != null)
        {
            action(nameof(gridTemplateRows), gridTemplateRows);
        }

        if (gridTemplateColumns != null)
        {
            action(nameof(gridTemplateColumns), gridTemplateColumns);
        }

        if (height != null)
        {
            action(nameof(height), height);
        }

        if (hyphens != null)
        {
            action(nameof(hyphens), hyphens);
        }

        if (icon != null)
        {
            action(nameof(icon), icon);
        }

        if (imageRendering != null)
        {
            action(nameof(imageRendering), imageRendering);
        }

        if (imageResolution != null)
        {
            action(nameof(imageResolution), imageResolution);
        }

        if (imageOrientation != null)
        {
            action(nameof(imageOrientation), imageOrientation);
        }

        if (imeMode != null)
        {
            action(nameof(imeMode), imeMode);
        }

        if (justifyContent != null)
        {
            action(nameof(justifyContent), justifyContent);
        }

        if (left != null)
        {
            action(nameof(left), left);
        }

        if (letterSpacing != null)
        {
            action(nameof(letterSpacing), letterSpacing);
        }

        if (lightingColor != null)
        {
            action(nameof(lightingColor), lightingColor);
        }

        if (lineHeight != null)
        {
            action(nameof(lineHeight), lineHeight);
        }

        if (listStyle != null)
        {
            action(nameof(listStyle), listStyle);
        }

        if (listStyleImage != null)
        {
            action(nameof(listStyleImage), listStyleImage);
        }

        if (listStylePosition != null)
        {
            action(nameof(listStylePosition), listStylePosition);
        }

        if (listStyleType != null)
        {
            action(nameof(listStyleType), listStyleType);
        }

        if (margin != null)
        {
            action(nameof(margin), margin);
        }

        if (marginBottom != null)
        {
            action(nameof(marginBottom), marginBottom);
        }

        if (marginLeft != null)
        {
            action(nameof(marginLeft), marginLeft);
        }

        if (marginRight != null)
        {
            action(nameof(marginRight), marginRight);
        }

        if (marginTop != null)
        {
            action(nameof(marginTop), marginTop);
        }

        if (marks != null)
        {
            action(nameof(marks), marks);
        }

        if (mask != null)
        {
            action(nameof(mask), mask);
        }

        if (maskType != null)
        {
            action(nameof(maskType), maskType);
        }

        if (maxHeight != null)
        {
            action(nameof(maxHeight), maxHeight);
        }

        if (maxWidth != null)
        {
            action(nameof(maxWidth), maxWidth);
        }

        if (minHeight != null)
        {
            action(nameof(minHeight), minHeight);
        }

        if (minWidth != null)
        {
            action(nameof(minWidth), minWidth);
        }

        if (mixBlendMode != null)
        {
            action(nameof(mixBlendMode), mixBlendMode);
        }

        if (navDown != null)
        {
            action(nameof(navDown), navDown);
        }

        if (navIndex != null)
        {
            action(nameof(navIndex), navIndex);
        }

        if (navLeft != null)
        {
            action(nameof(navLeft), navLeft);
        }

        if (navRight != null)
        {
            action(nameof(navRight), navRight);
        }

        if (navUp != null)
        {
            action(nameof(navUp), navUp);
        }

        if (objectFit != null)
        {
            action(nameof(objectFit), objectFit);
        }

        if (objectPosition != null)
        {
            action(nameof(objectPosition), objectPosition);
        }

        if (opacity != null)
        {
            action(nameof(opacity), opacity);
        }

        if (order != null)
        {
            action(nameof(order), order);
        }

        if (orphans != null)
        {
            action(nameof(orphans), orphans);
        }

        if (outline != null)
        {
            action(nameof(outline), outline);
        }

        if (outlineColor != null)
        {
            action(nameof(outlineColor), outlineColor);
        }

        if (outlineOffset != null)
        {
            action(nameof(outlineOffset), outlineOffset);
        }

        if (outlineStyle != null)
        {
            action(nameof(outlineStyle), outlineStyle);
        }

        if (outlineWidth != null)
        {
            action(nameof(outlineWidth), outlineWidth);
        }

        if (overflow != null)
        {
            action(nameof(overflow), overflow);
        }

        if (overflowWrap != null)
        {
            action(nameof(overflowWrap), overflowWrap);
        }

        if (overflowX != null)
        {
            action(nameof(overflowX), overflowX);
        }

        if (overflowY != null)
        {
            action(nameof(overflowY), overflowY);
        }

        if (overflowClipBox != null)
        {
            action(nameof(overflowClipBox), overflowClipBox);
        }

        if (padding != null)
        {
            action(nameof(padding), padding);
        }

        if (paddingBottom != null)
        {
            action(nameof(paddingBottom), paddingBottom);
        }

        if (paddingLeft != null)
        {
            action(nameof(paddingLeft), paddingLeft);
        }

        if (paddingRight != null)
        {
            action(nameof(paddingRight), paddingRight);
        }

        if (paddingTop != null)
        {
            action(nameof(paddingTop), paddingTop);
        }

        if (pageBreakAfter != null)
        {
            action(nameof(pageBreakAfter), pageBreakAfter);
        }

        if (pageBreakBefore != null)
        {
            action(nameof(pageBreakBefore), pageBreakBefore);
        }

        if (pageBreakInside != null)
        {
            action(nameof(pageBreakInside), pageBreakInside);
        }

        if (perspective != null)
        {
            action(nameof(perspective), perspective);
        }

        if (perspectiveOrigin != null)
        {
            action(nameof(perspectiveOrigin), perspectiveOrigin);
        }

        if (pointerEvents != null)
        {
            action(nameof(pointerEvents), pointerEvents);
        }

        if (position != null)
        {
            action(nameof(position), position);
        }

        if (quotes != null)
        {
            action(nameof(quotes), quotes);
        }

        if (resize != null)
        {
            action(nameof(resize), resize);
        }

        if (right != null)
        {
            action(nameof(right), right);
        }

        if (tableLayout != null)
        {
            action(nameof(tableLayout), tableLayout);
        }

        if (tabSize != null)
        {
            action(nameof(tabSize), tabSize);
        }

        if (textAlign != null)
        {
            action(nameof(textAlign), textAlign);
        }

        if (textAlignLast != null)
        {
            action(nameof(textAlignLast), textAlignLast);
        }

        if (textCombineHorizontal != null)
        {
            action(nameof(textCombineHorizontal), textCombineHorizontal);
        }

        if (textDecoration != null)
        {
            action(nameof(textDecoration), textDecoration);
        }

        if (textDecorationColor != null)
        {
            action(nameof(textDecorationColor), textDecorationColor);
        }

        if (textDecorationLine != null)
        {
            action(nameof(textDecorationLine), textDecorationLine);
        }

        if (textDecorationStyle != null)
        {
            action(nameof(textDecorationStyle), textDecorationStyle);
        }

        if (textIndent != null)
        {
            action(nameof(textIndent), textIndent);
        }

        if (textOrientation != null)
        {
            action(nameof(textOrientation), textOrientation);
        }

        if (textOverflow != null)
        {
            action(nameof(textOverflow), textOverflow);
        }

        if (textRendering != null)
        {
            action(nameof(textRendering), textRendering);
        }

        if (textShadow != null)
        {
            action(nameof(textShadow), textShadow);
        }

        if (textTransform != null)
        {
            action(nameof(textTransform), textTransform);
        }

        if (textUnderlinePosition != null)
        {
            action(nameof(textUnderlinePosition), textUnderlinePosition);
        }

        if (top != null)
        {
            action(nameof(top), top);
        }

        if (touchAction != null)
        {
            action(nameof(touchAction), touchAction);
        }

        if (transform != null)
        {
            action(nameof(transform), transform);
        }

        if (transformOrigin != null)
        {
            action(nameof(transformOrigin), transformOrigin);
        }

        if (transformStyle != null)
        {
            action(nameof(transformStyle), transformStyle);
        }

        if (transition != null)
        {
            action(nameof(transition), transition);
        }

        if (transitionDelay != null)
        {
            action(nameof(transitionDelay), transitionDelay);
        }

        if (transitionDuration != null)
        {
            action(nameof(transitionDuration), transitionDuration);
        }

        if (transitionProperty != null)
        {
            action(nameof(transitionProperty), transitionProperty);
        }

        if (transitionTimingFunction != null)
        {
            action(nameof(transitionTimingFunction), transitionTimingFunction);
        }

        if (unicodeBidi != null)
        {
            action(nameof(unicodeBidi), unicodeBidi);
        }

        if (unicodeRange != null)
        {
            action(nameof(unicodeRange), unicodeRange);
        }

        if (verticalAlign != null)
        {
            action(nameof(verticalAlign), verticalAlign);
        }

        if (visibility != null)
        {
            action(nameof(visibility), visibility);
        }

        if (whiteSpace != null)
        {
            action(nameof(whiteSpace), whiteSpace);
        }

        if (widows != null)
        {
            action(nameof(widows), widows);
        }

        if (width != null)
        {
            action(nameof(width), width);
        }

        if (willChange != null)
        {
            action(nameof(willChange), willChange);
        }

        if (wordBreak != null)
        {
            action(nameof(wordBreak), wordBreak);
        }

        if (wordSpacing != null)
        {
            action(nameof(wordSpacing), wordSpacing);
        }

        if (wordWrap != null)
        {
            action(nameof(wordWrap), wordWrap);
        }

        if (writingMode != null)
        {
            action(nameof(writingMode), writingMode);
        }

        if (zIndex != null)
        {
            action(nameof(zIndex), zIndex);
        }

        if (borderInlineStyle != null)
        {
            action(nameof(borderInlineStyle), borderInlineStyle);
        }

        if (webkitBackgroundClip != null)
        {
            action("WebkitBackgroundClip", webkitBackgroundClip);
        }

        if (webkitTextFillColor != null)
        {
            action("WebkitTextFillColor", webkitTextFillColor);
        }

        if (webkitFontSmoothing != null)
        {
            action("WebkitFontSmoothing", webkitFontSmoothing);
        }

        if (mozOsxFontSmoothing != null)
        {
            action("MozOsxFontSmoothing", mozOsxFontSmoothing);
        }
    }
}