using ReactWithDotNet.ThirdPartyLibraries._Swiper_;

namespace ReactWithDotNet.WebSite.Showcases;

class SwiperThumbsGalleryDemo : ReactPureComponent
{
    protected override Element render()
    {

        var thumbsClassName = $"{nameof(SwiperThumbsGalleryDemo)}-thumbn";

        var mainSlides=Enumerable.Range(1, 10).Select(i => new SwiperSlide
        {
            new img
            {
                src = $"https://swiperjs.com/demos/images/nature-{i}.jpg",
                style =
                {
                    width = "100%",
                    height = "400px"
                }
            }
        });
        
        
        var thumbnSlides =Enumerable.Range(1, 10).Select(i => new SwiperSlide
        {
            new img
            {
                src = $"https://swiperjs.com/demos/images/nature-{i}.jpg",
                style =
                {
                    width  = "100px",
                    height = "50px"
                }
            }
        });
        
        return new div(Width(900), HeightAuto, BoxSizingBorderBox, Padding(10),Border("1px solid #d4dad4"))
        {
            
            new Swiper(mainSlides)
            {
                spaceBetween = 5,
                navigation   = { enabled = true},
                thumbs       = { swiper  = $".{thumbsClassName}" },
                modules      = new []{"FreeMode","Navigation" ,"Thumbs"}
            },
            new Swiper(thumbnSlides)
            {
                spaceBetween = 10,
                slidesPerView = 8,
                freeMode = true,
                watchSlidesProgress = true,
                navigation   = { enabled = true},
                className = thumbsClassName,
                modules      = new []{"FreeMode","Navigation" ,"Thumbs"},
                style =
                {
                    //width = "600px"
                }
            },
            
           
        };
    }
}