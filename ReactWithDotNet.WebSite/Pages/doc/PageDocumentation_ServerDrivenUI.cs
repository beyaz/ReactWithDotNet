namespace ReactWithDotNet.WebSite.Pages;

sealed class PageDocumentation_ServerDrivenUI : PageDocumentation
{
    protected override Element CreateContent()
    {
        
    
        return new FlexRow(JustifyContentCenter, WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1(FontSize32, FontWeight400, LineHeight32, MarginBottom(1.2 * rem))
                {
                    "Server Driven UI"
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },

                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta 2"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "2 Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Quis vel iste dicta"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Sit commodi iste iure molestias qui amet voluptatem sed quaerat. Nostrum aut pariatur. Sint ipsa praesentium dolor error cumque velit tenetur."
                },

            }
        };
    
    }
}