using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

class StyleSearchInput : Component
{
    Element IconSearch => new svg(svg.Size(18), ViewBox(0, 0, 18, 18),MarginLeftRight(5))
    {
        new path { fill ="#dbdde5",  d = "M14.8539503,14.1467096 C15.0478453,14.3412138 15.0475893,14.6560006 14.8533783,14.8501892 C14.6592498,15.0442953 14.3445263,15.0442862 14.1504091,14.8501689 L12.020126,12.7261364 C11.066294,13.5214883 9.8390282,14 8.5,14 C5.46243388,14 3,11.5375661 3,8.5 C3,5.46243388 5.46243388,3 8.5,3 C11.5375661,3 14,5.46243388 14,8.5 C14,9.83874333 13.5216919,11.0657718 12.726644,12.0195172 L14.8539503,14.1467096 Z M8.5,13 C10.9852814,13 13,10.9852814 13,8.5 C13,6.01471863 10.9852814,4 8.5,4 C6.01471863,4 4,6.01471863 4,8.5 C4,10.9852814 6.01471863,13 8.5,13 Z" }
    };

    Element IconClose => new svg(svg.Size(18), ViewBox(0, 0, 18, 18))
    {
        new path { fill ="#65676b", d = "M8.44 9.5L6 7.06A.75.75 0 1 1 7.06 6L9.5 8.44 11.94 6A.75.75 0 0 1 13 7.06L10.56 9.5 13 11.94A.75.75 0 0 1 11.94 13L9.5 10.56 7.06 13A.75.75 0 0 1 6 11.94L8.44 9.5z" }
    };
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
            new FlexRow
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
                        FlexGrow(1), FontFamily("inherit"), FontSize("inherit")
                    }
                },
                    
                ////IconSearch,
                //new FlexRowCentered(PaddingLeftRight(10), CursorDefault)
                //{
                //    "-",
                        
                //    FontSize19,
                //    FontWeight600,
                //    Border(Solid(1, "#dbdde5")),
                //    Hover(Border(Solid(1, "#bcc4e3")))
                //}


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

        var suggestionItemList = GetProperties();

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

      
        

    IReadOnlyList<PropertyInfo> GetProperties()
    {
        return typeof(Mixin).GetProperties(BindingFlags.Static | BindingFlags.Public)
            .Where(p => p.Name.Contains(Value + "", StringComparison.OrdinalIgnoreCase))
            .Take(5).ToList();
    }
        
    Element ToOption(PropertyInfo p, int index)
    {
        return new div
        {
            p.Name,
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
                
            Value = GetProperties()[SelectedSuggestionOffset.Value].Name;
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
}


delegate Task OnDesignerManagedStyleChanged(string newValue);