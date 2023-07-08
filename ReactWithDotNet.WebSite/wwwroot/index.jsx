

// import core library
import ReactWithDotNet from "./react-with-dotnet/react-with-dotnet";

// you can comment these imports according to your project dependency
import "./react-with-dotnet/libraries/mui-core/all";
import "./react-with-dotnet/libraries/google-map-react/all";
import "./react-with-dotnet/libraries/primereact/all";
import "./react-with-dotnet/libraries/react-awesome-reveal/all";
import "./react-with-dotnet/libraries/react-free-scrollbar/all";
import "./react-with-dotnet/libraries/react-xarrows/all";
import "./react-with-dotnet/libraries/rsuite/all";
import "./react-with-dotnet/libraries/swiper/all";
import "./react-with-dotnet/libraries/uiw-react-codemirror/all";
import "./react-with-dotnet/libraries/framer-motion/all";
import "./react-with-dotnet/libraries/nextui-org/all";
import "./react-with-dotnet/libraries/uiw-react-textarea-code-editor/all";
import "./react-with-dotnet/libraries/react-simple-code-editor/all";


export { ReactWithDotNet };


    


import React, { useState } from 'react';

ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.WebSite.HeaderComponents.CollapseContainer', props =>
{
    const [isOpen, setIsOpen] = useState(false);

    return (
        <div onClick={() => setIsOpen(!isOpen)}>
            {isOpen ? props.ContentOnOpened : props.ContentOnClosed}
        </div>
    );
});