
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const FreeScrollBar = React.lazy(() => import('./FreeScrollBar'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.react_free_scrollbar.FreeScrollBar", FreeScrollBar);
