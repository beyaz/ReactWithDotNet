import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const ReactFlow = React.lazy(() => import('./ReactFlow'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.xyflow_react.ReactFlow", ReactFlow);


function RegisterGlobalStyles()
{
    ReactWithDotNet.TryLoadCssByHref(
        "https://unpkg.com/@xyflow/react@12.0.0/dist/style.css"
    );
}



var isFirstLoad = false;

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.BeforeAnyThirdPartyComponentAccess(dotNetFullClassNameOf3rdPartyComponent =>
{
    if (isFirstLoad)
    {
        return;
    }

    if (dotNetFullClassNameOf3rdPartyComponent.indexOf('ReactWithDotNet.ThirdPartyLibraries.xyflow_react.') === 0 )
    {
        isFirstLoad = true;

        RegisterGlobalStyles();
    }
});