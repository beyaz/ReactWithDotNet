namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class AllInitialLettersModel
{
    public string SelectedTabIdentifier { get; set; }
}

class AllInitialLetters : ReactComponent<AllInitialLettersModel>
{
    protected override void constructor()
    {
        state = new AllInitialLettersModel
        {
            SelectedTabIdentifier = Tabs.FirstOrDefault().contenType.FullName
        };

        var value = Context.Query[QueryKey.FactIndex];
        if (value is not null && int.TryParse(value, out int index) && index >0 && index<Tabs.Count)
        {
            state.SelectedTabIdentifier = Tabs[index].contenType.FullName;
        }

    }

    static List<(string TabHeader, Type contenType)> Tabs =new()
    {
        ("Kaf 1", typeof(InitialLetterGroup_Qaaf_50)),
        ("Kaf 2", typeof(InitialLetterGroup_Qaaf_42)),
        ("Ya Sin", typeof(InitialLetterGroup_Chapter36_YaSin)),
        ("Ayn Sin Kaf", typeof(InitialLetterGroup_Chapter42_AinSinKaf)),
        ("Meryem", typeof(InitialLetterGroup_Chapter19)),
        ("Elif Lam Mim", typeof(InitialLetterGroup_Alif_Laam_Miim)),
        ("Elif Lam Ra", typeof(InitialLetterGroup_Alif_Laam_Raa)),
        ("Elif Lam Mim Sad", typeof(InitialLetterGroup_Alif_Laam_Miim_Sad)),
        ("Elif Lam Mim Ra", typeof(InitialLetterGroup_Alif_Laam_Miim_Ra)),

        ("Saad", typeof(InitialLetterGroup_Saad)),
        ("Ha Mim 1", typeof(InitialLetterGroup_HaMim)),
        ("Ha Mim 2", typeof(InitialLetterGroup_HaMimSeparated)),
        ("Ta Sin Mim", typeof(InitialLetterGroup_TaSinMim)),

        ("Nun", typeof(InitialLetterGroup_NunWawNun)),
    };
    protected override Element render()
    {
        var contentContainer = new FlexColumn(JustifyContentFlexStart,WidthHeightMaximized, MarginTop(30))
        {
            CreateTabContent()
        };

        var headers = new FlexColumn(TextAlignCenter, CursorPointer, JustifyContentFlexStart)
        {
            Children(Tabs.Select(x => CreateTabHeader(x.TabHeader, x.contenType.FullName)))
        };

        return new FlexRow(WidthMaximized, Border($"1px solid {BorderColor}"), PositionRelative)
        {
            headers,
            contentContainer,
            new div{PositionAbsolute, Top(0),Bottom(0),Width(1),MarginLeft(100+10+10+1),BackgroundColor(BorderColor) }
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
                Width(100),
                Color("rgba(0, 0, 0, 0.6)"),
                Padding(10),
                // BorderRight($"1px solid {(isSelected ? BluePrimary : BorderColor)}"),
                When(isSelected,BorderRight($"1px solid {(isSelected ? BluePrimary : BorderColor)}")),
                When(isSelected, Background("#deecf9"), Color(BluePrimary))
            },

            onClick = OnTabHeaderClick
        };
    }
    
    void OnTabHeaderClick(MouseEvent e)
    {
        state.SelectedTabIdentifier = e.FirstNotEmptyId;

        UpdateUrl();
    }

    void UpdateUrl()
    {
        var index = Tabs.FindIndex(x => x.contenType.FullName == state.SelectedTabIdentifier);
        
        Client.PushHistory("", $"/?{QueryKey.Page}={PageId.InitialLetters}&{QueryKey.FactIndex}={index}");
    }
}