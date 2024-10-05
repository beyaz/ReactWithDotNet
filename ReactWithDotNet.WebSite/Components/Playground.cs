using ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

namespace ReactWithDotNet.WebSite.Components;

record PlaygroundState
{
    public IReadOnlyList<(string fileName, string fileContent)> Files { get; init; }

    public bool ResetMode { get; init; }

    public string SelectedFileName { get; init; }

    public Type TypeOfTargetComponent { get; init; }
}

class Playground : Component<PlaygroundState>
{
    public IReadOnlyList<(string fileName, string fileContent)> Files { get; init; }

    public string SelectedFileName { get; init; }

    public Type TypeOfTargetComponent { get; init; }

    protected override Task constructor()
    {
        state = new()
        {
            Files                 = Files ?? new List<(string fileName, string fileContent)>(),
            SelectedFileName      = SelectedFileName,
            TypeOfTargetComponent = TypeOfTargetComponent
        };

        if (SelectedFileName is null && Files?.Count > 0)
        {
            state = state with { SelectedFileName = Files[0].fileName };
        }

        return base.constructor();
    }

    protected override Element render()
    {
        var width = Width(100 * percent) + MD(Width(50 * percent));

        return new FlexColumn(SizeFull, BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), BorderRadius(3), CursorDefault)
        {
            Part_AppBar,

            new FlexRow(SizeFull, JustifyContentSpaceBetween, FlexWrap)
            {
                new FlexRowCentered(width, Height(300), BorderRight(Solid(1, rgb(235, 236, 240))))
                {
                    new CSharpCodePanel
                    {
                        Code = state.Files?.FirstOrDefault(x => x.fileName == state.SelectedFileName).fileContent
                    }
                },

                new FlexRowCentered(width, Height(300), Background(rgb(246, 247, 249)), MinSize(200))
                {
                    Part_Preview
                }
            }
        };
    }

    Task FinishResetOperation()
    {
        state = state with { ResetMode = false };

        return Task.CompletedTask;
    }

    Element iconCsharp()
    {
        return new svg(ViewBox("0 0 32 32"), Size(16, 16), Fill("red"))
        {
            new path { d = "M19.792,7.071h2.553V9.624H24.9V7.071h2.552V9.624H30v2.552h-2.55v2.551H30V17.28H27.449v2.552H24.9v-2.55l-2.55,0,0,2.552H19.793v-2.55l-2.553,0V14.725h2.553V12.179H17.24V9.622h2.554Zm2.553,7.658H24.9V12.176H22.345Z" },
            new path { d = "M14.689,24.013a10.2,10.2,0,0,1-4.653.915,7.6,7.6,0,0,1-5.89-2.336A8.839,8.839,0,0,1,2,16.367,9.436,9.436,0,0,1,4.412,9.648a8.181,8.181,0,0,1,6.259-2.577,11.1,11.1,0,0,1,4.018.638v3.745a6.81,6.81,0,0,0-3.723-1.036,4.793,4.793,0,0,0-3.7,1.529,5.879,5.879,0,0,0-1.407,4.142,5.774,5.774,0,0,0,1.328,3.992,4.551,4.551,0,0,0,3.575,1.487,7.288,7.288,0,0,0,3.927-1.108Z" }
        };
    }

    Task OnResetClicked(MouseEvent e)
    {
        state = state with { ResetMode = true };

        Client.GotoMethod(20, FinishResetOperation);

        return Task.CompletedTask;
    }

    Task OnSourceFileClicked(MouseEvent e)
    {
        state = state with { SelectedFileName = e.target.id };

        return Task.CompletedTask;
    }

    Element Part_AppBar()
    {
        return new FlexRow(Height(40), PaddingLeftRight(24), FontSize13, JustifyContentSpaceBetween)
        {
            Background("white"), Color(Theme.Gray400), BorderBottom(Solid(1, Theme.Gray100)),

            new FlexRow(Gap(15))
            {
                state.Files?.Select(fileInfo => new FlexRowCentered
                {
                    iconCsharp,
                    SpaceX(5),

                    fileInfo.fileName,

                    OnClick(OnSourceFileClicked), Id(fileInfo.fileName),

                    fileInfo.fileName == state.SelectedFileName
                        ? new[]
                        {
                            Color(Theme.Gray700),
                            BorderBottom(Solid(1, Theme.Gray950))
                        }
                        : new[]
                        {
                            FontSize("0.9rem"),
                            Hover(Color(Theme.Gray700))
                        }
                })
            },

            new ResetButton { Clicked = OnResetClicked }
        };
    }

    Element Part_Preview()
    {
        if (state.ResetMode)
        {
            return new FlexRowCentered(SizeFull)
            {
                new LoadingIcon { Color = Theme.Blue700 } + Size(20, 20)
            };
        }

        if (state.TypeOfTargetComponent is null)
        {
            return new pre
            {
                "Target type not specified yet"
            };
        }

        return (Element)Activator.CreateInstance(state.TypeOfTargetComponent);
    }

    class ResetButton : Component<ResetButton.ResetButtonState>
    {
        const double size = 24;

        public MouseEventHandler Clicked;

        protected override Element render()
        {
            return new Tooltip
            {
                x =>
                {
                    x.arrow = true;
                    x.title = "Restart app";
                },

                new FlexRowCentered(OnClickPreview(() => state.IsRunning = true), OnClick(Clicked), Hover(Border(Solid(0.1, Theme.grey_100)), BorderRadius(5)))
                {
                    state.IsRunning ? new IconLoading() : new IconReset(),

                    SpaceX(5),
                    new span { "Reset", FontSize12 }
                }
            };
        }

        internal class ResetButtonState
        {
            public bool IsRunning { get; set; }
        }

        class IconLoading : PureComponent
        {
            protected override Element render()
            {
                return new LoadingIcon { Color = Theme.Blue700 } + Size(size / 2, size / 2);
            }
        }

        class IconReset : PureComponent
        {
            protected override Element render()
            {
                return new svg(svg.ViewBox(0, 0, size, size), svg.Fill(Theme.Blue400), Size(size / 2, size / 2), Hover(Fill(Theme.Blue700)))
                {
                    new path { d = "M13.8982 5.20844C12.4626 4.88688 10.9686 4.93769 9.55821 5.35604L11.8524 3.06184C11.8989 3.0154 11.9357 2.96028 11.9608 2.89961C11.986 2.83894 11.9989 2.77391 11.9989 2.70824C11.9989 2.64256 11.986 2.57754 11.9608 2.51686C11.9357 2.45619 11.8989 2.40107 11.8524 2.35464L11.1456 1.64784C11.0992 1.60139 11.0441 1.56455 10.9834 1.53942C10.9227 1.51428 10.8577 1.50134 10.792 1.50134C10.7263 1.50134 10.6613 1.51428 10.6006 1.53942C10.54 1.56455 10.4848 1.60139 10.4384 1.64784L6.14571 5.94054C6.00654 6.07969 5.89615 6.2449 5.82083 6.42673C5.74551 6.60855 5.70675 6.80343 5.70675 7.00024C5.70675 7.19704 5.74551 7.39192 5.82083 7.57374C5.89615 7.75557 6.00654 7.92078 6.14571 8.05994L10.4387 12.3529C10.5325 12.4465 10.6595 12.4991 10.792 12.4991C10.9245 12.4991 11.0516 12.4465 11.1453 12.3529L11.8527 11.6455C11.9463 11.5518 11.9989 11.4247 11.9989 11.2922C11.9989 11.1598 11.9463 11.0327 11.8527 10.9389L8.77481 7.86104C9.99795 7.16236 11.415 6.8801 12.8125 7.05678C14.21 7.23347 15.5122 7.85953 16.523 8.84064C17.5338 9.82176 18.1983 11.1048 18.4165 12.4964C18.6347 13.888 18.3947 15.3129 17.7328 16.5562C17.0708 17.7996 16.0227 18.7942 14.7463 19.3902C13.47 19.9861 12.0345 20.1511 10.6563 19.8603C9.27798 19.5695 8.03152 18.8387 7.10469 17.778C6.17786 16.7172 5.62086 15.384 5.51761 13.9791C5.51156 13.8512 5.45689 13.7303 5.36477 13.6413C5.27265 13.5522 5.15001 13.5017 5.02191 13.5H4.02081C3.95297 13.4996 3.88574 13.5129 3.8232 13.5392C3.76065 13.5655 3.70408 13.6042 3.6569 13.6529C3.60972 13.7017 3.57291 13.7595 3.54869 13.8228C3.52448 13.8862 3.51336 13.9538 3.51601 14.0216C3.61349 15.5965 4.1473 17.1132 5.0577 18.4019C5.9681 19.6906 7.21917 20.7006 8.6709 21.3188C10.1226 21.937 11.7178 22.139 13.2778 21.9022C14.8378 21.6654 16.3011 20.9992 17.504 19.978C18.7069 18.9569 19.6019 17.6212 20.0889 16.1203C20.5759 14.6195 20.6356 13.0128 20.2614 11.4799C19.8872 9.94705 19.0938 8.54858 17.97 7.44098C16.8462 6.33339 15.4363 5.56037 13.8982 5.20844V5.20844Z" }
                };
            }
        }
    }
}