using System.Collections;

namespace ReactWithDotNet;

public abstract class ReactPureComponent : IEnumerable
{
    List<StyleModifier> styleModifiers;

    public static ReactPureComponent operator +(ReactPureComponent reactPureComponent, StyleModifier styleModifier)
    {
        (reactPureComponent.styleModifiers ??= new List<StyleModifier>()).Add(styleModifier);

        return reactPureComponent;
    }

    public void Add(ReactPureComponentModifier modifier)
    {
        modifier?.Modify(this);
    }

    public void Add(StyleModifier styleModifier)
    {
        (styleModifiers ??= new List<StyleModifier>()).Add(styleModifier);
    }

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException("You should not enumerate react pure component.");
    }

    internal Element InvokeRender()
    {
        var root = render();

        if (styleModifiers is not null)
        {
            foreach (var styleModifier in styleModifiers)
            {
                ModifyHelper.ProcessModifier(root, styleModifier);
            }
        }

        return root;
    }

    protected abstract Element render();
}

public abstract class ReactPureComponentModifier
{
    internal abstract void Modify(ReactPureComponent pureComponent);
}

partial class Mixin
{
    public static ReactPureComponentModifier CreatePureComponentModifier<TPureComponent>(Action<TPureComponent> modifyAction) where TPureComponent : ReactPureComponent
    {
        return new ReactPureComponentModifier<TPureComponent>(modifyAction);
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