
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const GoogleMap = React.lazy(() => import('./GoogleMap'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.GoogleMapReact.GoogleMap", GoogleMap);
