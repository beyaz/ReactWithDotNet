
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";



import 'rsuite/dist/rsuite.min.css';



ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.ReactSuite.AutoComplete", React.lazy(() => import('./AutoComplete')));

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.ReactSuite.AutoComplete::OnChange", function (args)
{
    return [args[0]];
});
