function canApplyHoverEffect(targetElement)
                {
                    if ( targetElement.id === 'ReactWithDotNetCurrentHighlightedElementSizeIndicator')
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
                
                function getMediaQueryStyle(element, property) 
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
                	            if ( '.' + element.classList[i] ===  styleRule.selectorText)
                	            {
                		            return styleRule.style.width;
                	            }
                            }
                  
                          }
                        }
                      }
                    } 
                	catch (e) 
                	{
                      console.error("Cannot access stylesheet:", e);
                    }
                  }
                  return null;
                }
                
                function GetSizeValueOf(element, propertyName) 
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
                   	
                   	if(matchedMediaQueryValue)
                   	if ( valueArray.indexOf( matchedMediaQueryValue ) >= 0 || matchedMediaQueryValue.indexOf('%') > 0 )
                    {
                        return "<span style = 'color:white;'>" + matchedMediaQueryValue + "</span>";
                    }
                    
                   	if ( valueArray.indexOf( calculatedValue ) >= 0 || calculatedValue.indexOf('%') > 0 )
                    {
                        return "<span style = 'color:white;'>" + calculatedValue + "</span>";
                    }
                    
                	if ( valueArray.indexOf( value ) >= 0 || value.indexOf('%') > 0 )
                	{
                		return "<span style = 'color:white;'>" + value + "</span>";
                	}
                	
                	if ( value.indexOf( 'px' ) > 0)
                	{
                		return "<span style = 'color:white;'>" + 'fixed' + "</span>";
                	}	
                	
                	return '';
                }
                
                function applyHoverEffect(targetElement)
                {
                    targetElement.classList.add('react-with-dotnet-designer-hover-effect');
                    
                    var rect = targetElement.getBoundingClientRect();
                    
                    var el = document.getElementById('ReactWithDotNetCurrentHighlightedElementSizeIndicator');
                    el.style.left = rect.left + rect.width / 2 - 50  + 'px';
                    el.style.top = rect.bottom + 4 +'px';
                    
                    
                    el.innerHTML = 
                    
                    "<div style = 'display: flex; flex-direction: column; line-height: 14px; padding:4px;  font-size:12px;' >" +
                        
                        "<div style='display: flex; gap: 3px;'><span>W</span>" + GetSizeValueOf(targetElement, 'width')  + NumberAsPixel(rect.width)  + "</div>" +
                        "<div style='display: flex; gap: 3px;'><span>H</span>" + GetSizeValueOf(targetElement, 'height') + NumberAsPixel(rect.height) + "</div>" +
                        
                    "</div>"
                    
                    ;
                    
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