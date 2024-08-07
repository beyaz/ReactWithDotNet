﻿
namespace ReactWithDotNet.WebSite.Pages;

class PageModifiers : PureComponent
{
    protected override Element render()
    {
        return new article(SizeFull, DisplayFlexColumn)
        { 
            new h2{"Modifiers"},
            
            new table(Height(600))
            {
                new tr
                {
                    new td
                    {
                        new p
                        {
                            "Clasical way to create element"
                        }
                    },
                    new td
                    {
                        new p
                        {
                            "We suggest better alternatives"
                        }
                    }
                },
                
                new tr
                {
                    new td
                    {
                        new CSharpCodePanel
                        {
                            Code = @"
new div
{
   new div
   {
      style = 
      {
         display = 'block',
         textAlign = 'center',
         color = 'red',
         fontSize = '15px'         
      }
   }
}

"
                        } 
                    },
                    new td
                    {
                        new CSharpCodePanel
                        {
                            Code = @"
new div
{
   new div
   {
      style = 
      {
         DisplayBlock,
         TextAlignCenter,
         Color('red'),
         FontSize15
      }
   }
}

"
                        }
                    }
                }
            },
          
           
           
           
           

           

           new p
           {
               "You can prefer locate styles in constructor for clear dom tree"
           },

           new CSharpCodePanel
           {
               Code = @"
new div
{
   new div(DisplayBlock, TextAlignCenter, Color('red'), FontSize15)
   {
      
   }
}

"
           }+Size(300,400)
        };
    }
}

class PageTechnicalDetail : PureComponent
{
    protected override Element render()
    {
        return new article(WidthFull)
        {
            new h2{"Technical Details"},
            new p
            {
                "How is it working? How to connect React and c# language?"
            },

            new p
            {
                "ReactWithDotNet is working on the .net core. Creates ReactNode hierarchy in c# language then serialize to client.",
                br,
                "Our client engine recalculates ReactNodes from incoming c# generated nodes",
                br,
                "If any react event occurs then serialize only sub react nodes values to server"
            },
            
            new ImageTutorialView
            {
                Items = new []
                {
                    new TutorialItem
                    {
                        Title = "Aloha",
                        Description = "accc",
                        ImageSrc = Asset("doc.svg")
                    },
                    new TutorialItem
                    {
                        Title       = "Aloha3",
                        Description = "accc2",
                        ImageSrc    = Asset("2.png")
                    },
                    new TutorialItem
                    {
                        Title       = "Aloha2",
                        Description = "accc2",
                        ImageSrc    = Asset("react.svg")
                    },
                    new TutorialItem
                    {
                        Title       = "Aloha3",
                        Description = "accc2",
                        ImageSrc    = Asset("1.png")
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