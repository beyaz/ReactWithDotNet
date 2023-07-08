
import ReactWithDotNet from "../../react-with-dotnet";

import Editor from 'react-simple-code-editor';

import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-markup';
import 'prismjs/components/prism-javascript';
import 'prismjs/themes/prism.css';

function HighlightByLanguage(language, code)
{
    if (language === "clike")
    {
        return highlight(code, languages.clike);
    }

     if (language === "html")
    {
        return highlight(code, languages.html);
    }

    throw 'Not implemented yet.' + language;
}

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.HighlightByLanguage", HighlightByLanguage);

export default Editor