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
            
            value          = state.SelectedEnvironmentName,
            onChange       = e => { state.SelectedEnvironmentName = e.value;  },
            completeMethod = e => { state.Suggestions             = state.ItemsSource.Where(x => x.Name.Contains(e.query)).ToList(); }
        };
    }
}