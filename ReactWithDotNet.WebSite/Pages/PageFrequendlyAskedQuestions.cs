using ReactWithDotNet.ThirdPartyLibraries.FramerMotion;

namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageFrequendlyAskedQuestions : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new section(DisplayFlexRowCentered)
            {
                new InlineFlexColumn(AlignItemsFlexStart, Flex(1, 1, 0), Gap(12), JustifyContentFlexStart)
                {
                    new h1
                    {
                        "Frequendly Asked Questions"
                    },
                    new FlexColumn(AlignItemsFlexStart, AlignSelfStretch, Gap(16), JustifyContentFlexStart)
                    {
                        new Accordion
                        {
                            new p
                            {
                                "What is ReactWithDotNet? What is the target? What problems does it solve?"
                            },
                            new p
                            {
                                "It is a project that is partly on the server and partly on the browser.",
                                "It is not only js or c# library."
                            }
                        },
                        new Accordion
                        {
                            new p
                            {
                                "Can you explain ReactWithDotNet working mechanism in simple way?"
                            },
                            new p
                            {
                                "Imagine you are building react UI nodes in c# server then ReactWithDotNet serialize this nodes to browser.",
                                "After this nodes come to browser then ReactWithDotNet integrates this incoming nodes to React.js system.",
                                "When any action occurs then this engine transfer only required states to server then recalculates UI nodes."
                            }
                        },
                        new Accordion
                        {
                            new p
                            {
                                "What is modifiers?"
                            },
                            new p
                            {
                                "Modifiers are small functions that modify html node or css properties."
                            }
                        }
                    }
                }
            }
        };
    }

    class Accordion : Component<Accordion.State>
    {
        public bool IsExpanded { get; init; }

        protected override Task constructor()
        {
            state = new()
            {
                IsExpanded = IsExpanded
            };

            return Task.CompletedTask;
        }

        protected override Element render()
        {
            return new FlexColumn(AlignItemsFlexStart, AlignSelfStretch, BackgroundWhite, Border("1px #D6DDE6 solid"), BorderRadius(8), Gap(16), JustifyContentFlexStart, Padding(20))
            {
                state.IsExpanded ? null : OnClick(ToggleCollapse),

                new FlexRow(AlignItemsFlexStart, AlignSelfStretch, JustifyContentSpaceBetween, Gap(8), CursorDefault)
                {
                    state.IsExpanded ? OnClick(ToggleCollapse) : null,

                    children[0],

                    new ArrowUpDownIcon { IsDirectionUp = !state.IsExpanded }
                },
                new div(SizeFull, state.IsExpanded ? null : DisplayNone)
                {
                    children[1]
                }
            };
        }

        Task ToggleCollapse(MouseEvent _)
        {
            state = state with { IsExpanded = !state.IsExpanded };

            return Task.CompletedTask;
        }

        sealed class ArrowUpDownIcon : PureComponent
        {
            public required bool IsDirectionUp { get; init; }

            public int? Size { get; init; }

            protected override Element render()
            {
                var element = new IconArrowUp
                {
                    Size.HasValue ? Size(Size.Value) : null
                };

                element.style.Add(style);

                if (IsDirectionUp is false)
                {
                    return new motion.div
                    {
                        initial =
                        {
                            rotate = -180
                        },
                        animate =
                        {
                            rotate = 0
                        },
                        children =
                        {
                            element
                        }
                    };
                }

                return new motion.div
                {
                    initial =
                    {
                        rotate = 0
                    },
                    animate =
                    {
                        rotate = -180
                    },
                    children =
                    {
                        element
                    }
                };
            }
        }

        internal record State
        {
            public bool IsExpanded { get; init; }
        }
    }
}