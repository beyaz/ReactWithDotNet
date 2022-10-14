
import ReactWithDotNet from "../ReactWithDotNet.jsx";

//import 'rsuite/styles/index.less';

import { AutoComplete } from 'rsuite';

const Prefix = "ReactWithDotNet.Libraries.ReactSuite.";

ReactWithDotNet.RegisterExternalJsObject(Prefix + "AutoComplete", AutoComplete);

ReactWithDotNet.RegisterExternalJsObject(Prefix + "AutoComplete::OnChange", function (args)
{
    return [args[0]];
})

