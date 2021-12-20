using System;
using System.Collections.Generic;
using System.Linq;
using ReactDotNet.PrimeReact;
using InputText = ReactDotNet.PrimeReact.InputText;
using  static ReactDotNet.Mixin;

namespace ReactDotNet.Demo.TodoSample
{

    [Serializable]
    public class TodoRecordList
    {
        public List<TodoRecord> Records { get; set; }
        public string AnyName { get; set; }

        public string B { get; set; }

    }

    class TodoRecordListView : ReactDotNet.ReactComponent<TodoRecordList>
    {

        public string Aloha { get; set; } = "ABC";

        #region Constructors
        public TodoRecordListView()
        {
            state.Records = new List<TodoRecord>
            {
                new TodoRecord { Id = 1, Text = "1" },
                new TodoRecord { Id = 2, Text = "2" }
            };
            state.AnyName = "NameX";
            state.B = "B";
        }
        #endregion

        #region Public Methods

        public override ReactElement render()
        {

           

            return new div("className-"+state.Records.Count)
            {
                new InputText
                {
                    valueBind = Bind(()=>state.AnyName)
                },
                new TodoRecordView{ MyProp1 = "A"},
                new TodoRecordView{ MyProp1 = "B"},
                new TodoRecordView{ MyProp1 = "C"},
                new Button(){ label = "Update", onClick = Update},
                new InputTextarea
                {
                    value = state.B,
                    rows = 3
                    
                },
            };
        }

        void Update()
        {
            state.B = state.AnyName;
        }
        #endregion
    }


    public class TodoRecordView : ReactComponent<TodoRecord>
    {
        public string MyProp1 { get; set; }

        void OnClicked()
        {
            state.ClickCount++;
        }

        public override ReactElement render()
        {
            return new div("C4")
            {
                new button{ text   = MyProp1+"Click Count: " + state.ClickCount, onClick = OnClicked},
                new Button
                {
                    label = MyProp1+"Click Count: " + state.ClickCount, onClick = OnClicked,
                    tooltip = "My tooltip",
                    tooltipOptions = { position = TooltipPositionType.left}
                }
            };
        }
    }
}