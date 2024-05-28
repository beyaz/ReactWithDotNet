

import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

import Autocomplete from '@mui/material/Autocomplete';

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.OnThirdPartyComponentPropsCalculated('ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete', props => 
{
    if (props.renderInput)
    {
        const element = props.renderInput;

        props.renderInput = (params) =>
        {
            return React.cloneElement(element, { ...params });
        }
    }

    if (props.getOptionLabel)
    {
        const isTwoObjectEquals = ReactWithDotNet.GetExternalJsObject('ReactWithDotNet::Core::IsTwoObjectEquals');

        const array = props.getOptionLabel;

        props.getOptionLabel = (option) =>
        {
            for (let i = 0; i < array.length; i++)
            {
                if (isTwoObjectEquals(array[i].option, option))
                {
                    return array[i].label;
                }
            }

            throw 'DeveloperError: getOptionLabel not covered.';
        };
    }

    return props;
});


ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.ThirdPartyLibraries.MUI.Material.Autocomplete::calculate_onChange_arguments', function (args)
{
    const calculateSyntheticMouseEventArguments = ReactWithDotNet.GetExternalJsObject('ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments');

    return [calculateSyntheticMouseEventArguments([args[0]])[0], args[1]];
});

export default Autocomplete;