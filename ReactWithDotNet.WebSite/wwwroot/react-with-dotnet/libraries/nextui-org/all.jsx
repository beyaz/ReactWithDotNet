
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.NextUI.Avatar", React.lazy(() => import('./Avatar')));