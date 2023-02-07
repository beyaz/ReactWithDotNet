import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';


function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.MUICore." + name, value);
}

// Connect components as Lazy
register("Switch", React.lazy(() => import('./Switch')));
register("FormGroup", FormGroup);
register("FormControlLabel", FormControlLabel);
