#pragma warning disable CS1591
using System.Text.Json.Serialization;
using System.Text.Json;

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
[JsonConverter(typeof(SwiperBreakpointJsonConverter))]
[FastSerialize]
public sealed class SwiperBreakpoint
{
    public double? slidesPerView { get; init; }
    public double? spaceBetween { get; init; }
    public double? slidesPerColumn { get; init; }
    
    public double? slidesPerGroup { get; init; }
    
    public bool? loop { get; init; }
    
    public bool? centeredSlides { get; init; }
    
    [SerializeAsNullWhenEmpty]
    public SwiperGridOption grid { get; } = new();
    
    [SerializeAsNullWhenEmpty]
    public SwiperPagination pagination { get; } = new();
    
    [SerializeAsNullWhenEmpty]
    public SwiperNavigationOption navigation { get; } = new();
    
    [SerializeAsNullWhenEmpty]
    public SwiperAutoplay autoplay { get; } = new();
    
    internal class SwiperBreakpointJsonConverter : JsonConverter<SwiperBreakpoint>
    {
        public override SwiperBreakpoint Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, SwiperBreakpoint value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            if (value.slidesPerView.HasValue)
            {
                writer.WritePropertyName(nameof(slidesPerView));
                writer.WriteNumberValue(value.slidesPerView.Value);
            }
            
            if (value.spaceBetween.HasValue)
            {
                writer.WritePropertyName(nameof(spaceBetween));
                writer.WriteNumberValue(value.spaceBetween.Value);
            }
            
            if (value.slidesPerColumn.HasValue)
            {
                writer.WritePropertyName(nameof(slidesPerColumn));
                writer.WriteNumberValue(value.slidesPerColumn.Value);
            }
            
            if (value.slidesPerGroup.HasValue)
            {
                writer.WritePropertyName(nameof(slidesPerGroup));
                writer.WriteNumberValue(value.slidesPerGroup.Value);
            }
            
            if (value.loop.HasValue)
            {
                writer.WritePropertyName(nameof(loop));
                writer.WriteBooleanValue(value.loop.Value);
            }
            
            if (value.centeredSlides.HasValue)
            {
                writer.WritePropertyName(nameof(centeredSlides));
                writer.WriteBooleanValue(value.centeredSlides.Value);
            }
            
            if (value.grid.rows.HasValue)
            {
                writer.WritePropertyName(nameof(grid));
                JsonSerializer.Serialize(writer, value.grid, options);
            }
            
            if (value.pagination.clickable.HasValue)
            {
                writer.WritePropertyName(nameof(pagination));
                JsonSerializer.Serialize(writer, value.pagination, options);
            }
            
            writer.WriteEndObject();
        }
        
    }
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