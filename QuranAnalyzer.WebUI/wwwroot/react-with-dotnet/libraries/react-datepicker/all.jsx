
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

const DatePicker = React.lazy(() => import('./DatePicker'));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.react_datepicker.DatePicker", DatePicker);