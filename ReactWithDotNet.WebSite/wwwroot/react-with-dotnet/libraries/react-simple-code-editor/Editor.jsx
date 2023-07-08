
import ReactWithDotNet from "../../react-with-dotnet";

import Editor from 'react-simple-code-editor';

import { highlight, languages } from 'prismjs/components/prism-core';
import 'prismjs/themes/prism.css';

// import languages
import 'prismjs/components/prism-clike';
import 'prismjs/components/prism-markup';
import 'prismjs/components/prism-javascript';


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

    if (language === "javascript")
    {
        return highlight(code, languages.javascript);
    }

    if (language === "js")
    {
        return highlight(code, languages.js);
    }

    throw 'Not implemented yet.' + language;
}

ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.HighlightByLanguage", HighlightByLanguage);

export default Editor