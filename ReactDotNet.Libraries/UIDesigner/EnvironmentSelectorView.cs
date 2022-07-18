using System;
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

    public string SelectedTreeNodeKeys { get; set; }
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

    static IEnumerable<TreeNode> GetNodes()
    {
        return new[]
        {
            new TreeNode
            {
                key = "0", data = "Aloha", icon = "img/Class.svg", label = "Aloha", children =
                {
                    new TreeNode { key = "0-0", data = "Aloha-0-0", label = "Aloha-0-0" },
                    new TreeNode { key = "0-1", data = "Aloha-0-1", label = "Aloha-0-1" },
                    new TreeNode { key = "0-2", data = "Aloha-0-2", label = "Aloha-0-1" }
                }
            },

            new TreeNode
            {
                key = "1", data = "Aloha", icon = "img/Class.svg", label = "Aloha", children =
                {
                    new TreeNode { key = "1-0", data = "Aloha-1-0", label = "Aloha-0-0" },
                    new TreeNode { key = "1-1", data = "Aloha-1-1", label = "Aloha-0-1" },
                    new TreeNode { key = "1-2", data = "Aloha-1-2", label = "Aloha-0-2" }
                }
            }
        };
    }



    
    
    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column"},
            children =
            {
                new SingleSelectionTree
                {
                    filter            = true,
                    filterPlaceholder = "Search Method",
                    nodeTemplate = item => new HPanel
                    {
                        new img { src = "dll.svg", width = 30, height = 30 }, new div(item.label) { style = { marginLeft = "5px" } }
                    },
                    value             = GetNodes(),
                    onSelectionChange = e => { state.SelectedTreeNodeKeys = e.value; },
                    selectionKeys     = state.SelectedTreeNodeKeys
                },

                new Dropdown<EnvironmentInfo>
                {
                    options     = state.ItemsSource,
                    value       = state.SelectedEnvironment,
                    onChange    = e => { state.SelectedEnvironment = e.value; },
                    optionLabel = nameof(EnvironmentInfo.Name),
                    placeholder = "Select environment",
                    itemTemplate = item => new HPanel
                    {
                        new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } }
                    },

                    valueTemplate = item => item == null ? new HPanel { new div("Seçiniz"){style = { margin = "5px"}} } :
                                                           new HPanel { new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } } },
                    filterBy  = nameof(EnvironmentInfo.Name),
                    showClear = true,
                    filter    = true,
                    style     = { width = "300px" }

                },


                new AutoComplete<EnvironmentInfo>
                {
                    suggestions = state.Suggestions, //.Select(x=>x.Name),
                    dropdown    = true,
                    field       = nameof(EnvironmentInfo.Name),

                    value = state.SelectedEnvironment ?? (object)state.SelectedEnvironmentAsString,
                    onChange = e =>
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
                        state.Suggestions = state.ItemsSource.Where(x => x.Name.Contains(e.query, StringComparison.OrdinalIgnoreCase)).ToList();
                    },
                    itemTemplate = item => new HPanel
                    {
                        new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } }
                    }
                }
            }
        };



        ;


        
    }
}