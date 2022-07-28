using Microsoft.AspNetCore.Mvc;

namespace QuranAnalyzer.WebUI;

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