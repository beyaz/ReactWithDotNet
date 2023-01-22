using System.Collections;
using System.Globalization;
using System.Text;
using static ReactWithDotNet.ElementSerializer;

namespace ReactWithDotNet;

public static class HtmlTextGenerator
{

    public static string ToHtml(ComponentResponse componentResponse)
    {
        return CalculateHtml((JsonMap)componentResponse.ElementAsJson, (JsonMap)componentResponse.DynamicStyles);
    }


    static string CalculateHtml(JsonMap element, JsonMap dynamicStyles)
    {
        var sb = new StringBuilder();

        Append(new OutputContext{ sb = sb, dynamicStyles = dynamicStyles },0,element);
        
        return sb.ToString();
    } 
    
    static string PascalToKebabCase(string dotnetPropertyName)
    {
        if (dotnetPropertyName == "className")
        {
            return "class";
        }

        if (dotnetPropertyName == "htmlFor")
        {
            return "for";
        }

        

        if (dotnetPropertyName == "cssFloat")
        {
            return "float";
        }

        if (dotnetPropertyName == "viewBox")
        {
            return "viewBox";
        }

        static IEnumerable<char> convertChar(char c, int index)
        {
            if (char.IsUpper(c) && index != 0) yield return '-';
            yield return char.ToLower(c,new CultureInfo("EN-us"));
        }

        return string.Concat(dotnetPropertyName.SelectMany(convertChar));
    }
    
    static void process(string name, object value, OutputContext context, int indent, ref string tag, ref bool hasInnerContent)
    {
        if (name == "key" || name == "DotNetProperties" || name == "onClick" || name == "$DotNetComponentUniqueIdentifier" ||
            name == "$State" ||
            name == "$Type" ||
            name == "$TypeOfState")
        {
            return;
        }

        var sb      = context.sb;
        
        var padding = "".PadLeft(indent, ' ');
        
        if (name == "$tag")
        {
            if (value is string valueAsString)
            {
                if (valueAsString == "html")
                {
                    sb.AppendLine("<!DOCTYPE html>");
                }


                tag = valueAsString;
                sb.Append(padding);
                sb.Append("<");
                sb.Append(tag);

                return;
            }

            return;

        }

        if (name == "style")
        {
            if (value is Style valueAsStyle)
            {
                sb.Append(" style=\"");

                valueAsStyle.VisitNotNullValues(add);

                void add(string propertyName, string propertyValue)
                {
                    sb.Append(PascalToKebabCase(propertyName));
                    sb.Append(":");
                    sb.Append(propertyValue);
                    sb.Append(';');
                }

                sb.Append('"');

                return;
            }

            return;
        }

        if (name[0] != '$')
        {
            if (value is string or int or double or bool)
            {
                sb.Append(" ");
                sb.Append(PascalToKebabCase(name));
                sb.Append("=");
                sb.Append('"');
                sb.Append(value);
                sb.Append('"');

                return;
            }
            value.ToString();
        }

        if (name == "$text")
        {
            sb.Append(">");
            hasInnerContent = true;

            sb.Append(value);
        }

        if (name == "$children")
        {
            sb.Append(">");
            hasInnerContent = true;

            var needNewLine = false;

            foreach (var child in (IEnumerable)value)
            {
                if (child is null)
                {
                    continue;
                }
                
                if (child is JsonMap childAsJsonMap)
                {
                    needNewLine = true;
                    sb.AppendLine();
                    Append(context, indent + 2, childAsJsonMap);
                }
                else if (child is string childAsString)
                {
                    sb.Append(childAsString);
                }
                else
                {
                    child.ToString();
                }

            }

            if (needNewLine)
            {
                sb.AppendLine();
            }

        }

        const string RootNode = "$RootNode";

        if (name == RootNode)
        {
            Append(context, sb.Length == 0 ? indent : indent + 2, (JsonMap)value);
        }

    }
    
    sealed class OutputContext
    {
        public int? HeadTagFinishIndex;
        public StringBuilder sb;
        public JsonMap dynamicStyles;
    }
    
    static void Append(OutputContext context, int indent, JsonMap element)
    {
        var          sb                         = context.sb;
        
        const string DotNetTypeOfReactComponent = "$Type";
        
        const  string RootNode = "$RootNode";

        var padding = "".PadLeft(indent, ' ');

        string tag = null;

        var hasInnerContent = false;
        
        element.Foreach((a,b)=>process(a,b,context,indent,ref tag,ref hasInnerContent));




        if (tag == null)
        {
            return;
        }

        if (tag == "head")
        {
            context.HeadTagFinishIndex = sb.Length;
        }
        
        if (hasInnerContent)
        {
            if (sb[^1] == '\n')
            {
                sb.Append(padding);
            }

            // try to insert dynamically created styles when reached to end of html
            {
                if (tag == "html")
                {
                    var headTagFinishIndex = context.HeadTagFinishIndex;
                    if (headTagFinishIndex.HasValue)
                    {
                        sb.Insert(headTagFinishIndex.Value, CalculateDynamicStylesAsHtmlStyleNode(context.dynamicStyles));
                    }
                }
            }


            sb.Append("<");
            if (!SelfClosingTags.Contains(tag))
            {
                sb.Append("/");
            }

            sb.Append(tag);
            sb.Append(">");
        }
        else
        {
            if (SelfClosingTags.Contains(tag))
            {
                sb.Append(">");
            }
            else
            {
                sb.Append("></");
                sb.Append(tag);
                sb.Append(">");
            }
        }
    }



    static readonly ReactContextKey<DynamicStyleContentForEmbeddInClient> DynamicStyles = new(nameof(DynamicStyles));
    static readonly ReactContextKey<int?> HeadTagFinishIndex = new(nameof(HeadTagFinishIndex));

    static readonly IReadOnlyList<string> SelfClosingTags = "area,base,br,col,embed,hr,img,input,keygen,link,meta,param,source,track,wbr".Split(',');

    public static string ToHtml(Element element, ReactContext reactContext = null)
    {
        var sb = new StringBuilder();

        reactContext ??= new ReactContext();

        reactContext.Set(DynamicStyles, new DynamicStyleContentForEmbeddInClient());

        Append(sb, 0, element, reactContext);

        return sb.ToString();
    }

    static void Append(StringBuilder sb, int indent, Element element, ReactContext reactContext)
    {
        if (element is HtmlElement htmlElement)
        {
            var tag = htmlElement.Type;

            if (element is HtmlTextNode textNode)
            {
                sb.Append(textNode.innerText);

                return;
            }

            var padding = "".PadLeft(indent, ' ');

            if (htmlElement is html)
            {
                sb.AppendLine("<!DOCTYPE html>");
            }

            sb.Append(padding);
            sb.Append("<");
            sb.Append(tag);

            if (htmlElement._style is not null)
            {
                var css = htmlElement._style.ToCss();
                if (!string.IsNullOrWhiteSpace(css))
                {
                    sb.Append(" style=\"");
                    sb.Append(css);
                    sb.Append("\"");
                }

                var pseudos = CalculatePseudos(htmlElement._style);
                if (pseudos?.Count > 0 || htmlElement._style._mediaQueries?.Count > 0)
                {
                    var className = reactContext.TryGetValue(DynamicStyles).GetClassName(new CssClassInfo
                    {
                        Name         = "rwd",
                        Pseudos      = pseudos,
                        MediaQueries = htmlElement._style._mediaQueries?.Select(pair => (pair.query, pair.style.ToCssWithImportant())).ToList()
                    });

                    htmlElement.AddClass(className);
                }
            }

            GetReactAttributedPropertiesOfType(element.GetType())
               .Foreach(propertyAccessInfo =>
                {
                    var propertyValue = propertyAccessInfo.GetValueFunc(element);
                    if (propertyValue != propertyAccessInfo.DefaultValue)
                    {
                        if (propertyValue is string)
                        {
                            sb.Append($" {getHtmlAttributeName(propertyAccessInfo.PropertyInfo.Name)}=\"");
                            sb.Append(propertyValue);
                            sb.Append("\"");
                        }
                    }
                });

            if (htmlElement._aria is not null)
            {
                foreach (var (name, value) in htmlElement._aria)
                {
                    sb.Append($" aria-{name}=\"");
                    sb.Append(value);
                    sb.Append("\"");
                }
            }

            if (htmlElement._data is not null)
            {
                foreach (var (name, value) in htmlElement._data)
                {
                    sb.Append($" data-{name}=\"");
                    sb.Append(value);
                    sb.Append("\"");
                }
            }

            if (htmlElement._children?.Count == 1 && htmlElement._children[0] is HtmlTextNode htmlTextNode)
            {
                sb.Append(">");

                sb.Append(htmlTextNode.innerText);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.dangerouslySetInnerHTML?.__html))
            {
                sb.Append(">");

                sb.Append(htmlElement.dangerouslySetInnerHTML?.__html);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");

                return;
            }

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText) && (htmlElement._children == null || htmlElement._children.Count == 0))
            {
                sb.Append(">");

                sb.Append(htmlElement.innerText);

                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");

                return;
            }

            var hasInnerContent = false;

            if (!string.IsNullOrWhiteSpace(htmlElement.innerText))
            {
                sb.Append(">");
                hasInnerContent = true;

                sb.Append(htmlElement.innerText);
            }

            if (htmlElement._children?.Count > 0)
            {
                sb.Append(">");
                hasInnerContent = true;

                foreach (var child in htmlElement.children)
                {
                    sb.AppendLine();
                    Append(sb, indent + 2, child, reactContext);
                }

                sb.AppendLine();
            }

            if (!hasInnerContent && element is script)
            {
                sb.Append("></");
                sb.Append(tag);
                sb.Append(">");
                return;
            }

            if (tag == "head")
            {
                reactContext.Set(HeadTagFinishIndex, sb.Length);
            }

            if (hasInnerContent)
            {
                // try to insert dynamically created styles when reached to end of html
                {
                    if (tag == "html")
                    {
                        var headTagFinishIndex = reactContext.TryGetValue(HeadTagFinishIndex);
                        if (headTagFinishIndex.HasValue)
                        {
                            sb.Insert(headTagFinishIndex.Value, CalculateDynamicStylesAsHtmlStyleNode(reactContext));
                        }
                    }
                }

                sb.Append(padding);
                sb.Append("<");
                if (!SelfClosingTags.Contains(tag))
                {
                    sb.Append("/");
                }

                sb.Append(tag);
                sb.Append(">");
            }
            else
            {
                if (SelfClosingTags.Contains(tag))
                {
                    sb.Append(">");
                }
                else
                {
                    sb.Append("></");
                    sb.Append(tag);
                    sb.Append(">");
                }
            }
        }
        else if (element is ReactStatefulComponent reactComponent)
        {
            reactComponent.Context = reactContext;

            reactComponent.InvokeConstructor();

            var root = reactComponent.InvokeRender();
            Append(sb, indent, root, reactContext);
        }

        static string getHtmlAttributeName(string dotnetPropertyName)
        {
            if (dotnetPropertyName == "httpEquiv")
            {
                return "http-equiv";
            }

            if (dotnetPropertyName == "className")
            {
                return "class";
            }

            return dotnetPropertyName;
        }

        static string CalculateDynamicStylesAsHtmlStyleNode(ReactContext reactContext)
        {
            var sb = new StringBuilder();

            var padding = "".PadLeft(4,' ');
            
            sb.AppendLine(padding+"<style>");
            reactContext.TryGetValue(DynamicStyles).CalculateCssClassList()?.Foreach((cssSelector, cssBody) =>
            {
                sb.Append(padding + cssSelector);
                sb.AppendLine(padding + "{");
                sb.Append(padding + "    ");
                sb.Append(cssBody);
                if (cssSelector.IndexOf("@media ", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    sb.AppendLine();
                    sb.AppendLine(padding + "}");
                }

                sb.AppendLine();
                sb.AppendLine(padding + "}");
            });
            sb.AppendLine(padding + "</style>");

            return sb.ToString();
        }
    }

    static string CalculateDynamicStylesAsHtmlStyleNode(JsonMap dynamicStylesMap)
    {
        var sb = new StringBuilder();

        var padding = "".PadLeft(4, ' ');

        sb.AppendLine(padding + "<style>");
        dynamicStylesMap?.Foreach((cssSelector, cssBody) =>
        {
            sb.Append(padding + cssSelector);
            sb.AppendLine(padding + "{");
            sb.Append(padding + "    ");
            sb.Append(cssBody);
            if (cssSelector.IndexOf("@media ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                sb.AppendLine();
                sb.AppendLine(padding + "}");
            }

            sb.AppendLine();
            sb.AppendLine(padding + "}");
        });
        sb.AppendLine(padding + "</style>");

        return sb.ToString();
    }
}
