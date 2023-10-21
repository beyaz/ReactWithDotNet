
using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Components;

class DemoPanel : Component
{
    public string CSharpCode { get; set; }

    public string FullNameOfElement { get; set; }

    public bool IsSourceCodeVisible { get; set; }

    public bool HideSourceCodeVisibilityButton{ get; set; }

    protected override Element render()
    {
        Element creatElement()
        {
            if (FullNameOfElement is not null)
            {
                var elementType = Type.GetType(FullNameOfElement);
                if (elementType is not null)
                {
                    return (Element)Activator.CreateInstance(elementType);
                }
            }

            return "Element is empty";
        }

        Element ShowHideButton()
        {
            return new FormControlLabel
            {
                label = IsSourceCodeVisible ? "Hide Source Code" : "Show Source Code",
                control = new Switch
                {
                    size     = "small",
                    value    = (!IsSourceCodeVisible).ToString(),
                    @checked = IsSourceCodeVisible,
                    onChange = _ =>
                    {
                         IsSourceCodeVisible = !IsSourceCodeVisible;
                         
                         return Task.CompletedTask;
                    }
                }
            };
        }

        Element createSourceCode()
        {
            return new fieldset(Border("1px solid #dee2e6"), WidthMaximized)
            {
                new legend{new img{Src(Asset("csharp.svg")), Width(25), Height(20)}},
                new FlexColumn(AlignItemsFlexStart,WidthMaximized, HeightMaximized)
                {
                    new CSharpCodePanel{ Code = CSharpCode}
                }
            };
        }
        
        return new FlexRow(BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), 
                           Padding(10), 
                           BorderRadius(5), 
                           MarginTopBottom(1), 
                           FlexWrap,
                           //MediaQueryOnDesktop(MinWidth(500)), 
                           WidthMaximized,
                           FontSize10)
        {
            new FlexRowCentered(BackgroundColor(Theme.grey_100), Padding(40), WidthMaximized, BorderRadius(10), PositionRelative)
            {
                creatElement,

                When(!HideSourceCodeVisibilityButton, new FlexRow(PositionAbsolute, Right(1), Bottom(1) )
                {
                    ShowHideButton
                })
                
            },
            When(IsSourceCodeVisible, createSourceCode)
        };
    }
}


class Playground : Component
{
    public IReadOnlyList<(string fileName, string fileContent)> Files { get; set; }

    Element Part_AppBar()
    {
        return new FlexRow(Height(40),PaddingLeftRight(24),JustifyContentSpaceBetween)
        {
            Background("white"), Color(Theme.Gray400),BorderBottom(Solid(1, Theme.Gray100)),
            
            
           new FlexRowCentered(Gap(15))
           {
               new FlexRowCentered(BorderBottom(Solid(1, Theme.Blue700)))
               {
                   "App.cs",
                   Color(Theme.Gray950),
                   BorderBottom(Solid(1, Theme.Gray950))
               },

               new FlexRowCentered
               {
                   "xyz.cs",
                   Hover(Color(Theme.Gray950))
               },
               new FlexRowCentered
               {
                   "xyzyy.cs"
               },
           },

            new FlexRowCentered
            {
                new svg(svg.ViewBox(0, 0, 28, 28), svg.Fill(Theme.Blue400), Size(28, 28), Hover(Fill(Theme.Blue700)))
                {
                    new path { d = "M5.853 18.647h8.735L9.45 31l16.697-17.647h-8.735L22.55 1 5.853 18.647z" }
                }
            }
        };
    }
    
    protected override Element render()
    {
        return new FlexColumn(WidthHeightMaximized, BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), BorderRadius(5), CursorDefault)
        {
            Height(400),
            Width(900),
            
            Part_AppBar,

            new FlexRow(WidthHeightMaximized,JustifyContentSpaceBetween)
            {
                new FlexRowCentered(Width("50%"), BorderRight(Solid(1,rgb(235, 236, 240))))
                {
                    new CSharpCodePanel
                    {
                        Code = string.Join(Environment.NewLine,Enumerable.Range(1,100).Select(x=>"code"))
                    }
                },

                new FlexRowCentered(Width("50%"), Background(rgb(246, 247, 249)))
                {
                    "output"
                }
            },
        };
    }
}