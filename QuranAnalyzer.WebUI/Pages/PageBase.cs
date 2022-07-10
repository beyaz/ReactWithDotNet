using ReactDotNet.Html5;

namespace QuranAnalyzer.WebUI.Pages;


public abstract class PageBase : ReactComponent
{
    public virtual string id { get; set; }
}