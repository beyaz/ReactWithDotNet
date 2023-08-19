#pragma warning disable CS1591
namespace ReactWithDotNet.ThirdPartyLibraries._Swiper_;

public class Swiper : ThirdPartyReactComponent
{
    public Swiper()
    {
    }

    public Swiper(params StyleModifier[] modifiers) : base(modifiers)
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

    public Swiper(IEnumerable<Element> children, params IModifier[] modifiers)
    {
        if (children is not null)
        {
            this.children.Clear();
            this.children.AddRange(children);
        }

        if (modifiers is not null)
        {
            foreach (var modifier in modifiers)
            {
                ModifyHelper.ProcessModifier(this, modifier);
            }
        }
    }

    [ReactProp]
    public SwiperAutoplay autoplay { get; } = new();

    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEnumerableIsEmpty))]
    public Dictionary<int, dynamic> breakpoints { get; } = new();

    [ReactProp]
    public bool? centeredSlides { get; set; }

    [ReactProp]
    public string direction { get; set; }

    [ReactProp]
    public string effect { get; set; }

    [ReactProp]
    public SwiperFadeEffect fadeEffect { get; } = new();

    [ReactProp]
    public bool? grabCursor { get; set; }

    [ReactProp]
    public bool? init { get; set; }

    [ReactProp]
    public bool? lazy { get; set; }

    [ReactProp]
    public bool? loop { get; set; }
    
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic thumbs { get; } = new ExpandoObject();
    
    [ReactProp]
    public string className { get; set; }

    [ReactProp]
    public IReadOnlyList<string> modules { get; set; }

    [ReactProp]
    public string name { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperNavigationOption navigation { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChange { get; set; }
    
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> onActiveIndexChange { get; set; }
    

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> onSlideChangeTransitionStart { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperPagination pagination { get; } = new();

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperScrollbar scrollbar { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Action<SwiperInstance> slideChangeTransitionEnd { get; set; }

    [ReactProp]
    public double? slidesPerView { get; set; }

    [ReactProp]
    public double? spaceBetween { get; set; }

    [ReactProp]
    public double? speed { get; set; }

    [ReactProp]
    public bool? freeMode { get; set; }
    
    [ReactProp]
    public bool? watchSlidesProgress { get; set; }

    [ReactProp]
    public bool? allowTouchMove { get; set; }
}

public sealed class SwiperNavigationOption
{
    public bool? enabled { get; set; }
    public string nextEl { get; set; }
    public string prevEl { get; set; }
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

public class SwiperSlide : ThirdPartyReactComponent;