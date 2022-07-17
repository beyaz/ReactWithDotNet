using System.Collections.Generic;
using System.Linq;
using ReactDotNet.PrimeReact;

namespace ReactDotNet.UIDesigner;

class EnvironmentInfo
{
    public string Name { get; set; }
}

class EnvironmentSelectorModel
{
    public IReadOnlyList<EnvironmentInfo> ItemsSource { get; set; }
    public IReadOnlyList<EnvironmentInfo> Suggestions { get; set; }
    public string SelectedEnvironmentName { get; set; }

    public EnvironmentInfo SelectedEnvironment { get; set; }

    public string SelectedEnvironmentAsString { get; set; }
}

class EnvironmentSelectorView : ReactComponent<EnvironmentSelectorModel>
{
    public EnvironmentSelectorView()
    {
        state = new EnvironmentSelectorModel
        {
            ItemsSource = new[] { new EnvironmentInfo { Name = "Development" }, new EnvironmentInfo { Name = "Test" } },
            Suggestions = new[] { new EnvironmentInfo{Name   = "Development" } , new EnvironmentInfo{Name  = "Test" } }
        };
    }
    public override Element render()
    {
        return new AutoComplete
        {
            suggestions    = state.Suggestions,//.Select(x=>x.Name),
            dropdown       = true,
            field          = nameof(EnvironmentInfo.Name),
            
            value          = state.SelectedEnvironment ?? (object)state.SelectedEnvironmentAsString,
            onChange       = e =>
            {
                state.SelectedEnvironment = e.GetValue<EnvironmentInfo>();
                if (state.SelectedEnvironment == null)
                {
                    state.SelectedEnvironmentAsString = e.GetValue<string>();
                }
                else
                {
                    state.SelectedEnvironmentAsString = null;
                }
            },
            completeMethod = e =>
            {
                state.Suggestions = state.ItemsSource.Where(x => x.Name.Contains(e.query)).ToList();
            },
            itemTemplate = new ItemTemplates<EnvironmentInfo> { Items = state.ItemsSource, Template = item => new div { innerText = item.Name + "aloha" } }


        };
    }
}