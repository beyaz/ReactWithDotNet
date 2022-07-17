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
        return new Dropdown
        {
            options = state.ItemsSource,
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

            optionLabel = nameof(EnvironmentInfo.Name),
            placeholder = "Select environment",
            itemTemplate = new ItemTemplates<EnvironmentInfo>
            {
                Items = state.ItemsSource,
                Template = item => new HPanel
                {
                    new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } }
                }
            },

            valueTemplate = new ItemTemplates<EnvironmentInfo>
            {
                Items = state.ItemsSource,
                Template = item => new HPanel
                {
                    new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } }
                },
                TemplateForNull = new HPanel
                {
                     new div("Seçiniz"){style = { margin = "5px"}}
                }
            },
            filterBy  = nameof(EnvironmentInfo.Name),
            showClear = true,
            filter    = true,
            style = { width = "400px"}

        };
        // 


        //return new AutoComplete
        //{
        //    suggestions = state.Suggestions, //.Select(x=>x.Name),
        //    dropdown = true,
        //    field = nameof(EnvironmentInfo.Name),

        //    value = state.SelectedEnvironment ?? (object)state.SelectedEnvironmentAsString,
        //    onChange = e =>
        //    {
        //        state.SelectedEnvironment = e.GetValue<EnvironmentInfo>();
        //        if (state.SelectedEnvironment == null)
        //        {
        //            state.SelectedEnvironmentAsString = e.GetValue<string>();
        //        }
        //        else
        //        {
        //            state.SelectedEnvironmentAsString = null;
        //        }
        //    },
        //    completeMethod = e =>
        //    {
        //        state.Suggestions = state.ItemsSource.Where(x => x.Name.Contains(e.query, StringComparison.OrdinalIgnoreCase)).ToList();
        //    },
        //    itemTemplate = new ItemTemplates<EnvironmentInfo>
        //    {
        //        Items = state.ItemsSource,
        //        Template = item => new HPanel
        //        {
        //            new img { src = "dll.svg", width = 30, height = 30 }, new div(item.Name) { style = { marginLeft = "5px" } }
        //        }
        //    }
        //};
    }
}