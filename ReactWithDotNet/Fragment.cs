namespace ReactWithDotNet;

public sealed class Fragment : Element
{
    internal List<IModifier> modifiers;

    public Fragment()
    {
    }

    public Fragment(params IModifier[] modifiers)
    {
        if (modifiers is null || modifiers.Length == 0)
        {
            return;
        }

        this.modifiers ??= new List<IModifier>();

        this.modifiers.AddRange(modifiers);
    }

    internal void ArrangeChildren()
    {
        if (modifiers is not null)
        {
            foreach (var modifier in modifiers)
            {
                foreach (var child in children)
                {
                    ModifyHelper.ProcessModifier(child,modifier);
                }
            }
        }
    }
    
}