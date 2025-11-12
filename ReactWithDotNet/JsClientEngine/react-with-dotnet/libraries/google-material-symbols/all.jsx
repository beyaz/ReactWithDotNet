import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const MaterialSymbol = React.lazy(() => import('./MaterialSymbol'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.GoogleMaterialSymbols.MaterialSymbol", MaterialSymbol);