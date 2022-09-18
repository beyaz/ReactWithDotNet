using static AlyaVillas.WebUI.Mixin;
using static AlyaVillas.WebUI.MediaSize;

namespace AlyaVillas.WebUI.Components
{
    public class Logo:ReactComponent
    {
        public string On { get; set; } = "light";
        
        protected override Element render()
        {
            return new a
            {
                style = { display = "flex", alignItems = "center"},
                title = SITE_SHORT_NAME,
                children =
                {
                    new img
                    {
                        src   = ImageUrl($"/brand/logo-on-{On}.svg"), alt = SITE_SHORT_NAME + " Logo",
                        style = { height = Context.Is(phone) ? "32px" : "48px", width = "auto"}
                    }
                }
            };
        }
    }
}
