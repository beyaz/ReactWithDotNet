import ReactWithDotNet from "../../react-with-dotnet";

import Slider from '@mui/material/Slider';


ReactWithDotNet.RegisterExternalJsObject('mui_slider_onChangeCommitted', function (args)
{
    return [args[1]];
});

ReactWithDotNet.RegisterExternalJsObject('mui_slider_onChange', function (args) {
    return [args[1]];
});


export default Slider;