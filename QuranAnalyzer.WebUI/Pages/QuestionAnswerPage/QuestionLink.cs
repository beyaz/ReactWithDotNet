namespace QuranAnalyzer.WebUI.Pages.QuestionAnswerPage;

class QuestionLink : ReactComponent
{
    public string Question { get; set; }

    public string Url { get; set; }

    protected override Element render()
    {
        // https://support.google.com/youtube/answer/9872296?hl=en-GB&ref_topic=9257501
        const string d = "M19 5v14H5V5h14m0-2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-5 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z";

        return new FlexRow(AlignItemsCenter)
        {
            new div(wh(24))
            {
                new svg
                {
                    new path
                    {
                        d    = d,
                        fill = "#6c93d0"
                    },
                    new path
                    {
                        d    = "M0 0h24v24H0z",
                        fill = "none"
                    }
                }
            },
            new a
            {
                href      = Url,
                innerText = Question,
                style =
                {
                    PaddingLeft(10),
                    PaddingTopBottom(10),
                    Color("#575757"),
                    Hover(Color("rgb(165 107 107)"), TextDecorationUnderline),
                    CursorPointer,
                    TextDecorationNone,

                }
            }
        };
    }
}