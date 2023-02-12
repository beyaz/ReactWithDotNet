namespace ReactWithDotNet;

public abstract class ReactPureComponent : Element
{
    internal List<IModifier> modifiers;

    internal Element InvokeRender() => render();

    protected abstract Element render();
}