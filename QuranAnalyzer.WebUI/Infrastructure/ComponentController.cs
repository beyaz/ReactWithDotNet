using System;
using Microsoft.AspNetCore.Mvc;

namespace ReactWithDotNet.Demo;

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

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ContentResult Index()
    {
        var html = System.IO.File.ReadAllText("index.html");
        return new ContentResult
        {
            Content     = html,
            ContentType = "text/html"
        };
    }
}