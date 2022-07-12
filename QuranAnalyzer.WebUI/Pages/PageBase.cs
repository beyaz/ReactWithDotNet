using ReactDotNet;

namespace QuranAnalyzer.WebUI.Pages;


public abstract class PageBase : ReactComponent
{
    public virtual string id { get; set; }
}