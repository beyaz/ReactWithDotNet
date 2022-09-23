

using System;
using System.Collections.Generic;

#pragma warning disable CS1591
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
    public bool? navigation { get; set; }

    [React]
    public bool? init { get; set; }

    [React]
    public bool? grabCursor { get; set; }

    [React]
    public int? loopAdditionalSlides { get; set; }

    [React]
    public SwiperPagination pagination { get; } = new();

    [React]
    public SwiperScrollbar scrollbar { get; } = new();

    [React]
    public SwiperAutoplay autoplay { get; } = new();
    

    [React]
    [ReactTransformValueInClient("ReactWithDotNet.Libraries.Swiper::ConvertToSwiperModules")]
    public IReadOnlyList<string> modules { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChangeTransitionStart { get; set; }
}

[Serializable]
public sealed class SwiperInstance
{
    public int realIndex { get; set; }
}

public sealed class SwiperPagination
{
    public bool? clickable { get; set; }
}

public sealed class SwiperScrollbar
{
    public bool? draggable { get; set; }
}

public sealed class SwiperAutoplay
{
    public double? delay { get; set; }
    public bool? disableOnInteraction { get; set; }
}

public class SwiperSlide : ThirdPartyReactComponent
{
    
}
