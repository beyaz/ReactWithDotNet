using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.Showcases
{
    public class MuiCardDemo : ReactPureComponent
    {
        protected override Element render()
        {
            return new Card
            {
                sx = { maxWidth = 345 },
                children =
                {
                    new CardMedia
                    {
                        sx = { height= 140 },
                        image = "https://mui.com/static/images/cards/contemplative-reptile.jpg",
                        title = "green iguana"
                    }
                }
            };
        }
    }
}
