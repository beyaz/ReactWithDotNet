
import ReactWithDotNet from "../react-with-dotnet";

// import Swiper core and required modules
import { Navigation, Pagination, Scrollbar, A11y } from 'swiper';
import { Swiper, SwiperSlide } from 'swiper/react';
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

ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.Libraries.Swiper::ConvertToSwiperModules', function (moduleNames)
{
    for (var i = 0; i < moduleNames.length; i++)
    {
        moduleNames[i] = ToSwiperModule(moduleNames[i]);
    }
    return moduleNames;
});

ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance', function (args)
{
    return [{ realIndex: args[0].realIndex }];
});

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.Swiper", Swiper);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.SwiperSlide", SwiperSlide);