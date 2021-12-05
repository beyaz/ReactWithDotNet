using System;
using System.Collections.Generic;
using System.Linq;
using Bridge.Html5;
using ReactDotNet;
using ReactDotNet.MaterialUI;
using ReactDotNet.PrimeReact;
using ReactElement = ReactDotNet.ReactElement;

namespace ReactDotNet.Demo
{ 
    [Serializable]
    public class TodoRecord
    {
        #region Public Properties
        public int ClickCount { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int DropdownSelectedValue { get; set; }
        #endregion
    }

    
    
    class TodoRecordView : ReactComponent<TodoRecordView,TodoRecord>
    {

        [React]
        public string Aloha { get; set; }

        [React]
        public TodoRecord TodoRecord { get; set; }

        #region Constructors
        public TodoRecordView()
        {
            state.Text = "TodoRecordView";
            
        }
        #endregion

        #region Public Methods
        public void onClick(SyntheticEvent<HTMLElement> e)
        {
            SetState(m =>
            {
                m.Text += "2";
                m.ClickCount++;
            });
        }


        public void onTooltipShow(TooltipEventParams e)
        {
            SetState(m =>
            {
                m.Text += "2";
                m.ClickCount++;
            });
        }

        public override ReactElement render()
        {
            var record = props.TodoRecord;

            
            if (state.ClickCount > 2)
            {

                var b = new button
                {
                    text = "Aloha-"
                };
                

                return b;
                return new PrimeReact.Button("google p-p-0")
                {
                    new i("pi pi-google p-px-2"),
                    new span("p-px-3") { text = "Google" }

                };


                //return new span("p-buttonset")
                //{
                //new PrimeReact.Button { label = "Save", icon   ="pi pi-check"},
                //new PrimeReact.Button { label = "Delete", icon ="pi pi-trash" ,Margin = { Left = 5, Right = 7}, tooltip = "Aloha1"},
                //new PrimeReact.Button { label = state.Text, icon ="pi pi-times", tooltip = "Aloha", tooltipOptions =
                //{
                //    position = TooltipPositionType.left,
                //    onShow = onTooltipShow,
                //    autoHide = false,
                //    @event = TooltipEventType.hover,
                //    style = { BackgroundColor = "yellow",Border = "5px solid red"}
                //}},
                return new PrimeReact.Button("google p-p-0*")
                    {
                        label = "ahh"
                        //new i("pi pi-google p-px-2"),
                        //new span("p-px-3") { Text = "Google" }

                    };
                //};

                return new Dropdown
                {
                    optionLabel = nameof(record.Text),
                    optionValue = nameof(record.Id),
                    options     = new[] { new TodoRecord { Id = 4, Text = "4" }, new TodoRecord { Id = 5, Text = "5" } },
                    value       = state.DropdownSelectedValue,
                    onChange    = e => { SetState(s => s.DropdownSelectedValue = (int)e.value); }
                };
            }
         

            if (state.ClickCount >=3)
            {
                record = new TodoRecord { Id = state.Id, Text = state.Text };
            }


            if (state.ClickCount > 4)
            {

                return new MaterialUI.Button
                {
                    Margin  = { Left = 11, Right = 11 },
                    variant = ButtonVariant.outlined, 
                    Label = state.ClickCount + "-y"
                };

                return new Dropdown
                {
                    optionLabel = nameof(record.Text),
                    optionValue = nameof(record.Id),
                    options     = new[] { new TodoRecord { Id = 4, Text = "4" }, new TodoRecord { Id = 5, Text = "5" } },
                    value       = state.DropdownSelectedValue,
                    onChange    = e => { SetState(s => s.DropdownSelectedValue = (int)e.value); }
                };
            }


            return new SplitPanel
            {
                Padding = { Left = 5, Right = 5 },
                Cols =
                {
                    new button { text = record.Id.ToString(), onClick = onClick, gravity = 2, Margin = { Left = 20, Right = 20, Bottom = 10, Top = 10}},
                    
                    new button { text = record.Text+"1e", onClick = onClick , Padding = { Left = 20, Right = 20, Bottom = 10, Top = 10}}
                }
            };
        }
        #endregion
    }


    [Serializable]
    public class TodoRecordList
    {
        public List<TodoRecord> Records { get; set; }
    }

    class TodoRecordListView : ReactDotNet.ReactComponent<TodoRecordListView,TodoRecordList>
    {

        [React]
        public string Aloha { get; set; }

        #region Constructors
        public TodoRecordListView()
        {
            state.Records = new List<TodoRecord>
            {
                new TodoRecord { Id = 1, Text = "1" },
                new TodoRecord { Id = 2, Text = "2" }
            };
        }
        #endregion

        #region Public Methods

        public override ReactElement render()
        {
            return new Panel
            {
                Padding = { Left = 5, Right = 5 },
                rows  = state.Records.Select(x => new TodoRecordView { Aloha = state.Records.Count.ToString(), TodoRecord = x})
            };
        }
        #endregion
    }
}