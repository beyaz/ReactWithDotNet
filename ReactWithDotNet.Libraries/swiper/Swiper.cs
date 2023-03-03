
#pragma warning disable CS1591
namespace ReactWithDotNet.Libraries.swiper;

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
    [React]
    public string direction { get; set; }

    [React]
    public bool? lazy { get; set; }
    
    
    [React]
    public SwiperAutoplay autoplay { get; } = new();

    [React]
    public string effect { get; set; }

    [React]
    public bool? grabCursor { get; set; }

    [React]
    public bool? init { get; set; }

    [React]
    public bool? loop { get; set; }

    [React]
    public int? loopAdditionalSlides { get; set; }

    [React]
    public IReadOnlyList<string> modules { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChangeTransitionStart { get; set; }
    
    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChange { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.Libraries.Swiper::GrabSwiperInstance")]
    public Action<SwiperInstance> slideChangeTransitionEnd { get; set; }
    

    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperPagination pagination { get; } = new();

    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperScrollbar scrollbar { get; } = new();

    [React]
    public double? slidesPerView { get; set; }

    [React]
    public double? spaceBetween { get; set; }

    [React]
    public double? speed { get; set; }

    [React]
    public bool? centeredSlides { get; set; }
    


    [React]
    public SwiperFadeEffect fadeEffect { get; } = new();

    

    [React]
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