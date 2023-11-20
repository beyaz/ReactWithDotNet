using System.Text;

namespace ReactWithDotNet;

partial class Style
{
    public static Style ParseCss(string css)
    {
        var style = new Style();

        style.Import(css);

        return style;
    }

    public void Import(string css)
    {
        if (css == null)
        {
            return;
        }

        foreach (var line in css.Trim().Split(";").Select(v => v.Trim()).Where(v => !string.IsNullOrWhiteSpace(v)))
        {
            // Skip css variables
            if (line.StartsWith("--"))
            {
                continue;
            }
            
            var array = line.Trim().Split(":").Select(v => v.Trim()).Where(v => !string.IsNullOrWhiteSpace(v)).ToArray();
            if (array.Length != 2)
            {
                throw CssParseException(line);
            }

            var cssAttributeName = array[0].Trim();
            if (cssAttributeName.StartsWith("/*", StringComparison.OrdinalIgnoreCase))
            {
                var endCommentIndex = cssAttributeName.LastIndexOf("*/", StringComparison.OrdinalIgnoreCase);
                if (endCommentIndex < 0)
                {
                    throw CssParseException(line);
                }

                cssAttributeName = cssAttributeName.Substring(endCommentIndex + 2).Trim();
            }

            this[cssAttributeName] = array[1];
        }
    }
    
    public void Import(IReadOnlyDictionary<string, string> map)
    {
        if (map == null)
        {
            return;
        }

        foreach (var (key, value) in map)
        {
            this[key] = value;
        }
    }

    public string ToCss()
    {
        return ToCss(isImportant: false);
    }

    public string ToCssWithImportant()
    {
        return ToCss(isImportant: true);
    }

    public override string ToString()
    {
        return ToCss();
    }

    static Exception CssParseException(string message)
    {
        return new Exception("Css parse error." + message);
    }

    string ToCss(bool isImportant)
    {
        var sb = new StringBuilder();

        var separator = isImportant ? " !important;" : ";";
        
        toCss(this, sb, separator);

        if (sb.Length == 0)
        {
            return null;
        }

        return sb.ToString();
    }
}