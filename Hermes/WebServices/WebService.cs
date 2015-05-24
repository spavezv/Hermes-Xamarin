
using System;
using System.IO;
using System.Json;
using System.Net;
using System.Threading.Tasks;

namespace Hermes.WebServices
{
  class WebService
  {
    public async Task<JsonValue> GetTask(string url)
    {
      // Create an HTTP web request using the URL:
      HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri(url));
      request.ContentType = "application/json";
      request.Accept = "application/json";
      request.Method = "GET";

      WebResponse response = null;

      try
      {
        // Send the request to the server and wait for the response:
        using (response = await request.GetResponseAsync())
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
  }
}