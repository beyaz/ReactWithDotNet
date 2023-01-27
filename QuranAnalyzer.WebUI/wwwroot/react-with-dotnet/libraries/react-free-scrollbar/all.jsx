
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const FreeScrollbar = React.lazy(() => import('./FreeScrollbar'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.react_free_scrollbar.FreeScrollBar", FreeScrollbar);
