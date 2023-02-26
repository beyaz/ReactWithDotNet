
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, cmp)
{
    const fullName = "ReactWithDotNet.Libraries.ReactSuite." + name;

    ReactWithDotNet.RegisterExternalJsObject(fullName, cmp);
}

var isFirstLoad = false;

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
function BeforeAny3rdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent)
{
    if (isFirstLoad)
    {
        return;
    }

    if (dotNetFullClassNameOf3rdPartyComponent.indexOf('.ReactSuite.') > 0 )
    {
        isFirstLoad = true;

        ReactWithDotNet.TryLoadCssByHref("https://cdnjs.cloudflare.com/ajax/libs/rsuite/5.28.0/rsuite.min.css");

        register("AutoComplete", React.lazy(() => import('./AutoComplete')));

        register("AutoComplete::OnChange", function (args)
        {
            return [args[0]];
        });
    }
}

ReactWithDotNet.BeforeAny3rdPartyComponentAccess(BeforeAny3rdPartyComponentAccess);