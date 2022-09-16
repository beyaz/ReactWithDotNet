
import React from 'react';

import ReactWithDotNet from "../ReactWithDotNet.jsx";

// import Swiper core and required modules
import { Navigation, Pagination, Scrollbar, A11y } from 'swiper';
import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import 'swiper/css/scrollbar';


ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.Swiper", React.forwardRef(function (props, ref) 
    {
        return (
            <Swiper ref={ref}
                // install Swiper modules
                  modules={[Navigation, Pagination, Scrollbar, A11y]}
                  spaceBetween={props.spaceBetween || 50}
                  slidesPerView={props.slidesPerView || 3}
                  navigation
                  pagination={{ clickable: true }}
                  scrollbar={{ draggable: true }}>
                {props.children}
            </Swiper>
        );
  }
));
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.swiper.SwiperSlide", SwiperSlide);