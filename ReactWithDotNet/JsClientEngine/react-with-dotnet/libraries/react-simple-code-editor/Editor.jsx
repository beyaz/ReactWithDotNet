
import ReactWithDotNet from "../../react-with-dotnet";

import Editor from 'react-simple-code-editor';

import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-clike';
//import 'prismjs/components/prism-javascript';
//import 'prismjs/themes/prism.css'; //Example style, you can use another

function HighlightByLanguage(language, code)
{
    if (language === "clike")
    {
        return highlight(code, languages.clike);
    }

    throw 'Not implemented yet.' + language;
}

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.HighlightByLanguage", HighlightByLanguage);

export default Editor