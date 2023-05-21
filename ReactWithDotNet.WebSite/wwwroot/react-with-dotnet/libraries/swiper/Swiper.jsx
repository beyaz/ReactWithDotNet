
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

    throw 'ReactWithDownet: Swiper module not recognized. Please customize here [integration->swiper.jsx]';
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

const SwiperForwarded = React.forwardRef((props, ref) => (

    <Swiper ref={ref}
        autoplay={props.autoplay}
        effect={props.effect}
        grabCursor={props.grabCursor}
        init={props.init}
        loop={props.loop}
        //loopAdditionalSlides={props.loopAdditionalSlides}
        onSlideChangeTransitionStart={props.onSlideChangeTransitionStart}
        onSlideChange={props.onSlideChange}
        slideChangeTransitionEnd={props.slideChangeTransitionEnd}
        pagination={props.pagination}
        scrollbar={props.scrollbar}
        slidesPerView={props.slidesPerView}
        spaceBetween={props.spaceBetween}
        speed={props.speed}
        centeredSlides={props.centeredSlides}
        fadeEffect={props.fadeEffect}
        navigation={props.navigation}
        modules={ConvertToSwiperModules(props.modules)} >
       {props.children}
    </Swiper>

));

export default SwiperForwarded










// -------------------------------







ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.ThirdPartyLibraries._Swiper_::ConvertToSwiperModules', );