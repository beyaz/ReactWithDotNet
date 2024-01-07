using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

class StyleSearchInput : Component
{

    
    
    protected override Element render()
    {

        return new FlexColumn
        {
            new style
            {
                """
                input:focus { outline:none; }
                """
            },
            new FlexRow(AlignItemsCenter)
            {
                new input
                {
                    type                     = "text",
                    valueBind                = () => Value,
                    valueBindDebounceTimeout = 700,
                    valueBindDebounceHandler = OnTypingFinished,
                    onBlur                   = OnBlur,
                    onKeyDown                = OnKeyDown,
                    style =
                    {
                        BackgroundWhite, 
                        Border(Solid(0.1,"#bcc4e3")),
                        BorderRadius(3),
                        PaddingLeft(3),
                        PaddingTopBottom(5),
                        FlexGrow(1), FontFamily("Arial"), FontSize12,
                        Color(rgb(0, 6, 36)),
                        LetterSpacing(0.3)
                    }
                },
                    
                new FlexRowCentered(PaddingLeftRight(4), CursorDefault)
                {
                    new DeleteIcon()
                }


            },
            Suggestions
        };
    }

    Task OnBlur(FocusEvent e)
    {
        ShowSuggestions = false;
            
        return Task.CompletedTask;
    }

    Element Suggestions()
    {
        if (ShowSuggestions is false)
        {
            return null;
        }

        var suggestionItemList = GetCurrentSuggestions();

        if (suggestionItemList.Count == 0)
        {
            return null;
        }
            
        return new FlexColumn(PositionRelative,SizeFull)
        {
            new FlexColumn(PositionAbsolute,SizeFull, HeightAuto, Background("white"), BoxShadow(0, 6, 6, 0, rgba(22,45,61,.06)),Padding(5), BorderRadius(5))
            {
                suggestionItemList.Select(ToOption)
            }
        };
    }

        
        
    public int? SelectedSuggestionOffset { get; set; }


    static readonly List<string> AllSuggestions = new();

    static StyleSearchInput()
    {
        foreach (var propertyInfo in typeof(Mixin).GetProperties(BindingFlags.Static | BindingFlags.Public))
        {
            AllSuggestions.Add(propertyInfo.Name);
        }
        
        AllSuggestions.Add("@Media Screen > 400");
    }

    IReadOnlyList<string> GetCurrentSuggestions()
    {
        return AllSuggestions.Where(x => x.Contains(Value + "", StringComparison.OrdinalIgnoreCase))
            .Take(5).ToList();
    }
        
    Element ToOption(string code, int index)
    {
        return new div
        {
            code,
            PaddingLeft(5),
            Color(rgb(0, 6, 36)),
                
            index == SelectedSuggestionOffset ? Color("#495cef") + Background("#e7eaff") : null
        };
    }
        
        
    [ReactKeyboardEventCallOnly("ArrowDown","ArrowUp","Enter")]
    Task OnKeyDown(KeyboardEvent e)
    {
        if (e.key == "ArrowDown")
        {
            SelectedSuggestionOffset ??= -1;

            SelectedSuggestionOffset++;
        }
            
        if (e.key == "ArrowUp")
        {
            SelectedSuggestionOffset ??= -1;

            SelectedSuggestionOffset--;
        }
            
        if (e.key == "Enter")
        {
            //DispatchEvent(()=>OnChange, GetProperties()[SelectedSuggestionOffset.Value].Name);
                
            //SelectedSuggestionOffset = null;

            ShowSuggestions = false;

            Client.DispatchEvent<OnDesignerManagedStyleChanged>("Background(\"yellow\")");
                
            Value = GetCurrentSuggestions()[SelectedSuggestionOffset.Value];
        }
            
            
        return Task.CompletedTask;
    }
        

        

    Task OnTypingFinished()
    {
        ShowSuggestions = true;
            
        SelectedSuggestionOffset = null;
            
        return Task.CompletedTask;
    }

    public bool ShowSuggestions { get; set; }
        
    public string Value { get; set; }
    
    
    class DeleteIcon : Component
    {
        public bool IsMouseEnter { get; set; }
        
        protected override Element render()
        {
            return new svg(svg.Size(24), ViewBox(0, 0, 24, 24))
            {
                OnMouseEnter(OnMouseEnterHandler),
                OnMouseLeave(OnMouseLeaveHandler),
                
                new path { fill = IsMouseEnter ? "#7f98f6": "#bcc4e3", d = "M17,17 C17,18.6568542 15.6568542,20 14,20 L9,20 C7.34314575,20 6,18.6568542 6,17 L6,7 L5,7 L5,6 L18,6 L18,7 L17,7 L17,17 Z M9,9 L10,9 L10,16 L9,16 L9,9 Z M11,9 L12,9 L12,16 L11,16 L11,9 Z M13,9 L14,9 L14,16 L13,16 L13,9 Z M7,17 C7,18.1045695 7.8954305,19 9,19 L14,19 C15.1045695,19 16,18.1045695 16,17 L16,7 L7,7 L7,17 Z M13,6 L13,5 L10,5 L10,6 L9,6 L9,5 C9,4.44771525 9.44771525,4 10,4 L13,4 C13.5522847,4 14,4.44771525 14,5 L14,6 L13,6 Z" }
            };
        }

        [CacheThisMethod]
        Task OnMouseEnterHandler(MouseEvent e)
        {
            IsMouseEnter = true;
            
            return Task.CompletedTask;
        }
        
        [CacheThisMethod]
        Task OnMouseLeaveHandler(MouseEvent e)
        {
            IsMouseEnter = false;
            
            return Task.CompletedTask;
        }
    }
}


delegate Task OnDesignerManagedStyleChanged(string newValue);