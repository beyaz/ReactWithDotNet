
import ReactWithDotNet from "../react-with-dotnet";

import "./uiw-react-codemirror";
import { Slider } from 'primereact/slider';
import { Tree } from 'primereact/tree';
import { ScrollPanel } from 'primereact/scrollpanel';


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

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.Slider", Slider);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.Tree", Tree);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.ScrollPanel", ScrollPanel);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.GrabOnlyValueParameterFromCommonPrimeReactEvent", function (argumentsAsArray)
{
    //const originalEvent = argumentsAsArray[0].originalEvent;

    const value = argumentsAsArray[0].value;

    return [{ value: value }];
});