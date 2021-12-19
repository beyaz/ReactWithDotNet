namespace ReactDotNet.Demo.TodoSample
{

   

    public class TodoRecordView : ReactComponent<TodoRecordView, TodoRecord>
    {
        void OnClicked()
        {
            state.ClickCount++;
        }

        public override ReactElement render()
        {
            return new div("C4")
            {
                new button{ text = "Click Count: " + state.ClickCount, onClick = OnClicked}
            };
        }
    }
}