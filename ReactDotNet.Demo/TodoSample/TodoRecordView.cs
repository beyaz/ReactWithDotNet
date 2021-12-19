using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactDotNet.Demo.TodoSample
{

    [Serializable]
    public class TodoRecordList
    {
        public List<TodoRecord> Records { get; set; }
    }

    class TodoRecordListView : ReactDotNet.ReactComponent<TodoRecordListView, TodoRecordList>
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
            return new div("className-"+state.Records.Count)
            {
                new TodoRecordView{ MyProp1 = "A"},
                new TodoRecordView{ MyProp1 = "B"},
                new TodoRecordView{ MyProp1 = "C"}
            };
        }
        #endregion
    }


    public class TodoRecordView : ReactComponent<TodoRecordView, TodoRecord>
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
                new button{ text = MyProp1+"Click Count: " + state.ClickCount, onClick = OnClicked}
            };
        }
    }
}