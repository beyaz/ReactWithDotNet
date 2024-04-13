namespace ReactWithDotNet;

public sealed class Fragment : Element
{
    internal List<Modifier> Modifiers;

    public Fragment()
    {
    }

    public Fragment(params Modifier[] modifiers)
    {
        if (modifiers is null || modifiers.Length == 0)
        {
            return;
        }

        (Modifiers = []).AddRange(modifiers);
    }

    internal void ArrangeChildren()
    {
        if (Modifiers is null)
        {
            return;
        }

        foreach (var modifier in Modifiers)
        {
            if (modifier is ElementModifier { IsModifyReactKey: true } elementModifier)
            {
                elementModifier.ModifyElement(this);
                continue;
            }

            foreach (var child in children)
            {
                ModifyHelper.ProcessModifier(child, modifier);
            }
        }
    }
}