

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

export default Autocomplete;