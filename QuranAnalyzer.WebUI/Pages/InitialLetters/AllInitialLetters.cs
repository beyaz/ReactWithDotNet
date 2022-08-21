namespace QuranAnalyzer.WebUI.Pages.InitialLetters;



class AllInitialLetters:ReactComponent<AllInitialLettersModel>
{
    public AllInitialLetters()
    {
        state = new AllInitialLettersModel();
    }

    Element CreateTabHeader(string text, string identifier)
    {
        return new div
        {
            id = identifier,
            text = text,
            
            style =
            {
                padding     = "10px",
                borderRight = state.SelectedTabIdentifier == identifier ? "1.7px  solid #1976d2" : null,
                color       = state.SelectedTabIdentifier == identifier ? "#1976d2" : null,
                
            },

            // onClick = _ => state.SelectedTabIdentifier = _
            onClick = OnTabHeaderClick

        };
    }

    void OnTabHeaderClick(string _) => state.SelectedTabIdentifier = _;
    public override Element render()
    {

        var contentContainer = new div
        {
            style = { width_height = "100%"},
            children =
            {
                CreateContent()
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
                CreateTabHeader("Kaf",nameof(InitialLetterGroup_Saad)),
                CreateTabHeader("Saad",nameof(InitialLetterGroup_Saad)),
                CreateTabHeader("Ha Mim",nameof(InitialLetterGroup_HaMim)),
                CreateTabHeader("Ha Mim",nameof(InitialLetterGroup_HaMimSeparated)),
                CreateTabHeader("Ta Sin Mim",nameof(InitialLetterGroup_TaSinMim)),

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

    Element CreateContent()
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

        return content;
    }
}


class AllInitialLettersModel
{
    public string SelectedTabIdentifier { get; set; } 
}
