
import ReactWithDotNet from "../ReactWithDotNet.jsx";

import "./react-simple-code-editor";

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

if (process.env.NODE_ENV !== 'production')
{
    ReactWithDotNet.RegisterExternalJsObject("InitializeUIDesignerEvents", InitializeUIDesignerEvents);
}
