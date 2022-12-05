import ReactWithDotNet from "../react-with-dotnet";

import GoogleMap from 'google-map-react';


function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.google_map_react." + name, value);
}

register("GoogleMap", GoogleMap);
