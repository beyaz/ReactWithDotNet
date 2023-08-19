using ReactWithDotNet.ThirdPartyLibraries._Swiper_;

namespace ReactWithDotNet.WebSite.Showcases;

class SwiperThumbsGalleryDemo : ReactComponent
{
    protected override Element render()
    {
        const int mainImageHeight = 350;

        const int thumbnailWidth = 120;
        const int thumbnailHeight = 78;

        var thumbsClassName = $"{nameof(SwiperThumbsGalleryDemo)}-thumbn";

        var mainSlides = Enumerable.Range(1, 10).Select(i => new SwiperSlide
        {
            new img
            {
                src = $"https://swiperjs.com/demos/images/nature-{i}.jpg",
                style =
                {
                    WidthMaximized,
                    Height(mainImageHeight)
                }
            }
        });

        var thumbnSlides = Enumerable.Range(1, 10).Select(i => new SwiperSlide
        {
            new FlexColumn(Gap(15))
            {
                new img
                {
                    src = $"https://swiperjs.com/demos/images/nature-{i}.jpg",
                    style =
                    {
                        Width(thumbnailWidth),
                        Height(thumbnailHeight)
                    }
                },
                new div(When(i-1!=ActiveImageIndex, DisplayNone))
                {
                    Background("#A08139"),
                    Height(4)
                }
            }
           
        });

        return new div(WidthMaximized, HeightAuto)
        {
            new Swiper(mainSlides)
            {
                navigation   = { enabled = true },
                thumbs       = { swiper  = $".{thumbsClassName}" },
                modules      = new[] { "FreeMode", "Navigation", "Thumbs" }
            },
            new Swiper(thumbnSlides)
            {
                allowTouchMove      = true,
                spaceBetween        = 10,
                slidesPerView       = 6,
                freeMode            = true,
                watchSlidesProgress = true,
                navigation          = { enabled = true },
                className           = thumbsClassName,
                modules             = new[] { "FreeMode", "Navigation", "Thumbs" },
                onActiveIndexChange = OnSlideChanged
            },

            //SpaceY(20),
            SpaceY(1) + Background("#C9C9C8")
        };
    }
    
    void OnSlideChanged(SwiperInstance instance)
    {
        ActiveImageIndex = instance.realIndex;
    }
    
    public int ActiveImageIndex { get; set; }
}