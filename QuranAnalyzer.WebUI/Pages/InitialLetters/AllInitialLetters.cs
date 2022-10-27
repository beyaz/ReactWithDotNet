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
        var contentContainer = new div(StretchWidthHeight)
        {
            CreateTabContent()
        };

        var headers = new FlexColumn(TextAlignCenter, CursorPointer)
        {
            Children(Tabs.Select(x => CreateTabHeader(x.TabHeader, x.contenType.FullName)))
        };

        return new FlexRow(Width100Percent, Border("1px solid #dee2e6"))
        {
            headers,
            contentContainer
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

        return new div
        {
            id   = identifier,
            text = text,

            style =
            {
                Color("rgba(0, 0, 0, 0.6)"),
                Padding(10),
                BorderRight($"1px solid {(isSelected ? BluePrimary : "#dee2e6")}"),
                When(isSelected, Background("#deecf9"), Color(BluePrimary))
            },

            onClick = OnTabHeaderClick
        };
    }
    
    void OnTabHeaderClick(MouseEvent e) => state.SelectedTabIdentifier = e.FirstNotEmptyId;
    #endregion
}