
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSplit.Split", React.lazy(() => import('./Split')));