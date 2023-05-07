using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Showcases;

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
                    sx    = { height = 140 },
                    image = "https://mui.com/static/images/cards/contemplative-reptile.jpg",
                    title = "green iguana"
                },
                new CardContent
                {
                    new Typography{ variant = "h5" , gutterBottom = true, component ="div",children = { "Lizard" }},
                    new Typography{ variant = "body2" , color = "text.secondary", children = {
                        @"  Lizards are a widespread group of squamate reptiles, with over 6,000
          species, ranging across all continents except Antarctica" 
                    }}
                },
                    
                new CardActions
                {
                    new Button{size ="small",children = { "Share" }},
                    new Button{size ="small",children = { "Learn More" }}

                }
            }
        };
    }
}

