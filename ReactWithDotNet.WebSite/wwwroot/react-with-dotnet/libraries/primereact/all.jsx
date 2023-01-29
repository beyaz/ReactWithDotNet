import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact." + name, value);
}

// Connect components as Lazy
register("Button", React.lazy(() => import('./Button')));
register("InputText", React.lazy(() => import('./InputText')));
register("InputTextarea", React.lazy(() => import('./InputTextarea')));
register("BlockUI", React.lazy(() => import('./BlockUI')));
register("Card", React.lazy(() => import('./Card')));
register("TabView", React.lazy(() => import('./TabView')));
register("TabPanel", React.lazy(() => import('./TabPanel')));
register("SplitterPanel", React.lazy(() => import('./SplitterPanel')));
register("Splitter", React.lazy(() => import('./Splitter')));
register("Slider", React.lazy(() => import('./Slider')));
register("ListBox", React.lazy(() => import('./ListBox')));
register("Dropdown", React.lazy(() => import('./Dropdown')));
register("Column", React.lazy(() => import('./Column')));
register("DataTable", React.lazy(() => import('./DataTable')));
register("Checkbox", React.lazy(() => import('./Checkbox')));
register("InputMask", React.lazy(() => import('./InputMask')));
register("AutoComplete", React.lazy(() => import('./AutoComplete')));
register("Tree", React.lazy(() => import('./Tree')));
register("InputSwitch", React.lazy(() => import('./InputSwitch')));
register("Panel", React.lazy(() => import('./Panel')));
register("Tooltip", React.lazy(() => import('./Tooltip')));
register("Message", React.lazy(() => import('./Message')));
register("ScrollPanel", React.lazy(() => import('./ScrollPanel')));
register("Dialog", React.lazy(() => import('./Dialog')));


// U T I L I T Y   F U N C T I O N S
register("Panel::GetHeaderTemplate", (key) => ReactWithDotNet.GetExternalJsObject(key));

register("GrabOnlyValueParameterFromCommonPrimeReactEvent", function (argumentsAsArray)
{
    //const originalEvent = argumentsAsArray[0].originalEvent;

    const value = argumentsAsArray[0].value;

    return [{ value: value }];
});

register("GrabWithoutOriginalEvent", function (argumentsAsArray)
{
    const newInstance = {};

    const obj = argumentsAsArray[0];

    for(var propertyName in obj)
	{
        if(obj.hasOwnProperty(propertyName))
        {
            const value = obj[propertyName];

            if (propertyName === 'originalEvent' && value && value._reactName)
            {
                continue;
            }

            newInstance[propertyName] = value;
        }
    }

    return [newInstance];
});