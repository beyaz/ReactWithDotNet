
#pragma warning disable CS1591
namespace ReactWithDotNet.ThirdPartyLibraries._Swiper_;

public class Swiper : ThirdPartyReactComponent
{
    public Swiper()
    {
        
    }

    public Swiper(params StyleModifier[] modifiers):base(modifiers)
    {
    }

    public Swiper(IEnumerable<Element> children)
    {
        if (children is not null)
        {
            this.children.Clear();
            this.children.AddRange(children);
        }
    }
    [ReactProp]
    public string direction { get; set; }

    [ReactProp]
    public bool? lazy { get; set; }
    
    
    [ReactProp]
    public SwiperAutoplay autoplay { get; } = new();

    [ReactProp]
    public string effect { get; set; }

    [ReactProp]
    public bool? grabCursor { get; set; }

    [ReactProp]
    public bool? init { get; set; }

    [ReactProp]
    public bool? loop { get; set; }

    //[ReactProp]
    //public int? loopAdditionalSlides { get; set; }

    [ReactProp]
    public IReadOnlyList<string> modules { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChangeTransitionStart { get; set; }
    
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChange { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> slideChangeTransitionEnd { get; set; }
    

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperPagination pagination { get; } = new();

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperScrollbar scrollbar { get; } = new();

    [ReactProp]
    public double? slidesPerView { get; set; }

    [ReactProp]
    public double? spaceBetween { get; set; }

    [ReactProp]
    public double? speed { get; set; }

    [ReactProp]
    public bool? centeredSlides { get; set; }
    


    [ReactProp]
    public SwiperFadeEffect fadeEffect { get; } = new();

    

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperNavigationOption navigation { get; } = new();
}

public sealed class SwiperNavigationOption
{
    public string prevEl { get; set; }
    public string nextEl { get; set; }
    public bool? enabled { get; set; }
}


[Serializable]
public sealed class SwiperInstance
{
    public int realIndex { get; set; }
}

public sealed class SwiperFadeEffect
{
    public bool? crossFade { get; set; }
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