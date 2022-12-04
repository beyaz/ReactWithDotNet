namespace QuranAnalyzer.WebUI.Pages.InitialLetters;

class AllInitialLettersModel
{
    public int SelectedTabIndex { get; set; }
}

class AllInitialLetters : ReactComponent<AllInitialLettersModel>
{
    protected override void constructor()
    {
        state = new AllInitialLettersModel();

        var value = Context.Query[QueryKey.FactIndex];
        if (value is not null && int.TryParse(value, out int index) && index >0 && index<Tabs.Count)
        {
            state.SelectedTabIndex = index;
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
            Children(Tabs.Select((x,i) => CreateTabHeader(x.TabHeader, i)))
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
        return (Element)Activator.CreateInstance(Tabs[state.SelectedTabIndex].contenType);
    }

    Element CreateTabHeader(string text, int index)
    {
        var isSelected = state.SelectedTabIndex == index;

        return new a(Href(GetTabUrl(index)))
        {
            text = text,

            style =
            {
                Width(100),
                Color("rgba(0, 0, 0, 0.6)"),
                Padding(10),
                TextDecorationNone,
                When(isSelected,BorderRight($"1px solid {(isSelected ? BluePrimary : BorderColor)}")),
                When(isSelected, Background("#deecf9"), Color(BluePrimary))
            }
        };
    }
    
    static string GetTabUrl(int index)
    {
        return $"/?{QueryKey.Page}={PageId.InitialLetters}&{QueryKey.FactIndex}={index}";
    }
}