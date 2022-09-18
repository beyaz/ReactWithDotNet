namespace AlyaVillas.WebUI.Views.Home;

public class Values : ReactComponent
{
    protected override Element render()
    {
        return new section
        {
            new container
            {
                new h1
                {
                    text = "Alya Villa Bodrum is waiting for you with its special comfort for your family.", style =
                    {
                        fontSize = "58px"
                    }
                }
            }
        };
    }
}

public class HomeView : ReactComponent
{
    protected override Element render()
    {
        return new main
        {
            new Hero(),
            new Values()
        };
    }
}