using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Web;

namespace ReactDotNet
{
    public static partial class Mixin
    {

        internal static IReadOnlyDictionary<string, string> ParseQueryString(string query)
        {
            var items = new Dictionary<string, string>();

            if (query == null)
            {
                return items;
            }

            var nameValueCollection = HttpUtility.ParseQueryString(query);
            foreach (var key in nameValueCollection.AllKeys)
            {
                items.Add(key, nameValueCollection.Get(key));
            }

            return items;
        }

        internal static void Apply(Element element, params ElementModifier[] modifiers)
        {
            foreach (var modifier in modifiers)
            {
                modifier?.Modify(element);
            }
        }

        public static  JsonNamingPolicy JsonNamingPolicy;

        public static string ToJson(object value)
        {
            var options = new JsonSerializerOptions();

            options = options.ModifyForReactDotNet();
            
            return JsonSerializer.Serialize(value, options);
        }

        public static void Import(this Style style, Style newStyle)
        {
            foreach (var (propertyInfo, newValue) in newStyle.GetValues())
            {
                propertyInfo.SetValue(style,newValue);
            }
        }

        internal static IReadOnlyList<(PropertyInfo propertyInfo, object newValue)> GetValues(this Style style)
        {
            var returnItems = new List<(PropertyInfo propertyInfo, object newValue)>();

            foreach (var propertyInfo in typeof(Style).GetProperties())
            {
                var value = propertyInfo.GetValue(style);

                var defaultValue = propertyInfo.PropertyType.GetDefaultValue();

                if (value == null)
                {
                    if (defaultValue == null)
                    {
                        continue;
                    }

                    returnItems.Add((propertyInfo, null));
                    continue;
                }

                if (!value.Equals(defaultValue))
                {
                    returnItems.Add((propertyInfo, value));
                }
            }

            return returnItems;
        }

        

        public static JsonSerializerOptions ModifyForReactDotNet(this JsonSerializerOptions options)
        {
            return JsonSerializationOptionHelper.Modify(options);
        }


        public static T Gravity<T>(this T element, int gravity) where T: Element
        {
            element.gravity = gravity;

            return element;
        }

        public static T IsVisible<T>(this T element, bool isVisible) where T : Element
        {
            element.style.visibility = isVisible ? Visibility.visible : Visibility.collapse;

            return element;
        }

        public static T MakeCenter<T>(this T element) where T : Element
        {
            element.style.display = Display.flex;
            element.style.justifyContent = JustifyContent.center;
            element.style.alignItems = AlignItems.center;

            return element;
        }

        public static T HasBorder<T>(this T element) where T : Element
        {
            element.style.border = "1px solid #ced4da";
            element.style.borderRadius = "3px";            

            return element;
        }

        public static T Padding<T>(this T element, int padding) where T : Element
        {
            element.style.padding = padding + "px";

            return element;
        }

        public static T Style<T>(this T element, Action<Style> modifyStyle) where T : Element
        {
            modifyStyle(element.style);

            return element;
        }

        public static TParent appendChild<TParent,TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
        {
            element.children.Add(child);

            return element;
        }
        

        public static string px(int value)
        {
            return value + "px";
        }

        /// <summary>
        ///  Relative to 1% of the height of the viewport*
        /// </summary>
        public static string vh(double value)
        {
            return value + "vh";
        }

        /// <summary>
        ///  Relative to 1% of the width of the viewport*
        /// </summary>
        public static string vw(double value)
        {
            return value + "vw";
        }


        public static string px(double value)
        {
            return value + "px";
        }
        public static ElementModifier lineHeight(string lineHeight)
        {
            return new ElementModifier(element => element.style.lineHeight = lineHeight);
        }
        public static ElementModifier fontSize(double value)
        {
            return new ElementModifier(element => element.style.fontSize = px(value));
        }
        public static ElementModifier fontSize(string fontSize)
        {
            return new ElementModifier(element => element.style.fontSize = fontSize);
        }
        public static ElementModifier fontFamily(string fontFamily)
        {
            return new ElementModifier(element => element.style.fontFamily = fontFamily);
        }
        public static ElementModifier fontWeight(int fontWeight)
        {
            return new ElementModifier(element => element.style.fontWeight = fontWeight.ToString());
        }
        public static ElementModifier background(string value)
        {
            return new ElementModifier(element => element.style.background = value);
        }

        public static ElementModifier margin(double margin)
        {
            return new ElementModifier(element => element.style.margin = px(margin));
        }

        public static ElementModifier padding(double padding)
        {
            return new ElementModifier(element => element.style.padding = px(padding));
        }
        public static ElementModifier paddingLeft(double paddingLeft)
        {
            return new ElementModifier(element => element.style.paddingLeft = px(paddingLeft));
        }
        public static ElementModifier paddingRight(double paddingRight)
        {
            return new ElementModifier(element => element.style.paddingRight = px(paddingRight));
        }

        public static ElementModifier paddingLeftRight(string paddingLeftRight)
        {
            return new ElementModifier(element =>
            {
                element.style.paddingLeft  = paddingLeftRight;
                element.style.paddingRight = paddingLeftRight;
            });
        }

        public static ElementModifier marginLeftRight(string marginLeftRight)
        {
            return new ElementModifier(element =>
            {
                element.style.marginLeft  = marginLeftRight;
                element.style.marginRight = marginLeftRight;
            });
        }
        



        public static ElementModifier paddingTop(double paddingTop)
        {
            return new ElementModifier(element => element.style.paddingTop = px(paddingTop));
        }

        public static ElementModifier paddingBottom(double paddingBottom)
        {
            return new ElementModifier(element => element.style.paddingBottom = px(paddingBottom));
        }

        public static ElementModifier height(double height)
        {
            return new ElementModifier(element => element.style.height = px(height));
        }

        public static ElementModifier height(string height)
        {
            return new ElementModifier(element => element.style.height = height);
        }

        public static ElementModifier width(double width)
        {
            return new ElementModifier(element => element.style.width = px(width));
        }
        public static ElementModifier width(string width)
        {
            return new ElementModifier(element => element.style.width = width);
        }
        public static ElementModifier marginLeft(double margin)
        {
            return new ElementModifier(element => element.style.marginLeft = px(margin));
        }
        public static ElementModifier marginRight(double margin)
        {
            return new ElementModifier(element => element.style.marginRight = px(margin));
        }

        public static ElementModifier marginTop(double margin)
        {
            return new ElementModifier(element => element.style.marginTop = px(margin));
        }

        public static ElementModifier marginLeft(string marginLeft)
        {
            return new ElementModifier(element => element.style.marginLeft = marginLeft);
        }
        public static ElementModifier marginRight(string marginRight)
        {
            return new ElementModifier(element => element.style.marginRight = marginRight);
        }

        public static ElementModifier marginTop(string marginTop)
        {
            return new ElementModifier(element => element.style.marginTop = marginTop);
        }

        public static ElementModifier marginBottom(double margin)
        {
            return new ElementModifier(element => element.style.marginBottom = px(margin));
        }

        public static ElementModifier visibility(Visibility visibility)
        {
            return new ElementModifier(element => element.style.visibility = visibility);
        }

        public static string percentOf(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture).Replace(",",".") + "%";
        }

        public static string AsPercent(this double value)
        {
            return percentOf(value);
        }

        public static string percentOf(int value)
        {
            return value.ToString(CultureInfo.InvariantCulture).Replace(",", ".") + "%";
        }

        public static ElementModifier className(string className)
        {
            return new ElementModifier(element => element.className = className);
        }
        public static ElementModifier alignSelf(AlignItems alignSelf)
        {
            return new ElementModifier(element => element.style.alignSelf = alignSelf);
        }

        public static ElementModifier border(string border)
        {
            return new ElementModifier(element => element.style.border = border);
        }


        public static ElementModifier borderLeft(string borderLeft)
        {
            return new ElementModifier(element => element.style.borderLeft = borderLeft);
        }

        public static ElementModifier borderRight(string borderRight)
        {
            return new ElementModifier(element => element.style.borderRight = borderRight);
        }

        public static ElementModifier borderTop(string borderTop)
        {
            return new ElementModifier(element => element.style.borderTop = borderTop);
        }

        public static ElementModifier borderBottom(string borderBottom)
        {
            return new ElementModifier(element => element.style.borderBottom = borderBottom);
        }

        public static ElementModifier borderRadius(string borderRadius)
        {
            return new ElementModifier(element => element.style.borderRadius = borderRadius);
        }

        public static ElementModifier borderColor(string borderColor)
        {
            return new ElementModifier(element => element.style.borderColor = borderColor);
        }
        

        public static ElementModifier color(string color)
        {
            return new ElementModifier(element => element.style.color = color);
        }

        public static ElementModifier id(string id)
        {
            return new ElementModifier(element => element.id = id);
        }
    }

    public static class HtmlEvent
    {
        public static ElementModifier onClick(Action<string> onClick)
        {
            return new ElementModifier(element => element.onClick = onClick);
        }
    }

}