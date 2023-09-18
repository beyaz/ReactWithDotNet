
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Editor = React.lazy(() => import('./Editor'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact.Editor", Editor);


ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact::OnChange', function (args)
{
    return [args[0]];
});