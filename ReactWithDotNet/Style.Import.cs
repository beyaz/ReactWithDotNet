using System.Text.Json.Serialization;

namespace ReactWithDotNet;

partial class Style
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public bool IsEmpty => isEmpty(this);
    
    
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

            return getByName(this, key.Replace("-", string.Empty));
        }
        set
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            setByName(this,key.Replace("-", string.Empty), value);
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

        if (newStyle.backdropFilter != null)
        {
            backdropFilter = newStyle.backdropFilter;
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

        if (newStyle.webkitBackgroundClip != null)
        {
            webkitBackgroundClip = newStyle.webkitBackgroundClip;
        }

        if (newStyle.webkitTextFillColor != null)
        {
            webkitTextFillColor = newStyle.webkitTextFillColor;
        }

        if (newStyle.webkitFontSmoothing != null)
        {
            webkitFontSmoothing = newStyle.webkitFontSmoothing;
        }

        if (newStyle.mozOsxFontSmoothing != null)
        {
            mozOsxFontSmoothing = newStyle.mozOsxFontSmoothing;
        }

        if (newStyle._hover is not null)
        {
            hover.Import(newStyle._hover);
        }

        if (newStyle._before is not null)
        {
            before.Import(newStyle._before);
        }

        if (newStyle._after is not null)
        {
            after.Import(newStyle._after);
        }

        if (newStyle._active is not null)
        {
            active.Import(newStyle._active);
        }

        if (newStyle._focus is not null)
        {
            focus.Import(newStyle._focus);
        }

        if (newStyle._mediaQueries is not null && newStyle._mediaQueries.Count > 0)
        {
            foreach (var mediaQuery in newStyle._mediaQueries)
            {
                MediaQueries.Add(new MediaQuery(mediaQuery.Query, mediaQuery.Style.Clone()));    
            }
        }
    }
}