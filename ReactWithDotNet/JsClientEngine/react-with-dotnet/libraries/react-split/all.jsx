import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Split = React.lazy(() => import('./Split'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._react_split_.Split", Split);