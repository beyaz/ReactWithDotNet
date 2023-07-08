
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Editor = React.lazy(() => import('./Editor'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.Editor", Editor);

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.GetHighlightFunction", (language) =>
{    
    return (code) =>
    {
        return ReactWithDotNet.GetExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.HighlightByLanguage")(language, code);
    };
});