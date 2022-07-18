using System;
using System.Collections.Generic;
using System.Linq;
using ReactDotNet.PrimeReact;
using static ReactDotNet.UIDesigner.DOM;

namespace ReactDotNet.UIDesigner;



class FolderSelectionViewModel
{
    public string SelectedFolder{ get; set; }

    public IReadOnlyList<string> Suggestions { get; set; } = new[] {@"d:\boa\server\bin\", @"d:\boa\client\bin\" };

    public string LastQuery { get; set; }

}

class FolderSelectionView : ReactComponent<FolderSelectionViewModel>
{
    public FolderSelectionView()
    {
        state = new FolderSelectionViewModel();
    }
    public override Element render()
    {
        return new AutoComplete
        {
            suggestions = state.Suggestions.Where(x => x.Contains(state.LastQuery??"", StringComparison.OrdinalIgnoreCase)).ToList(),

            value = state.SelectedFolder,
            onChange = e =>
            {
                state.SelectedFolder = e.GetValue<string>();
            },
            completeMethod = e =>
            {
                state.LastQuery = e.query;
            },
            itemTemplate = item => new div
            {
                style = { display = "flex", alignItems = "center"},
                children =
                {
                    new img { src = "img/Folder.svg", width = 20, height = 20 }, 
                    new div { text = item, style = { marginLeft = "7px" } }
                }
            }
        };
    }
}


