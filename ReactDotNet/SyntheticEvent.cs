using System;
using System.Collections.Generic;
using System.Text;

namespace ReactDotNet;


    public class SyntheticEvent
    {
        public ShadowHtmlElement target { get; set; }
    }

    public class ShadowHtmlElement
    {
        public int selectionStart { get; set; }
        public string value { get; set; }

    }

