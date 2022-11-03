using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.DefinitionPage;

public class DefinitionView : ReactComponent
{
    protected override Element render()
    {
        return new Article
        {
            new VSpace(10),
            new LargeTitle("Tanım"),
            new VSpace(15),

            
" Yüzyıllar boyu bu harfler için farklı farklı bir çok fikir ortaya atılmıştır.",
            " Size bir mektup geldiğini hayal edin ve mektubun ilk satırında sadece bir K harfi olduğunu düşünün. " ,
            " İster istemez burada bir kasıt ararsınız. Bir açıklama beklersiniz.",


            new VSpace(15)
        };
    }
}