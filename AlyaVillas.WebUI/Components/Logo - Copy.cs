using ReactWithDotNet.Libraries.swiper;

namespace AlyaVillas.WebUI.Components;

public class BrownButton : ReactComponent
{
    public string Text { get; set; } = "Dummy";
        
    public string Icon { get; set; }

    public Action<MouseEvent> OnClick { get; set; }

    protected override Element render()
    {
        return new div
        {
            onClick = OnClick,
            //onMouseEnter = OnMouseEnter,
            style =
            {
                padding        = "12px",
                display        = "flex",
                justifyContent = "center",
                alignItems     = "center", flexDirection = "row", background = "#B19045", color = "#F7F1E4"
            },
            children =
            {
                new div(Text){style = { marginRight                           = "8px"}},
                new i {className    = $"icon {Icon}", style = { color = "white"}},
            }
        };
    }

    void OnMouseEnter(MouseEvent obj)
    {
        
        
    }
}


class VillaShower:ReactComponent
{
    protected override Element render()
    {
        return new div
        {
            style = { width_height = "400px" },
            children =
            {
                new Swiper
                {
                    new SwiperSlide
                    {
                        new div{text = "A" ,style = {    width_height = "200px", border  = "1px solid red"}}
                    },
                    new SwiperSlide
                    {
                        new div{text = "B" ,style = {    width_height = "200px", border = "1px solid red"}}
                    },
                    new SwiperSlide
                    {
                        new div{text = "A" ,style = {    width_height = "200px", border = "1px solid red"}}
                    },
                    new SwiperSlide
                    {
                        new div{text = "B" ,style = {    width_height = "200px", border = "1px solid red"}}
                    },
                    new SwiperSlide
                    {
                        new div{text = "A" ,style = {    width_height = "200px", border = "1px solid red"}}
                    },
                    new SwiperSlide
                    {
                        new div{text = "B" ,style = {    width_height = "200px", border = "1px solid red"}}
                    }
                }
            }
        };

    }
}