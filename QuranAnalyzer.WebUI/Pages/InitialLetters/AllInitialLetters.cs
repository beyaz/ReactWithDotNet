namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class AllInitialLettersModel
{
    #region Public Properties
    public string SelectedTabIdentifier { get; set; }
    #endregion
}

class AllInitialLetters : ReactComponent<AllInitialLettersModel>
{
    protected override void constructor()
    {
        state = new AllInitialLettersModel
        {
            SelectedTabIdentifier = Tabs.FirstOrDefault().contenType.FullName
        };
    }

    #region Static Fields
    static (string TabHeader, Type contenType)[] Tabs =
    {
        ("Kaf 1", typeof(InitialLetterGroup_Qaaf_50)),
        ("Kaf 2", typeof(InitialLetterGroup_Qaaf_42)),
        ("Ya-Sin", typeof(InitialLetterGroup_Chapter36_YaSin)),
        ("42_AinSinKaf", typeof(InitialLetterGroup_Chapter42_AinSinKaf)),
        ("Meryem", typeof(InitialLetterGroup_Chapter19)),
        ("Elif Lam Mim", typeof(InitialLetterGroup_Alif_Laam_Miim)),
        ("Elif Lam Ra", typeof(InitialLetterGroup_Alif_Laam_Raa)),
        ("Elif Lam Mim Sad", typeof(InitialLetterGroup_Alif_Laam_Miim_Sad)),
        ("Elif Lam Mim Ra", typeof(InitialLetterGroup_Alif_Laam_Miim_Ra)),

        ("Saad", typeof(InitialLetterGroup_Saad)),
        ("Ha Mim", typeof(InitialLetterGroup_HaMim)),
        ("Ha Mim", typeof(InitialLetterGroup_HaMimSeparated)),
        ("Ta Sin Mim", typeof(InitialLetterGroup_TaSinMim)),

        ("Nun", typeof(InitialLetterGroup_NunWawNun)),
    };
    #endregion

    #region Methods
    protected override Element render()
    {
        var contentContainer = new div
        {
            style = { width_height = "100%" },
            children =
            {
                CreateTabContent()
            }
        };

        var headers = new div
        {
            style =
            {
                display       = "flex",
                flexDirection = "column",
                whiteSpace    = "normal",
                alignItems    = "stretch",
                color         = "rgba(0, 0, 0, 0.6)",
                textAlign     = "center",
                cursor        = "pointer"
            },
            Children = Tabs.Select(x => CreateTabHeader(x.TabHeader, x.contenType.FullName))
        };

        return new div
        {
            style = { display = "flex", flexDirection = "row", width = "100%", border = "1px solid #dee2e6" },
            children =
            {
                headers,
                contentContainer
            }
        };
    }

    Element CreateTabContent()
    {
        Element content = new div();

        if (state.SelectedTabIdentifier.HasValue())
        {
            content = (Element)Activator.CreateInstance(Type.GetType(state.SelectedTabIdentifier) ?? throw new TypeLoadException(state.SelectedTabIdentifier));
        }

        return content;
    }

    Element CreateTabHeader(string text, string identifier)
    {
        var isSelected = state.SelectedTabIdentifier == identifier;

        var element = new div
        {
            id   = identifier,
            text = text,

            style =
            {
                padding     = "10px",
                borderRight = $"1px solid {(isSelected ? "#1976d2" : "#dee2e6")}",
            },

            onClick = OnTabHeaderClick
        };

        if (isSelected)
        {
            element.style.background = "rgb(203 213 223 / 8%)";
            element.style.color      = "#1976d2";
        }

        return element;
    }

    void OnTabHeaderClick(MouseEvent e) => state.SelectedTabIdentifier = e.FirstNotEmptyId;
    #endregion
}