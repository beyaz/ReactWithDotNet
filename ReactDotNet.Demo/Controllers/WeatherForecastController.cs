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
    [Serializable]
    public class ComponentRequest
    {
        public string FullName { get; set; }
    }

    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpPost]
        [Route("HandleRequest")]
        public ReactElement HandleRequest(ComponentRequest request)
        {
            return new div("C4")
            {
                text = request.FullName,
                style =
                {
                    Width        = "50px",
                    Display      = Display.Flex,
                    Height       = "600px",
                    AlignContent = AlignContent.FlexStart
                }
            };


            //    new div("croot")
            //{
            //    new div("C1"),
            //    new div("C2"),
            //    new div("C3"),
            //    new div("C4") { text = "Aloha", style = { Width = "5px", Display = Display.Flex}}
            //};
        }

        //[HttpGet]
        //public ReactElement Get()
        //{
        //    return new div("C4") { text = "Aloha", style =
        //    {
        //        Width = "50px", 
        //        Display = Display.Flex, 
        //        Height = "600px",
        //        AlignContent = AlignContent.FlexStart
        //    } };


        //    //    new div("croot")
        //    //{
        //    //    new div("C1"),
        //    //    new div("C2"),
        //    //    new div("C3"),
        //    //    new div("C4") { text = "Aloha", style = { Width = "5px", Display = Display.Flex}}
        //    //};
        //}
    }
}
