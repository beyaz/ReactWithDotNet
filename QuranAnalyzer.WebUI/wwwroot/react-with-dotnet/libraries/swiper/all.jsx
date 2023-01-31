import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

// NOTE: Swiper has bug when Lazy load, we need to load sync mode
import { SwiperSlide } from 'swiper/react';

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.Swiper", React.lazy(() => import('./Swiper')));
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.SwiperSlide", SwiperSlide);

ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance', function (args)
{
    return [{ realIndex: args[0].realIndex }];
});