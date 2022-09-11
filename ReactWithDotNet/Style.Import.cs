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