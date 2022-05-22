using System;
using System.Linq;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.QuestionAnswerPage;



[Serializable]
public sealed class QuestionAnswerPageModel
{
    public string Title { get; set; }
    public string Summary { get; set; }

    public QuestionAnswerPair[] QuestionsAndAnswers { get; set; }

}

[Serializable]
public sealed class QuestionAnswerPair
{
    public string Question { get; set; }
    public string Answer { get; set; }

}


public class View : PageBase
{

    public override string id { get; set; } = nameof(QuestionAnswerPage);

    public override Element render()
        {

            var model = ResourceHelper.Read<QuestionAnswerPageModel>("Pages.QuestionAnswerPage.Data.yaml");


            

        return new div
            {
                new Space{Height = 30},
                new div(paddingLeftRight(px(15)))
                {
                    new div(model.Title) | marginBottom(px(16)) | fontWeight(500) | TextAlign.center,

                    new p(model.Summary),

                    new div(model.QuestionsAndAnswers.Select(asLink))
                }
            };


        static Element asLink(QuestionAnswerPair x)
        {
            return new div
                {
                    new a(Display.flex, AlignItems.center)
                    {
                        new svg
                        {
                            viewBox = "0 0 24 24",
                            height  = "20px",
                            width   = px(20),
                            children =
                            {
                                new path {d = "M19 5v14H5V5h14m0-2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-5 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z", fill = "#0b57d0"},
                                new path {d = "M0 0h24v24H0z", fill= "none"}
                            }

                        },
                        new div(x.Question) | paddingLeft(14) | paddingTopBottom(px(10))
                    }
                }
            ;
            // https://support.google.com/youtube/answer/9872296?hl=en-GB&ref_topic=9257501
        }


    }
    }

