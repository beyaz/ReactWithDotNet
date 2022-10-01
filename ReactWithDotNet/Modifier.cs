namespace ReactWithDotNet;

public class Modifier
{
    readonly Action<HtmlElement> htmlElementModifier;
    readonly Action<Style> modifyStyle;

    public Modifier(Action<Style> action)
    {
        modifyStyle = action ?? throw new ArgumentNullException(nameof(action));
    }

    public Modifier(Action<HtmlElement> action)
    {
        htmlElementModifier = action ?? throw new ArgumentNullException(nameof(action));
    }

    public static Modifier operator |(Modifier a, Modifier b)
    {
        void modify(Style style)
        {
            a.Apply(style);
            b.Apply(style);
        }

        return new Modifier(modify);
    }

    public static implicit operator Modifier(string text)
    {
        return Mixin.Text(text);
    }

    public void Apply(HtmlElement instance)
    {
        if (htmlElementModifier != null)
        {
            htmlElementModifier(instance);
        }
        else
        {
            modifyStyle(instance.style);
        }
    }

    public void Apply(Style style)
    {
        modifyStyle(style);
    }
}