import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.HeroUI." + name, value);
}

register("Popover", React.lazy(() => import('./Checkbox')));