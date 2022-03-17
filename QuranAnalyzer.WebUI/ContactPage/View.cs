using System;
using ReactDotNet;

namespace QuranAnalyzer.WebUI.ContactPage;

[Serializable]
public sealed class Model
{
    public string Header { get; set; }
    public string Note { get; set; }
}


public class View : ReactComponent
    {
        public Model model { get; set; }

        public override Element render()
        {
            return new div
            {
                new div {text = model.Header},
                new div {text = model.Note}
            };
        }
    }

