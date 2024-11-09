function canApplyHoverEffect(targetElement)
{
    if ( targetElement === leftPaddingIndicatorLineElement || 
         targetElement === leftPaddingIndicatorBoxElement ||
         targetElement === sizeIndicatorBoxElement
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

function WrapInSpanIfHasValue(value)
{
    if (value == null || value === '')
    {
        return '';
    }
    return "<span style = 'color:white;'>" + value + "</span>";
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
            return "<span style = 'color:white;'>" + matchedMediaQueryValue + "</span>";
        }
    }

    if (valueArray.indexOf(calculatedValue) >= 0 || calculatedValue.indexOf('%') > 0)
    {
        return "<span style = 'color:white;'>" + calculatedValue + "</span>";
    }

    if (valueArray.indexOf(value) >= 0 || value.indexOf('%') > 0)
    {
        return "<span style = 'color:white;'>" + value + "</span>";
    }

    if (value.indexOf('px') > 0)
    {
        if (propertyName === 'width' || propertyName === 'height')
        {
            return "<span style = 'color:white;'>" + 'fixed' + "</span>";
        }

        return "<span style = 'color:white;'>" + value + "</span>";
    }

    return '';
}

let leftPaddingIndicatorLineElement = null;
let leftPaddingIndicatorBoxElement = null;
let sizeIndicatorBoxElement = null;

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
    }
    
    // LEFT PADDING
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
        
        let paddingLeft =  getComputedStyle(targetElement).paddingLeft;
        if (paddingLeft == null || paddingLeft === '' || paddingLeft === '0px')
        {
            leftPaddingIndicatorLineElement.style.display = 'none';
            leftPaddingIndicatorBoxElement.style.display = 'none';
        }
        else
        {
            let paddingLeftAsNumber = parseFloat(paddingLeft.replace('px', ''));

            // line
            {
                leftPaddingIndicatorLineElement.style.display = 'block';

                leftPaddingIndicatorLineElement.style.position = 'fixed';
                leftPaddingIndicatorLineElement.style.left = rect.left + 'px';
                leftPaddingIndicatorLineElement.style.top = rect.bottom - rect.height / 2 -4 + 'px';

                leftPaddingIndicatorLineElement.style.width = paddingLeftAsNumber + 'px';
                leftPaddingIndicatorLineElement.style.height = '1px';
                leftPaddingIndicatorLineElement.style.backgroundColor = 'blue';
            }
            
            // box
            {
                leftPaddingIndicatorBoxElement.style.display = 'block';
                leftPaddingIndicatorBoxElement.innerHTML = paddingLeft;

                const boxRect = leftPaddingIndicatorBoxElement.getBoundingClientRect();

                leftPaddingIndicatorBoxElement.style.position = 'fixed';
                leftPaddingIndicatorBoxElement.style.zIndex = '999999';
                leftPaddingIndicatorBoxElement.style.padding = '3px';
                leftPaddingIndicatorBoxElement.style.background = '#4597F7';
                leftPaddingIndicatorBoxElement.style.color = '#DECBFC';
                leftPaddingIndicatorBoxElement.style.fontSize = '10px';
                leftPaddingIndicatorBoxElement.style.lineHeight = '10px';

                leftPaddingIndicatorBoxElement.style.borderRadius = '4px';
                leftPaddingIndicatorBoxElement.style.left = rect.left + paddingLeftAsNumber / 2 - boxRect.width / 2 + 'px';
                leftPaddingIndicatorBoxElement.style.top = rect.bottom - rect.height / 2 - boxRect.height / 2 - 3 + 'px';
            }
            
            
            
            
           
        }
        
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