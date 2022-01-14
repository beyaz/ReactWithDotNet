using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;
using System.Collections.Generic;
using System.Threading;

namespace KT_TFS_Commit_Helper
{
    [Serializable]
    public class ClientTask
    {
        public bool ComebackWithLastAction{ get; set; }
    }

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
                    new SolutionInfo{Name="CCA"}
                },
                ChangedFiles = new FileInfo[0],
            };
        }

        void OnSolutionSelected(string solutionName)
        {
            state.SelectedSolution = solutionName;
            state.Comment = "Deneme";
            state.IsBlocked = false;
            state.ChangedFiles = new[]
            {
                new FileInfo{ Name = "A.cs"},
                new FileInfo{ Name = "B.cs"},
                new FileInfo{ Name = "C.cs"}
            };
        }
        public override Element render()
        {
            static div makeCenter(div div)
            {
                div.style.Display = Display.Flex;
                div.style.JustifyContent = JustifyContent.Center;
                div.style.AlignItems = AlignItems.Center;

                return div;
            }

            void Push()
            {
                if (state.IsBlocked == false)
                {
                    state.IsBlocked = true;
                    state.ClientTask = new ClientTask {  ComebackWithLastAction = true };
                    return;
                }

                Thread.Sleep(13000);

                state.IsBlocked = false;
            }

            return new BlockUI
            {
                blocked = state.IsBlocked,
                template = makeCenter(new div { new i { className = "pi pi-spin pi-spinner" }, new div { Margin = { Left = 5}, style = { Color = "White" }, text = "pushing..." } }),
                Children =
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
                            listStyle={ MaxHeight = "250px" }
                        },

                        new VPanel
                        {
                            new InputText{value = state.SelectedSolution},
                            new HPanel
                            {
                                new InputText{value = state.Comment},
                                new Button{ label = "Push",   onClick = Push }
                            },
                            new ListBox
                            {
                                options=state.ChangedFiles,
                                optionLabel = "name",
                                optionValue =  "name",
                                listStyle={ MaxHeight = "250px" }
                            }
                        }
                    }
                }
             };
        }
    }
}