using System;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class TopNavigationPanel : ReactComponent
{
    public bool HamburgerMenuIsOpen { get; set; }
    public Action<string> OnHamburgerMenuClicked { get; set; }
    
    public override Element render()
    {
        return new nav
        {
            children =
            {
                new SvgHamburgerIcon { HamburgerMenuIsOpen = HamburgerMenuIsOpen, OnHamburgerMenuClicked = OnHamburgerMenuClicked },
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