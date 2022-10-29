namespace QuranAnalyzer.WebUI.Components;

class SvgHamburgerIconModel
{
    public bool HamburgerMenuIsOpen { get; set; }
}

class SvgHamburgerIcon : ReactComponent<SvgHamburgerIconModel>
{
    public void CloseHamburgerMenu(MouseEvent e)
    {
        state.HamburgerMenuIsOpen = false;
        Client.HamburgerMenuClosed();
    }

    public void OpenHamburgerMenu(MouseEvent e)
    {
        state.HamburgerMenuIsOpen = true;
        Client.HamburgerMenuOpened();
    }

    protected override Element render()
    {
        if (Context.Contains(ContextKey.HamburgerMenuIsOpen))
        {
            state.HamburgerMenuIsOpen = Context.TryGetValue(ContextKey.HamburgerMenuIsOpen);
        }

        const string color = "black";
        if (state.HamburgerMenuIsOpen)
        {
            return new svg
            {
                children =
                {
                    new path { d = "M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8z", fill = color }
                },
                style =
                {
                    Height(20),
                    Margin(14)
                },
                viewBox = "0 0 15 15",
                onClick = CloseHamburgerMenu,
            };
        }

        return new svg
        {
            viewBox = "0 0 24 24",
            onClick = OpenHamburgerMenu,
            children =
            {
                new path { d = "M3 18h18v-2H3v2zm0-5h18v-2H3v2zm0-7v2h18V6H3z", fill = color }
            },
            style =
            {
                Height(24),
                Margin(12)
            }
        };
    }
}