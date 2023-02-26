
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, cmp)
{
    const fullName = "ReactWithDotNet.Libraries.ReactSuite." + name;

    ReactWithDotNet.RegisterExternalJsObject(fullName, cmp);
}

register("AutoComplete", React.lazy(() => import('./AutoComplete')));

register("AutoComplete::OnChange", function (args)
{
    return [args[0]];
});



var requiredStylesLoaded = false;

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
function BeforeAny3rdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent)
{
    if (requiredStylesLoaded)
    {
        return;
    }

    if (dotNetFullClassNameOf3rdPartyComponent.indexOf('.ReactSuite.') > 0 )
    {
        requiredStylesLoaded = true;

        ReactWithDotNet.TryLoadCssByHref("https://cdnjs.cloudflare.com/ajax/libs/rsuite/5.28.0/rsuite.min.css");
    }
}

ReactWithDotNet.BeforeAny3rdPartyComponentAccess(BeforeAny3rdPartyComponentAccess);