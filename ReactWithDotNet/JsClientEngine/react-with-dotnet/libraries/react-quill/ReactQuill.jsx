import ReactQuill from 'react-quill';
import 'react-quill/dist/quill.snow.css';

import ImageResize from 'quill-image-resize-module-react';

ReactQuill.Quill.register('modules/imageResize', ImageResize);


/**
 * @param {string} dotNetFullClassNameOf3rdPartyComponent
 */
ReactWithDotNet.OnThirdPartyComponentPropsCalculated('ReactWithDotNet.ThirdPartyLibraries._ReactQuill_.ReactQuill', (props, callerComponent) =>
{
    if (props.modules == null)
    {
        props.modules =
        {
            toolbar: [
                [{ header: [1, 2, 3, 4, 5, 6, false] }, { font: [] }],
                ['bold', 'italic', 'underline', 'strike', 'blockquote'],
                [{ align: [] }],
                [{ color: [] }],
                [{ background: [] }],
                [
                    { list: 'ordered' },
                    { list: 'bullet' },
                    { indent: '-1' },
                    { indent: '+1' },
                ],
                ['link', 'image', 'video'],
                ['clean'],
            ],
            clipboard:
            {
                // toggle to add extra line breaks when pasting HTML:
                matchVisual: false,
            },
            imageResize:
            {
                modules: ["Resize", "DisplaySize"]
            }
        };
    }
    if (props.formats == null)
    {
        props.formats = [
            "header",
            "font",
            "size",
            "bold",
            "italic",
            "underline",
            "align",
            "strike",
            "script",
            "blockquote",
            "background",
            "list",
            "bullet",
            "indent",
            "link",
            "image",
            "color",
            "code-block"
        ];
    }

    return props;
});

export default ReactQuill;