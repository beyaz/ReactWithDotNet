using ReactWithDotNet.ThirdPartyLibraries.FramerMotion;

namespace ReactWithDotNet.WebSite.Pages;

public sealed record FaqItemInfo
{
    public required string Title { get; init; }
    public required string HtmlContent { get; init; }
}

sealed class PageFrequendlyAskedQuestions : PureComponent
{
    public required IReadOnlyList<FaqItemInfo> FaqList { get; init; } = [new FaqItemInfo{  Title = "abc", HtmlContent = "fgh"}];

    public string Id { get; init; }

    protected override Element render()
    {
        if (FaqList.Count == 0)
        {
            return new div{id = Id};
        }

        return new section(DisplayFlexRowCentered)
        {
            new InlineFlexColumn(AlignItemsFlexStart, Flex(1, 1, 0), Gap(12), JustifyContentFlexStart)
            {
                new InlineFlexRow(AlignItemsCenter, AlignSelfStretch, Gap(423), JustifyContentFlexStart)
                {
                    new div(Id(Id))
                    {
                        "Sıkça Sorulan Sorular",
                        Color("#1C2B3D"),
                        FontFamily("Inter"),
                        FontSize26,
                        FontWeight700,
                        LetterSpacing(0.25),
                        LineHeight(32.11),
                        WordWrapBreakWord
                    }
                },
                new FlexColumn(AlignItemsFlexStart, AlignSelfStretch, Gap(16), JustifyContentFlexStart)
                {
                    FaqList.Select(ToItem)
                }
            }
        };

        Element ToItem(FaqItemInfo Item, int Index)
        {
            var isCollapsed = Index > 0;
            
            return FC(_ => new FlexColumn(AlignItemsFlexStart, AlignSelfStretch, BackgroundWhite, Border("1px #D6DDE6 solid"), BorderRadius(8), Gap(16), JustifyContentFlexStart, Padding(20))
            {
                isCollapsed ? OnClick(ToggleCollapse) : null,
                
                new InlineFlexRow(AlignItemsCenter, AlignSelfStretch, JustifyContentFlexStart)
                {
                    isCollapsed ? null: OnClick(ToggleCollapse),
                    
                    new div
                    {
                        Item.Title,
                        Color("#1C2B3D"),
                        Flex(1, 1, 0),
                        FontFamily("Inter"),
                        FontSize20,
                        FontWeight700,
                        LineHeight(26.68),
                        WordWrapBreakWord
                    },
                    new ArrowUpDownIcon { IsCollapsed = isCollapsed }
                },
                isCollapsed
                    ? null
                    : new div
                    {
                        DangerouslySetInnerHTML(Item.HtmlContent),
                        new[]
                        {
                            AlignSelfStretch,
                            Color("#1C2B3D"),
                            FontFamily("Inter"),
                            FontSize14,
                            FontWeight400,
                            LineHeight(20.02),
                            WordWrapBreakWord
                        }
                    }
            });

            Task ToggleCollapse(MouseEvent _)
            {
                isCollapsed = !isCollapsed;

                return Task.CompletedTask;
            }
        }
    }
    
    
    sealed class ArrowUpDownIcon : PureComponent
    {
        public MouseEventHandler Click;
    
        public required bool IsCollapsed { get; init; }
    
        public int? Size { get; set; }

        protected override Element render()
        {
            var element = new IconArrowUp
            {
                OnClick(Click),
                Size.HasValue ? Size(Size.Value) : null
            };
        
            element.style.Add(style);

            if (IsCollapsed is false)
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
}