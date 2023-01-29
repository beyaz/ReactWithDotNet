
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Editor = React.lazy(() => import('./Editor'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.uiw.react_codemirror.CodeMirror", Editor);
