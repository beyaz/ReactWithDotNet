namespace ReactWithDotNet.WebSite.Components;


class FaqItemState
{
    public bool IsOpen { get; set; }
}

[ReactHigherOrderComponent]
class FaqItem : Component<FaqItemState>
{
    public string Title { get; set; }
    
    public Element Content { get; set; }
    
    protected override Element render()
    {
        return new FlexColumn(Padding(16))
        {
            new FlexRow(JustifyContentSpaceBetween)
            {
                new h3(FontWeight700)
                {
                    Title
                },
                
                new svg(ViewBox(0, 0, 24, 24), Size(24,24), OnClick(OnDropDownClicked))
                {
                    new path{d="M8.12 9.29 12 13.17l3.88-3.88c.39-.39 1.02-.39 1.41 0 .39.39.39 1.02 0 1.41l-4.59 4.59c-.39.39-1.02.39-1.41 0L6.7 10.7a.9959.9959 0 0 1 0-1.41c.39-.38 1.03-.39 1.42 0z"}
                }
            },
            
            state.IsOpen?
            Content:null
        };

    }

    void OnDropDownClicked(MouseEvent obj)
    {
        state.IsOpen = !state.IsOpen;
    }
}