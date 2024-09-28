
namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageModifiers : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        { 
            new h1{"Modifiers"},
            
            new FlexColumn
            {
                new p
                {
                    "Clasical way to create element"
                },
                
                new div(Height(250), Width(350))
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