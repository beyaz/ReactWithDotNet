using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.Pages;

class PageShowcase : ReactComponent
{
    public string SearchValue { get; set; }
    
    protected override Element render()
    {
        return new div
        {
            new Paper
            {
                component = "form", sx = { p = "2px 4px", display = "flex", alignItems = "center", width = 400 },
                children =
                {
                    // new InputBase { sx  = { ml = 1, flex = 1 }, placeholder = "Search in samples" },
                    new TextField { sx = { ml = 1, flex = 1 },valueBind = ()=>SearchValue, valueBindDebounceTimeout = 500, valueBindDebounceHandler = OnSearchFinished},
                    new IconButton
                    {
                        type="button",
                        sx = { p= "10px" },
                        children =
                        {
                            new span { className = "material-icons", text = "search" }
                        }
                    }
                }
            },
            
            Enumerable.Range(0, SearchValue?.Length ?? 5).Select(i => new div{i.ToString(), Padding(5)})

        };
    }

    void OnSearchFinished()
    {
    }
}