import ReactWithDotNet from "../react-with-dotnet";

import 'rsuite/dist/rsuite.min.css';

import { AutoComplete } from 'rsuite';

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.ReactSuite." + name, value);
}


register("AutoComplete", AutoComplete);
register("AutoComplete::OnChange", function (args)
{
    return [args[0]];
});

