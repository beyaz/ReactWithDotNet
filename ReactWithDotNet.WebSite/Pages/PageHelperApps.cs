using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.HelperApps;

namespace ReactWithDotNet.WebSite.Pages;

class PageHelperApps : Component
{
    public string SelectedAppName { get; set; } = nameof(HtmlToCSharpView);

    protected override Element render()
    {
        return new FlexColumn(WidthMaximized,AlignItemsCenter)
        {
            new FlexRow(Gap(10))
            {
                new Button
                {
                    variant  = "outlined",
                    onClick  = _ => SelectedAppName = nameof(HtmlToCSharpView),
                    disabled = SelectedAppName == nameof(HtmlToCSharpView),
                    children = { "Html to CSharp" }
                },
                new Button
                {
                    variant  = "outlined",
                    onClick  = _ => SelectedAppName = nameof(FigmaCss2ReactInlineStyleConverterView),
                    disabled = SelectedAppName == nameof(FigmaCss2ReactInlineStyleConverterView),
                    children = { "Figma css to React Inline style" }
                }
            },

            creatElement() + HeightMaximized + WidthMaximized
        };

        Element creatElement()
        {
            if (SelectedAppName == nameof(HtmlToCSharpView))
            {
                return new HtmlToCSharpView();
            }

            if (SelectedAppName == nameof(FigmaCss2ReactInlineStyleConverterView))
            {
                return new FigmaCss2ReactInlineStyleConverterView();
            }
            

            return "No app selected";
        }
    }
}