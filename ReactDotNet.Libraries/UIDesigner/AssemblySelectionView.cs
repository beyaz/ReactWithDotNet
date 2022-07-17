using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReactDotNet.PrimeReact;

namespace ReactDotNet.UIDesigner;



class AssemblySelectionViewModel
{
    public string SelectedFolder{ get; set; }

    public IReadOnlyList<string> Suggestions { get; set; } = new[] {@"d:\boa\server\bin", @"d:\boa\client\bin" };

    public string LastQuery { get; set; }

}

class AssemblySelectionView : ReactComponent<AssemblySelectionViewModel>
{
    public AssemblySelectionView()
    {
        state = new AssemblySelectionViewModel();
    }
    public override Element render()
    {
        return new AutoComplete
        {
            suggestions = Directory.EnumerateFiles(@"d:\boa\server\bin\").Where(x => x.Contains(state.LastQuery??"", StringComparison.OrdinalIgnoreCase)).Take(10),

            value = state.SelectedFolder,
            onChange = e =>
            {
                state.SelectedFolder = e.GetValue<string>();
            },
            completeMethod = e =>
            {
                state.LastQuery = e.query;
            }
        };
    }
}