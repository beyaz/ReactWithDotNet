using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using PuppeteerSharp;
using PuppeteerSharp.Media;
using static ___Module___.FpExtensions;

namespace ___Module___
{
    /// <summary>
    /// </summary>
    delegate Task<byte[]> TakeScreenShotFunc(string url);

    /// <summary>
    ///     The URL screen shot taker
    /// </summary>
    static class UrlScreenShotTaker
    {
        #region Static Fields
        //Instantiate a Singleton of the Semaphore with a value of 1. This means that only 1 thread can be granted access at a time.
        static readonly SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);
        static TakeScreenShotFunc TakeScreenShotFunc;
        #endregion

        #region Public Methods
        /// <summary>
        ///     Gets the URL screen shot taker function.
        /// </summary>
        public static async Task<TakeScreenShotFunc> GetUrlScreenShotTakerFuncAsync()
        {
            if (TakeScreenShotFunc != null)
            {
                return TakeScreenShotFunc;
            }

            Action<string> trace = Log;

            trace("Creating browser headless mode...");
            

            //Asynchronously wait to enter the Semaphore. If no-one has been granted access to the Semaphore, code execution will proceed, otherwise this thread waits here until the semaphore is released 
            await semaphoreSlim.WaitAsync();

            var browserFetcher = new BrowserFetcher();

            await browserFetcher.DownloadAsync();

            var browser = await Puppeteer.LaunchAsync(new LaunchOptions { Headless = true });
            var page    = await browser.NewPageAsync();

            trace("New page opened in browser...");

            async Task<byte[]> takeScreenShot(string filePath)
            {

                trace("Taking screenshot started.");

                var url = "file:///D:/work/git/ReactDotNet/ReactDotNet.Demo/bin/Debug/index.html";

                var reactComponentTypeName = Path.GetFileNameWithoutExtension(filePath);

                await page.GoToAsync(url);

                await page.EvaluateExpressionAsync<int>("ReactDotNet.ReactElementPreview.RenderPreview('" + reactComponentTypeName + "', 'app')");

                var height = await page.EvaluateExpressionAsync<int>("document.getElementById('app').offsetHeight");
                var width  = await page.EvaluateExpressionAsync<int>("document.getElementById('app').offsetWidth");

                //await page.SetViewportAsync(new ViewPortOptions
                //{
                //    Width  = width  + 10,
                //    Height = height + 10
                //});

                var arr = await page.ScreenshotDataAsync(new ScreenshotOptions { Clip = new Clip { Width = width + 15, Height = height + 15 } });

                trace("Taking screenshot finished.");

                return arr;
            }

            async Task dispose()
            {
                trace("Disposing browser...");

                try
                {
                    await page.DisposeAsync();
                    await browser.DisposeAsync();
                    await browser.CloseAsync();

                    TakeScreenShotFunc = null;
                }
                catch (Exception e)
                {
                    trace($"Disposing browser is failed. @exception = {e}");
                    return;
                }

                trace("Disposed successfully.");
            }

            

            if (Application.Current?.MainWindow != null)
            {
                Application.Current.MainWindow.Closed += (s, e) => { RunSynchronously(dispose); };
            }

            TakeScreenShotFunc = url => Execute(takeScreenShot, url);

            //When the task is ready, release the semaphore. It is vital to ALWAYS release the semaphore when we are ready, or else we will end up with a Semaphore that is forever locked.
            //This is why it is important to do the Release within a try...finally clause; program execution may crash or take a different path, this way you are guaranteed execution
            semaphoreSlim.Release();

            trace("Created browser headless mode is finished.");

            return TakeScreenShotFunc;
        }
        #endregion
    }
}