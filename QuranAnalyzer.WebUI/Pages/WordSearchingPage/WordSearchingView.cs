using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

namespace QuranAnalyzer.WebUI.Pages.WordSearchingPage;

class WordSearchingViewModel
{
    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public string SearchScript { get; set; }

    public string SearchScriptErrorMessage { get; set; }

    public bool IsHelpVisible { get; set; }
}

class WordSearchingView : ReactComponent<WordSearchingViewModel>
{
    protected override void componentDidMount()
    {
        Client.OnArabicKeyboardPressed(ArabicKeyboardPressed);
    }

    protected override void constructor()
    {
        state = new WordSearchingViewModel();

        var value = Context.Query[QueryKey.SearchQuery];
        if (value is not null)
        {
            var parseResponse = SearchScript.ParseScript(value);
            if (parseResponse.IsFail)
            {
                state.SearchScriptErrorMessage = parseResponse.FailMessage;
                Client.GotoMethod(3000, ClearErrorMessage);
                return;
            }

            state.SearchScript = parseResponse.Value.AsReadibleString();
        }
    }

    Element Help()
    {
        void onClickHandler(MouseEvent _)
        {
            state.ClickCount = 0;
            
            state.IsHelpVisible = !state.IsHelpVisible;
        }

        return new FlexRow(AlignItemsCenter, Gap(5), CursorPointer, OnClick(onClickHandler))
        {
            new img
            {
                wh(20),
                state.IsHelpVisible ? SrcArrowUp : SrcArrowDown
            },
            "Örnek arama komutları"
        };
    }
     
    static Element HelpDetail()
    {
        return new div(TextAlignCenter)
        {
            MarginTop(30),
            
           new table
           {
               new tbody
               {
                   new tr
                   {
                       new th{"Komut"}+FontWeight500,new th{"Açıklama"}+FontWeight500
                   },
                   new tr{Height(15)},
                   new tr
                   {
                       commandText("* | الله" ),
                       new td{ "(Tüm Kuran boyunca geçen ", (b)"الله", " kelimesini aratır)" }
                   },
                   new tr{Height(10)},
                   new tr
                   {
                       commandText("2:* | الله" ),
                       new td{ "(2. surede geçen ", (b)"الله", " kelimesini aratır)" }
                   },
                   new tr{Height(10)},
                   new tr
                   {
                       commandText("2:*, 3:*, 7:5-40 | الله"),
                       new td{ "(2. surenin tamamında, 3. surenin tamamında ve 7. surenin 5 ila 40. ayetler arasında geçen ", (b)"الله", " kelimesini aratır)" }
                   },
                   new tr{Height(10)},
                   new tr
                   {
                       commandText("2:*, -2:4, -2:8 | الله"),
                       new td{ "(2. surenin tamamında(4. ve 8. ayetler hariç), geçen ", (b)"الله", " kelimesini aratır)" }
                   },
                   new tr{Height(10)},
                   new tr
                   {
                       commandText("*, -9:128, -9:129 | الله"),
                       new td{ "(Tüm mushaf boyunca (9:128 ve 9:129 hariç), geçen ", (b)"الله", " kelimesini aratır)" }
                   },
                   new tr{Height(10)},
                   new tr
                   {
                       commandText("2:17 --> 5:4 | الله"),
                       new td{ "(2. surenin 17. ayeti ile 5. surenin 4. ayeti arasında geçen ", (b)"الله", " kelimesini aratır)" }
                   }
               }
           }
        };

        static Element commandText(string text)
        {
            return new td { (b)text } + Width(150);
        }
    }
    
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

            new h4 { text = "Kelime Arama", style = { textAlign = "center" } },

            new FlexColumn
            {
                new FlexColumn
                {
                    new div { text = "Arama Komutu", style = { fontWeight = "500", fontSize = "0.9rem", marginBottom = "2px" } },

                    new TextArea { ValueBind = () => state.SearchScript, style = { FontFamily_Lateef } }, // rows = 2, autoResize = true,

                    new ErrorText { Text = state.SearchScriptErrorMessage }
                },

                Space(15),

                new FlexRow(JustifyContentSpaceBetween)
                {
                    Help(),
                    new ActionButton { Label = "Ara", OnClick = OnCaclculateClicked }
                },

                When(state.IsHelpVisible, HelpDetail)
            }
        };

        if (state.ClickCount == 0)
        {
            return Container(Panel(searchPanel));
        }

        var searchScript = SearchScript.ParseScript(state.SearchScript).Unwrap();

        if (state.IsBlocked)
        {
            return Container(Panel(searchPanel));
        }

        var matchMap = new Dictionary<string, List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>>();

        var summaryInfoList = new List<SummaryInfo>();

        foreach (var (chapterFilter, searchWord) in searchScript.Lines)
        {
            var verseList = VerseFilter.GetVerseList(chapterFilter).Unwrap();

            foreach (var verse in verseList)
            {
                var startAndEndPointsOfSameWords = verse.GetStartAndEndPointsOfSameWords(searchWord);
                if (startAndEndPointsOfSameWords.Count > 0)
                {
                    if (!matchMap.ContainsKey(verse.Id))
                    {
                        matchMap.Add(verse.Id, new List<(IReadOnlyList<LetterInfo> searchWord, IReadOnlyList<(LetterInfo start, LetterInfo end)> startPoints)>());
                    }

                    matchMap[verse.Id].Add((searchWord, startAndEndPointsOfSameWords));

                    // update summary
                    {
                        if (summaryInfoList.All(x => x.Name != searchWord.AsText()))
                        {
                            summaryInfoList.Add(new SummaryInfo { Name = searchWord.AsText() });
                        }

                        summaryInfoList.First(x => x.Name == searchWord.AsText()).Count += startAndEndPointsOfSameWords.Count;
                    }
                }
            }
        }

        var sumOfChapterNumbers = 0;
        var sumOfVerseNumbers   = 0;
        var sumOfCounts = 0;

        var resultVerseList     = new List<WordColorizedVerse>();

        foreach (var (verseId, matchList) in matchMap.ToList().OrderBy(x => x.Key, new VerseNumberComparer()))
        {
            resultVerseList.Add(new WordColorizedVerse
            {
                Verse     = VerseFilter.GetVerseById(verseId),
                MatchList = matchList
            });

            sumOfChapterNumbers += int.Parse(verseId.Split(':')[0]);
            sumOfVerseNumbers   += int.Parse(verseId.Split(':')[1]);

            sumOfCounts += matchList.Sum(x=>x.startPoints.Count).Unwrap();
        }

        sumOfChapterNumbers.ToString();
        
        
        var results = new Element[]
        {
            new h4("Sonuçlar"),

            new CountsSummaryView { Counts = summaryInfoList },
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

    void ArabicKeyboardPressed(string letter)
    {
        state.SearchScriptErrorMessage = null;
        state.ClickCount               = 0;
        state.SearchScript             = state.SearchScript?.Trim() + " " + letter;
    }

    void ClearErrorMessage()
    {
        state.SearchScriptErrorMessage = null;
    }

    void OnCaclculateClicked()
    {
        state.SearchScriptErrorMessage = null;
        if (state.SearchScript.HasNoValue())
        {
            state.SearchScriptErrorMessage = "Arama Komutu doldurulmalıdır";
            Client.GotoMethod(1000, ClearErrorMessage);
            return;
        }

        var scriptParseResponse = SearchScript.ParseScript(state.SearchScript);
        if (scriptParseResponse.IsFail)
        {
            state.SearchScriptErrorMessage = scriptParseResponse.FailMessage;
            Client.GotoMethod(3000, ClearErrorMessage);
            return;
        }

        var script = scriptParseResponse.Value;

        state.ClickCount++;

        if (state.IsBlocked == false)
        {
            state.IsBlocked = true;
            Client.PushHistory("", $"/?{QueryKey.Page}={PageId.WordSearchingPage}&{QueryKey.SearchQuery}={script.AsString()}");
            Client.GotoMethod(OnCaclculateClicked);
            return;
        }

        state.IsBlocked = false;
    }
}