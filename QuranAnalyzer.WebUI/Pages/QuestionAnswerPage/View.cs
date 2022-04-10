using System;
using ReactDotNet;

namespace QuranAnalyzer.WebUI.Pages.QuestionAnswerPage;

[Serializable]
public sealed class Model
{
    public string Header { get; set; }
    public string Note { get; set; }
}


public class View : ReactComponent
    {

    public override string id { get; set; } = nameof(QuestionAnswerPage);

    public override Element render()
        {

            var model = ResourceHelper.Read<ContactPage.Model>("Pages.QuestionAnswerPage.Data.yaml");

        return new div
            {
                new div {text = model.Header},
                new div {text = model.Note}
            };
        }
    }

