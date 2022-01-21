using System;
using System.Collections.Generic;
using System.Linq;

namespace ReactDotNet
{
    
    

    [Serializable]
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

    [Serializable]
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

    [Serializable]
    public class ReactAttribute : Attribute
    {
    }

    [Serializable]
    public class ReactBindAttribute : Attribute
    {
        public string TargetProp { get;  set; }
        public string JsValueAccess { get;  set; }
        public string EventName { get;  set; }
    }

    [Serializable]
    public class ReactDefaultValueAttribute : Attribute
    {        
       public string DefaultValue { get;  set; }
    }




    public class button : HtmlElement
    {
        public button()
        {
        }

        public button(string className)
        {
            this.className = className;
        }

    }

    public class span : HtmlElement
    {
        public span()
        {
        }

        public span(string className)
        {
            this.className = className;
        }
    }

    public class i : HtmlElement
    {
        public i() { }

        public i(string className)
        {
            this.className = className;
        }
    }

    public class div : HtmlElement
    {
        public div() { }

        public div(string className)
        {
            this.className = className;
        }
    }



    public class h5 : HtmlElement
    {
        public h5() { }

        public h5(string className)
        {
            this.className = className;
        }
    }

    public class h4 : HtmlElement
    {
        public h4() { }

        public h4(string className)
        {
            this.className = className;
        }
    }

    public class h3 : HtmlElement
    {
        public h3() { }

        public h3(string className)
        {
            this.className = className;
        }
    }

    public class h2 : HtmlElement
    {
        public h2() { }

        public h2(string className)
        {
            this.className = className;
        }
    }

    public class h1 : HtmlElement
    {
        public h1() { }

        public h1(string className)
        {
            this.className = className;
        }
    }


    public class img : HtmlElement
    {
        public img() { }

        public img(string className)
        {
            this.className = className;
        }

        [React]
        public string src { get; set; }

        [React]
        public string alt { get; set; }

        [React]
        public int width { get; set; }

        [React]
        public int height { get; set; }
    }

    public class HPanel : div
    {
        public override string Tag => nameof(div);

        public HPanel()
        {
            style.Display = Display.Flex;
            style.FlexDirection = FlexDirection.Row;
            style.AlignItems = AlignItems.Stretch;
            style.Width = "100%";
        }

        protected internal override void BeforeSerialize()
        {
            base.BeforeSerialize();

            var children = Children;

            if (children.Any(x => x.gravity.HasValue))
            {
                foreach (var child in children)
                {
                    child.style.Width = (GravityCalculator.CalculateGravity(child, children) * 100).AsPercent();
                }
            }
        }
    }

    
    public sealed class VPanel : div
    {
        public override string Tag => nameof(div);
        
        public VPanel()
        {
            style.Display = Display.Flex;
            style.FlexDirection = FlexDirection.Column;
            style.AlignItems = AlignItems.Stretch;
            style.Height = "100%";
        }

        protected internal override void BeforeSerialize()
        {
            base.BeforeSerialize();

            var children = Children;

            if (children.Any(x => x.gravity.HasValue))
            {
                foreach (var child in children)
                {
                    child.style.Height = (GravityCalculator.CalculateGravity(child, children) * 100).AsPercent();
                }
            }
        }
    }
    public class Panel : Element
    {
        public List<Element> Cols { get; } = new List<Element>();

        public IEnumerable<Element> rows;

        public List<Element> Rows { get; } = new List<Element>();

        public Element render()
        {
            // todo: implement
            throw new NotImplementedException();
            /*
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
                return new div();
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

                

                var childElements = Cols.Select(x => x.ToReactElement()).ToList();

                UniqueKeyInitializer.InitializeKeyIfNotExists(childElements);

                return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties(), Children = childElements };



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

                

                var childElements = Rows.Select(x => x.ToReactElement()).ToList();

                UniqueKeyInitializer.InitializeKeyIfNotExists(childElements);

                return new ReactElement { Tag = "div", Props = this.CollectReactAttributedProperties(), Children = childElements };
            }*/
        }
    }

    static class GravityCalculator
    {
        public static double CalculateGravity(Element htmlElement, IReadOnlyList<Element> siblings)
        {
            var total = siblings.Sum(x => x.gravity ?? 1);

            var gravity = htmlElement.gravity ?? 1;

            return (double)gravity / total;
        }
    }


    public class svg : HtmlElement
    {
        [React]
        public string xmlns { get; set; } = "http://www.w3.org/2000/svg";
        
        [React]
        public string viewBox { get; set; }
    }

    public class path : HtmlElement
    {
        [React]
        public string d { get; set; }

         [React]
        public string fill { get; set; }
    }

     public class rect  : HtmlElement
    {
        [React]
        public int y { get; set; }

      
    }

    public class nav : HtmlElement
    {
        public nav() { }

        public nav(string className)
        {
            this.className = className;
        }
    }
   
}