using System;
using System.Linq;
using ReactWithDotNet;

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


public class View : ReactComponent
{
    class QuestionLink: ReactComponent
    {
        public string Question { get; set; }

        protected override Element render()
        {
            return new div
                { 
                    style={display = "flex", alignItems = "center"},
                   children=
                   {
                       new div
                       {
                           style ={width_height = "24px"},
                           children =
                           {
                               new svg
                               {
                                   new path
                                   {
                                       d    = "M19 5v14H5V5h14m0-2H5c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h14c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2zm-5 14H7v-2h7v2zm3-4H7v-2h10v2zm0-4H7V7h10v2z",
                                       fill = "#0b57d0"

                                   },
                                   new path
                                   {
                                       d    = "M0 0h24v24H0z",
                                       fill = "none"
                                   }
                               }
                           }
                       }
                      ,
                       new div
                       {
                           innerText = Question,
                           style     ={ paddingLeft = "14px", paddingTopBottom = "10px"}
                       }
                   }

                }
                ;
            // https://support.google.com/youtube/answer/9872296?hl=en-GB&ref_topic=9257501
        }
    }

    protected override Element render()
        {

            var model = ResourceHelper.Read<QuestionAnswerPageModel>("Pages.QuestionAnswerPage.Data.yaml");


            

        return new div
            {
                new VSpace(230),
                new div
                {
                    style={paddingLeftRight = "15px"},
                    children=
                    {
                        new div
                        {
                            innerText = model.Title,
                            style=
                            {
                                marginBottom = "16px",
                                fontWeight = "500",
                                textAlign = "center"
                            }
                        },

                        new p(model.Summary),

                        new div
                        {
                            Children =model.QuestionsAndAnswers.Select(x=>new QuestionLink{ Question = x.Question })
                        }
                    }
                }
            };
        


    }
    }

