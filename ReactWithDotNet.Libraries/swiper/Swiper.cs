

namespace ReactWithDotNet.Libraries.swiper;

public class Swiper : ThirdPartyReactComponent
{
    [React]
    public int? spaceBetween { get; set; }


    [React]
    public int? slidesPerView { get; set; }

    [React]
    public int? speed { get; set; }
    
    [React]
    public string effect { get; set; }

    [React]
    public bool? loop { get; set; }

    [React]
    public bool? grabCursor { get; set; }

    [React]
    public int? loopAdditionalSlides { get; set; }
}

public class SwiperSlide : ThirdPartyReactComponent
{
    
}
