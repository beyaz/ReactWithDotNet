using ReactWithDotNet.Libraries.ReactSuite;

namespace ReactWithDotNet.WebSite.Showcases;

public class RSuiteAutoCompleteDemo : ReactPureComponent
{
    protected override Element render()
    {
        return new div(WidthHeightMaximized)
        {
            new AutoComplete
            {
                placeholder = "Search in names",
               data=new []{"Eugenia",
                   "Bryan",
                   "Linda",
                   "Nancy",
                   "Lloyd",
                   "Alice",
                   "Julia",
                   "Albert",
                   "Louisa",
                   "Lester",
                   "Lola",
                   "Lydia",
                   "Hal",
                   "Hannah",
                   "Harriet",
                   "Hattie",
                   "Hazel",
                   "Hilda"},
               
               style = { width = "224px"}
            }
        };
    }
}

