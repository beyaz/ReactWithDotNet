
import React from 'react';

import ReactWithDotNet from "../../react-with-dotnet";

// https://uiwjs.github.io/react-codemirror/#/theme/data/github/light
import CodeMirror from '@uiw/react-codemirror';
import { json } from '@codemirror/lang-json';
import { html } from '@codemirror/lang-html';
import { css } from '@codemirror/lang-css';
import { java } from '@codemirror/lang-java';
import { githubLight} from '@uiw/codemirror-theme-github';

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror." + name, value);
}

register("CodeMirror::OnChange", function (args)
{
    return [/*newText*/args[0]];
});

function calculateExtensions(/*string[]*/stringArray)
{
    return stringArray.map(name =>
    {
        if (name == "json")
        {
            return json();
        }

        if (name == "html")
        {
            return html();
        }

        if (name == "css")
        {
            return css();
        }

        if (name == "java")
        {
            return java();
        }

        if (name == "githubLight")
        {
            return githubLight;
        }

        throw 'Implement here: ' + name;
    });
}


const Editor = React.forwardRef((props, ref) => (

    <CodeMirror ref={ref} value={props.value} basicSetup={props.basicSetup} onChange={props.onChange} extensions={calculateExtensions(props.extensions)} >
       {props.children}
    </CodeMirror>

));

export default Editor