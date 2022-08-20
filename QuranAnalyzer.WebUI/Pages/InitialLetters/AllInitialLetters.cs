namespace QuranAnalyzer.WebUI.Pages.InitialLetters;



class AllInitialLetters:ReactComponent<AllInitialLettersModel>
{
    public AllInitialLetters()
    {
        state = new AllInitialLettersModel();
    }
    
    public override Element render()
    {

        var contentContainer = new div
        {
            style = { width_height = "100%"}
        };

        var headers =  new div
        {
            style    = { display = "flex", flexDirection = "column",borderRight = "3px solid blue"},
            children =
            {
                new div{text = "Kaf"},
                new div{text = "Kaf"},
                new div{text = "Ha-Mim"} 
            }
        };

        return new div
        {
            style = {display  = "flex", flexDirection = "row"},
            children =
            {
                contentContainer,
                headers
            }
        };

    }
}


class AllInitialLettersModel
{
}
