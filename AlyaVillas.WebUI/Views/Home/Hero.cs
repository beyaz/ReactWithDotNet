using static ReactWithDotNet.Mixin;

namespace AlyaVillas.WebUI.Views.Home
{
    public class Hero:ReactComponent
    {
        protected override Element render()
        {

            var heroImage = new div
            {
                className = "heroImage",
                style =
                {
                    position = "absolute",
                    top="0",
                    bottom = "0",
                    left="0",
                    right="0",
                    zIndex = "1",
                    background= $"url({Mixin.ImageUrl("/assets/pages/home/hero/hero-image.jpg")})" ,
                    backgroundSize = "100%"
                }
            };

            var content = new FlexColumn
            {
                new h1(TitleStyle,
                       Text("Alya Villa Bodrum is waiting for you with its special comfort for your family."),
                       MarginTopBottom(36),
                       Color("white"),
                       FontSize(58),
                       MaxWidth(960))
            };
            return new section
            {
                style =
                {
                    position = "relative",
                    marginTop = "-90px",
                    height = "100vh",
                    maxHeight = "900px",
                    display = "flex",
                    alignItems = "center"
                },
                children =
                {
                    new div
                    {
                        style =
                        {
                            position = "absolute",
                            top = "0",bottom = "0", left = "0",right = "0",backgroundColor = "rgba(74,74,73,0.5)",
                            zIndex = "2"
                        }
                    },
                    heroImage,
                    new container(Zindex(2), DisplayFlex, FlexDirectionColumn, AlignItemsCenter)
                    {
                        content,
                        new img{src = Mixin.ImageUrl("/assets/pages/home/hero/seperatordouble.svg"),style = { width = "85px", height = "27px", marginBottom = "50px"}}
                    }
                }
            };

        }
    }
}
