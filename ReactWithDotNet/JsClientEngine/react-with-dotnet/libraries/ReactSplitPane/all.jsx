
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSplitPane.SplitPane", React.lazy(() => import('./SplitPane')));