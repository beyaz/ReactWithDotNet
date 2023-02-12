namespace QuranAnalyzer.WebUI.Components;

public class TextArea : ReactPureComponent
{
    Expression<Func<string>> ValueBind;

    public static IModifier Bind(Expression<Func<string>> expression) => CreateModifier<TextArea>(x => x.ValueBind = expression);

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

public class TextInput : ReactPureComponent
{
    Expression<Func<string>> ValueBind;

    public static IModifier Bind(Expression<Func<string>> expression) => CreateModifier<TextInput>(x => x.ValueBind = expression);

    protected override Element render()
    {
        return new input
        {
            type      = "text",
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
}

public class Label : ReactPureComponent
{
    public string Text;

    protected override Element render()
    {
        return new label
        {
            text = Text, style = { FontSize14, FontWeight600 }
        };
    }
}