const isProduction = process.env.NODE_ENV === 'production';

// import core library
import ReactWithDotNet from "./ReactWithDotNet";

// import helper tool for design your component in hotreload mode
import "./integration/ReactWithDotNet-UIDesigner";


// your app specific imports and codes
import './app.css'

import React from 'react';