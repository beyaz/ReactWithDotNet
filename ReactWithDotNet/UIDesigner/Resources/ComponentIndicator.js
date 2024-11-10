function canApplyHoverEffect(targetElement)
{
    if ( targetElement === sizeIndicatorBoxElement
        
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
    )
    {
        return false;
    }
    
    const style = window.getComputedStyle(targetElement);

    return style.outlineStyle === 'none';
}

function NumberToString(number) 
{
    if (Number.isInteger(number)) 
    {
        return number.toString();
    } 
    else 
    {
        return number.toFixed(2);
    }
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

let paddingLeftIndicatorLineElement = null;
let paddingLeftIndicatorBoxElement = null;

let paddingRightIndicatorLineElement = null;
let paddingRightIndicatorBoxElement = null;

let paddingTopIndicatorLineElement = null;
let paddingTopIndicatorBoxElement = null;

let paddingBottomIndicatorLineElement = null;
let paddingBottomIndicatorBoxElement = null;


let marginLeftIndicatorLineElement = null;
let marginLeftIndicatorBoxElement = null;

let marginRightIndicatorLineElement = null;
let marginRightIndicatorBoxElement = null;

let marginTopIndicatorLineElement = null;
let marginTopIndicatorBoxElement = null;

let marginBottomIndicatorLineElement = null;
let marginBottomIndicatorBoxElement = null;

function applyHoverEffect(targetElement)
{
    targetElement.classList.add('react-with-dotnet-designer-hover-effect');

    const computedStyle = getComputedStyle(targetElement);

    const rect = targetElement.getBoundingClientRect();

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
            "<div style='display: flex; gap: 4px;'><span>W</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'width')) + NumberToString(rect.width) + "</div>" +
            "<div style='display: flex; gap: 4px;'><span>H</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'height')) + NumberToString(rect.height) + "</div>" +
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
                    lineElement.style.width = computedValueAsNumber + 'px';
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
                        let positionLeftAsNumber = rect.left + computedValueAsNumber / 2 - boxRect.width / 2;
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
                    lineElement.style.width = computedValueAsNumber + 'px';
                    lineElement.style.left = rect.left + rect.width - computedValueAsNumber + 'px';
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
                        boxElement.style.left = rect.left + rect.width - computedValueAsNumber / 2 - boxRect.width / 2 + 'px';
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
                        boxElement.style.top = rect.top + computedValueAsNumber / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingTop');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber + 'px';
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
                        boxElement.style.top = rect.top + rect.height - computedValueAsNumber / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'paddingBottom');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber + 'px';
                    lineElement.style.left = rect.left + rect.width / 2 + 'px';
                    lineElement.style.top = rect.top + rect.height - computedValueAsNumber + 'px';
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
                    lineElement.style.width = computedValueAsNumber + 'px';
                    lineElement.style.left = rect.left - computedValueAsNumber + 'px';
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
                        let positionLeftAsNumber = rect.left - computedValueAsNumber / 2 - boxRect.width / 2;
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
                    lineElement.style.width = computedValueAsNumber + 'px';
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
                        let positionLeftAsNumber = rect.left + rect.width + computedValueAsNumber / 2 - boxRect.width / 2;
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
                        boxElement.style.top = rect.top - computedValueAsNumber / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginTop');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber + 'px';
                    lineElement.style.top = rect.top - computedValueAsNumber + 'px';
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
                        boxElement.style.top = rect.top + rect.height + computedValueAsNumber / 2 - boxRect.height / 2 + 'px';
                    }
                }

                // line
                {
                    applySharedLineStyles(lineElement.style, 'marginBottom');

                    lineElement.style.width = '1px';
                    lineElement.style.height = computedValueAsNumber + 'px';
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

        if (targetProperty.indexOf('margin') === 0)
        {
            style.background = '#EA3FB8';
        }
    }

    function applySharedLineStyles(style, targetProperty)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.backgroundColor = '#4597F7';

        if (targetProperty.indexOf('margin') === 0)
        {
            style.backgroundColor = '#EA3FB8';
        }
    }
}

function removeHoverEffect(targetElement)
{
    targetElement.classList.remove('react-with-dotnet-designer-hover-effect');

    function displayNone(element)
    {
        if(element == null)
        {
            return;
        }
        
        element.style.display = 'none';
    }

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

document.addEventListener('mouseover', (e) => 
{
    if (canApplyHoverEffect(e.target))
    {
        applyHoverEffect(e.target);
    }
});

document.addEventListener('mouseout', (e) => 
{
    removeHoverEffect(e.target);
});