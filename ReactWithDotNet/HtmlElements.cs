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
        sb.Append("{");
        
        var stylee = new Style(classInfo._styleModifiers);

        sb.AppendLine(stylee.ToCss());
        
        sb.Append("}");
        
        if (stylee._hover is not null)
        {
            sb.Append(nameOfClass);
            sb.Append(":hover {");
            sb.AppendLine(stylee._hover.ToCssWithImportant());
            sb.Append("}");
        }

        innerText += sb;
        
        
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