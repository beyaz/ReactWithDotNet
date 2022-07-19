using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class AssemblySelectionView : ReactComponent
{
    public string LastQuery { get; set; }
    public string SelectedFolder { get; set; }
    public string SelectedAssembly { get; set; }

    public Action<AutoCompleteChangeParams> OnChange { get; set; }
    public Action<AutoCompleteCompleteMethodParams> CompleteMethod { get; set; }

    public override Element render()
    {
        var suggestions = new List<string>();

        if (!string.IsNullOrWhiteSpace(SelectedFolder))
        {
            try
            {
                suggestions = Directory.EnumerateFiles(SelectedFolder).Select(Path.GetFileName).Where(x => x.Contains(LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return new div
        {
            style = { display = "flex", flexDirection = "column" },
            children =
            {
                new label { text = "Search Assembly", style = { marginBottom = "5px", fontWeight = "bold" } },
                
                new span
                {
                    className = "p-fluid",
                    children =
                    {
                        new AutoComplete
                        {
                            suggestions = suggestions,

                            value          = SelectedAssembly,
                            onChange       = OnChange,
                            completeMethod = CompleteMethod
                        }
                    }
                }
               
            }
        };
    }
}