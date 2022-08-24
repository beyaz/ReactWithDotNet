namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class AllInitialLettersModel
{
    public string SelectedTabIdentifier { get; set; }
}


class AllInitialLetters :ReactComponent<AllInitialLettersModel>
{
    public AllInitialLetters()
    {
        state = new AllInitialLettersModel();
    }

    Element CreateTabHeader(string text, string identifier)
    {
        var isSelected = state.SelectedTabIdentifier == identifier;
        
        return new div
        {
            id = identifier,
            text = text,
            
            style =
            {
                padding     = "10px",
                borderRight = $"1px solid {(isSelected ? "#1976d2" : "#dee2e6")}",
                color       = isSelected ? "#1976d2" : null,
                
            },

            onClick = OnTabHeaderClick
        };
    }

    void OnTabHeaderClick(string tabIdentifier) => state.SelectedTabIdentifier = tabIdentifier;
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

        var headers = new div
        {
            style =
            {
                display     = "flex", 
                flexDirection = "column",
                whiteSpace  = "normal",
                alignItems  = "stretch",
                color       = "rgba(0, 0, 0, 0.6)",
                textAlign   = "center",
                cursor      = "pointer"

            },
            children =
            {
                CreateTabHeader("Kaf 1", typeof(InitialLetterGroup_Qaaf_50).FullName),
                CreateTabHeader("Kaf 2", typeof(InitialLetterGroup_Qaaf_42).FullName),
                CreateTabHeader("Ya-Sin", typeof(InitialLetterGroup_Chapter36_YaSin).FullName),
                CreateTabHeader("42_AinSinKaf", typeof(InitialLetterGroup_Chapter42_AinSinKaf).FullName),
                CreateTabHeader("Meryem", typeof(InitialLetterGroup_Chapter19).FullName),
                CreateTabHeader("Elif Lam Mim", typeof(InitialLetterGroup_Alif_Laam_Miim).FullName),
                CreateTabHeader("Elif Lam Ra", typeof(InitialLetterGroup_Alif_Laam_Raa).FullName),
                CreateTabHeader("Elif Lam Mim Sad", typeof(InitialLetterGroup_Alif_Laam_Miim_Sad).FullName),
                CreateTabHeader("Elif Lam Mim Ra", typeof(InitialLetterGroup_Alif_Laam_Miim_Ra).FullName),
                
                CreateTabHeader("Saad", typeof(InitialLetterGroup_Saad).FullName),
                CreateTabHeader("Ha Mim", typeof(InitialLetterGroup_HaMim).FullName),
                CreateTabHeader("Ha Mim", typeof(InitialLetterGroup_HaMimSeparated).FullName),
                CreateTabHeader("Ta Sin Mim", typeof(InitialLetterGroup_TaSinMim).FullName),

                CreateTabHeader("Nun Waw Nun", typeof(InitialLetterGroup_NunWawNun).FullName),
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
        
        if (state.SelectedTabIdentifier.HasValue())
        {
            content = (Element)Activator.CreateInstance(Type.GetType(state.SelectedTabIdentifier) ?? throw new TypeLoadException(state.SelectedTabIdentifier));
        }
        
        return content;
    }
}


