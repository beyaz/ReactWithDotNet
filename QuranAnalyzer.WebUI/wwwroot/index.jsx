
import './app.css'

import ReactWithDotNet from "./ReactWithDotNet";

import "./integration/primereact";

// react-xarrows
import Xarrow from "react-xarrows";


// react-simple-code-editor
import Editor  from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-json';
import 'prismjs/themes/prism.css';

const GetHighlightFunction = (language) =>
{
    const lang = languages[language];
    
     return code =>
     {
         if (code == null)
         {
             return "";
         }
         return highlight(code, lang);
     };
};


RegisterScrollEvents = function()
{
    var currentScrollY = 0;

    function handleScroll(e)
    {
        var scrollY = e.target.scrollTop;

        function canFireAction()
        {
            if (scrollY > 0)
            {
                return currentScrollY === 0;
            }

            if (currentScrollY > 0)
            {
                return true;   
            }

            return false;
        }

        if (canFireAction())
        {
            currentScrollY = scrollY;

            ReactWithDotNet.DispatchEvent('MainContentDivScrollChanged', [scrollY]);
        }
        else
        {
            currentScrollY = scrollY;
        }
    }

    ReactWithDotNet.OnDocumentReady(function()
    {
        document.getElementById("main").addEventListener('scroll', handleScroll);
    });
}



function InitializeUIDesignerEvents(timeoutInMilliseconds)
{
    OnBrowserInactive(timeoutInMilliseconds, () =>
    {
        ReactWithDotNet.DispatchEvent('OnBrowserInactive',[]);
    });
}

function OnBrowserInactive(timeoutInMilliseconds, callback)
{
    var wait = setTimeout(callback, timeoutInMilliseconds);
    document.onmousemove = document.mousedown = document.mouseup = document.onkeydown = document.onkeyup = document.focus = function ()
    {
        clearTimeout(wait);
        wait = setTimeout(callback, timeoutInMilliseconds);
    };
}


if (process.env.NODE_ENV === 'production')
{
    // specify your production required components or functions
}
else
{
    // react-xarrows
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_xarrows.Xarrow", Xarrow);

    // react-prism-editor
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.Editor", Editor);
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.highlight", GetHighlightFunction);

    ReactWithDotNet.RegisterExternalJsObject("RegisterScrollEvents", RegisterScrollEvents);
    ReactWithDotNet.RegisterExternalJsObject("InitializeUIDesignerEvents", InitializeUIDesignerEvents);
}