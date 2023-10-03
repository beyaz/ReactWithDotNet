
import React from 'react';
import ReactWithDotNet from "../../react-with-dotnet";


var isFirstAccess = true;

const Prefix = "ReactWithDotNet.ThirdPartyLibraries.ReactSuite.";

ReactWithDotNet.RegisterExternalJsObject(Prefix + "Modal", React.lazy(() => import('./Modal')));

ReactWithDotNet.RegisterExternalJsObject(Prefix + "AutoComplete", React.lazy(() => import('./AutoComplete')));
ReactWithDotNet.RegisterExternalJsObject(Prefix + "AutoComplete::OnChange", args => [args[0]]);


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