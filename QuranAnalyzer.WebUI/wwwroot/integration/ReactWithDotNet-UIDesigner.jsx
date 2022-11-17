
import ReactWithDotNet from "../ReactWithDotNet.jsx";

import "./uiw.react-codemirror";

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

ReactWithDotNet.RegisterExternalJsObject("InitializeUIDesignerEvents", InitializeUIDesignerEvents);