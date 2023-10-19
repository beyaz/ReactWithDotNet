

using ReactWithDotNet.ThirdPartyLibraries.ReactSuite;

namespace ReactWithDotNet.WebSite.Showcases;

public class RSuiteAutoCompleteDemo : Component
{
    public string SelectedValue { get; set; }
    
    protected override Element render()
    {
        return new div(WidthHeightMaximized)
        {
            new AutoComplete
            {
                onChange = e => Task.FromResult(SelectedValue = e),

                placeholder = "Search in names",
                data = new[]
                {
                    "Eugenia",
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
                    "Hilda"
                },

                style = { width = "224px" }
            },
            new div(SelectedValue)
        };
    }
}

