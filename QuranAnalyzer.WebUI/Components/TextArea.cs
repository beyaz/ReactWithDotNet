namespace QuranAnalyzer.WebUI.Components;

public class TextArea : ReactPureComponent
{
    Expression<Func<string>> ValueBind;

    public static ReactPureComponentModifier Bind(Expression<Func<string>> expression) =>
        CreatePureComponentModifier<TextArea>(x => x.ValueBind = expression);

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
    public Expression<Func<string>> ValueBind;

    public static ReactPureComponentModifier Bind(Expression<Func<string>> expression) =>
        CreatePureComponentModifier<TextInput>(x => x.ValueBind = expression);

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