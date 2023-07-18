

import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

import Autocomplete from '@mui/material/Autocomplete';

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.OnThirdPartyComponentPropsCalculated('ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete', props => {
    if (props.renderInput)
    {
        var element = props.renderInput;

        props.renderInput = (params) =>
        {
            return React.cloneElement(element, { ...params });
        }
    }

    return props;
});


ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments', function (args)
{
    const calculateSyntheticMouseEventArguments = ReactWithDotNet.GetExternalJsObject('ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments');

    return [calculateSyntheticMouseEventArguments([args[0]])[0], args[1]];
});

export default Autocomplete;