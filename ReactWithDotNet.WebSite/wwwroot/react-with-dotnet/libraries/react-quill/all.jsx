
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const ReactQuill = React.lazy(() => import('./ReactQuill'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._ReactQuill_.ReactQuill", ReactQuill);
