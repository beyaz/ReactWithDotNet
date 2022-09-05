using System;
using System.Collections.Generic;
using System.Linq;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class FolderSelectionView 
{
    public string LastQuery { get; set; }
    public string SelectedFolder { get; set; }
    public IReadOnlyList<string> Suggestions { get; set; }

    public Action<AutoCompleteChangeParams> OnChange { get; set; }
    public Action<AutoCompleteCompleteMethodParams> CompleteMethod { get; set; }

    public  Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column", alignItems = "stretch" },
            children =
            {
                new label { text = "Search Folder", style = { marginBottom = "5px", fontWeight = "bold" } },

                new span
                {
                    className = "p-fluid", 
                    children =
                    {
                        new AutoComplete
                        {
                            suggestions    = Suggestions.Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).ToList(),
                            value          = SelectedFolder,
                            onChange       = OnChange,
                            completeMethod = CompleteMethod,
                            itemTemplate = item => new div
                            {
                                style = { display = "flex", alignItems = "center" },
                                children =
                                {
                                    new img { src  = "img/Folder.svg", width = 20, height = 20 },
                                    new div { text = item, style             = { marginLeft = "7px" } }
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}