using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace ReactDotNet
{
    public static class Mixin
    {
        public static readonly JsonNamingPolicy JsonNamingPolicy =  JsonNamingPolicy.CamelCase;

        public static string ToJson(object value)
        {
            return JsonSerializer.Serialize(value, new JsonSerializerOptions().ModifyForReactDotNet());
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
            element.style.Visibility = isVisible ? Visibility.Visible : Visibility.Collapse;

            return element;
        }

        public static T MakeCenter<T>(this T element) where T : Element
        {
            element.style.Display = Display.Flex;
            element.style.JustifyContent = JustifyContent.Center;
            element.style.AlignItems = AlignItems.Center;

            return element;
        }

        public static T HasBorder<T>(this T element) where T : Element
        {
            element.style.Border = "1px solid #ced4da";
            element.style.BorderRadius = "3px";

            return element;
        }
    }
}