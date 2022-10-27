namespace QuranAnalyzer.WebUI.Components;

public class TextArea:ReactComponent
{
    public Expression<Func<string>> ValueBind;
        
    protected override Element render()
    {
        return new textarea { valueBind = ValueBind, rows = 2 };
    }
}