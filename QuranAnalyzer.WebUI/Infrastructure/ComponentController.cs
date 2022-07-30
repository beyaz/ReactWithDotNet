using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI;

//[ApiController]
//[Route("[controller]")]
//public class ComponentController : ControllerBase
//{
//    [HttpPost]
//    [Route(nameof(HandleRequest))]
//    public ComponentResponse HandleRequest(ComponentRequest request)
//    {
//        return ComponentRequestHandler.HandleRequest(request, Type.GetType);
//    }


//    [HttpGet]
//    [Route(nameof(PreviewComponentForDesigner))]
//    public ContentResult PreviewComponentForDesigner()
//    {
//        var html = System.IO.File.ReadAllText( Path.Combine("wwwroot", "StateTestUI.html"));
//        return new ContentResult
//        {
//            Content     = html,
//            ContentType = "text/html"
//        };
//    }
//}

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    [HttpPost]
    [Route(nameof(HandleRequest))]
    public ComponentResponse HandleRequest(ComponentRequest request)
    {
        return ComponentRequestHandler.HandleRequest(request, Type.GetType);
    }


    [HttpGet]
    [Route(nameof(Index))]
    public ContentResult Index()
    {
        var html = System.IO.File.ReadAllText(Path.Combine("wwwroot", "index.html"));
        return new ContentResult
        {
            
            Content     = html,
            ContentType = "text/html"
        };
    }

    [HttpGet]
    [Route(nameof(PreviewComponentForDesigner))]
    public ContentResult PreviewComponentForDesigner()
    {
        var html = System.IO.File.ReadAllText(Path.Combine("wwwroot", "StateTestUI.html"));
        return new ContentResult
        {
            Content     = html,
            ContentType = "text/html"
        };
    }
}