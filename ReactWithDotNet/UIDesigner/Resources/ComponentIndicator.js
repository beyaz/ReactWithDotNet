function canApplyHoverEffect(targetElement)
{
    if (targetElement === document.body)
    {
        return false;
    }

    if (targetElement.parentElement === document.body)
    {
        return false;
    }

    if (targetElement.tagName === "HTML")
    {
        return false;
    }

    return !(

        targetElement === sizeIndicatorBoxElement ||
        targetElement === overlayElement

        // padding
        || targetElement === paddingLeftIndicatorLineElement
        || targetElement === paddingLeftIndicatorBoxElement

        || targetElement === paddingRightIndicatorLineElement
        || targetElement === paddingRightIndicatorBoxElement

        || targetElement === paddingTopIndicatorLineElement
        || targetElement === paddingTopIndicatorBoxElement

        || targetElement === paddingBottomIndicatorLineElement
        || targetElement === paddingBottomIndicatorBoxElement

        // margin
        || targetElement === marginLeftIndicatorLineElement
        || targetElement === marginLeftIndicatorBoxElement

        || targetElement === marginRightIndicatorLineElement
        || targetElement === marginRightIndicatorBoxElement

        || targetElement === marginTopIndicatorLineElement
        || targetElement === marginTopIndicatorBoxElement

        || targetElement === marginBottomIndicatorLineElement
        || targetElement === marginBottomIndicatorBoxElement
    );
}

function NumberToString(number) 
{
    if (Number.isInteger(number)) 
    {
        return number.toString();
    } 
    else 
    {
        return removeFromEnd( removeFromEnd(number.toFixed(2), '.00'), '0');
    }
}

function removeFromEnd(str, suffix) 
{
  if (str.endsWith(suffix)) 
  {
    return str.slice(0, -suffix.length);
  }
  
  return str;
}

function getMediaQueryStyle(element, propertyName)
{
    const stylesheets = Array.from(document.styleSheets);

    for (const stylesheet of stylesheets)
    {
        try
        {
            const rules = stylesheet.cssRules;

            for (const rule of rules)
            {
                if (rule.media && window.matchMedia(rule.media.mediaText).matches)
                {
                    // Check if media query applies to current viewport
                    const styleRules = rule.cssRules || rule.rules;

                    for (const styleRule of styleRules)
                    {
                        for (let i = 0; i < element.classList.length; i++)
                        {
                            if ('.' + element.classList[i] === styleRule.selectorText)
                            {
                                if (propertyName === 'width')
                                {
                                    return styleRule.style.width;    
                                }
                                if (propertyName === 'height')
                                {
                                    return styleRule.style.height;
                                }

                                // padding
                                {
                                    if (propertyName === 'paddingLeft')
                                    {
                                        return styleRule.style.paddingLeft;
                                    }
                                    if (propertyName === 'paddingRight')
                                    {
                                        return styleRule.style.paddingRight;
                                    }
                                    if (propertyName === 'paddingTop')
                                    {
                                        return styleRule.style.paddingTop;
                                    }
                                    if (propertyName === 'paddingBottom')
                                    {
                                        return styleRule.style.paddingBottom;
                                    }
                                }

                                // margin
                                {
                                    if (propertyName === 'marginLeft')
                                    {
                                        return styleRule.style.marginLeft;
                                    }
                                    if (propertyName === 'marginRight')
                                    {
                                        return styleRule.style.marginRight;
                                    }
                                    if (propertyName === 'marginTop')
                                    {
                                        return styleRule.style.marginTop;
                                    }
                                    if (propertyName === 'marginBottom')
                                    {
                                        return styleRule.style.marginBottom;
                                    }
                                }
                                console.log("NotImplemented:" + propertyName);
                            }
                        }
                    }
                }
            }
        }
        catch (e)
        {
        }
    }
    
    return null;
}

function GetStyleValue(element, propertyName)
{
    let value = element.style[propertyName] + '';

    let calculatedValue = getComputedStyle(element)[propertyName] + '';

    let matchedMediaQueryValue = getMediaQueryStyle(element, propertyName);

    let valueArray =
    [
        'auto', 'fit-content', 'max-content',
        'min-content', '-webkit-fill-available',
        'inherit', 'initial', 'revert',
        'revert-layer', 'unset'
    ];

    if (matchedMediaQueryValue)
    {
        if (valueArray.indexOf(matchedMediaQueryValue) >= 0 || matchedMediaQueryValue.indexOf('%') > 0)
        {
            return matchedMediaQueryValue;
        }
    }

    if (valueArray.indexOf(calculatedValue) >= 0 || calculatedValue.indexOf('%') > 0)
    {
        return calculatedValue;
    }

    if (valueArray.indexOf(value) >= 0 || value.indexOf('%') > 0)
    {
        return value;
    }

    if (value.indexOf('px') > 0)
    {
        return value;
    }

    return '';
}

let sizeIndicatorBoxElement = null;

let overlayElement = null;

// padding
let paddingLeftIndicatorLineElement = null;
let paddingLeftIndicatorBoxElement = null;

let paddingRightIndicatorLineElement = null;
let paddingRightIndicatorBoxElement = null;

let paddingTopIndicatorLineElement = null;
let paddingTopIndicatorBoxElement = null;

let paddingBottomIndicatorLineElement = null;
let paddingBottomIndicatorBoxElement = null;


// margin
let marginLeftIndicatorLineElement = null;
let marginLeftIndicatorBoxElement = null;

let marginRightIndicatorLineElement = null;
let marginRightIndicatorBoxElement = null;

let marginTopIndicatorLineElement = null;
let marginTopIndicatorBoxElement = null;

let marginBottomIndicatorLineElement = null;
let marginBottomIndicatorBoxElement = null;

const SpacingDivs =
{
    clear: [],

    dirty: [],

    clearSpacingDivs: function ()
    {
        while(SpacingDivs.dirty.length > 0)
        {
            const div = SpacingDivs.dirty.pop();

            div.style.display = 'none';

            SpacingDivs.clear.push(div);
        }
    },
    
    getNewSpacingDiv: function()
    {
        if (SpacingDivs.clear.length > 0)
        {
            const div = SpacingDivs.clear.pop();

            SpacingDivs.dirty.push(div);

            div.style.display = 'block';

            return div;
        }

        // create new div
        {
            const div = document.createElement('div');
            div.style.display = 'block';
            div.style.position = 'fixed';
            div.style.zIndex = 999;
            div.style.color = div.style.background = '#538fe3';
            div.style.pointerEvents = 'none';  
            document.body.appendChild(div);

            SpacingDivs.dirty.push(div);

            return div;
        }
    },

    showAllSpacings: function (container) 
    {
        const containerComputedStyle = getComputedStyle(container);

        const containerIsFlexRow = containerComputedStyle.display === 'flex' && (containerComputedStyle.flexDirection === 'row' || containerComputedStyle.flexDirection === 'row-reverse');

        const containerIsFlexColumn = containerComputedStyle.display === 'flex' && (containerComputedStyle.flexDirection === 'column' || containerComputedStyle.flexDirection === 'column-reverse');

        if (!(containerIsFlexRow || containerIsFlexColumn))
        {
            return;
        }

        const children = container.children;

        const containerRect = container.getBoundingClientRect();

        let length = children.length - 1;
        if (length > 50)
        {
            length = 50;
        }

        for (let i = 0; i < length; i++) 
        {
            const first = children[i];
            const second = children[i + 1];

            const firstRect = first.getBoundingClientRect();
            const secondRect = second.getBoundingClientRect();

            if (containerIsFlexRow)
            {
                const spacing = secondRect.x - firstRect.x - firstRect.width;
                if (spacing <= 0) 
                {
                    continue;
                }

                // line
                {
                    const line = SpacingDivs.getNewSpacingDiv();

                    const style = line.style;

                    style.left   = `${firstRect.x + firstRect.width}px`;
                    style.top    = `${containerRect.top + containerRect.height/2}px`;
                    style.height = '0.5px';
                    style.width  = `${NumberToString(spacing)}px`;
                }

                // label
                {
                    const label = SpacingDivs.getNewSpacingDiv();
                
                    label.textContent = `${Math.round(spacing)}`;

                    const style = label.style;

                    // text style
                    style.fontSize     = '8px';
                    style.fontWeight   = '600';
                    style.width        = 'fit-content';
                    style.height       = 'fit-content';
                    style.lineHeight   = '12px';
                    style.textAlign    = 'center';
                    style.background   = 'aliceblue';
                    style.borderRadius = '4px';

                    style.left = `${firstRect.x + firstRect.width + spacing / 2 - (label.getBoundingClientRect().width / 2) }px`;
                    style.top  = `${containerRect.top + containerRect.height/2 - (label.getBoundingClientRect().height / 2)}px`;
                }
            }

            if (containerIsFlexColumn)
            {
                const spacing = secondRect.y - firstRect.y - firstRect.height;
                if (spacing <= 0) 
                {
                    continue;
                }

                // line
                {
                    const line = SpacingDivs.getNewSpacingDiv();

                    const style = line.style;

                    style.top = `${firstRect.y + firstRect.height}px`;
                    style.left = `${containerRect.x + containerRect.width / 2}px`;
                    style.width = '0.5px';
                    style.height = `${NumberToString(spacing)}px`;
                }

                // label
                {
                    const label = SpacingDivs.getNewSpacingDiv();

                    label.textContent = `${Math.round(spacing)}`;

                    const style = label.style;

                    // text style
                    style.fontSize     = '8px';
                    style.fontWeight   = '600';
                    style.width        = 'fit-content';
                    style.height       = 'fit-content';
                    style.lineHeight   = '12px';
                    style.textAlign    = 'center';
                    style.background   = 'aliceblue';
                    style.borderRadius = '4px';        

                    style.left = `${containerRect.x + containerRect.width / 2  - (label.getBoundingClientRect().width / 2) }px`;
                    style.top =  `${firstRect.y + firstRect.height + spacing / 2 - (label.getBoundingClientRect().height / 2)}px`;
                }
            }
        }
    }
}

function applyBackgroundEffect(targetElement, level)
{
    const computedStyle = getComputedStyle(targetElement);

    const rect = targetElement.getBoundingClientRect();

    const lineColor = level === 1 ? '#fde047' : '#47fdf5';

    const lineDegree = level === 1 ? 45 : -45;

    const linearGradientValueForOverlayBackground = `repeating-linear-gradient(${lineDegree}deg, ${lineColor} 0, ${lineColor} 1px, transparent 0, transparent 50%)`;
    const backgroundSizeValueForOverlayBackground = '5px 5px';

    if(targetElement.tagName.toUpperCase() === 'IMG' || targetElement.tagName.toUpperCase() === 'SVG')
    {
        // overlay on element
        
        if (overlayElement === null)
        {
            overlayElement = document.createElement('div');
            document.body.appendChild(overlayElement);

            overlayElement.style.position = 'fixed';
            overlayElement.style.outline = '1px dashed #bfdbfe';
            overlayElement.style.backgroundImage = linearGradientValueForOverlayBackground;
            overlayElement.style.backgroundSize = backgroundSizeValueForOverlayBackground;
            overlayElement.style.zIndex = '999999999';
            overlayElement.style.pointerEvents = 'none';
        }

        overlayElement.style.display = 'block';

        // align
        {
            overlayElement.style.width = rect.width + 'px';
            overlayElement.style.height = rect.height + 'px';
            overlayElement.style.left = rect.left + 'px';
            overlayElement.style.top = rect.top + 'px';
        }
    }
    else // apply background
    {
        targetElement.outlineReal = targetElement.style.outline;
        targetElement.backgroundImageReal = targetElement.style.backgroundImage
        targetElement.backgroundSizeReal = targetElement.style.backgroundSize;
        targetElement.backgroundReal = targetElement.style.background;
        targetElement.backgroundColorReal = targetElement.style.backgroundColor;
        
        targetElement.style.outline = '1px dashed #4597F7';
        
        const hasNoBackgroundImage = 
            targetElement.style.backgroundImage === 'none' || 
            targetElement.style.backgroundImage === 'initial' ||
            targetElement.style.backgroundImage === '' ||
            targetElement.style.backgroundImage == null;
        
        if (!hasNoBackgroundImage)
        {
            targetElement.style.backgroundImage = linearGradientValueForOverlayBackground + "," + targetElement.style.backgroundImage;
            targetElement.style.backgroundSize = backgroundSizeValueForOverlayBackground + ', cover';
        }
        else
        {
            if (computedStyle.background === '')
            {
                targetElement.style.backgroundImage = linearGradientValueForOverlayBackground;
                targetElement.style.backgroundSize = backgroundSizeValueForOverlayBackground;
            }
            else 
            {
                targetElement.style.background = linearGradientValueForOverlayBackground + "," + computedStyle.background;
                targetElement.style.backgroundSize = backgroundSizeValueForOverlayBackground;
            }
        }

       
        if (level === 1)
        {        
            for (var i = 0; i < targetElement.children.length; i++)
            {
                const child = targetElement.children[i];

                applyBackgroundEffect(child, 2);
            }

            SpacingDivs.showAllSpacings(targetElement);
        }
    }
}

let topLeftCircle = null;
let topRightCircle = null;
let bottomLeftCircle = null;
let bottomRightCircle = null;

/**
 * @param {HTMLElement} targetElement
 */
function applyCirclesOnCorners(targetElement)
{
    const circleSize = 4;

    const rect = targetElement.getBoundingClientRect();

    arrangeTopLeft((topLeftCircle = topLeftCircle ?? createCircle()));
    arrangeTopRight((topRightCircle = topRightCircle ?? createCircle()));
    arrangeBottomLeft((bottomLeftCircle = bottomLeftCircle ?? createCircle()));
    arrangeBottomRight((bottomRightCircle = bottomRightCircle ?? createCircle()));
    

    function arrangeTopLeft(circleElement)
    {
        circleElement.style.left = rect.left - circleSize/2 + 'px';
        circleElement.style.top = rect.top - circleSize/2 + 'px';
    }

    function arrangeTopRight(circleElement)
    {
        circleElement.style.left = rect.right - circleSize/2 + 'px';
        circleElement.style.top = rect.top - circleSize/2 + 'px';
    }

    function arrangeBottomLeft(circleElement)
    {
        circleElement.style.left = rect.left - circleSize/2 + 'px';
        circleElement.style.top = rect.top + rect.height - circleSize/2 + 'px';
    }

    function arrangeBottomRight(circleElement)
    {
        circleElement.style.left = rect.right - circleSize/2 + 'px';
        circleElement.style.top = rect.top + rect.height - circleSize/2 + 'px';
    }

    function createCircle()
    {
        var circleElement = document.createElement('div');

        document.body.appendChild(circleElement);

        circleElement.style.display = 'block';
        circleElement.style.position = 'fixed';
        circleElement.style.zIndex = '999999';
        circleElement.style.borderRadius = '8px';
        circleElement.style.background = 'white';
        circleElement.style.border = '0.5px solid #4597F7';
        circleElement.style.width = circleSize + 'px';
        circleElement.style.height = circleSize + 'px';

        return circleElement;
    }
}


function applyHoverEffect(targetElement)
{
    const computedStyle = getComputedStyle(targetElement);

    const rect = targetElement.getBoundingClientRect();    

    applyCirclesOnCorners(targetElement);

    // applyBackgroundEffect(targetElement, 1);
        
    // Size indicator box
    {
        if (sizeIndicatorBoxElement === null)
        {
            sizeIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(sizeIndicatorBoxElement);
        }

        sizeIndicatorBoxElement.style.display = 'block';
        sizeIndicatorBoxElement.style.position = 'fixed';
        sizeIndicatorBoxElement.style.zIndex = '999999';
        sizeIndicatorBoxElement.style.borderRadius = '3px';
        sizeIndicatorBoxElement.style.background = '#4597F7';
        sizeIndicatorBoxElement.style.color = '#DECBFC';
        sizeIndicatorBoxElement.style.fontFamily = 'monospace';

        sizeIndicatorBoxElement.innerHTML =
            "<div style = 'display: flex; flex-direction: column; line-height: 12px; padding:3px;  font-size:11px;' >" +
            "    <div style='display: flex; gap: 4px;'><span>W</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'width')) + NumberToString(rect.width) + "</div>" +
            "    <div style='display: flex; gap: 4px;'><span>H</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'height')) + NumberToString(rect.height) + "</div>" +
            "    <div style='display:flex; justify-content:center;'>" + targetElement.tagName.toLowerCase() + "</div>" +
            "</div>";

        // align
        {
            const popupRect = sizeIndicatorBoxElement.getBoundingClientRect();

            let leftPositionAsNumber = rect.left + rect.width / 2 - popupRect.width / 2;
            if (leftPositionAsNumber < 0)
            {
                leftPositionAsNumber = 0;
            }
            sizeIndicatorBoxElement.style.left = leftPositionAsNumber + 'px';

            let topPositionAsNumber = rect.bottom + 3;
            if (computedStyle.marginBottom != null && computedStyle.marginBottom.indexOf('px') > 0)
            {
                topPositionAsNumber += parseFloat(computedStyle.marginBottom.replace('px', ''));
            }
            sizeIndicatorBoxElement.style.top = topPositionAsNumber + 'px';
        }

        function WrapInSpanIfHasValue(value)
        {
            if (value == null || value === '')
            {
                return '';
            }

            if (value.indexOf('px') > 0)
            {
                value = 'fixed';
            }

            return "<span style = 'color:white;'>" + value + "</span>";
        }
    }

    // initialize elements
    {
        // padding
        {
            // left
            if (paddingLeftIndicatorLineElement === null)
            {
                paddingLeftIndicatorLineElement = document.createElement('div');
                document.body.appendChild(paddingLeftIndicatorLineElement);
            }

            if (paddingLeftIndicatorBoxElement === null)
            {
                paddingLeftIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(paddingLeftIndicatorBoxElement);
            }

            // right
            if (paddingRightIndicatorLineElement === null)
            {
                paddingRightIndicatorLineElement = document.createElement('div');
                document.body.appendChild(paddingRightIndicatorLineElement);
            }

            if (paddingRightIndicatorBoxElement === null)
            {
                paddingRightIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(paddingRightIndicatorBoxElement);
            }

            // top
            if (paddingTopIndicatorLineElement === null)
            {
                paddingTopIndicatorLineElement = document.createElement('div');
                document.body.appendChild(paddingTopIndicatorLineElement);
            }

            if (paddingTopIndicatorBoxElement === null)
            {
                paddingTopIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(paddingTopIndicatorBoxElement);
            }

            // bottom
            if (paddingBottomIndicatorLineElement === null)
            {
                paddingBottomIndicatorLineElement = document.createElement('div');
                document.body.appendChild(paddingBottomIndicatorLineElement);
            }

            if (paddingBottomIndicatorBoxElement === null)
            {
                paddingBottomIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(paddingBottomIndicatorBoxElement);
            }
        }

        // margin
        {
            // left
            if (marginLeftIndicatorLineElement === null)
            {
                marginLeftIndicatorLineElement = document.createElement('div');
                document.body.appendChild(marginLeftIndicatorLineElement);
            }

            if (marginLeftIndicatorBoxElement === null)
            {
                marginLeftIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(marginLeftIndicatorBoxElement);
            }

            // right
            if (marginRightIndicatorLineElement === null)
            {
                marginRightIndicatorLineElement = document.createElement('div');
                document.body.appendChild(marginRightIndicatorLineElement);
            }

            if (marginRightIndicatorBoxElement === null)
            {
                marginRightIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(marginRightIndicatorBoxElement);
            }

            // top
            if (marginTopIndicatorLineElement === null)
            {
                marginTopIndicatorLineElement = document.createElement('div');
                document.body.appendChild(marginTopIndicatorLineElement);
            }

            if (marginTopIndicatorBoxElement === null)
            {
                marginTopIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(marginTopIndicatorBoxElement);
            }

            // bottom
            if (marginBottomIndicatorLineElement === null)
            {
                marginBottomIndicatorLineElement = document.createElement('div');
                document.body.appendChild(marginBottomIndicatorLineElement);
            }

            if (marginBottomIndicatorBoxElement === null)
            {
                marginBottomIndicatorBoxElement = document.createElement('div');
                document.body.appendChild(marginBottomIndicatorBoxElement);
            }
        }
    }

    const zoom = GetZoom() / 100;

    // P A D D I N G
    {
        // left
        {
            let lineElement = paddingLeftIndicatorLineElement;
            let boxElement = paddingLeftIndicatorBoxElement;

            let computedValue = computedStyle.paddingLeft;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingLeft');

                    lineElement.style.height = '1px';
                    lineElement.style.width = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + 'px';
                    lineElement.style.top = rect.bottom - rect.height / 2 + 'px';
                }

                // box
                {
                    applySharedBoxStyles(boxElement.style, 'paddingLeft');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'paddingLeft');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        let positionLeftAsNumber = rect.left + computedValueAsNumber * zoom / 2 - boxRect.width / 2;
                        if (positionLeftAsNumber < 0)
                        {
                            positionLeftAsNumber = 0;
                        }
                        boxElement.style.left = positionLeftAsNumber + 'px';
                        boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2 + 'px';
                    }
                }
            }
        }

        // right
        {
            let lineElement = paddingRightIndicatorLineElement;
            let boxElement = paddingRightIndicatorBoxElement;

            let computedValue = computedStyle.paddingRight;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingRight');

                    lineElement.style.height = '1px';
                    lineElement.style.width = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + rect.width - computedValueAsNumber * zoom + 'px';
                    lineElement.style.top = rect.top + rect.height / 2 + 'px';
                }

                // box
                {
                    applySharedBoxStyles(boxElement.style, 'paddingRight');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'paddingRight');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        boxElement.style.left = rect.left + rect.width - computedValueAsNumber * zoom / 2 - boxRect.width / 2 + 'px';
                        boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2 + 'px';
                    }
                }
            }
        }

        // top
        {
            let lineElement = paddingTopIndicatorLineElement;
            let boxElement = paddingTopIndicatorBoxElement;

            let computedValue = computedStyle.paddingTop;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // box
                {
                    applySharedBoxStyles(boxElement.style, 'paddingTop');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'paddingTop');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        boxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                        boxElement.style.top = rect.top + computedValueAsNumber * zoom / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingTop');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber * zoom + 'px';
                    lineElement.style.top = rect.top + 'px';
                    lineElement.style.left = rect.left + rect.width / 2 + 'px';
                }
            }
        }

        // bottom
        {
            let lineElement = paddingBottomIndicatorLineElement;
            let boxElement = paddingBottomIndicatorBoxElement;

            let computedValue = computedStyle.paddingBottom;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // box
                {
                    applySharedBoxStyles(boxElement.style, 'paddingBottom');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'paddingBottom');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        boxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                        boxElement.style.top = rect.top + rect.height - computedValueAsNumber * zoom / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingBottom');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + rect.width / 2 + 'px';
                    lineElement.style.top = rect.top + rect.height - computedValueAsNumber * zoom + 'px';
                }
            }
        }
    }

    // M A R G I N
    {
        // Left
        {
            let lineElement = marginLeftIndicatorLineElement;
            let boxElement = marginLeftIndicatorBoxElement;

            let computedValue = computedStyle.marginLeft;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginLeft');

                    lineElement.style.height = '1px';
                    lineElement.style.width = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left - computedValueAsNumber * zoom + 'px';
                    lineElement.style.top = rect.bottom - rect.height / 2 + 'px';
                }

                // box
                {
                    applySharedBoxStyles(boxElement.style, 'marginLeft');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'marginLeft');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        let positionLeftAsNumber = rect.left - computedValueAsNumber * zoom / 2 - boxRect.width / 2;
                        if (positionLeftAsNumber < 0)
                        {
                            positionLeftAsNumber = 0;
                        }
                        boxElement.style.left = positionLeftAsNumber + 'px';
                        boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2 + 'px';
                    }
                }
            }
        }

        // right
        {
            let lineElement = marginRightIndicatorLineElement;
            let boxElement = marginRightIndicatorBoxElement;

            let computedValue = computedStyle.marginRight;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginRight');

                    lineElement.style.height = '1px';
                    lineElement.style.width = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + rect.width + 'px';
                    lineElement.style.top = rect.bottom - rect.height / 2 + 'px';
                }

                // box
                {
                    applySharedBoxStyles(boxElement.style, 'marginRight');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'marginRight');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        let positionLeftAsNumber = rect.left + rect.width + computedValueAsNumber * zoom / 2 - boxRect.width / 2;
                        if (positionLeftAsNumber < 0)
                        {
                            positionLeftAsNumber = 0;
                        }
                        boxElement.style.left = positionLeftAsNumber + 'px';
                        boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2 + 'px';
                    }
                }
            }
        }

        // top
        {
            let lineElement = marginTopIndicatorLineElement;
            let boxElement = marginTopIndicatorBoxElement;

            let computedValue = computedStyle.marginTop;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // box
                {
                    applySharedBoxStyles(boxElement.style, 'marginTop');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'marginTop');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        boxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                        boxElement.style.top = rect.top - computedValueAsNumber * zoom / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginTop');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber * zoom + 'px';
                    lineElement.style.top = rect.top - computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + rect.width / 2 + 'px';
                }
            }
        }

        // bottom
        {
            let lineElement = marginBottomIndicatorLineElement;
            let boxElement = marginBottomIndicatorBoxElement;

            let computedValue = computedStyle.marginBottom;
            let computedValueAsNumber = parseFloat(computedValue.replace('px', ''));

            if (computedValue === '' || computedValue === '0px')
            {
                lineElement.style.display = 'none';
                boxElement.style.display = 'none';
            }
            else
            {
                // box
                {
                    applySharedBoxStyles(boxElement.style, 'marginBottom');

                    // content
                    {
                        let finalInnerHTML = NumberToString(computedValueAsNumber);
                        {
                            let valueAsKeyword = GetStyleValue(targetElement, 'marginBottom');

                            if (!(valueAsKeyword == null || valueAsKeyword === '' || valueAsKeyword.indexOf('px') > 0))
                            {
                                finalInnerHTML += "<br> (" + valueAsKeyword + ")";
                            }
                        }
                        boxElement.innerHTML = finalInnerHTML;
                    }

                    // align
                    {
                        const boxRect = boxElement.getBoundingClientRect();
                        boxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                        boxElement.style.top = rect.top + rect.height + computedValueAsNumber * zoom / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginBottom');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber * zoom + 'px';
                    lineElement.style.left = rect.left + rect.width / 2 + 'px';
                    lineElement.style.top = rect.top + rect.height + 'px';
                }
            }
        }
    }

    function applySharedBoxStyles(style, targetProperty)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.zIndex = '999999';
        style.borderRadius = '3px';
        
        style.paddingLeft = '2px';
        style.paddingRight = '2px';
        style.paddingTop = '1px';
        style.paddingBottom = '1px';
        
        style.background = '#4597F7';
        style.color = '#DECBFC';
        style.fontSize = '10px';
        style.lineHeight = '10px';
        style.textAlign = 'center';
        style.fontFamily = 'monospace';
        style.pointerEvents = 'none';

        if (targetProperty.indexOf('margin') === 0)
        {
            style.background = '#EA3FB8';
        }
    }

    function applySharedLineStyles(style, targetProperty)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.pointerEvents = 'none';
        style.backgroundColor = '#4597F7';

        if (targetProperty.indexOf('margin') === 0)
        {
            style.backgroundColor = '#EA3FB8';
        }
    }
}

function tryRemoveHoverEffectFromLastIndicatedElement()
{
    if (lastIndicatedElement)
    {
        removeHoverEffect(lastIndicatedElement, 1);

        lastIndicatedElement = null;
    }
    
}
function removeHoverEffect(element, level)
{
    element.style.outline = element.outlineReal
    element.style.backgroundImage = element.backgroundImageReal;
    element.style.backgroundSize = element.backgroundSizeReal;
    element.style.background = element.backgroundReal;
    element.style.backgroundColor = element.backgroundColorReal;

    if (level === 1)
    {
        for (var i = 0; i < element.children.length; i++)
        {
            const child = element.children[i];

            removeHoverEffect(child, 2);
        }

        SpacingDivs.clearSpacingDivs();
    }
    
    
    function displayNone(element)
    {
        if(element == null)
        {
            return;
        }
        if (element.style.display === 'none')
        {
            return;
        }
        element.style.display = 'none';
    }

    displayNone(overlayElement);
    displayNone(sizeIndicatorBoxElement);

    // padding
    displayNone(paddingLeftIndicatorLineElement);
    displayNone(paddingLeftIndicatorBoxElement);

    displayNone(paddingRightIndicatorLineElement);
    displayNone(paddingRightIndicatorBoxElement);

    displayNone(paddingTopIndicatorLineElement);
    displayNone(paddingTopIndicatorBoxElement);

    displayNone(paddingBottomIndicatorLineElement);
    displayNone(paddingBottomIndicatorBoxElement);

    // margin
    displayNone(marginLeftIndicatorLineElement);
    displayNone(marginLeftIndicatorBoxElement);

    displayNone(marginRightIndicatorLineElement);
    displayNone(marginRightIndicatorBoxElement);

    displayNone(marginTopIndicatorLineElement);
    displayNone(marginTopIndicatorBoxElement);

    displayNone(marginBottomIndicatorLineElement);
    displayNone(marginBottomIndicatorBoxElement);    
}


let lastIndicatedElement = null;

document.addEventListener("mousemove", (event) => 
{
    if (event.target === document)
    {
        return;
    }
    
    let currentElement = null;
    {
        const elementsUnderMouse = document.elementsFromPoint(event.clientX, event.clientY);

        let hasFound  = false;
        for (const element of elementsUnderMouse)
        {
            if (canApplyHoverEffect(element) === false)
            {
                continue;
            }
            hasFound = true;
            currentElement = element;
            break;
        }

        if (hasFound === false)
        {
            tryRemoveHoverEffectFromLastIndicatedElement();
            return;
        }
    }

    // Check if the mouse has entered a new element
    if (currentElement !== lastIndicatedElement) 
    {
        tryRemoveHoverEffectFromLastIndicatedElement();

        lastIndicatedElement = currentElement;
            
        applyHoverEffect(lastIndicatedElement);
    }
});

document.addEventListener("scroll", () =>
{
    tryRemoveHoverEffectFromLastIndicatedElement();
});

document.addEventListener("DOMContentLoaded", () => 
{
    document.body.addEventListener("mouseout", () => 
    {
        tryRemoveHoverEffectFromLastIndicatedElement();
    });
});

function GetZoom()
{
    if (window.parent)
    {
        if (window.parent.ComponentIndicatorZoom)
        {
            return window.parent.ComponentIndicatorZoom;
        }
    }

    return 100;
}