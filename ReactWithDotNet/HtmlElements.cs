using System.Text;

namespace ReactWithDotNet;

[Serializable]
public sealed class InputValueBinder
{
    public static implicit operator InputValueBinder(string value)
    {
        return new();
    }

    public static implicit operator InputValueBinder(double value)
    {
        return new();
    }
}

public sealed class HtmlTextNode : HtmlElement
{
    internal StringBuilder stringBuilder;
}

sealed class br : HtmlElement;

public sealed class style : HtmlElement
{
    public void Add(CssClass classInfo)
    {
        if (classInfo == null)
        {
            throw new ArgumentNullException(nameof(classInfo));
        }

        var nameOfClass = classInfo._name?.Trim();
        if (string.IsNullOrWhiteSpace(nameOfClass))
        {
            throw new ArgumentException(classInfo._name);
        }

        if (nameOfClass[0] != '.')
        {
            nameOfClass = "." + nameOfClass;
        }

        var sb = new StringBuilder();
        sb.Append(Environment.NewLine);
        sb.Append(nameOfClass);
        sb.AppendLine("{");

        var styleInstance = new Style(classInfo._styleModifiers);

        sb.AppendLine(styleInstance.ToCss());

        sb.AppendLine("}");

        writePseudo(sb, nameOfClass, styleInstance._hover, "hover");
        writePseudo(sb, nameOfClass, styleInstance._before, "before");
        writePseudo(sb, nameOfClass, styleInstance._after, "after");
        writePseudo(sb, nameOfClass, styleInstance._active, "active");
        writePseudo(sb, nameOfClass, styleInstance._focus, "focus");
        
        // todo: media queries

        innerText += sb.ToString();
        return;

        static void writePseudo(StringBuilder sb, string cssClassName, Style pseudo, string pseudoName)
        {
            if (pseudo is null)
            {
                return;
            }

            sb.Append(cssClassName);
            sb.AppendLine($":{pseudoName} {{");
            sb.AppendLine(pseudo.ToCssWithImportant());
            sb.AppendLine("}");
        }
    }
}

public sealed class CssClass
{
    internal readonly string _name;
    internal readonly IReadOnlyList<StyleModifier> _styleModifiers;

    public CssClass(string name, IReadOnlyList<StyleModifier> styleModifiers)
    {
        _name           = name;
        _styleModifiers = styleModifiers;
    }
}