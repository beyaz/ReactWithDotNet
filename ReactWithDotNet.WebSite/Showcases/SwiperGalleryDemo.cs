using ReactWithDotNet.ThirdPartyLibraries._Swiper_;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class SwiperGalleryDemo : PureComponent
{
    protected override Element render()
    {
        var slides = Enumerable.Range(1, 8).Select(i => new SwiperSlide
        {
            new img
            {
                src = $"https://swiperjs.com/demos/images/nature-{i}.jpg",
                style =
                {
                    WidthFull,
                    Height(350),
                    ObjectFitCover
                }
            }
        });

        return new div(WidthFull, HeightAuto)
        {
            new Swiper(slides)
            {
                navigation = { enabled = true },
                modules    = ["FreeMode", "Navigation"],
                breakpoints =
                {
                    {500,new (){ slidesPerView  = 1}}
                }
            }
        };
    }
}