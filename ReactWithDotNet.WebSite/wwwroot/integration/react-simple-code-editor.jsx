
import ReactWithDotNet from "../ReactWithDotNet.jsx";

// react-simple-code-editor
import Editor  from 'react-simple-code-editor';
import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-json';
import 'prismjs/components/prism-css';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-csharp';
import 'prismjs/themes/prism.css';

const GetHighlightFunction = (language) =>
{
    const lang = languages[language];
    
    return code =>
    {
        if (code == null)
        {
            return "";
        }
        return highlight(code, lang);
    };
};



// react-prism-editor
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.Editor", Editor);
ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.react_simple_code_editor.highlight", GetHighlightFunction);