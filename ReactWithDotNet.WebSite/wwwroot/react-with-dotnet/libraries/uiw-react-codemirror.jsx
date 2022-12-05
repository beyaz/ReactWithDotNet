import ReactWithDotNet from "../react-with-dotnet";

// https://uiwjs.github.io/react-codemirror/#/theme/data/github/light
import CodeMirror from '@uiw/react-codemirror';
import { json } from '@codemirror/lang-json';
import { html } from '@codemirror/lang-html';
import { css } from '@codemirror/lang-css';
import { java } from '@codemirror/lang-java';
import { githubLight} from '@uiw/codemirror-theme-github';

function register(name, value)
{
    ReactWithDotNet.RegisterExternalJsObject("ReactWithDotNet.Libraries.uiw.react_codemirror." + name, value);
}

register("CodeMirror", CodeMirror);
register("CodeMirror::OnChange", function (args)
{
    return [/*newText*/args[0]];
});

register("CodeMirror::ConvertToExtension", function (/*string[]*/stringArray)
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
});
