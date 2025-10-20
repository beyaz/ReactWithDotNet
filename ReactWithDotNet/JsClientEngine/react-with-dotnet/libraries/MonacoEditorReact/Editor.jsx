import Editor from '@monaco-editor/react';


/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.OnThirdPartyComponentPropsCalculated('ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact.Editor', (props, callerComponent) =>
{
    props.beforeMount = (monaco) => 
    {
        // TypeScript / JavaScript
        monaco.languages.typescript.typescriptDefaults.setDiagnosticsOptions({
            noSemanticValidation: true,
            noSyntaxValidation: true,
        });
        monaco.languages.typescript.javascriptDefaults.setDiagnosticsOptions({
            noSemanticValidation: true,
            noSyntaxValidation: true,
        });

        // JSON
        monaco.languages.json.jsonDefaults.setDiagnosticsOptions({
            validate: false,
            enableSchemaRequest: false,
        });

        // CSS, SCSS, LESS
        monaco.languages.css.cssDefaults.setOptions({ validate: false });
        monaco.languages.css.scssDefaults.setOptions({ validate: false });
        monaco.languages.css.lessDefaults.setOptions({ validate: false });

        // HTML
        monaco.languages.html.htmlDefaults.setOptions({ validate: false });
    };

    return props;
});

export default Editor;