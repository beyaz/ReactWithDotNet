namespace QuranAnalyzer.WebUI.Components;

public class TextArea : ReactComponent
{
    public Expression<Func<string>> ValueBind;

    protected override Element render()
    {
        return new textarea
        {
            valueBind = ValueBind,
            rows      = 6,
            style =
            {
                ComponentBorder,
                BorderRadius(5),

                Focus(Border($"1px solid {BluePrimary}"))
            }
        };
    }
}

public class TextInput: ReactComponent
{
    public Expression<Func<string>> ValueBind;

    protected override Element render()
    {
        return new input
        {
            type = "text",
            valueBind = ValueBind,
            style =
            {
                ComponentBorder,
                BorderRadius(5),
                Padding(5),
                Focus(Border($"1px solid {BluePrimary}"))
            }
        };
    }

    public static ComponentModifier Bind(Expression<Func<string>> expression) => new(x => ((TextInput)x).ValueBind = expression);
}


public class Label : ReactComponent
{
    public string Text;

    protected override Element render()
    {
        return new label
        {
            text = Text, style = { FontSize14,FontWeight600 }
        };
    }
}