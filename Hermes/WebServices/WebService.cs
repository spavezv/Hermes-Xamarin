
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Hermes.Models;

namespace Hermes.WebServices
{
    class WebService
    {
        public async Task<JsonValue> GetTask(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Method = "GET";

            try
            {
                using (WebResponse response = await request.GetResponseAsync())
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                        return jsonDoc;
                    }
                }
            }
            catch
            {
                return null;
            }
        }


        public async Task<JsonValue> PutTask(string url, String json)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Accept = "text/plain";
            request.Method = "PUT";
            try
            {
                using (var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream,
                                                                       request.EndGetRequestStream, null))
                {
                    byte[] requestAsBytes = Encoding.UTF8.GetBytes(json);
                    await stream.WriteAsync(requestAsBytes, 0, requestAsBytes.Length);
                }
            }
            catch
            {
                return null;
            }
            try
            {
                using (WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
                {
                    var responseStream = responseObject.GetResponseStream();
                    var sr = new StreamReader(responseStream);
                    string received = await sr.ReadToEndAsync();
                    return received;
                }
            }
            catch
            {
                return null;
            }
        }

        public async Task<JsonValue> PostTask(string url, String json)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
            request.ContentType = "application/json";
            request.Accept = "text/plain";
            request.Method = "POST";

            try
            {
                using (var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream,
                    request.EndGetRequestStream, null))
                {
                    byte[] requestAsBytes = Encoding.UTF8.GetBytes(json);
                    await stream.WriteAsync(requestAsBytes, 0, requestAsBytes.Length);
                }
            }
            catch
            {
                return null;
            }
            try
            {
                using (WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
                {
                    var responseStream = responseObject.GetResponseStream();
                    var sr = new StreamReader(responseStream);
                    string received = await sr.ReadToEndAsync();
                    return received;
                }
            }
            catch
            {
                return null;
            }
        }


    }
}