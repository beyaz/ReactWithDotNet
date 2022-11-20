import ReactWithDotNet from "../react-with-dotnet";

// https://uiwjs.github.io/react-codemirror/#/theme/data/github/light
import CodeMirror from '@uiw/react-codemirror';
import { json } from '@codemirror/lang-json';
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

        if (name == "githubLight")
        {
            return githubLight;
        }

        throw 'Implement here: ' + name;
    });
});
