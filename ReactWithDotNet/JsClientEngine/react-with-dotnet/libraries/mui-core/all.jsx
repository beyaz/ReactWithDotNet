import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

import FormGroup from '@mui/material/FormGroup';
import FormControlLabel from '@mui/material/FormControlLabel';


function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.MUI.Material." + name, value);
}

// Connect components as Lazy
register("Accordion", React.lazy(() => import('./Accordion')));
register("AccordionSummary", React.lazy(() => import('./AccordionSummary')));
register("AccordionDetails", React.lazy(() => import('./AccordionDetails')));

register("Autocomplete", React.lazy(() => import('./Autocomplete')));
register("Switch", React.lazy(() => import('./Switch')));
register("Tooltip",     React.lazy(() => import('./Tooltip')));
register("Button",      React.lazy(() => import('./Button')));
register("Input",       React.lazy(() => import('./Input')));
register("InputBase",   React.lazy(() => import('./InputBase')));
register("Paper",       React.lazy(() => import('./Paper')));
register("Divider",     React.lazy(() => import('./Divider')));
register("IconButton",  React.lazy(() => import('./IconButton')));
register("TextField",   React.lazy(() => import('./TextField')));
register("CardMedia",   React.lazy(() => import('./CardMedia')));
register("Card",        React.lazy(() => import('./Card')));
register("CardContent", React.lazy(() => import('./CardContent')));
register("CardActions", React.lazy(() => import('./CardActions')));
register("Typography", React.lazy(() => import('./Typography')));
register("ExpandMoreIcon", React.lazy(() => import('./ExpandMoreIcon')));
register("Grid", React.lazy(() => import('./Grid')));


register("CircularProgress", React.lazy(() => import('./CircularProgress'))); 

register("Slider", React.lazy(() => import('./Slider')));

register("FormGroup",        FormGroup);
register("FormControlLabel", FormControlLabel);