
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const ReactPlayer = React.lazy(() => import('./ReactPlayer'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.React_Player.ReactPlayer", ReactPlayer);