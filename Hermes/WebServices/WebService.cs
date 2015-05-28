
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
      Console.WriteLine("URL: " + url);

      // Create an HTTP web request using the URL:
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
      request.ContentType = "application/json";
      request.Accept = "application/json";
      request.Method = "GET";

      try
      {
        // Send the request to the server and wait for the response:
        using (WebResponse response = await request.GetResponseAsync())
        {
          // Get a stream representation of the HTTP web response:
          using (Stream stream = response.GetResponseStream())
          {
            // Use this stream to build a JSON document object:
            JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            
            // Return the JSON document:
            return jsonDoc;
          }
        }
      }
      catch
      {
        return null;
      }
    }


    public async Task<JsonValue> PostTask(string url, String json)
    {
      Console.WriteLine("URL: " + url);

      // Create an HTTP web request using the URL:
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
      request.ContentType = "application/json";
      request.Accept = "text/plain";
      request.Method = "POST";

      Console.WriteLine("antes de enviar");
      try
      {
        using (var stream = await Task.Factory.FromAsync<Stream>(request.BeginGetRequestStream,
                                                               request.EndGetRequestStream, null))
        {
          byte[] requestAsBytes = Encoding.UTF8.GetBytes(json);
          await stream.WriteAsync(requestAsBytes, 0, requestAsBytes.Length);
          Console.WriteLine("envió");
        }
      }
      catch
      {
        Console.WriteLine("catch al enviar");
        return null;
      }
      Console.WriteLine("antes de recibir");
      try
      {
        using (WebResponse responseObject = await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, request))
        {
          var responseStream = responseObject.GetResponseStream();
          var sr = new StreamReader(responseStream);
          string received = await sr.ReadToEndAsync();
          Console.WriteLine("recibido " + received);
          Console.WriteLine("recibió");
          return received;
        }
      }
      catch
      {
        Console.WriteLine("catch al recibir");
        return null;
      }
    }


  }
}