
import React from 'react';
import ReactWithDotNet from "../../react-with-dotnet";

function RegisterReactSuiteComponent(name, cmp)
{
    const Prefix = "ReactWithDotNet.ThirdPartyLibraries.ReactSuite.";

    ReactWithDotNet.RegisterExternalJsObject(Prefix + name, cmp);
}

RegisterReactSuiteComponent("AutoComplete", React.lazy(() => import('./AutoComplete')));
RegisterReactSuiteComponent("AutoComplete::OnChange", args => [args[0]]);

RegisterReactSuiteComponent("Modal", React.lazy(() => import('./Modal')));
RegisterReactSuiteComponent("Modal+Header", React.lazy(() => import('./Modal.Header')));
RegisterReactSuiteComponent("Modal+Body", React.lazy(() => import('./Modal.Body')));

var isFirstAccess = true;

ReactWithDotNet.OnFindExternalObject(name =>
{
    if (name.indexOf('.ReactSuite.') < 0)
    {
        return null;
    }

    if (isFirstAccess)
    {
        isFirstAccess = false;
        ReactWithDotNet.TryLoadCssByHref("https://cdnjs.cloudflare.com/ajax/libs/rsuite/5.28.0/rsuite.min.css");
    }

    return null;
});