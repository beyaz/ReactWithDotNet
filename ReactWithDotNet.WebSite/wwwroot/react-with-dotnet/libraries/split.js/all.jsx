import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Split = React.lazy(() => import('./Split'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.split_js.Split", Split);