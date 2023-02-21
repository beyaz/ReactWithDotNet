
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const SyntaxHighlighter = React.lazy(() => import('./SyntaxHighlighter'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.react_syntax_highlighter.SyntaxHighlighter", SyntaxHighlighter);
