using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.LetterAnalyzerPage;

[Serializable]
public class ViewModel
{
    public string InputText { get; set; }
    
    public bool IsBlocked { get; set; }

    public string ErrorMessage { get; set; }

    public int ClickCount { get; set; }
}

class View : ReactComponent<ViewModel>
{
    protected override Element render()
    {
        var searchPanel = new[]
        {
            When(state.IsBlocked, () => new div { PositionAbsolute, LeftRight(0), TopBottom(0), BackgroundColor("rgba(0, 0, 0, 0.3)"), Zindex(3) }),
            When(state.IsBlocked, () => new FlexRowCentered
            {
                PositionAbsolute, FontWeight700, LeftRight(0), TopBottom(0), Zindex(4),
                Children(new LoadingIcon { wh(17), mr(5) }, "Lütfen bekleyiniz...")
            }),
            new h4 { text = "Harf Analiz", style = { TextAlignCenter } },
            new FlexColumn
            {
                new FlexColumn
                {
                    new div { text = "Arapça metni aşağıdaki kutucuğa yapıştırınız", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },

                    new TextArea { ValueBind = () => state.InputText },

                    new ErrorText { Text = state.ErrorMessage }
                },



                Space(20),

                new FlexRow(JustifyContentFlexEnd)
                {
                    new ActionButton { Label = "Analiz Et", OnClick = OnCaclculateClicked }
                }
            }
        };

        if (state.ClickCount == 0)
        {
            return Container(Panel(searchPanel));
        }


        if (state.IsBlocked)
        {
            return Container(Panel(searchPanel));
        }

        var resultVerseList = new List<LetterColorizer>();

       
       
        var results = new Element[]
        {
            new h4("Sonuçlar"),
            
            new VSpace(30),
            new div
            {
                Children(resultVerseList)
            }
        };

        return Container(Panel(searchPanel), Panel(results));
    }

    static Element Container(params Element[] panels)
    {
        return new FlexColumn(Gap(10), AlignItemsStretch, WidthMaximized, MaxWidth(800))
        {
            Children(panels)
        };
    }

    static Element Panel(IEnumerable<Element> rows)
    {
        return new FlexColumn(BorderRadius(5), ComponentBorder, PaddingLeftRight(15), PaddingBottom(15), PositionRelative)
        {
            Children(rows)
        };
    }

  

    void ClearErrorMessage()
    {
        state.ErrorMessage = null;
    }

   

    void OnCaclculateClicked()
    {
        state.ErrorMessage = null;

        if (state.InputText.HasNoValue())
        {
            state.ErrorMessage = "Arama Komutu doldurulmalıdır";
            Client.GotoMethod(1000, ClearErrorMessage);
            return;
        }

   

        state.ClickCount++;

        

        state.IsBlocked = false;
    }
}