
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Editor = React.lazy(() => import('./Editor'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror.CodeMirror", Editor);
