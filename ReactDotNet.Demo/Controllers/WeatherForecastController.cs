using System;
using Microsoft.AspNetCore.Mvc;


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

    class TodoRecordView : ReactComponent<TodoRecordView, TodoRecord>
    {
        void OnClicked()
        {
            state.Text += "a";
        }

        public override ReactElement render()
        {
            return new button { text = state.Text , Padding = { Left = 5, Right = 5}, onClick = OnClicked };
        }
    }
}

namespace ReactDotNet.Demo
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        
        [HttpGet]
        public ReactElement Get()
        {
            return new div("C4") { text = "Aloha", style =
            {
                Width = "5px", 
                Display = Display.Flex, 
                Height = "6px",
                AlignContent = AlignContent.FlexStart
            } };


            //    new div("croot")
            //{
            //    new div("C1"),
            //    new div("C2"),
            //    new div("C3"),
            //    new div("C4") { text = "Aloha", style = { Width = "5px", Display = Display.Flex}}
            //};
        }
    }
}
