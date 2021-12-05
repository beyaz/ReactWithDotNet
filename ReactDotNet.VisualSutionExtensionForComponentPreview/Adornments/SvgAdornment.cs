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
using FP;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Threading;
using static ___Module___.FileTracer;
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
        string filePath;

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
                 filePath = textDocument.FilePath;
            }

            if (startListen == null)
            {
                Configuration.GetOutputJsFilePath()
                             .Match(outputJsFilePath =>
                                    {
                                        startListen = FileWatcherHelper.CreateFileWatcher(outputJsFilePath, () => GenerateImageAsync().FireAndForget(), Trace).startListen;
                                        startListen();
                                        Trace($"Started to listen file: {outputJsFilePath}");
                                    },
                                    exceptions => Trace($"File listen failed: {string.Join(Environment.NewLine, exceptions)}"));


            }

            GenerateImageAsync().FireAndForget();
        }

        static Action startListen;

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

                Trace("Started to take screenshut.");

                var takeScreenShotFunc = await UrlScreenShotTaker.GetUrlScreenShotTakerFuncAsync();

                var arr = await takeScreenShotFunc(filePath);

                Source = ByteImageConverter.ByteToImage(arr);

                UpdateAdornmentLocation(Source.Width, Source.Height);

                Trace("Finished taking screenshut.");
            }
            catch (Exception ex)
            {
                Trace(ex.ToString());
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
