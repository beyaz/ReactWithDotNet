namespace QuranAnalyzer.WebUI.Pages.InitialLetters;



class AllInitialLetters:ReactComponent<AllInitialLettersModel>
{
    public AllInitialLetters()
    {
        state = new AllInitialLettersModel();
    }
    
    public override Element render()
    {

        Element content = new div();
        if (state.SelectedTabIdentifier == nameof(InitialLetterGroup_HaMim))
        {
            content = new InitialLetterGroup_HaMim();
        }
        else if (state.SelectedTabIdentifier == nameof(InitialLetterGroup_HaMimSeparated))
        {
            content = new InitialLetterGroup_HaMimSeparated();
        }
        else if (state.SelectedTabIdentifier == nameof(InitialLetterGroup_TaSinMim))
        {
            content = new InitialLetterGroup_TaSinMim();
        }
        else if (state.SelectedTabIdentifier == nameof(InitialLetterGroup_Saad))
        {
            content = new InitialLetterGroup_Saad();
        }
        
        var contentContainer = new div
        {
            style = { width_height = "100%"},
            children =
            {
                content
            }
        };

        var headers =  new div
        {
            style = { display = "flex", flexDirection           = "column",
                borderRight   = "1px solid #dee2e6", 
                whiteSpace = "normal" ,
                alignItems = "stretch",
                color         = "rgba(0, 0, 0, 0.6)",
                textAlign = "center",
                cursor = "pointer"

            },
            children =
            {
                new div{text = "Kaf", style    = { padding = "10px", }},
                new div{text = "Kaf", style    =
                {
                    padding = "10px", borderRight = "2px solid #1976d2", color = "#1976d2",
                    
                }},

                new div{text = "Saad",
                    style =
                    {
                        padding     = "10px" ,
                        borderRight = state.SelectedTabIdentifier ==nameof(InitialLetterGroup_Saad) ? "2px solid #1976d2" : null, 
                        
                        color = state.SelectedTabIdentifier ==nameof(InitialLetterGroup_Saad) ? "#1976d2" :null,

                    },
                    
                    onClick = _=>state.SelectedTabIdentifier =nameof(InitialLetterGroup_Saad)
                    
                } ,
                
                new div{text = "Ha Mim", style = { padding = "10px" , },onClick = _=>state.SelectedTabIdentifier =nameof(InitialLetterGroup_HaMim)} ,

                new div{text = "Ha Mim Seperated", style = { padding = "10px" , },onClick = _=>state.SelectedTabIdentifier =nameof(InitialLetterGroup_HaMimSeparated)} ,

                new div{id="taSinMim", text = "Ta Sin Mim", style = { padding = "10px" , }, onClick = _=>state.SelectedTabIdentifier =nameof(InitialLetterGroup_TaSinMim)}
            }
        };

        return new div
        {
            style = {display = "flex", flexDirection = "row", width = "100%", border = "1px solid #dee2e6" },
            children =
            {
                headers,
                contentContainer,
                
            }
        };

    }

}


class AllInitialLettersModel
{
    public string SelectedTabIdentifier { get; set; } 
}
