using System;
using Microsoft.AspNetCore.Mvc;

namespace ReactDotNet.Demo
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(HandleRequest))]
        public Element HandleRequest(ComponentRequest request)
        {
            return ComponentRequestHandler.HandleRequest(request,Type.GetType);
        }
    }
}
