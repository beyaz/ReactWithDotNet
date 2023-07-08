
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const CodeEditor = React.lazy(() => import('./CodeEditor'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.UIW.ReactTextareaCodeEditor.CodeEditor", CodeEditor);


