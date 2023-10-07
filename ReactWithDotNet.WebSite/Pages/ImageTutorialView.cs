using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Pages;

class TutorialItem
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageSrc { get; set; }
}

class TutorialItemView : PureComponent
{
    public TutorialItem Model { get; set; }

    protected override Element render()
    {
        if (Model == null)
        {
            return "Model is empty";
        }

        return new article
        {
            When(Model.Title is not null, new h2{Model.Title}),
            
            new div{Model.Description},
            
            new img(Src(Model.ImageSrc), BorderRadiusForPaper, Width(400), HeightAuto, Title(Model.ImageSrc))
        };
    }
}

class ImageTutorialView : Component
{
    public IReadOnlyList<TutorialItem> Items { get; set; }
    
    public int  SelectedIndex { get; set; }
    
    protected override Element render()
    {
        return new FlexRowCentered(PositionRelative, Padding(10), BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px") , Padding(10),BorderRadiusForPaper)
        {
            new TutorialItemView{ Model = Items[SelectedIndex]},
            
            new FlexColumn(Gap(5),PositionAbsolute,Right(5), Top(5))
            {
                new FlexRow(Gap(5))
                {
                    new Button
                    {
                        variant  = "outlined",
                        onClick  = _ => SelectedIndex--,
                        disabled = SelectedIndex<=0,
                        children = { "<" }
                    },
                    new Button
                    {
                        variant  = "outlined",
                        onClick  = _ => SelectedIndex++,
                        disabled = SelectedIndex >= Items.Count-1,
                        children = { ">" } 
                    }
                },
                new FlexRowCentered{$"{SelectedIndex+1} of {Items.Count}"}
            },
           
        };
    }
}