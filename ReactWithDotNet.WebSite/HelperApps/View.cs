using ReactWithDotNet.Libraries.mui.material;

namespace ReactWithDotNet.WebSite.HelperApps;

class View : ReactComponent
{
    protected override Element render()
    {
        return new FlexColumn
        {
            new FlexRow
            {
                new Button{ variant = "outlined", onClick = OnClick, children = { "Html to CSharp" }},
                new Button{ variant = "outlined", children = { "Figma css to React Inline style" }}

            }
        };
    }

    public bool IsHtmlToCSharpSelected { get; set; }
    
    void OnClick(MouseEvent e)
    {
        IsHtmlToCSharpSelected = true;
    }
}