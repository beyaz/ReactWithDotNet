using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using ReactDotNet;

namespace ReactDotNet.PrimeReact
{
   
    public class ElementBase : ThirdPartyComponent
    {
        public override IReadOnlyList<string> jsLocation => new[] { "primereact", GetType().Name };

        public ElementBase(params ElementModifier[] modifiers) : base(modifiers)
        {
        }

        public ElementBase()
        {
            
        }


        [React]
        public string tooltip { get; set; }

        [React]
        public TooltipOptions tooltipOptions { get; set; } 
    }



    public class InputText : ElementBase
    {
        static string IfNullThenReturnEmptyString(string value)
        {
            return value;
        }

        [React]
        [ReactDefaultValue(DefaultValue = "" )]
        public string value { get; set; }

        [React]
        [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value",  eventName = "onChange")]
        public Expression<Func<string>> valueBind { get; set; }
        
        [React]
        public string placeholder { get; set; }
    }
    

   

    public class InputTextarea : ElementBase
    {

        [React]
        public string value { get; set; }

        [React]
        public int rows { get; set; }
    }
    /*

   

    public class SplitPanel : Element
    {
        public List<IElement> Cols { get; } = new List<IElement>();

        public override ReactElement ToReactElement()
        {
            if (Cols.Count != 2)
            {
                throw PrimeException("SplitPanel should have least two children.");
            }

            UniqueKeyInitializer.InitializeKeyIfNotExists(Cols);

            var gravityOfItem0 = Cols[0].gravity ?? 1;
            var gravityOfItem1 = Cols[1].gravity ?? 1;

            var rate = 100 / (gravityOfItem0 + gravityOfItem1);

            var attributeFor0 = NewObject(x =>
            {
                x.size  = gravityOfItem0 * rate;
                x.style = NewObject(css => css.height = "100%");
                x.key   = "0";
            });

            var attributeFor1 = NewObject(x =>
            {
                x.size  = gravityOfItem1 * rate;
                x.style = NewObject(css => css.height = "100%");
                x.key   = "1";
            });


            var child0 = Cols[0].ToReactElement();
            var child1 = Cols[1].ToReactElement();

            var @Splitter      = FindPrimeType("Splitter");
            var @splitterPanel = FindPrimeType("SplitterPanel");



            var children = new[]
            {
                React.createElement(@splitterPanel, attributeFor0.As<ObjectLiteral>(), child0),
                React.createElement(@splitterPanel, attributeFor1.As<ObjectLiteral>(), child1)
            };


            return React.createElement(@Splitter, this.CollectReactAttributedProperties(), children);

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

        public static Type  FindPrimeType(string tag)
        {
            var primeReact = Script.Get<object>("primereact");
            if (primeReact == null)
            {
                throw new TypeException("PrimeReact must be initialize.");
            }

            var cmp = primeReact[tag];

            if (cmp == null)
            {
                throw new TypeException($"PrimeReact component not found. tag is '{tag}'");
            }

            return cmp.As<Type>();
        }
    }*/
}


