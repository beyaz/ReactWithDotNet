using System;
using System.Collections.Generic;
using System.Linq;

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


   

    public class button : Element
    {
        public button()
        {
        }

        public button(string className)
        {
            this.className = className;
        }

    }

    public class span : Element
    {
        public span()
        {
        }

        public span(string className)
        {
            this.className = className;
        }
    }

    public class i : Element
    {
        public i() { }

        public i(string className)
        {
            this.className = className;
        }
    }

    public class div : Element
    {
        public div() { }

        public div(string className)
        {
            this.className = className;
        }
    }

    public class h5 : Element
    {
        public h5() { }

        public h5(string className)
        {
            this.className = className;
        }
    }

    public class h4 : Element
    {
        public h4() { }

        public h4(string className)
        {
            this.className = className;
        }
    }

    public class h3 : Element
    {
        public h3() { }

        public h3(string className)
        {
            this.className = className;
        }
    }

    public class h2 : Element
    {
        public h2() { }

        public h2(string className)
        {
            this.className = className;
        }
    }

    public class h1 : Element
    {
        public h1() { }

        public h1(string className)
        {
            this.className = className;
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

                return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties() };
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

                var childElements = Cols.Select(x => x.ToReactElement());

                return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties(), Children = childElements.ToList() };



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

                var childElements = Rows.Select(x => x.ToReactElement());

                return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties(), Children = childElements.ToList() };
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
}