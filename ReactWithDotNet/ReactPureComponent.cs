namespace ReactWithDotNet;

public abstract class ReactPureComponent : Element
{
    internal List<IModifier> modifiers;

    internal Element InvokeRender() => render();

    protected abstract Element render();
}

abstract class ReactPureComponentModifier : IModifier
{
    internal abstract void Modify(ReactPureComponent pureComponent);
}

partial class Mixin
{
    public static IModifier CreatePureComponentModifier<TPureComponent>(Action<TPureComponent> modifyAction) where TPureComponent : ReactPureComponent
    {
        return new ReactPureComponentModifier<TPureComponent>(modifyAction);
    }

    public static StyleModifier CreateModifier(Action<Style> modifyAction)
    {
        return new StyleModifier(modifyAction);
    }

    public static HtmlElementModifier CreateModifier(Action<HtmlElement> modifyAction)
    {
        return new HtmlElementModifier(modifyAction);
    }

    public static ComponentModifier CreateModifier(Action<ReactStatefulComponent> modifyAction)
    {
        return new ComponentModifier(modifyAction);
    }
}

sealed class ReactPureComponentModifier<TPureComponent> : ReactPureComponentModifier where TPureComponent : ReactPureComponent
{
    internal readonly Action<TPureComponent> modify;

    public ReactPureComponentModifier(Action<TPureComponent> modifyPureComponent)
    {
        modify = modifyPureComponent ?? throw new ArgumentNullException(nameof(modifyPureComponent));
    }

    internal override void Modify(ReactPureComponent pureComponent)
    {
        if (pureComponent == null)
        {
            return;
        }

        modify((TPureComponent)pureComponent);
    }
}