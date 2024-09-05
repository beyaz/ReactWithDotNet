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

    public Swiper(IEnumerable<Element> children, params Modifier[] modifiers)
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

    /// <summary>
    /// Sample: <br/>
    /// // when window width is &gt;= 320px <br/>
    /// 320: { <br/>
    ///  slidesPerView: 2, <br/>
    ///  spaceBetween: 20 <br/>
    /// },
    /// <br/>
    /// <br/>
    /// // when window width is &gt;= 480px <br/>
    /// 480: { <br/>
    ///   slidesPerView: 3, <br/>
    ///   spaceBetween: 30 <br/>
    /// }<br/>
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEnumerableIsEmpty))]
    public Dictionary<int, SwiperBreakpoint> breakpoints { get; } = new();

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
    public SwiperThumbs thumbs { get; } = new ();
    
    //[ReactProp]
    //public string className { get; set; }

    [ReactProp]
    public IReadOnlyList<string> modules { get; set; }

    [ReactProp]
    public string name { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperNavigationOption navigation { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Func<SwiperInstance, Task> onSlideChange { get; set; }
    
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Func<SwiperInstance, Task> onActiveIndexChange { get; set; }
    

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Func<SwiperInstance, Task> onSlideChangeTransitionStart { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperPagination pagination { get; } = new();

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperScrollbar scrollbar { get; } = new();

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet.ThirdPartyLibraries._Swiper_::GrabSwiperInstance")]
    public Func<SwiperInstance, Task> slideChangeTransitionEnd { get; set; }

    [ReactProp]
    public double? slidesPerView { get; set; }
    
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public SwiperGridOption grid { get; } = new();
    
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
    
    
    internal static (bool needToExport, object value)
        GetPropertyValueForSerializeToClient(object component, string propertyName)
    {
        var instance = (Swiper)component;

        if (propertyName == nameof(thumbs) && instance.thumbs is { swiper: not null })
        {
            instance.thumbs.swiper.name ??= Guid.NewGuid().ToString("N");

            return (true, new { swiperInstanceName = instance.thumbs.swiper.name });
        }

        return default;
    }
}

public sealed class SwiperNavigationOption
{
    public bool? enabled { get; set; }
    public string nextEl { get; set; }
    public string prevEl { get; set; }
}

public sealed class SwiperGridOption
{
    public double? rows { get; set; }
    
    /// <summary>
    ///     Can be 'column' or 'row'. Defines how slides should fill rows, by column or by row
    /// </summary>
    public string fill { get; set; }
}


[Serializable]
public sealed class SwiperInstance
{
    public int activeIndex { get; set; }
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

[Serializable]
public sealed record SwiperBreakpoint
{
    public double? slidesPerView { get; init; }
    public double? spaceBetween { get; init; }
    public double? slidesPerColumn { get; init; }
}

[Serializable]
public sealed class SwiperThumbs
{
    public Swiper swiper { get; set; }
}

public class SwiperSlide : ThirdPartyReactComponent
{
    public SwiperSlide(params StyleModifier[] modifiers)
    {
        style.Add(modifiers);
    }
    
    public SwiperSlide()
    {
        
    }
}