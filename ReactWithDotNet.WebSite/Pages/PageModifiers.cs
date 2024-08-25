
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