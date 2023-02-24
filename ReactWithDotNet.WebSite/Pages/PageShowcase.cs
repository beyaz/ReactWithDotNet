using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.Pages;

class PageShowcase : ReactComponent
{
    public string SearchValue { get; set; }
    
    protected override Element render()
    {
        return new FlexColumn
        {
            new FlexRow(Gap(5),AlignItemsCenter)
            {
                new span(FontSize(40))
                {
                    className = "material-icons",
                    text      = "filter_list"
                },
                new TextField
                {
                    valueBind                = ()=>SearchValue,
                    valueBindDebounceTimeout = 500,
                    valueBindDebounceHandler = OnSearchFinished
                }
            },
            
            new FlexRow
            {
                Enumerable.Range(0, SearchValue?.Length ?? 5).Select(i => new div{i.ToString(), Padding(5)})
            }
            

        };
    }

    void OnSearchFinished()
    {
    }
}