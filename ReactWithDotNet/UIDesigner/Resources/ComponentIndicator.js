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
    }

    // PADDING LEFT
    {
        let paddingLeft =  getComputedStyle(targetElement).paddingLeft;
        let paddingLeftAsNumber = parseFloat(paddingLeft.replace('px', ''));
        
        if (paddingLeft === '' || paddingLeft === '0px')
        {
            leftPaddingIndicatorLineElement.style.display = 'none';
            leftPaddingIndicatorBoxElement.style.display = 'none';
        }
        else
        {
            // line
            {
                applySharedLineStyles(leftPaddingIndicatorLineElement.style);

                leftPaddingIndicatorLineElement.style.height = '1px';
                leftPaddingIndicatorLineElement.style.width = paddingLeftAsNumber + 'px';
                leftPaddingIndicatorLineElement.style.left = rect.left + 'px';
                leftPaddingIndicatorLineElement.style.top = rect.bottom - rect.height / 2 + 'px';
            }
            
            // box
            {
                applySharedBoxStyles(leftPaddingIndicatorBoxElement.style);

                let paddingLeftValue = GetStyleValue(targetElement, 'paddingLeft');
                
                let finalInnerHTML = NumberToString(paddingLeftAsNumber);
                if (!(paddingLeftValue == null || paddingLeft === '' || paddingLeftValue.indexOf('px') > 0))
                {
                    finalInnerHTML += "<br> (" + paddingLeftValue + ")";
                }
                
                leftPaddingIndicatorBoxElement.innerHTML = finalInnerHTML;

                const boxRect = leftPaddingIndicatorBoxElement.getBoundingClientRect();
                let positionLeftAsNumber = rect.left + paddingLeftAsNumber / 2 - boxRect.width / 2;
                if(positionLeftAsNumber < 0)
                {
                    positionLeftAsNumber = 0;
                }
                leftPaddingIndicatorBoxElement.style.left = positionLeftAsNumber + 'px';
                leftPaddingIndicatorBoxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
            }
        }
    }

    // PADDING RIGHT
    {
        let paddingRight =  getComputedStyle(targetElement).paddingRight;
        let paddingRightAsNumber = parseFloat(paddingRight.replace('px', ''));

        if (paddingRight === '' || paddingRight === '0px')
        {
            rightPaddingIndicatorLineElement.style.display = 'none';
            rightPaddingIndicatorBoxElement.style.display = 'none';
        }
        else
        {
            // line
            {
                applySharedLineStyles(rightPaddingIndicatorLineElement.style);

                rightPaddingIndicatorLineElement.style.height = '1px';
                rightPaddingIndicatorLineElement.style.width = paddingRightAsNumber + 'px';
                rightPaddingIndicatorLineElement.style.left = rect.left + rect.width - paddingRightAsNumber + 'px';
                rightPaddingIndicatorLineElement.style.top = rect.top + rect.height / 2 + 'px';
            }

            // box
            {
                applySharedBoxStyles(rightPaddingIndicatorBoxElement.style);

                let paddingRightValue = GetStyleValue(targetElement, 'paddingRight');

                let finalInnerHTML = NumberToString(paddingRightAsNumber);
                if (!(paddingRightValue == null || paddingRight === '' || paddingRight.indexOf('px') > 0))
                {
                    finalInnerHTML += "<br> (" + paddingRightValue + ")";
                }

                rightPaddingIndicatorBoxElement.innerHTML = finalInnerHTML;

                const boxRect = rightPaddingIndicatorBoxElement.getBoundingClientRect();
                rightPaddingIndicatorBoxElement.style.left = rect.left + rect.width - paddingRightAsNumber/2 - boxRect.width / 2 + 'px';
                rightPaddingIndicatorBoxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
            }
        }
    }
    
    // PADDING TOP
    {
        let paddingTop =  getComputedStyle(targetElement).paddingTop;
        let paddingTopAsNumber = parseFloat(paddingTop.replace('px', ''));
        
        if (paddingTop === '' || paddingTop === '0px')
        {
            topPaddingIndicatorLineElement.style.display = 'none';
            topPaddingIndicatorBoxElement.style.display = 'none';
        }
        else
        {
            // box
            {
                applySharedBoxStyles(topPaddingIndicatorBoxElement.style);

                let paddingTopValue = GetStyleValue(targetElement, 'paddingTop');

                let finalInnerHTML = NumberToString(paddingTopAsNumber);
                if (!(paddingTopValue == null || paddingTop === '' || paddingTop.indexOf('px') > 0))
                {
                    finalInnerHTML += " (" + paddingTopValue + ")";
                }

                topPaddingIndicatorBoxElement.innerHTML = finalInnerHTML;

                const boxRect = topPaddingIndicatorBoxElement.getBoundingClientRect();
                topPaddingIndicatorBoxElement.style.left = rect.left + rect.width / 2  - boxRect.width / 2 + 'px';
                topPaddingIndicatorBoxElement.style.top = rect.top + paddingTopAsNumber / 2 - boxRect.height / 2 + 'px';
            }
            
            // line
            {
                applySharedLineStyles(topPaddingIndicatorLineElement.style);

                topPaddingIndicatorLineElement.style.width = '1px';
                topPaddingIndicatorLineElement.style.height = paddingTopAsNumber + 'px';
                topPaddingIndicatorLineElement.style.top = rect.top + 'px';
                topPaddingIndicatorLineElement.style.left = rect.left + rect.width / 2 + 'px';
            }
        }
    }

    // PADDING BOTTOM
    {
        let paddingBottom = getComputedStyle(targetElement).paddingBottom;
        let paddingBottomAsNumber = parseFloat(paddingBottom.replace('px', ''));

        if (paddingBottom === '' || paddingBottom === '0px')
        {
            bottomPaddingIndicatorLineElement.style.display = 'none';
            bottomPaddingIndicatorBoxElement.style.display = 'none';
        }
        else
        {
            // box
            {
                applySharedBoxStyles(bottomPaddingIndicatorBoxElement.style);

                let paddingBottomValue = GetStyleValue(targetElement, 'paddingBottom');

                let finalInnerHTML = NumberToString(paddingBottomAsNumber);
                if (!(paddingBottomValue == null || paddingBottom === '' || paddingBottom.indexOf('px') > 0))
                {
                    finalInnerHTML += " (" + paddingBottomValue + ")";
                }

                bottomPaddingIndicatorBoxElement.innerHTML = finalInnerHTML;

                const boxRect = bottomPaddingIndicatorBoxElement.getBoundingClientRect();
                bottomPaddingIndicatorBoxElement.style.left = rect.left + rect.width / 2 - boxRect.width / 2 + 'px';
                bottomPaddingIndicatorBoxElement.style.top = rect.top + rect.height - paddingBottomAsNumber / 2 - boxRect.height / 2 + 'px';
            }

            // line
            {
                applySharedLineStyles(bottomPaddingIndicatorLineElement.style);

                bottomPaddingIndicatorLineElement.style.width = '1px';
                bottomPaddingIndicatorLineElement.style.height = paddingBottomAsNumber + 'px';
                bottomPaddingIndicatorLineElement.style.left = rect.left + rect.width / 2 + 'px';
                bottomPaddingIndicatorLineElement.style.top = rect.top + rect.height - paddingBottomAsNumber + 'px';
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