
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
