using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;
using ___Module___;
using EnvDTE;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Threading;
using Configuration = ___Module___.Configuration;
using Image = System.Windows.Controls.Image;
using Task = System.Threading.Tasks.Task;

namespace SvgViewer
{
    internal class SvgAdornment : Image
    {

        private readonly ITextView _view;
        readonly ITextDocumentFactoryService textDocumentFactoryService;

        bool isClosed;

        public SvgAdornment(IWpfTextView view, ITextDocumentFactoryService textDocumentFactoryService)
        {
            MouseDown += (s, e) =>
            {
                if (e.ClickCount==2)
                {
                    isClosed = true;

                    Dispatcher.BeginInvoke((Action)(() => { Visibility = Visibility.Collapsed; }), DispatcherPriority.Background);
                }
            };

           

            _view                           = view;
            this.textDocumentFactoryService = textDocumentFactoryService;

            Visibility = Visibility.Hidden;

            IAdornmentLayer adornmentLayer = view.GetAdornmentLayer(AdornmentLayer.LayerName);

            if (adornmentLayer.IsEmpty)
            {
                adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, this, null);
            }

            _view.TextBuffer.PostChanged += OnTextBufferChanged;
            _view.Closed += OnTextviewClosed;
            _view.ViewportHeightChanged += SetAdornmentLocation;
            _view.ViewportWidthChanged += SetAdornmentLocation;


            if (textDocumentFactoryService.TryGetTextDocument(_view.TextBuffer, out var textDocument))
            {
                textDocument.FileActionOccurred += (s, e) =>
                {
                    if (e.FileActionType == FileActionTypes.ContentSavedToDisk)
                    {
                        GenerateImageAsync().FireAndForget();
                    }
                };
            }


            GenerateImageAsync().FireAndForget();
        }

        private void OnTextBufferChanged(object sender, EventArgs e)
        {
            //var lastVersion = _view.TextBuffer.CurrentSnapshot.Version.VersionNumber;

            //ThreadHelper.JoinableTaskFactory.RunAsync(async () =>
            //{
            //    await Task.Delay(500);

            //    if (_view.TextBuffer.CurrentSnapshot.Version.VersionNumber == lastVersion)
            //    {
            //        await GenerateImageAsync();
            //    }
            //});
        }

        private void OnTextviewClosed(object sender, EventArgs e)
        {
            _view.Closed -= OnTextviewClosed;
            _view.TextBuffer.PostChanged -= OnTextBufferChanged;
            _view.ViewportHeightChanged -= SetAdornmentLocation;
            _view.ViewportWidthChanged -= SetAdornmentLocation;
        }

        async Task GenerateImageAsync()
        {
            if (isClosed)
            {
                return;
            }

            await TaskScheduler.Default;

            try
            {

                

                if (!HasReactComponent())
                {
                    Source = null;
                    return;
                }

                


                

                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                ToolTip = "Double click for close";

                ITextDocument textDocument;

                var reactComponentTypeName = string.Empty;

                if (!textDocumentFactoryService.TryGetTextDocument(_view.TextBuffer, out textDocument))
                {
                    return;
                }

               

                var takeScreenShotFunc = await UrlScreenShotTaker.GetUrlScreenShotTakerFuncAsync();

                await Task.Delay(Configuration.Config.WaitTimeInMillisecondsForRefresh);

                var arr = await takeScreenShotFunc(textDocument.FilePath);


                Source = ByteImageConverter.ByteToImage(arr);

                UpdateAdornmentLocation(Source.Width, Source.Height);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Write(ex);
            }
        }

       

        public class ByteImageConverter
        {
            public static ImageSource ByteToImage(byte[] imageData)
            {
                BitmapImage  biImg = new BitmapImage();
                MemoryStream ms    = new MemoryStream(imageData);
                biImg.BeginInit();
                biImg.StreamSource = ms;
                biImg.EndInit();

                ImageSource imgSrc = (ImageSource)biImg;

                return imgSrc;
            }
        }
        

       
        bool HasReactComponent()
        {

            try
            {
                return _view.TextBuffer.CurrentSnapshot.GetText().Contains("ReactComponent<");
            }
            catch (XmlException)
            {
                return false;
            }
        }

        

        private void SetAdornmentLocation(object sender, EventArgs e)
        {
            UpdateAdornmentLocation(ActualWidth, ActualHeight);
        }

        private void UpdateAdornmentLocation(double width, double height)
        {
            var left = _view.ViewportRight - width - 20;
            if (left < 0)
            {
                left = 100;
            }

            Canvas.SetLeft(this, left);
            
            var top = _view.ViewportBottom - height - 20;
            if (top<0)
            {
                top = 0;
            }
            Canvas.SetTop(this, top);

            if (!isClosed)
            {
                Visibility = Visibility.Visible;
            }
            
            
        }

        

    }
}
