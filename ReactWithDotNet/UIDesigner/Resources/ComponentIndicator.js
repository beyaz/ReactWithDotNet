function canApplyHoverEffect(targetElement)
{
    if ( targetElement === sizeIndicatorBoxElement
        || targetElement === leftPaddingIndicatorLineElement 
        || targetElement === leftPaddingIndicatorBoxElement
        
        || targetElement === rightPaddingIndicatorLineElement
        || targetElement === rightPaddingIndicatorBoxElement

        || targetElement === topPaddingIndicatorLineElement
        || targetElement === topPaddingIndicatorBoxElement

        || targetElement === bottomPaddingIndicatorLineElement
        || targetElement === bottomPaddingIndicatorBoxElement

        || targetElement === marginLeftIndicatorLineElement
        || targetElement === marginLeftIndicatorBoxElement
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
                                if (propertyName === 'paddingLeft')
                                {
                                    return styleRule.style.paddingLeft;
                                }
                                if (propertyName === 'marginLeft')
                                {
                                    return styleRule.style.marginLeft;
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

let leftPaddingIndicatorLineElement = null;
let leftPaddingIndicatorBoxElement = null;

let rightPaddingIndicatorLineElement = null;
let rightPaddingIndicatorBoxElement = null;

let topPaddingIndicatorLineElement = null;
let topPaddingIndicatorBoxElement = null;

let bottomPaddingIndicatorLineElement = null;
let bottomPaddingIndicatorBoxElement = null;



let marginLeftIndicatorLineElement = null;
let marginLeftIndicatorBoxElement = null;



function applyHoverEffect(targetElement)
{
    targetElement.classList.add('react-with-dotnet-designer-hover-effect');

    const rect = targetElement.getBoundingClientRect();

    // Size indicator box
    {
        if(sizeIndicatorBoxElement === null)
        {
            sizeIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(sizeIndicatorBoxElement);
        }

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

        const popupRect = sizeIndicatorBoxElement.getBoundingClientRect();
        let leftPositionAsNumber= rect.left + rect.width / 2 - popupRect.width / 2;
        if (leftPositionAsNumber < 0 )
        {
            leftPositionAsNumber = 0;
        }
        
        sizeIndicatorBoxElement.style.left = leftPositionAsNumber + 'px';
        sizeIndicatorBoxElement.style.top = rect.bottom + 4 + 'px';

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
        // left
        if(leftPaddingIndicatorLineElement === null)
        {
            leftPaddingIndicatorLineElement = document.createElement('div');
            document.body.appendChild(leftPaddingIndicatorLineElement);
        }

        if(leftPaddingIndicatorBoxElement === null)
        {
            leftPaddingIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(leftPaddingIndicatorBoxElement);
        }

        // right
        if(rightPaddingIndicatorLineElement === null)
        {
            rightPaddingIndicatorLineElement = document.createElement('div');
            document.body.appendChild(rightPaddingIndicatorLineElement);
        }

        if(rightPaddingIndicatorBoxElement === null)
        {
            rightPaddingIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(rightPaddingIndicatorBoxElement);
        }

        // top
        if(topPaddingIndicatorLineElement === null)
        {
            topPaddingIndicatorLineElement = document.createElement('div');
            document.body.appendChild(topPaddingIndicatorLineElement);
        }

        if(topPaddingIndicatorBoxElement === null)
        {
            topPaddingIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(topPaddingIndicatorBoxElement);
        }

        // bottom
        if(bottomPaddingIndicatorLineElement === null)
        {
            bottomPaddingIndicatorLineElement = document.createElement('div');
            document.body.appendChild(bottomPaddingIndicatorLineElement);
        }

        if(bottomPaddingIndicatorBoxElement === null)
        {
            bottomPaddingIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(bottomPaddingIndicatorBoxElement);
        }

        // marginLeft
        if(marginLeftIndicatorLineElement === null)
        {
            marginLeftIndicatorLineElement = document.createElement('div');
            document.body.appendChild(marginLeftIndicatorLineElement);
        }

        if(marginLeftIndicatorBoxElement === null)
        {
            marginLeftIndicatorBoxElement = document.createElement('div');
            document.body.appendChild(marginLeftIndicatorBoxElement);
        }
    }

    // PADDING LEFT
    {
        let lineElement = leftPaddingIndicatorLineElement;
        let boxElement = leftPaddingIndicatorBoxElement;
        
        let paddingLeft =  getComputedStyle(targetElement).paddingLeft;
        let paddingLeftAsNumber = parseFloat(paddingLeft.replace('px', ''));
        
        if (paddingLeft === '' || paddingLeft === '0px')
        {
            lineElement.style.display = 'none';
            boxElement.style.display = 'none';
        }
        else
        {
            // line
            {
                applySharedLineStyles(lineElement.style);

                lineElement.style.height = '1px';
                lineElement.style.width = paddingLeftAsNumber + 'px';
                lineElement.style.left = rect.left + 'px';
                lineElement.style.top = rect.bottom - rect.height / 2 + 'px';
            }
            
            // box
            {
                applySharedBoxStyles(boxElement.style);

                let paddingLeftValue = GetStyleValue(targetElement, 'paddingLeft');
                
                let finalInnerHTML = NumberToString(paddingLeftAsNumber);
                if (!(paddingLeftValue == null || paddingLeft === '' || paddingLeftValue.indexOf('px') > 0))
                {
                    finalInnerHTML += "<br> (" + paddingLeftValue + ")";
                }

                boxElement.innerHTML = finalInnerHTML;

                const boxRect = boxElement.getBoundingClientRect();
                let positionLeftAsNumber = rect.left + paddingLeftAsNumber / 2 - boxRect.width / 2;
                if(positionLeftAsNumber < 0)
                {
                    positionLeftAsNumber = 0;
                }
                boxElement.style.left = positionLeftAsNumber + 'px';
                boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
            }
        }
    }

    // PADDING RIGHT
    {
        let lineElement = rightPaddingIndicatorLineElement;
        let boxElement = rightPaddingIndicatorBoxElement;
        
        let paddingRight =  getComputedStyle(targetElement).paddingRight;
        let paddingRightAsNumber = parseFloat(paddingRight.replace('px', ''));

        if (paddingRight === '' || paddingRight === '0px')
        {
            lineElement.style.display = 'none';
            boxElement.style.display = 'none';
        }
        else
        {
            // line
            {
                applySharedLineStyles(lineElement.style);

                lineElement.style.height = '1px';
                lineElement.style.width = paddingRightAsNumber + 'px';
                lineElement.style.left = rect.left + rect.width - paddingRightAsNumber + 'px';
                lineElement.style.top = rect.top + rect.height / 2 + 'px';
            }

            // box
            {
                applySharedBoxStyles(boxElement.style);

                let paddingRightValue = GetStyleValue(targetElement, 'paddingRight');

                let finalInnerHTML = NumberToString(paddingRightAsNumber);
                if (!(paddingRightValue == null || paddingRight === '' || paddingRight.indexOf('px') > 0))
                {
                    finalInnerHTML += "<br> (" + paddingRightValue + ")";
                }

                boxElement.innerHTML = finalInnerHTML;

                const boxRect = boxElement.getBoundingClientRect();
                boxElement.style.left = rect.left + rect.width - paddingRightAsNumber/2 - boxRect.width / 2 + 'px';
                boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
            }
        }
    }
    
    // PADDING TOP
    {
        let lineElement = topPaddingIndicatorLineElement;
        let boxElement = topPaddingIndicatorBoxElement;
        
        let paddingTop =  getComputedStyle(targetElement).paddingTop;
        let paddingTopAsNumber = parseFloat(paddingTop.replace('px', ''));
        
        if (paddingTop === '' || paddingTop === '0px')
        {
            lineElement.style.display = 'none';
            boxElement.style.display = 'none';
        }
        else
        {
            // box
            {
                applySharedBoxStyles(boxElement.style);

                let paddingTopValue = GetStyleValue(targetElement, 'paddingTop');

                let finalInnerHTML = NumberToString(paddingTopAsNumber);
                if (!(paddingTopValue == null || paddingTop === '' || paddingTop.indexOf('px') > 0))
                {
                    finalInnerHTML += " (" + paddingTopValue + ")";
                }

                boxElement.innerHTML = finalInnerHTML;

                const boxRect = boxElement.getBoundingClientRect();
                boxElement.style.left = rect.left + rect.width / 2  - boxRect.width / 2 + 'px';
                boxElement.style.top = rect.top + paddingTopAsNumber / 2 - boxRect.height / 2 + 'px';
            }
            
            // line
            {
                applySharedLineStyles(lineElement.style);

                lineElement.style.width = '1px';
                lineElement.style.height = paddingTopAsNumber + 'px';
                lineElement.style.top = rect.top + 'px';
                lineElement.style.left = rect.left + rect.width / 2 + 'px';
            }
        }
    }

    // PADDING BOTTOM
    {
        let lineElement = bottomPaddingIndicatorLineElement;
        let boxElement = bottomPaddingIndicatorBoxElement;
        
        let paddingBottom = getComputedStyle(targetElement).paddingBottom;
        let paddingBottomAsNumber = parseFloat(paddingBottom.replace('px', ''));

        if (paddingBottom === '' || paddingBottom === '0px')
        {
            lineElement.style.display = 'none';
            boxElement.style.display = 'none';
        }
        else
        {
            // box
            {
                applySharedBoxStyles(boxElement.style);

                let paddingBottomValue = GetStyleValue(targetElement, 'paddingBottom');

                let finalInnerHTML = NumberToString(paddingBottomAsNumber);
                if (!(paddingBottomValue == null || paddingBottom === '' || paddingBottom.indexOf('px') > 0))
                {
                    finalInnerHTML += " (" + paddingBottomValue + ")";
                }

                boxElement.innerHTML = finalInnerHTML;

                const boxRect = boxElement.getBoundingClientRect();
                boxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                boxElement.style.top = rect.top + rect.height - paddingBottomAsNumber / 2 - boxRect.height / 2 + 'px';
            }

            // line
            {
                applySharedLineStyles(lineElement.style);

                lineElement.style.width = '1px';
                lineElement.style.height = paddingBottomAsNumber + 'px';
                lineElement.style.left = rect.left + rect.width / 2 + 'px';
                lineElement.style.top = rect.top + rect.height - paddingBottomAsNumber + 'px';
            }
        }
    }

    // Left
    {
        let lineElement = marginLeftIndicatorLineElement;
        let boxElement = marginLeftIndicatorBoxElement;
        
        let marginLeft =  getComputedStyle(targetElement).marginLeft;
        let marginLeftAsNumber = parseFloat(marginLeft.replace('px', ''));

        if (marginLeft === '' || marginLeft === '0px')
        {
            lineElement.style.display = 'none';
            boxElement.style.display = 'none';
        }
        else
        {
            // line
            {
                applySharedLineStyles(lineElement.style);

                lineElement.style.height = '1px';
                lineElement.style.width = marginLeftAsNumber + 'px';
                lineElement.style.left = rect.left - marginLeftAsNumber + 'px';
                lineElement.style.top = rect.bottom - rect.height / 2 + 'px';
            }

            // box
            {
                applySharedBoxStyles(boxElement.style);

                let marginLeftValue = GetStyleValue(targetElement, 'marginLeft');

                let finalInnerHTML = NumberToString(marginLeftAsNumber);
                if (!(marginLeftValue == null || marginLeft === '' || marginLeftValue.indexOf('px') > 0))
                {
                    finalInnerHTML += "<br> (" + marginLeftValue + ")";
                }

                boxElement.innerHTML = finalInnerHTML;

                const boxRect = boxElement.getBoundingClientRect();
                let positionLeftAsNumber = rect.left - marginLeftAsNumber / 2 - boxRect.width / 2;
                if(positionLeftAsNumber < 0)
                {
                    positionLeftAsNumber = 0;
                }
                boxElement.style.left = positionLeftAsNumber + 'px';
                boxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
            }
        }
    }
    
    
    function applySharedBoxStyles(style)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.zIndex = '999999';
        style.borderRadius = '3px';
        style.padding = '2px';
        style.background = '#4597F7';
        style.color = '#DECBFC';
        style.fontSize = '10px';
        style.lineHeight = '10px';
        style.textAlign = 'center';
        style.fontFamily = 'monospace';
    }

    function applySharedLineStyles(style)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.backgroundColor = '#4597F7';
    }
}

function removeHoverEffect(targetElement)
{
    targetElement.classList.remove('react-with-dotnet-designer-hover-effect');
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