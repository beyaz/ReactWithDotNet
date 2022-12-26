
import ReactWithDotNet from "../react-with-dotnet";

import "./uiw-react-codemirror";

import { Tree } from 'primereact/tree';
import { InputText } from 'primereact/inputtext';

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.Tree", Tree);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.InputText", InputText);

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.PrimeReact.GrabOnlyValueParameterFromCommonPrimeReactEvent", function (argumentsAsArray)
{
    //const originalEvent = argumentsAsArray[0].originalEvent;

    const value = argumentsAsArray[0].value;

    return [{ value: value }];
});