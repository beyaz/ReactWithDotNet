import ReactWithDotNet from "../ReactWithDotNet.jsx";

import  GoogleMap  from 'google-map-react';

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.google_map_react." + name, value);
}

register("GoogleMap", GoogleMap);
