using System;
using Bridge;
using Bridge.Html5;

namespace ReactDotNet.MaterialUI
{

    public class ElementBase : Element
    {
        public override ReactElement ToReactElement()
        {
            var tag = GetType().Name;

            var type = Helper.FindMaterialUIType(tag);

            return React.createElement(type, this.CollectReactAttributedProperties());
        }
    }

    [Enum(Emit.StringNameLowerCase)]
    public enum ButtonVariant
    {
        text , outlined , contained
    }

    public class Button : ElementBase
    {

        [React]
        public Action<SyntheticEvent<HTMLElement>> onClick { get; set; }


        [React]
        public Union<string, ButtonVariant> variant { get; set; }

        public string Label { get; set; }

        public override ReactElement ToReactElement()
        {
            var tag = GetType().Name;

            var type = Helper.FindMaterialUIType(tag);

            return React.createElement(type, this.CollectReactAttributedProperties(), Label);
        }
    }

    static class Helper
    {
        public static Exception PrimeException(string message)
        {
            return new Exception(message);
        }

        public static object NewObject(Action<dynamic> modify)
        {
            var obj = ObjectLiteral.Create<object>();

            modify(obj);

            return obj;
        }

        public static Type FindMaterialUIType(string tag)
        {
            var root = Script.Get<object>("MaterialUI");
            if (root == null)
            {
                throw new TypeException("MaterialUI must be initialize.");
            }

            var cmp = root[tag];

            if (cmp == null)
            {
                throw new TypeException($"MaterialUI component not found. tag is '{tag}'");
            }

            return cmp.As<Type>();
        }
    }
}
