using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class Events
{
    public const string FolderChanged = nameof(FolderChanged);
    public const string AssemblyChanged = nameof(AssemblyChanged);
}

class FolderSelectionViewModel
{
    public string SelectedFolder{ get; set; }

    public IReadOnlyList<string> Suggestions { get; set; } = new[] {@"d:\boa\server\bin\", @"d:\boa\client\bin\" };

    public string LastQuery { get; set; }

}

class FolderSelectionViewPure : ReactComponent
{
    public string lastQuery;
    public string selectedFolder;
    public  IReadOnlyList<string> suggestions;


    public Action<AutoCompleteChangeParams> onChange { get; set; }
    public Action<AutoCompleteCompleteMethodParams> completeMethod { get; set; }

    public override Element render()
    {
        return new AutoComplete
        {
            suggestions = suggestions.Where(x => x.Contains(lastQuery ?? "", StringComparison.OrdinalIgnoreCase)).ToList(),

            value = selectedFolder,
            onChange = onChange,
            completeMethod =completeMethod,
            itemTemplate = item => new div
            {
                style = { display = "flex", alignItems = "center" },
                children =
                {
                    new img { src  = "img/Folder.svg", width = 20, height = 20 },
                    new div { text = item, style             = { marginLeft = "7px" } }
                }
            }
        };
    }
}
class FolderSelectionView : ReactComponent<FolderSelectionViewModel>
{
    public string SelectedFolder { get; set; }
    
    public FolderSelectionView()
    {
        state = new FolderSelectionViewModel();
        
        StateInitialized += () =>
        {
            state.SelectedFolder = SelectedFolder ?? state.SelectedFolder;
        };

    }
    
    public override Element render()
    {
        return new FolderSelectionViewPure
        {
           
        };



        //return new AutoComplete
        //{
        //    suggestions = state.Suggestions.Where(x => x.Contains(state.LastQuery??"", StringComparison.OrdinalIgnoreCase)).ToList(),

        //    value = state.SelectedFolder,
        //    onChange = e =>
        //    {
        //        state.SelectedFolder = e.GetValue<string>();
        //        if (Directory.Exists(state.SelectedFolder))
        //        {
        //            Context.ClientTasks = new[] { new ClientTaskDispatchEvent { EventName = Events.FolderChanged, EventArguments = new object[] { state.SelectedFolder } } };
        //        }
                
        //    },
        //    completeMethod = e =>
        //    {
        //        state.LastQuery = e.query;
        //    },
        //    itemTemplate = item => new div
        //    {
        //        style = { display = "flex", alignItems = "center"},
        //        children =
        //        {
        //            new img { src = "img/Folder.svg", width = 20, height = 20 }, 
        //            new div { text = item, style = { marginLeft = "7px" } }
        //        }
        //    }
        //};
    }

    
}


