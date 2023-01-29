import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.Swiper", React.lazy(() => import('./Swiper')));
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.SwiperSlide", React.lazy(() => import('./SwiperSlide')));


ReactWithDotNet.RegisterExternalJsObject('ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance', function (args)
{
    return [{ realIndex: args[0].realIndex }];
});