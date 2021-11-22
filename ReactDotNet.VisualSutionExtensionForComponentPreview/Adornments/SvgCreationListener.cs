using System.ComponentModel.Composition;
using Community.VisualStudio.Toolkit;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace SvgViewer
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType(ContentTypes.CSharp)]
    //[ContentType("svg")]
    [TextViewRole(PredefinedTextViewRoles.PrimaryDocument)]
    internal sealed class SvgAdornmentProvider : IWpfTextViewCreationListener
    {
        [Import]
        public ITextDocumentFactoryService textDocumentFactory { get; set; }

        public void TextViewCreated(IWpfTextView textView)
        {
            textView.Properties.GetOrCreateSingletonProperty(() => new SvgAdornment(textView, textDocumentFactory));
        }
    }
}
