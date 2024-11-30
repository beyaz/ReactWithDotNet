namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageReactContexts : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new h1 { "React context" },
            SpaceY(8),
            new FlexColumn
            {
                new p
                {
                    "React Context is a powerful feature that allows you to manage and share state or data across a component tree without having to pass props down manually at every level.",
                    br,
                    "ReactWithDotNet has only one context. Every component can access this context",
                    br,
                    "Let's show you on an example"
                },

                SpaceY(8),
                
                new div(Height(360))
                {
                    new CodeViewer(CodeViewer.LangCSharp)
                    {
                       
                    }
                }
            },

            SpaceY(50),
            
            new FlexColumn
            {
                new p
                {
                    "We suggest better alternatives"
                },
                SpaceY(8),
                new div(Height(200))
                {
                    new RenderPreview
                    {
                        RenderPartOfCSharpCode =
                            """
                            
                            new button
                            {
                               "button",
                               DisplayFlexRowCentered,
                               Padding(8, 12),
                               Border(1, solid, Blue),
                               BorderRadius(3),
                               FontSize15,
                               Background(White),
                               Hover(Background(WhiteSmoke), BorderColor(Red))
                            }
                            
                            """
                    }
                },
                SpaceY(50),
                new p
                {
                    "Also you can move any modifiers to constructor"
                },
                SpaceY(8),
                new div(Height(170))
                {
                    new RenderPreview
                    {
                        RenderPartOfCSharpCode =
                            """
                            
                            new button(DisplayFlexRowCentered, Padding(8, 12))
                            {
                               "button",
                               Border(1, solid, Blue),
                               BorderRadius(3),
                               FontSize15,
                               Background(White),
                               Hover(Background(WhiteSmoke), BorderColor(Red))
                            }
                            
                            """
                    }
                }
                
            },
            SpaceY(80),
            new p
            {
                "Custom modifier can be declared as below and can be use anywhere.",
            },
            SpaceY(8),
            new div(Height(300))
            {
                new CSharpCodePanel
                {
                    Code = """
                           
                           static class SharedStyles
                           {
                               public static Style SectionStyle =>
                               [
                                   Background(White),
                                   BorderRadius(8),
                                   Padding(6)
                               ];
                           }
                           
                           static Element CreateUserSection(string userName)
                           {
                               return new div(SharedStyles.SectionStyle)
                               {
                                   userName
                               };
                           }
                           
                           """
                }
            },
            
            SpaceY(80),
            new p
            {
                "Tailwind like library class names be can use like ",
            },
            SpaceY(8),
            new div(Height(100))
            {
                new CSharpCodePanel
                {
                    Code = """

                           new div("w-full text-gray-600", FontSize15)
                           {
                               "Any text"
                           }

                           """
                }
            },
            
            SpaceY(50)
        };
    }
}

