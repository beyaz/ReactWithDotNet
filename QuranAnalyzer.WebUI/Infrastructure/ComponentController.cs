using System;
using Microsoft.AspNetCore.Mvc;

namespace ReactWithDotNet.Demo
{
    [ApiController]
    [Route("[controller]")]
    public class ComponentController : ControllerBase
    {
        [HttpPost]
        [Route(nameof(HandleRequest))]
        public ComponentResponse HandleRequest(ComponentRequest request)
        {
            return ComponentRequestHandler.HandleRequest(request, Type.GetType);
        }
    }
}
