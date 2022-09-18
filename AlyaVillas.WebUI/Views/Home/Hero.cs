using static ReactWithDotNet.Mixin;

namespace AlyaVillas.WebUI.Views.Home
{
    public class Hero:ReactComponent
    {
        protected override Element render()
        {

            var heroImage = new div
            {
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
                style = { },
                children =
                {
                    new h1{text = "Alya Villa Bodrum is waiting for you with its special comfort for your family.", style =
                    {
                        marginTopBottom = "36px",
                        color = "white",
                        fontSize = "58px",
                        maxWidth = "960px",
                        
                        fontWeight = "400",
                        lineHeight = "1.25",
                        fontFamily = "Marcellus,-apple-system,BlinkMacSystemFont,Segoe UI,Roboto,Oxygen,Ubuntu,Cantarell,Open Sans,Helvetica Neue,sans-serif"
                    }}
                }
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
