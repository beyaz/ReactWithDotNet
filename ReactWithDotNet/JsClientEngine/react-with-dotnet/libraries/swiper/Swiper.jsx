
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

// import Swiper core and required modules
import { Navigation, Pagination, Scrollbar, A11y } from 'swiper';
import { Swiper } from 'swiper/react';
import SwiperCore, { Autoplay, EffectFade } from 'swiper';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';


SwiperCore.use([Autoplay, EffectFade]);

function ToSwiperModule(moduleName)
{
    if (moduleName === 'Navigation' || moduleName === Navigation)
    {
        return Navigation;
    }

    if (moduleName === 'Pagination' || moduleName === Pagination)
    {
        return Pagination;
    }

    if (moduleName === 'Scrollbar' || moduleName === Scrollbar)
    {
        return Scrollbar;
    }

    if (moduleName === 'A11y' || moduleName === A11y)
    {
        return A11y;
    }

    throw 'ToSwiperModule -> invalidArgument: ' + moduleName;
}

function ConvertToSwiperModules(moduleNames)
{
    if (moduleNames)
    {
        for (var i = 0; i < moduleNames.length; i++)
        {
            moduleNames[i] = ToSwiperModule(moduleNames[i]);
        }
    }
   
    return moduleNames;
}

/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.OnThirdPartyComponentPropsCalculated('ReactWithDotNet.ThirdPartyLibraries._Swiper_.Swiper', props =>
{
    if (props.modules)
    {
        ConvertToSwiperModules(props.modules);
    }

    return props;
});

export default Swiper;