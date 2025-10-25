using System.Reflection;
using static ReactWithDotNet.Transformers.AlreadyCalculatedModifierMarker;

namespace ReactWithDotNet.Transformers;




public static class ToModifierTransformer
{
    public static bool IsVariableName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        // is number
        if (char.IsDigit(value[0]) || value[0] == '.')
        {
            // 2ab = > false
            if ((from c in value where char.IsLetter(c) select c).Any())
            {
                return false;
            }

            // 2.5 => true
            return true;
        }

        return value.All(char.IsLetterOrDigit);
    }
    
    public static (bool success, string pseudo) TryGetPseudoForCSharp(string pseudo)
    {
        if (pseudo.Equals(nameof(Hover), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(Hover));
        }
        
        if (pseudo.Equals(nameof(Focus), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(Focus));
        }
        
        if (pseudo.Equals(nameof(SM), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(SM));
        }
        
        if (pseudo.Equals(nameof(MD), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(MD));
        }
        
        if (pseudo.Equals(nameof(LG), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(LG));
        }
        
        
        if (pseudo.Equals(nameof(XL), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(XL));
        }
        
        if (pseudo.Equals(nameof(XXL), StringComparison.OrdinalIgnoreCase))
        {
            return (true, nameof(XXL));
        }
        
        return (false, null);
    }

    static readonly IReadOnlyList<(string Name, string Value)> GlobalDeclaredStringFields =
        (from type in new[] { typeof(Mixin), typeof(Tailwind), typeof(WebColors) }
            from f in type.GetFields(BindingFlags.Static | BindingFlags.Public)
            where f.FieldType == typeof(string)
            select (f.Name, (string)f.GetValue(null))).ToList(); 
    
       
        
        

    public static (bool success, string modifierCode) TryConvertToModifier(string tagName, string name, string value, bool ignoreVariable = false)
    {
        if ((tagName == "iframe" || tagName == "script") && name.Equals("src", StringComparison.OrdinalIgnoreCase))
        {
            return Success($"{tagName}.Src({value})");
        }

        if (tagName == "svg" && name.Equals("size", StringComparison.OrdinalIgnoreCase) && double.TryParse(value.RemovePixelFromEnd(), out _))
        {
            return Success($"svg.Size({value.RemovePixelFromEnd()})");
        }

        if (tagName == "symbol" && name.Equals("viewBox", StringComparison.OrdinalIgnoreCase))
        {
            return Success($"symbol.ViewBox(\"{value}\")");
        }

        if (tagName == "source" && name.Equals("src", StringComparison.OrdinalIgnoreCase))
        {
            return Success($"source.Src(\"{value}\")");
        }

        if (tagName == "svg" && name.Equals("width", StringComparison.OrdinalIgnoreCase) && double.TryParse(value, out _))
        {
            return Success($"svg.Width({value})");
        }

        if (tagName == "svg" && name.Equals("height", StringComparison.OrdinalIgnoreCase) && double.TryParse(value, out _))
        {
            return Success($"svg.Height({value})");
        }

        if (name == "focusable" && tagName == "svg")
        {
            if ("true".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return Success($"svg.{nameof(svg.FocusableTrue)}");
            }

            if ("false".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return Success($"svg.{nameof(svg.FocusableFalse)}");
            }

            if ("auto".Equals(value, StringComparison.OrdinalIgnoreCase))
            {
                return Success($"svg.{nameof(svg.FocusableAuto)}");
            }

            return Success($"svg.Focusable(\"{value}\")");
        }

        if (name == "type" && tagName == "button")
        {
            return Success($"button.Type(\"{value}\")");
        }

        if (name.Equals("viewbox", StringComparison.OrdinalIgnoreCase) && tagName == "svg")
        {
            var parseResponse = tryParseViewBoxValues(value);
            if (parseResponse.success)
            {
                return Success($"ViewBox({string.Join(", ", parseResponse.parameters)})");
            }

            return Success($"ViewBox(\"{value}\")");
        }

        var response = TryConvertToModifier_From_Mixin_Extension(name, value, ignoreVariable);
        if (response.success)
        {
            return response;
        }

        var propertyInfo = TryFindProperty(tagName, name);
        if (propertyInfo is not null)
        {
            if (propertyInfo.PropertyType == typeof(double?) || propertyInfo.PropertyType == typeof(double))
            {
                if (double.TryParse(value, out var valueAsDouble))
                {
                    return Success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}({valueAsDouble})");
                }
            }

            if (propertyInfo.PropertyType == typeof(int?))
            {
                if (int.TryParse(value, out var valueAsInt32))
                {
                    return Success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}({valueAsInt32})");
                }
            }

            if (propertyInfo.DeclaringType == typeof(HtmlElement))
            {
                if (propertyInfo.Name == nameof(HtmlElement.onClick))
                {
                    return Success($"{nameof(OnClick)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onMouseEnter))
                {
                    return Success($"{nameof(OnMouseEnter)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onMouseLeave))
                {
                    return Success($"{nameof(OnMouseLeave)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onDoubleClick))
                {
                    return Success($"{nameof(OnDoubleClick)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onScroll))
                {
                    return Success($"{nameof(OnScroll)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onPaste))
                {
                    return Success($"{nameof(OnPaste)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onCut))
                {
                    return Success($"{nameof(OnCut)}({value})");
                }
                if (propertyInfo.Name == nameof(HtmlElement.onCopy))
                {
                    return Success($"{nameof(OnCopy)}({value})");
                }
            }
            
            return Success($"{tagName}.{UpperCaseFirstChar(propertyInfo.Name)}(\"{value}\")");
        }

        return (success: false, modifierCode: $"/* {tagName}.{name} = \"{value}\"*/");

        static (bool, string modifierCode) Success(string modifierCode) => (true, modifierCode);

        static string UpperCaseFirstChar(string str)
        {
            return char.ToUpper(str[0], new("en-US")) + str[1..];
        }

        static (bool success, string[] parameters) tryParseViewBoxValues(string value)
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parameters.Length == 4)
            {
                return (true, parameters);
            }

            return default;
        }
    }

    public static (bool success, string modifierCode) TryConvertToModifier_From_Mixin_Extension(string name, string value, bool ignoreVariable = false)
    {
        var success = (string modifierCode) => (true, modifierCode);

        value ??= string.Empty;

        // data- or aria-
        {
            if (isAriaAttribute(name))
            {
                return success(toAriaModifier(name, value));
            }

            if (isDataAttribute(name))
            {
                return success(toDataModifier(name, value));
            }

            static bool isDataAttribute(string name)
            {
                return name.StartsWith("data-", StringComparison.OrdinalIgnoreCase);
            }

            static string toDataModifier(string name, string value)
            {
                return $"Data(\"{name.RemoveFromStart("data-")}\", \"{value}\")";
            }

            static bool isAriaAttribute(string name)
            {
                return name.StartsWith("aria-", StringComparison.OrdinalIgnoreCase);
            }

            static string toAriaModifier(string name, string value)
            {
                return $"Aria(\"{name.RemoveFromStart("aria-")}\", \"{value}\")";
            }
        }

        if (name == "target" && value == "_blank")
        {
            return success("TargetBlank");
        }

        if (name.Equals("Width", StringComparison.OrdinalIgnoreCase) && value == "100%")
        {
            return success("WidthFull");
        }

        if (name.Equals("Height", StringComparison.OrdinalIgnoreCase) && value == "100%")
        {
            return success("HeightFull");
        }

        if (name.Equals("boxShadow", StringComparison.OrdinalIgnoreCase))
        {
            var parseResponse = TryParseBoxShadow(value);
            if (parseResponse.success)
            {
                return success($"BoxShadow({string.Join(", ", parseResponse.parameters)})");
            }

            return success($"{CamelCase(name)}(\"{value}\")");
        }

        if (name == "borderLeft" || name == "borderRight" || name == "borderTop" || name == "borderBottom")
        {
            var parameterList = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            if (parameterList.Count == 3 && parameterList[0].EndsWithPixel())
            {
                return success($"{CamelCase(name)}({parameterList[0].RemovePixelFromEnd()}, {asParameter(parameterList[1])}, {asParameter(parameterList[2])})");

                static string asParameter(string parameter)
                {
                    if (parameter == "solid")
                    {
                        return "solid";
                    }

                    return '"' + parameter + '"';
                }
            }
        }

        if (IsMarkedAsAlreadyCalculatedModifier(value))
        {
            return success(UnMarkAsAlreadyCalculatedModifier(value));
        }

        if (name == nameof(Style.padding) || name == nameof(Style.margin) || name == nameof(Style.borderRadius))
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();

            if (parameters.TrueForAll(ToModifierTransformerExtensions.IsEndsWithPixel) || 
                parameters.TrueForAll(ToModifierTransformerExtensions.IsDouble))
            {
                var methodName = CamelCase(name);

                string joinAllParameters()
                {
                    return $"{methodName}({string.Join(", ", parameters.Select(x => x.RemovePixelFromEnd()))})";
                }

                switch (parameters.Count)
                {
                    // 5px 5px
                    case 2 when parameters[0] == parameters[1]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()})");

                    // 5px 5px 5px 5px
                    case 4 when parameters[0] == parameters[1] &&
                                parameters[0] == parameters[2] &&
                                parameters[0] == parameters[3]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()})");

                    // 5px 6px 5px 6px
                    case 4 when parameters[0] == parameters[2] &&
                                parameters[1] == parameters[3]:
                        return success($"{methodName}({parameters[0].RemovePixelFromEnd()}, {parameters[1].RemovePixelFromEnd()})");

                    // 5px 6px
                    // 5px 6px 8px
                    // 5px 6px 7px 8px

                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return success(joinAllParameters());
                }
            }
        }

        if (name == "flex")
        {
            var parameters = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToList();
            if (parameters.Count == 3 && parameters.TrueForAll(x => double.TryParse(x, out _)))
            {
                return success($"Flex({parameters[0]}, {parameters[1]}, {parameters[2]})");
            }

            if (parameters.Count == 3 && double.TryParse(parameters[0], out _) && double.TryParse(parameters[1], out _) && parameters[2] == "auto")
            {
                return success($"Flex({parameters[0]}, {parameters[1]}, {parameters[2]})");
            }
        }
        
        if (name == "flexWrap")
        {
            if (value == "wrap")
            {
                return success(nameof(FlexWrap));
            }
            
            if (value == "nowrap")
            {
                return success(nameof(FlexNoWrap));
            }


        }

        var modifierFullName = $"{CamelCase(name)}{CamelCase(value.RemovePixelFromEnd())}";

        if (typeof(Mixin).GetProperty(modifierFullName) is not null)
        {
            return success(modifierFullName);
        }

        
        
        if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(string)]) is not null)
        {
            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(double)]) is not null &&
                value.EndsWithPixel() && value.IndexOf(' ') < 0)
            {
                return success($"{CamelCase(name)}({value.RemovePixelFromEnd()})");
            }

            if (value.StartsWith("rgb(", StringComparison.OrdinalIgnoreCase) &&
                value.EndsWith(")", StringComparison.OrdinalIgnoreCase))
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (TryGetGlobalDeclaredStringConstValue(value) is not null)
            {
                return success($"{CamelCase(name)}({TryGetGlobalDeclaredStringConstValue(value)})");
            }

            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(int)]) is not null &&
                int.TryParse(value, out _))
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (typeof(Mixin).GetMethod(CamelCase(name), [typeof(double)]) is not null &&
                double.TryParse(value, out _))
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (name.Equals("Border", StringComparison.OrdinalIgnoreCase))
            {
                var valueParts = value.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (valueParts.Length == 3)
                {
                    if (valueParts[0].EndsWithPixel())
                    {
                        return success($"Border({valueParts[0].RemovePixelFromEnd()}, {getStringParameter(valueParts[1])}, {getStringParameter(valueParts[2])})");
                    }
                }
            }

            if (IsVariableName(value) && ignoreVariable)
            {
                return success($"{CamelCase(name)}({value})");
            }

            if (value.StartsWith("{") && value.EndsWith("}"))
            {
                return success($"{CamelCase(name)}({value.RemoveFromStart("{").RemoveFromEnd("}")})");    
            }
            
            return success($"{CamelCase(name)}(\"{value}\")");
        }

        // try get from Style
        {
            var styleField = typeof(Style.Names).GetField(name);
            
            if (styleField is not null)
            {
                return success($"CreateStyleModifier(x => x.{styleField.Name} = {value})");
            }
        }
        
        return default;

        static (bool success, IReadOnlyList<string> parameters) TryParseBoxShadow(string boxShadow)
        {
            // sample: "0.1px 1px 2px rgba(28, 43, 61, 0.12)"

            if (boxShadow is null)
            {
                return default;
            }

            var index = boxShadow.IndexOf("rgba", StringComparison.OrdinalIgnoreCase);
            if (index <= 0)
            {
                index = boxShadow.IndexOf("rgb", StringComparison.OrdinalIgnoreCase);
            }

            if (index > 0)
            {
                var firstPart = boxShadow.Substring(0, index);

                var list = firstPart.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                if (list.TrueForAll(x => x.EndsWithPixel()))
                {
                    var parameters = list.Select(x => x.RemovePixelFromEnd()).ToList();
                    parameters.Add(boxShadow.Substring(index));
                    return (success: true, parameters);
                }
            }

            return default;
        }
    }

    public static PropertyInfo TryFindProperty(string htmlTagName, string attributeName)
    {
        var propertyName = string.Join(string.Empty, attributeName.Split(":-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()));

        return TryFindTypeOfHtmlTag(htmlTagName)?.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

        static Type TryFindTypeOfHtmlTag(string htmlTagName)
        {
            return typeof(div).Assembly.GetType(nameof(ReactWithDotNet) + "." + htmlTagName, false, true);
        }
    }

    public static string TryGetGlobalDeclaredStringConstValue(string value)
    {
        if (value == none ||
            value == auto ||
            value == inset ||
            value == inherit ||
            value == transparent ||
            value == solid ||
            value == dotted)
        {
            return value;
        }

        var item = GlobalDeclaredStringFields.FirstOrDefault(x => x.Value?.Equals(value, StringComparison.OrdinalIgnoreCase) is true);
        if (item.Name is not null)
        {
            return item.Name;
        }
        
        item = GlobalDeclaredStringFields.FirstOrDefault(x => x.Name?.Equals(value, StringComparison.OrdinalIgnoreCase) is true);
        
        return item.Name;
    }

    static string CamelCase(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return str;
        }

        if (str.IndexOf('-') > 0)
        {
            return string.Join(string.Empty, str.Split("-").Select(CamelCase));
        }

        if (str == "lowercase")
        {
            return "LowerCase";
        }

        if (str == "uppercase")
        {
            return "UpperCase";
        }

        return char.ToUpper(str[0], new("en-US")) + str.Substring(1);
    }

    static string getStringParameter(string prm)
    {
        return TryGetGlobalDeclaredStringConstValue(prm) ?? '"' + prm + '"';
    }
}

public static class AlreadyCalculatedModifierMarker
{
    public static bool IsMarkedAsAlreadyCalculatedModifier(string modifierCode)
    {
        return modifierCode?.Length > 2 && modifierCode[0] == '|';
    }

    public static string MarkAsAlreadyCalculatedModifier(string modifierCode)
    {
        return "|" + modifierCode;
    }

    public static string UnMarkAsAlreadyCalculatedModifier(string modifierCode)
    {
        return modifierCode.Substring(1);
    }
}

static class ToModifierTransformerExtensions
{
    public static bool EndsWithPixel(this string value)
    {
        return value?.EndsWith("px", StringComparison.OrdinalIgnoreCase) == true;
    }

    public static bool IsEndsWithPixel(string x)
    {
        return x.EndsWith("px", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    ///     Removes from end.
    /// </summary>
    public static string RemoveFromEnd(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data.EndsWith(value, comparison))
        {
            return data.Substring(0, data.Length - value.Length);
        }

        return data;
    }
    
    public static bool IsDouble(this string value)
    {
        return double.TryParse(value, out _);
    }

    /// <summary>
    ///     Removes value from start of str
    /// </summary>
    public static string RemoveFromStart(this string data, string value, StringComparison comparison = StringComparison.OrdinalIgnoreCase)
    {
        if (data == null)
        {
            return null;
        }

        if (data.StartsWith(value, comparison))
        {
            return data.Substring(value.Length, data.Length - value.Length);
        }

        return data;
    }

    public static string RemovePixelFromEnd(this string value)
    {
        return value?.RemoveFromEnd("px");
    }
}