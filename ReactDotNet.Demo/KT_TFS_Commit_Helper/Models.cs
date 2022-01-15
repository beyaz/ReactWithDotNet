using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KT_TFS_Commit_Helper
{
   
    [Serializable]
    public class FileInfo
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class SolutionInfo
    {
        public string Name { get; set; }
    }

    [Serializable]
    public class MainViewModel
    {
        public SolutionInfo[] Solutions { get; set; }

        public string SelectedSolution { get; set; }

        public string Comment { get; set; }

        public FileInfo[] ChangedFiles { get; set; }
        public bool IsBlocked { get;  set; }

        public ClientTask ClientTask { get; set; } 

        public bool CanShowSuccessIcon { get; set; }
    }


    class MainView : ReactComponent<MainViewModel>
    {
        public MainView()
        {
            state = new MainViewModel
            {
                Solutions = new[]
                {
                    new SolutionInfo{Name="CCO"},
                    new SolutionInfo{Name="CCA"},
                    new SolutionInfo{Name="CCA_1"},
                    new SolutionInfo{Name="CCA_2"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"},
                    new SolutionInfo{Name="CCA_3"}
                },
                ChangedFiles = new FileInfo[0],
            };
        }

        void OnSolutionSelected(string solutionName)
        {
            state.SelectedSolution = solutionName;
            state.Comment = "Deneme";
            state.IsBlocked = false;
            state.CanShowSuccessIcon = false;

            state.ChangedFiles = new[]
            {
                new FileInfo{ Name = "A.cs"},
                new FileInfo{ Name = "B.cs"},
                new FileInfo{ Name = "C.cs"}
            };
        }
        public override Element render()
        {
            void Push()
            {
                if (state.IsBlocked == false)
                {
                    state.IsBlocked = true;
                    state.ClientTask = new ClientTask {  ComebackWithLastAction = true };
                    return;
                }

                Thread.Sleep(3000);

                state.IsBlocked = false;
                state.CanShowSuccessIcon = true;
            }

            return new BlockUI
            {
                blocked = state.IsBlocked,
                template = new div { new i { className = "pi pi-spin pi-spinner" }, new div { Margin = { Left = 5 }, style = { Color = "White" }, text = "pushing..." } }.MakeCenter(),
                Children =
                {
                    new div
                    {
                        new HPanel
                        {
                            new ListBox
                            {
                                options=state.Solutions,
                                value=state.SelectedSolution,
                                onChange= e => OnSolutionSelected(e.value+ string.Empty),
                                optionLabel = "name",
                                optionValue =  "name",
                                filter = true,
                                // listStyle={ MaxHeight = "250px" }
                            },

                            new VPanel
                            {
                               gravity = 5,
                               Margin={ Left = 10 },
                               Children =
                               {
                                    new span{text = state.SelectedSolution},
                                    new HPanel
                                    {
                                        new InputText{value = state.Comment, gravity = 5},
                                        new Button{ label = "Push",   onClick = Push }
                                    },
                                    new ListBox
                                    {
                                        options=state.ChangedFiles,
                                        optionLabel = "name",
                                        optionValue =  "name",
                                        //listStyle = { MaxHeight = "250px" }
                                    }.IsVisible(!state.CanShowSuccessIcon).Style(s=>s.MarginTop=10.AsPixel()),
                                    new div
                                    {
                                        new i { className = "pi pi-check-circle" },
                                        new div { Margin = { Left = 5}, text = "Success" }
                                    }.MakeCenter().IsVisible(state.CanShowSuccessIcon)
                                }
                            }
                        },

                    }.HasBorder().Padding(5).Style(s=>s.MinHeight=400.AsPixel()).MakeCenter().Style(s=>s.Margin=50.AsPixel())
                }
            };
        }
    }
}