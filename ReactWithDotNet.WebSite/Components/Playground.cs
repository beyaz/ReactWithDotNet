using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;
using ReactWithDotNet.WebSite.HelperApps;

namespace ReactWithDotNet.WebSite.Components;

class Playground : Component
{
   
    
    class ExecuteButton : Component
    {
        public bool IsRunning { get; set; }

        public MouseEventHandler Clicked;
        
        protected override Element render()
        {
            return new Tooltip
            {
                arrow = true,
                title = "Run",

                children =
                {
                    new FlexRowCentered(OnClickPreview(()=>IsRunning = true), OnClick(Clicked))
                    {
                        IsRunning ? new LoadingIcon{Color = Theme.Blue700}+Size(20,20) :
                            new svg(svg.ViewBox(0, 0, 28, 28), svg.Fill(Theme.Blue400), Size(28, 28), Hover(Fill(Theme.Blue700)))
                            {
                                new path { d = "M5.853 18.647h8.735L9.45 31l16.697-17.647h-8.735L22.55 1 5.853 18.647z" }
                            }
                    }
                }
            };
        }
    }
    
    public IReadOnlyList<(string fileName, string fileContent)> Files { get; set; }

    public string SelectedFileName { get; set; }

    
    Task OnSourceFileClicked(MouseEvent e)
    {
        SelectedFileName = e.target.id;

        return Task.CompletedTask;
    }
    
    Element Part_AppBar()
    {
        return new FlexRow(Height(40), PaddingLeftRight(24), JustifyContentSpaceBetween)
        {
            Background("white"), Color(Theme.Gray400), BorderBottom(Solid(1, Theme.Gray100)),


            new FlexRow(Gap(15))
            {
                Files?.Select(fileInfo => new FlexRowCentered
                {
                    fileInfo.fileName,

                    OnClick(OnSourceFileClicked), Id(fileInfo.fileName),

                    fileInfo.fileName == SelectedFileName
                        ? new[]
                        {
                            Color(Theme.Gray950),
                            BorderBottom(Solid(1, Theme.Gray950))
                        }
                        : new[]
                        {
                            FontSize("0.9rem"),
                            Hover(Color(Theme.Gray950))
                        }
                })
            },

            new ExecuteButton { Clicked = ExecuteClicked }
        };
    }

    public bool ForceExecute { get; set; }
    
    Task ExecuteClicked(MouseEvent e)
    {
        ForceExecute = true;

        return Task.CompletedTask;
    }
    
    Element CreatePreview()
    {
        if (Files?.Count  > 0)
        {
            var (isTypeFound, type, assemblyLoadContext, sourceCodeHasError, sourceCodeError) = 
                DynamicCode.LoadAndFindType(Files.Select(x=>x.fileContent).ToArray(), "Preview.SampleComponent");
            
            if (isTypeFound)
            {
                var instance = type.Assembly.CreateInstance("Preview.SampleComponent");
                
                return (ReactWithDotNet.Component)instance;
            }

            if (sourceCodeHasError)
            {
                return sourceCodeError;
            }

            DynamicCode.TryClear(assemblyLoadContext);
        }

        return null;
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
                        Code = Files?.FirstOrDefault(x=>x.fileName == SelectedFileName).fileContent
                    }
                },

                new FlexRowCentered(Width("50%"), Background(rgb(246, 247, 249)))
                {
                    ForceExecute ? CreatePreview() : null
                }
            },
        };
    }
}