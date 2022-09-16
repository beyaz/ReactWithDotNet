

namespace ReactWithDotNet.Libraries.swiper;

public class Swiper : ThirdPartyReactComponent
{
    [React]
    public int? spaceBetween { get; set; }


    [React]
    public int? slidesPerView { get; set; }
}

public class SwiperSlide : ThirdPartyReactComponent
{
    
}
