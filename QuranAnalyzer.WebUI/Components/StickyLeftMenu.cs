namespace QuranAnalyzer.WebUI.Components;

class StickyLeftMenu : ReactComponent // TODO: Check name
{
    [ReactCustomEvent]
    public Action<int?> Click { get; set; }

    public IReadOnlyList<string> Labels { get; set; }

    public int? SelectedIndex { get; set; }

    protected override Element render()
    {
        return new div
        {
            new FlexColumn
            {
                Children(Labels?.Select((x, i) => createText(x, i == SelectedIndex, i))),
                new div
                {
                    PositionAbsolute,
                    TopBottom(17),
                    Left(3.5),
                    BorderLeft("1px solid #EEEEEE"),
                }
            }
        };

        Element createText(string text, bool isSelected, int index)
        {
            var textColor = isSelected ? "#1EA7FD" : "#666666";

            var circleColor = isSelected ? "#1EA7FD" : "#DDDDDD";

            return new FlexRow(Gap(10), AlignItemsCenter, MarginTopBottom(5), Id(index), OnClick(OnClicked))
            {
                new div
                {
                    Top(10),
                    Background(circleColor),
                    wh(8),
                    BorderRadius("1em"),
                    Zindex(1)
                },
                new FlexRowCentered
                {
                    Text(text),
                    Background("#EEF2FF"),
                    BorderRadius("50%"),
                    wh(30),
                    Border("1px solid #DDDDDD"),
                    When(isSelected, Border("1px solid #1EA7FD")),
                    Hover(Color("#1e0ee7"), CursorPointer),
                    Color(textColor)
                }
            };
        }
    }

    void OnClicked(MouseEvent _)
    {
        SelectedIndex = int.TryParse(_.target.id, out var index) ? index : null;

        DispatchEvent(() => Click, SelectedIndex);
    }
}