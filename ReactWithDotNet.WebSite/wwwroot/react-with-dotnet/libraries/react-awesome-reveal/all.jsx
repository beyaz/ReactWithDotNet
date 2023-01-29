import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.react_awesome_reveal." + name, value);
}

register("AttentionSeeker", React.lazy(() => import('./AttentionSeeker')));
register("Bounce", React.lazy(() => import('./Bounce')));
register("Fade", React.lazy(() => import('./Fade')));
register("Flip", React.lazy(() => import('./Flip')));
register("Hinge", React.lazy(() => import('./Hinge')));
register("JackInTheBox", React.lazy(() => import('./JackInTheBox')));
register("Rotate", React.lazy(() => import('./Rotate')));
register("Slide", React.lazy(() => import('./Slide')));
register("Zoom", React.lazy(() => import('./Zoom')));