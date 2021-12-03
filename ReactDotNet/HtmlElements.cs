using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bridge;
using Bridge.Html5;
using ReactDotNet;
using ReactDotNet.PrimeReact;

namespace ReactDotNet
{
    public interface IElement
    {
        string key { get; set; }

        CSSStyleDeclaration style { get; }

        int? gravity { get; }

        ReactElement ToReactElement();
    }

    public sealed class MarginThickness
    {
        readonly CSSStyleDeclaration style;

        public MarginThickness(CSSStyleDeclaration style)
        {
            this.style = style;
        }

        public double? Left
        {
            set
            {
                if (value.HasValue)
                {
                    style.MarginLeft = value.Value.AsPixel();
                }
                else
                {
                    style.MarginLeft = null;
                }
            }
        }

        public double? Top
        {
            set
            {
                if (value.HasValue)
                {
                    style.MarginTop = value.Value.AsPixel();
                }
                else
                {
                    style.MarginTop = null;
                }
            }
        }

        public double? Right
        {
            set
            {
                if (value.HasValue)
                {
                    style.MarginRight = value.Value.AsPixel();
                }
                else
                {
                    style.MarginRight = null;
                }
            }
        }

        public double? Bottom
        {
            set
            {
                if (value.HasValue)
                {
                    style.MarginBottom = value.Value.AsPixel();
                }
                else
                {
                    style.MarginBottom = null;
                }
            }
        }
    }

    public sealed class PaddingThickness
    {
        readonly CSSStyleDeclaration style;

        public PaddingThickness(CSSStyleDeclaration style)
        {
            this.style = style;
        }

        public double? Left
        {
            set
            {
                if (value.HasValue)
                {
                    style.PaddingLeft = value.Value.AsPixel();
                }
                else
                {
                    style.PaddingLeft = null;
                }
            }
        }

        public double? Top
        {
            set
            {
                if (value.HasValue)
                {
                    style.PaddingTop = value.Value.AsPixel();
                }
                else
                {
                    style.PaddingTop = null;
                }
            }
        }

        public double? Right
        {
            set
            {
                if (value.HasValue)
                {
                    style.PaddingRight = value.Value.AsPixel();
                }
                else
                {
                    style.PaddingRight = null;
                }
            }
        }

        public double? Bottom
        {
            set
            {
                if (value.HasValue)
                {
                    style.PaddingBottom = value.Value.AsPixel();
                }
                else
                {
                    style.PaddingBottom = null;
                }
            }
        }
    }

    public class ReactAttribute : Attribute
    {
    }


    public class Text: IElement
    {
        public readonly string Value;

        public Text(string value)
        {
            Value = value;

            Margin  = new MarginThickness(style);
            Padding = new PaddingThickness(style);
        }

        public int? gravity { get; set; }

        public MarginThickness Margin { get; }
        public PaddingThickness Padding { get; }

        [React]
        public string key { get; set; }

        [React]
        public string className { get; set; }

        [React]
        public CSSStyleDeclaration style { get; } = ObjectLiteral.Create<CSSStyleDeclaration>();

        public virtual ReactElement ToReactElement()
        {
            return React.createElement("text", this.CollectReactAttributedProperties(), Value);
        }
    }

    public class Button : Element
    {
        
        [React]
        public Action<SyntheticEvent<HTMLElement>> onClick { get; set; }

       

        public override ReactElement ToReactElement()
        {
            var atr = this.CollectReactAttributedProperties();
            var c   = children.Select(x => x.ToReactElement());
            
            return React.createElement("button", atr,"ggg", c);
        }
    }



    public class Space : Element
    {
        public double? Height
        {
            set
            {
                if (value.HasValue)
                {
                    style.Height = value.Value.AsPixel();
                }
                else
                {
                    style.Height = null;
                }
            }
        }

        public double? Width
        {
            set
            {
                if (value.HasValue)
                {
                    style.Width = value.Value.AsPixel();
                }
                else
                {
                    style.Width = null;
                }
            }
        }

        public override ReactElement ToReactElement()
        {
            return React.createElement("div", this.CollectReactAttributedProperties());
        }
    }

    public class ElementCollection : Element, IEnumerable<IElement>
    {
        protected readonly List<IElement> children = new List<IElement>();

        public override ReactElement ToReactElement()
        {
            UniqueKeyInitializer.InitializeKeyIfNotExists(children);

            return React.createElement(this.GetType().Name, this.CollectReactAttributedProperties(), children.Select(x => x.ToReactElement()));
        }

        public IEnumerator<IElement> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }

        public void Add(IElement element)
        {
            children.Add(element);
        }
    }

    public class span : ElementCollection
    {
        public span()
        {
            
        }

        public span(string className)
        {
            this.className = className;
        }

        public string Text { get; set; }

        public override ReactElement ToReactElement()
        {
            UniqueKeyInitializer.InitializeKeyIfNotExists(children);

            return React.createElement(nameof(span), this.CollectReactAttributedProperties(), Text, children.Select(x => x.ToReactElement()));
        }
    }

    public class i : ElementCollection
    {
        public i()
        {

        }

        public i(string className)
        {
            this.className = className;
        }


        public override ReactElement ToReactElement()
        {
            UniqueKeyInitializer.InitializeKeyIfNotExists(children);

            return React.createElement(nameof(i), this.CollectReactAttributedProperties(), children.Select(x => x.ToReactElement()));
        }

    }

    public class Panel : Element
    {
        public List<IElement> Cols { get; } = new List<IElement>();

        public IEnumerable<IElement> rows;

        public List<IElement> Rows { get; } = new List<IElement>();

        public override ReactElement ToReactElement()
        {
            if (rows != null)
            {
                var list = rows.ToList();
                if (list.Count > 0)
                {
                    Rows.AddRange(rows);
                }
            }

            if (Cols.Count > 0 && Rows.Count > 0)
            {
                throw new Exception("Cols and Rows can not be assigned same time.");
            }

            if (Cols.Count == 0 && Rows.Count == 0)
            {
                return React.createElement("div", this.CollectReactAttributedProperties());
            }

            if (Cols.Count > 0)
            {
                style.Display       = Display.Flex;
                style.FlexDirection = FlexDirection.Row;
                style.AlignItems    = AlignItems.Stretch;
                style.Height        = "100%";

                if (Cols.Any(x => x.gravity.HasValue))
                {
                    Cols.ForEach(child => { child.style.Width = GravityCalculator.CalculateGravity(child, Cols) * 100 + "%"; });
                }

                UniqueKeyInitializer.InitializeKeyIfNotExists(Cols);

                var children = Cols.Select(x => x.ToReactElement());

                return React.createElement("div", this.CollectReactAttributedProperties(), children);
            }

            // rows
            {
                style.Display       = Display.Flex;
                style.FlexDirection = FlexDirection.Column;
                style.AlignItems    = AlignItems.Stretch;
                style.Width         = "100%";

                if (Rows.Any(x => x.gravity.HasValue))
                {
                    Rows.ForEach(child => { child.style.Height = GravityCalculator.CalculateGravity(child, Rows) * 100 + "%"; });
                }

                UniqueKeyInitializer.InitializeKeyIfNotExists(Rows);

                var children = Rows.Select(x => x.ToReactElement());

                return React.createElement("div", this.CollectReactAttributedProperties(), children);
            }
        }
    }

    static class GravityCalculator
    {
        public static double CalculateGravity(IElement htmlElement, IReadOnlyList<IElement> siblings)
        {
            var total = siblings.Sum(x => x.gravity ?? 1);

            var gravity = htmlElement.gravity ?? 1;

            return (double)gravity / total;
        }
    }

    static class NumberExtensions
    {
        public static string AsPixel(this double value)
        {
            return value + "px";
        }
    }

    static class UniqueKeyInitializer
    {
        public static void InitializeKeyIfNotExists(IReadOnlyList<IElement> siblings)
        {
            var key = 0;

            foreach (var sibling in siblings)
            {
                if (sibling.key == null)
                {
                    sibling.key = key++.ToString();
                }
            }
        }
    }

    static class ReactAttributeCollector
    {

        [Template("Bridge.isPlainObject({0})")]
        static extern bool IsPlainObject(object value);

        [Template("Bridge.toPlain({0})")]
        static extern object ToPlain(object value);

        static bool IsBoxed(object value)
        {
            return value != null && value["$boxed"].As<bool>();
        }

        [Template("Bridge.unbox({0})")]
        static extern object GetBoxedValue(object value);

        public static ObjectLiteral CollectReactAttributedProperties(this IElement element)
        {
            var attributes = ObjectLiteral.Create<ObjectLiteral>();

            foreach (var propertyInfo in element.GetType().GetReactAttributedProperties())
            {
                var value = propertyInfo.GetValue(element);

                if (value == null)// if (value == null && propertyInfo.PropertyType.IsClass)
                {
                    continue;
                }
                
                if (IsBoxed(value))
                {
                    value = GetBoxedValue(value);
                }
                else
                {
                    value = ToPlain(value);
                }

                if (IsPlainObject(value) && Script.Write<bool>("Object.keys(value).length === 0"))
                {
                    continue;
                }
                
                attributes[propertyInfo.Name] = value;
            }

            return attributes;
        }

        public static IEnumerable<PropertyInfo> GetReactAttributedProperties(this Type type)
        {
            foreach (var propertyInfo in type.GetProperties().Where(p => p.CanRead && p.GetCustomAttributes(typeof(ReactAttribute)).Length > 0))
            {
                yield return propertyInfo;
            }
        }
    }
}