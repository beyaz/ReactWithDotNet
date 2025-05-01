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

        var (map, exception) = ParseCssAsDictionary(css);
        if (exception is not null)
        {
            throw exception;
        }
        
        foreach (var (key, value) in map)
        {
            this[key] = value;
        }
    }
    
    public static (IReadOnlyDictionary<string,string> value, Exception exception) ParseCssAsDictionary(string css)
    {
        if (css == null)
        {
            return (null, new ArgumentNullException(nameof(css)));
        }

        var map = new Dictionary<string, string>();
        
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
                return (null, CssParseException(line));
            }

            var cssAttributeName = array[0].Trim();
            if (cssAttributeName.StartsWith("/*", StringComparison.OrdinalIgnoreCase))
            {
                var endCommentIndex = cssAttributeName.LastIndexOf("*/", StringComparison.OrdinalIgnoreCase);
                if (endCommentIndex < 0)
                {
                    return (null, CssParseException(line));
                }

                cssAttributeName = cssAttributeName.Substring(endCommentIndex + 2).Trim();
            }

            map[cssAttributeName] = array[1];
        }

        return (map, null);
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
    
    public Exception TryImport(IReadOnlyDictionary<string, string> map)
    {
        if (map == null)
        {
            return null;
        }

        foreach (var (key, value) in map)
        {
            var exception = TrySet(key, value);
            if (exception is not null)
            {
                return exception;
            }
        }
        
        return null;
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
        return new ("Css parse error." + message);
    }
    
    string ToCss(bool isImportant)
    {
        var sb = new StringBuilder();

        ReadOnlySpan<char> semiColumn = ":";
        
        ReadOnlySpan<char> separator = isImportant ? " !important;" : ";";
        
        var currentNode = headNode;
        
        while (currentNode != null)
        {
            sb.Append(currentNode.NameInfo.NameInKebabCase);
            sb.Append(semiColumn);
            sb.Append(currentNode.Value);
            sb.Append(separator);

            if (currentNode.Next == null)
            {
                break;
            }
            
            currentNode = currentNode.Next;
        }

        if (sb.Length == 0)
        {
            return null;
        }

        return sb.ToString();
    }
}