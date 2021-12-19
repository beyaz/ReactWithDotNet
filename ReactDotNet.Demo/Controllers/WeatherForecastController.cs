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

namespace ReactDotNet.Demo
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentController : ControllerBase
    {
        [HttpPost]
        [Route("HandleRequest")]
        public ComponentRequest HandleRequest(ComponentRequest request)
        {
            return ComponentRequestHandler.HandleRequest(request,Type.GetType);
        }
    }
}
