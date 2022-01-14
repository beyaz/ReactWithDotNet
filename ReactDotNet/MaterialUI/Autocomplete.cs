using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ReactDotNet.MaterialUI
{
    public class ElementBase : ThirdPartyComponent
    {
        public override IReadOnlyList<string> JsLocation => new[] { "MaterialUI", GetType().Name };
    }

    public enum TextFieldVariant
    {
        outlined, filled, standard
    }

    public class TextField : ElementBase
    {

        [React]
        public string label { get; set; }


        [React]
        public TextFieldVariant? variant { get; set; }

        [React]
        public string value { get; set; }

        [React]
        [ReactBind(TargetProp = nameof(value), JsValueAccess = "e.target.value", EventName = "onChange")]
        public Expression<Func<string>> valueBind { get; set; }

    }
    
    public class Autocomplete : ElementBase
    {

        [React]
        public string value { get; set; }


        [React]
        public string[] options { get; set; }


        [React]
        public Func<ElementBase> renderInput { get; set; }
        

    }
}
