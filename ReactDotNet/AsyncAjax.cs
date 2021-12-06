using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bridge.Html5;
using Newtonsoft.Json;

namespace ReactDotNet
{
    public class AsyncAjax : IPromise
    {
        string _error;
        string ResponseText;

        string Data { get; set; }
        string Url { get; set; }

        string method = "GET";

        public static async Task<T> Get<T>(string url)
        {
            var json = await Get(url, null);
            
            return JsonConvert.DeserializeObject<T>(json);
        }
        public static async Task<string> Get(string url, string json)
        {
            var promise = new AsyncAjax
            {
                Url    = url,
                Data   = json,
                method = "GET"
            };
            var resultHandler = (Func<AsyncAjax, string>)(request => request.ResponseText);

            var errorHandler = (Func<AsyncAjax, Exception>)(me =>
            {

                return new IOException(me._error);
            });

            var task = Task.FromPromise<string>(promise, resultHandler, errorHandler);

            await task;

            return task.Result;
        }

        public static async Task<string> PostJson(string url, string json)
        {
            var promise = new AsyncAjax
            {
                Url    = url,
                Data   = json,
                method = "POST"
            };
            var resultHandler = (Func<AsyncAjax, string>)(request => request.ResponseText);

            var errorHandler = (Func<AsyncAjax, Exception>)(me =>
            {
                
                return new IOException(me._error);
            });

            var task = Task.FromPromise<string>(promise, resultHandler, errorHandler);

            await task;

            return task.Result;
        }

        public static async Task<string> PostJson(string url, string json, Action<string> onError = null)
        {
            var promise = new AsyncAjax
            {
                Url = url,
                Data = json,
                method = "POST"
            };
            var resultHandler = (Func<AsyncAjax, string>)(request => request.ResponseText);

            var errorHandler = (Func<AsyncAjax, Exception>)(me =>
            {
                onError?.Invoke(me._error);

                return new IOException(me._error);
            });

            var task = Task.FromPromise<string>(promise, resultHandler, errorHandler);

            await task;

            return task.Result;
        }

        public void Then(Delegate fulfilledHandler, Delegate errorHandler = null, Delegate progressHandler = null)
        {

            var xhr = new XMLHttpRequest();

            xhr.OnReadyStateChange = () =>
            {
                if (xhr.ReadyState != AjaxReadyState.Done)
                {
                    return;
                }
                if (xhr.Status >= 200 && xhr.Status < 300)
                {
                    ResponseText = xhr.ResponseText; //OK
                    fulfilledHandler.Call(null, this);
                }
                else
                {

                    _error = "Ajax Error Occured. For this reason 'POST' operation was canceled." + Environment.NewLine +
                             "@status   :" + xhr.StatusText + Environment.NewLine +
                             "@errror   :" + xhr.ResponseText + Environment.NewLine +
                             "@POST Data:" + Data + Environment.NewLine +
                             "@Url      :" + Url;

                    errorHandler?.Call(null, this);
                }
            };
            
            xhr.Open(method, Url, true); //Async
            xhr.Send(Data);
            
        }
    }
}
