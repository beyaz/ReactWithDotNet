using System;

namespace ReactDotNet.Demo.TodoSample
{
    [Serializable]
    public class TodoRecord
    {
        public int ClickCount { get; set; }
        public int Id { get; set; }
        public string Text { get; set; }
        public int DropdownSelectedValue { get; set; }
    }
}