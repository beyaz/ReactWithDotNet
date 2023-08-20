import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

import { motion } from "framer-motion"

motion.div

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.FramerMotion." + name, value);
}

// Connect components as Lazy
register("motion+div", React.lazy(() => import('./div')));
register("AnimatePresence", React.lazy(() => import('./AnimatePresence')));
register("motion+button", React.lazy(() => import('./button')));


