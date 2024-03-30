using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.HelperApps;

namespace ReactWithDotNet.WebSite.Pages;

class PageHelperApps : Component
{
    public string SelectedAppName { get; set; } = nameof(HtmlToCSharpView);

    protected override Element render()
    {
        return new FlexColumn(WidthFull,AlignItemsCenter)
        {
            new FlexRow(Gap(10))
            {
                new Button
                {
                    variant  = "outlined",
                    onClick  = _ => { SelectedAppName = nameof(HtmlToCSharpView); return Task.CompletedTask; },
                    disabled = SelectedAppName == nameof(HtmlToCSharpView),
                    children = { "Html to CSharp" }
                }
            },

            creatElement() + HeightMaximized + WidthFull
        };

        Element creatElement()
        {
            if (SelectedAppName == nameof(HtmlToCSharpView))
            {
                return new HtmlToCSharpView();
            }

            
            

            return "No app selected";
        }
    }
}