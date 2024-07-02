
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const Uploady = React.lazy(() => import('./Uploady'));
const UploadButton = React.lazy(() => import('./UploadButton'));
const UploadProgress = React.lazy(() => import('./UploadProgress'));


ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.Uploady", Uploady);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.UploadButton", UploadButton);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries._uploady_.UploadProgress", UploadProgress);

