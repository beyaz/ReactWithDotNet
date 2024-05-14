using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

namespace ReactWithDotNet.WebSite.Showcases;




public class PrimeReactTabViewDemo : PureComponent
{
    
    protected override Element render()
    {
        return new div(SizeFull)
        {
            PrimeReactCssLibs,
            new TabView
            {
                 new TabPanel
                    {
                        header = "Header I",
                        leftIcon = "pi pi-calendar mr-2",
                        children =
                        {
                            new p
                            {
                                text = @"
            Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. 
            Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo
            consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. 
            Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.
        ",
                                className = "m-0"
                            }
                        }
                    },
                    new TabPanel
                    {
                        header = "Header II",
                        closable = true,
                        children =
                        {
                            new p
                            {
                                text = @"
            Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, 
            eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo
            enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui 
            ratione voluptatem sequi nesciunt. Consectetur, adipisci velit, sed quia non numquam eius modi.
        ",
                                className = "m-0"
                            }
                        }
                    },
                    new TabPanel
                    {
                        headerTemplate = new FlexRowCentered(CursorPointer,ClassName("p-tabview-nav-link"), Height(50), Gap(8))
                        {
                            new Avatar
                            {
                                image = "https://primefaces.org/cdn/primereact/images/avatar/amyelsner.png",
                                shape = "circle", className ="mx-2"
                            } + Size(28),
                           "Amy Elsner"
                        },
                        header = "Header III",
                        children =
                        {
                            new p
                            {
                                text = @"
            At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti 
            quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in
            culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. 
            Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus.
        ",
                                className = "m-0"
                            }
                        }
                    }
            }
        };
    }
}

