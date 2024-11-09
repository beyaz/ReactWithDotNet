function canApplyHoverEffect(targetElement)
{
    if ( targetElement === sizeIndicatorBoxElement
        || targetElement === leftPaddingIndicatorLineElement 
        || targetElement === leftPaddingIndicatorBoxElement
        
        || targetElement === topPaddingIndicatorLineElement
        || targetElement === topPaddingIndicatorBoxElement
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

function NumberAsPixel(number) 
{
    return NumberToString(number) + 'px';
}

function getMediaQueryStyle(element, propertyName)
{
    const stylesheets = Array.from(document.styleSheets);

    for (const stylesheet of stylesheets)
    {
        try
        {
            const rules = stylesheet.cssRules || stylesheet.rules;

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

let topPaddingIndicatorLineElement = null;
let topPaddingIndicatorBoxElement = null;

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
        sizeIndicatorBoxElement.style.borderRadius = '4px';
        sizeIndicatorBoxElement.style.padding = '3px';        
        sizeIndicatorBoxElement.style.background = '#4597F7';
        sizeIndicatorBoxElement.style.color = '#DECBFC';
        sizeIndicatorBoxElement.style.fontSize = '10px';
        sizeIndicatorBoxElement.style.lineHeight = '10px';
        
        sizeIndicatorBoxElement.style.left = rect.left + rect.width / 2 - 50 + 'px';
        sizeIndicatorBoxElement.style.top = rect.bottom + 4 + 'px';
        
        sizeIndicatorBoxElement.innerHTML =
            "<div style = 'display: flex; flex-direction: column; line-height: 14px; padding:4px;  font-size:12px;' >" +
            "<div style='display: flex; gap: 4px;'><span>W</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'width')) + NumberAsPixel(rect.width) + "</div>" +
            "<div style='display: flex; gap: 4px;'><span>H</span>" + WrapInSpanIfHasValue(GetStyleValue(targetElement, 'height')) + NumberAsPixel(rect.height) + "</div>" +
            "</div>";

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
                
                let finalInnerHTML = NumberAsPixel(paddingLeftAsNumber);
                if (!(paddingLeftValue == null || paddingLeft === '' || paddingLeft === '0px' || paddingLeft === paddingLeftValue))
                {
                    finalInnerHTML += "<br> (" + paddingLeftValue + ")";
                }
                
                leftPaddingIndicatorBoxElement.innerHTML = finalInnerHTML;

                const boxRect = leftPaddingIndicatorBoxElement.getBoundingClientRect();
                leftPaddingIndicatorBoxElement.style.left = rect.left + paddingLeftAsNumber / 2 - boxRect.width / 2 + 'px';
                leftPaddingIndicatorBoxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2  + 'px';
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

                let paddingLeftValue = GetStyleValue(targetElement, 'paddingTop');

                let finalInnerHTML = paddingTop;
                if (!(paddingLeftValue == null || paddingTop === '' || paddingTop === '0px' || paddingTop === paddingLeftValue))
                {
                    finalInnerHTML += " (" + paddingLeftValue + ")";
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
                // topPaddingIndicatorLineElement.style.bottom = rect.bottom - rect.height / 2 + 'px';
            }

            






        }
    }

    function applySharedBoxStyles(style)
    {
        style.display = 'block';
        style.position = 'fixed';
        style.zIndex = '999999';
        style.borderRadius = '3px';
        style.padding = '3px';
        style.background = '#4597F7';
        style.color = '#DECBFC';
        style.fontSize = '10px';
        style.lineHeight = '10px';
        style.textAlign = 'center';
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