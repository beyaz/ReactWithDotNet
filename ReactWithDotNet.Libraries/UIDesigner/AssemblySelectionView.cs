using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;



class AssemblySelectionViewModel
{
    public string SelectedFolder{ get; set; }

    public string SelectedAssembly { get; set; }

    public IReadOnlyList<string> Suggestions { get; set; } = new[] {@"d:\boa\server\bin", @"d:\boa\client\bin" };

    public string LastQuery { get; set; }

}

class AssemblySelectionView : ReactComponent<AssemblySelectionViewModel>
{
    public string SelectedFolder { get; set; }
    public string SelectedAssembly { get; set; }
    
    public AssemblySelectionView()
    {
        state            =  new AssemblySelectionViewModel();
        StateInitialized += () =>
        {
            state.SelectedFolder   = SelectedFolder ?? state.SelectedFolder;
            state.SelectedAssembly = SelectedAssembly ?? state.SelectedAssembly;
        };
    }

    public void ComponentDidMount()
    {
        Context.ClientTasks = new[] { new ClientTaskListenEvent { EventName = Events.FolderChanged, RouteToMethod = nameof(OnFolderChanged) } };
    }

    public void OnFolderChanged(string folderPath)
    {
        state.SelectedFolder = folderPath;
    }

    static (T value, Exception exception) Try<T>(Func<T> func)
    {
        try
        {
            return (func(), null);
        }
        catch (Exception exception)
        {
            return (default, exception);
        }
    }

    public override Element render()
    {
        var suggestions = new List<string>();

        if (!string.IsNullOrWhiteSpace(state.SelectedFolder))
        {
            try
            {
                suggestions = Directory.EnumerateFiles(state.SelectedFolder).Select(Path.GetFileName).Where(x => x.Contains(state.LastQuery ?? "", StringComparison.OrdinalIgnoreCase)).Take(10).ToList();
            }
            catch (Exception)
            {
                // ignored
            }
        }
        
            
            
        return new AutoComplete
        {
            suggestions = suggestions,

            value = state.SelectedAssembly,
            onChange = e =>
            {
                state.SelectedAssembly = e.GetValue<string>();

                if (File.Exists(Path.Combine(state.SelectedFolder, state.SelectedAssembly)))
                {
                    Context.ClientTasks = new[] { new ClientTaskDispatchEvent { EventName = Events.AssemblyChanged, EventArguments = new object[] { Path.Combine(state.SelectedFolder, state.SelectedAssembly) } } };
                }
            },
            completeMethod = e =>
            {
                state.LastQuery = e.query;
            }
        };
    }
}