namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageModifiers : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new h1 { "Modifiers" },

            new FlexColumn
            {
                new p
                {
                    "Clasical way to create element"
                },

                new div(Height(300))
                {
                    new RenderPreview
                    {
                        RenderPartOfCSharpCode =
                            """

                            new button
                            {
                               text = "button",
                               style = 
                               {
                                  display = "flex",
                                  justifyContent = "center",
                                  alignItems = "center",
                                  paddingLeft = "12px",
                                  paddingRight = "12px",
                                  paddingTop = "8px",
                                  paddingBottom = "8px",
                                  border = "1px solid blue",
                                  fontSize = "15px",
                                  borderRadius = "3px",
                                  background = "White",
                                  hover = 
                                  {
                                      background = "WhiteSmoke",
                                  }
                               }
                            }


                            """
                    }
                }
            },

            new FlexColumn
            {
                new p
                {
                    "We suggest better alternatives"
                },

                new div(Height(250))
                {
                    new RenderPreview
                    {
                        RenderPartOfCSharpCode =
                            """
                            new div
                            {
                               new div
                               {
                                  style = 
                                  {
                                     DisplayBlock,
                                     TextAlignCenter,
                                     Color("Red"),
                                     FontSize15
                                  }
                               }
                            }

                            """
                    }
                }
            }
        };
    }
}

class PageDocuments : PureComponent
{
    protected override Element render()
    {
        return new FlexRow(Gap(150), WidthFull, JustifyContentSpaceAround)
        {
            "PageDocuments"
        };
    }
}