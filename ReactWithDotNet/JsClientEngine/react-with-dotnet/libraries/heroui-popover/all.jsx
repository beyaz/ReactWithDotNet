import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.HeroUI." + name, value);
}

register("Popover", React.lazy(() => import('./Popover')));
register("PopoverTrigger", React.lazy(() => import('./PopoverTrigger')));
register("PopoverContent", React.lazy(() => import('./PopoverContent')));