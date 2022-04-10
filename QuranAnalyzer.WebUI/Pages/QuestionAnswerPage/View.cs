using System;
using ReactDotNet;

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
                new div {text = model.Title},
                new div {text = model.Summary}
            };
        }
    }

