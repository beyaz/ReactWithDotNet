using ReactWithDotNet.ThirdPartyLibraries.ReactSuite;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class RSuiteAutoCompleteDemo : Component<RSuiteAutoCompleteDemo.State>
{
    protected override Element render()
    {
        string[] dataSource =
        [
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
        ];

        return new div(SizeFull)
        {
            new AutoComplete<string>
            {
                onChange = e =>
                {
                    state = state with { SelectedValue = e };

                    return Task.CompletedTask;
                },

                placeholder = "Search in names",

                data = dataSource,

                style = { Width(250) }
            },
            new div { state.SelectedValue }
        };
    }

    internal record State
    {
        public string SelectedValue { get; init; }
    }
}