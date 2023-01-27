
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Xarrow = React.lazy(() => import('./Xarrow'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_xarrows.Xarrow", Xarrow);