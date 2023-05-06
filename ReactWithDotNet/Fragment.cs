namespace ReactWithDotNet;

public sealed class Fragment : Element
{
    internal List<IModifier> Modifiers;

    public Fragment()
    {
    }

    public Fragment(params IModifier[] modifiers)
    {
        if (modifiers is null || modifiers.Length == 0)
        {
            return;
        }

        (this.Modifiers ??= new List<IModifier>()).AddRange(modifiers);
    }

    internal void ArrangeChildren()
    {
        if (Modifiers is null)
        {
            return;
        }

        
        
        foreach (var modifier in Modifiers)
        {
            if (modifier is ElementModifier { isModifyReactKey: true } elementModifier)
            {
                elementModifier.modifyElement(this);
                continue;
            }

            foreach (var child in children)
            {
                ModifyHelper.ProcessModifier(child, modifier);
            }
        }
    }
}