using System;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class TopNavigationPanel : ReactComponent
{
    public override Element render()
    {
        return new nav
        {
            children =
            {
                new SvgHamburgerIcon(),
                new div
                {
                    new SiteTitle("19 Sistemi Nedir")
                }
            },
            style =
            {
                display        = "flex",
                justifyContent = "flex-start",
                alignItems     = "center"
            }
        };
    }
}